using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.BL.Interfaces;
using Restaurant.BL.Models;
using Restaurant.EF.Exceptions;
using Restaurant.EF.Mappers;
using Restaurant.EF.Models;

namespace Restaurant.EF.Repositories
{
    public class RepoReservatiesEF : IReservatieRepo
    {
        private readonly RestaurantContext ctx;

        public RepoReservatiesEF(string connectionString)
        {
            this.ctx = new RestaurantContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Reservatie AddReservatie(int klantenNr, int restaurantId, Reservatie reservatie)
        {
            try
            {
                GebruikerEF gEF = ctx.Gebruiker.Find(klantenNr);
                //RestaurantEF rEF = ctx.Restaurant.Find(restaurantId);
                RestaurantEF rEF = ctx.Restaurant.Where(x => x.Id == restaurantId).Include(x => x.Tafels).SingleOrDefault();

                if (gEF == null)
                {
                    throw new RepositoryException($"Gebruiker met ID {klantenNr} is niet gevonden!");
                }

                if (gEF.Reservaties == null)
                {
                    gEF.Reservaties = new List<ReservatiesEF>();
                }

                // Iterate through available tables and find a suitable one
                foreach (TafelsEF tafel in rEF.Tafels.OrderBy(t => t.AantalPlaatsen))
                {
                    // Check if the table is available at the specified time
                    if (!IsTableOccupied(tafel.Id, reservatie.DatumUur, reservatie.DatumUur.AddHours(1.5)))
                    {
                        // Check if the table has enough seats
                        if (tafel.AantalPlaatsen >= reservatie.AantalPlaatsen)
                        {
                            // Create a new reservation
                            Reservatie r = new Reservatie(reservatie.AantalPlaatsen, reservatie.DatumUur, tafel.Id);
                            ReservatiesEF newReservatieEF = MapReservatiesEF.MapToDB(klantenNr, restaurantId, 0, r, ctx);

                            // Add the new reservation to the user and restaurant
                            gEF.Reservaties.Add(newReservatieEF);
                            rEF.Reservaties.Add(newReservatieEF);

                            SaveAndClear();

                            return reservatie;
                        }
                    }
                }

                // If no suitable table is found, throw an exception or handle accordingly
                throw new Exception("No suitable table found for the reservation.");
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AddReservatie", ex);
            }
        }

        // Check if a table is occupied during the specified time period
        private bool IsTableOccupied(int tafelNr, DateTime start, DateTime end)
        {
            return ctx.Reservatie.Any(r =>
                r.TafelNummer == tafelNr &&
                r.DatumUur < end &&
                r.DatumUur.AddHours(1.5) > start);
        }

        public void UpdateReservatie(int klantenNr, int restaurantId, int reservatieNr, Reservatie reservatie)
        {
            try
            {
                ReservatiesEF rEF = ctx.Reservatie.Find(reservatieNr);

                if (rEF == null)
                {
                    throw new RepositoryException($"Reservatie met ReservatieNummer{reservatieNr} is niet gevonden!");
                }
                else
                {
                    rEF = MapReservatiesEF.MapToDB(klantenNr, restaurantId, reservatieNr, reservatie, ctx);
                }

                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateReservatie", ex);
            }
        }

        public void DeleteReservatie(int klantenNr, int reservatieNr)
        {
            try
            {
                var reservatieToDelete = ctx.Reservatie.SingleOrDefault(x => x.ReservatieNummer == reservatieNr && x.Gebruiker.KlantenNummer == klantenNr);

                if (reservatieToDelete != null)
                {
                    if (reservatieToDelete.DatumUur.Date > DateTime.Today)
                    {
                        ctx.Reservatie.Remove(reservatieToDelete);
                        SaveAndClear();
                    }
                    else
                    {
                        throw new RepositoryException("De reservatie kan niet worden verwijderd omdat de datum vandaag is of in het verleden ligt.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("DeleteGebruiker", ex);
            }
        }

        public List<Reservatie> GetAllReservationsByKlantenNr(int klantenNr, DateTime? beginDatum, DateTime? eindDatum)
        {
            try
            {
                IQueryable<ReservatiesEF> query = ctx.Reservatie.Where(r => r.Gebruiker.KlantenNummer == klantenNr);

                if (beginDatum.HasValue)
                {
                    if (eindDatum.HasValue)
                    {
                        query = query.Where(x => x.DatumUur.Date >= beginDatum && x.DatumUur.Date <= eindDatum);
                    }
                    else
                    {
                        query = query.Where(x => x.DatumUur.Date == beginDatum);
                    }
                }

                List<Reservatie> reservaties = MapReservatiesEF.MapToDomain(
                    query.Include(r => r.Gebruiker)
                    .Include(r => r.Restaurant)
                    .AsNoTracking()
                    .ToList()
                );

                return reservaties;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetAllReservationsByKlantenNr", ex);
            }
        }

        public Reservatie GetReservationsByReservatieNr(int reservatieNr)
        {
            try
            {
                var reservation = ctx.Reservatie
                    .Include(r => r.Gebruiker)
                    .Include(r => r.Restaurant)
                    .Where(r => r.ReservatieNummer == reservatieNr)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (reservation != null)
                {
                    Reservatie mappedReservation = MapReservatiesEF.MapToDomain(reservation);
                    return mappedReservation;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetAllReservations", ex);
            }
        }

        public List<Reservatie> GetAllReservationsByRestauranNaam(string restauranNaam, DateTime? beginDatum, DateTime? eindDatum)
        {
            try
            {
                IQueryable<ReservatiesEF> query = ctx.Reservatie.Where(r => r.Restaurant.Naam == restauranNaam);

                if (beginDatum.HasValue)
                {
                    if (eindDatum.HasValue)
                    {
                        query = query.Where(x => x.DatumUur.Date >= beginDatum && x.DatumUur.Date <= eindDatum);
                    }
                    else
                    {
                        query = query.Where(x => x.DatumUur.Date == beginDatum);
                    }
                }

                List<Reservatie> reservaties = MapReservatiesEF.MapToDomain(
                    query.Include(r => r.Gebruiker)
                    .Include(r => r.Restaurant)
                    .AsNoTracking()
                    .ToList()
                );

                return reservaties;
                return null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetAllReservationsByRestauranNaam", ex);
            }
        }

        public bool ExistsResertvatie(int reservatieNr)
        {
            try
            {
                return ctx.Reservatie.Any(x => x.ReservatieNummer == reservatieNr);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("ExistsResertvatie", ex);
            }
        }
    }
}
