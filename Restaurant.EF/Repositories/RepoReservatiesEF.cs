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

        public Reservatie AddReservatie(Reservatie reservatie)
        {
            try
            {
                ReservatiesEF reservatiesEF = MapReservatiesEF.MapToDB(reservatie, ctx);
                ctx.Reservatie.Add(reservatiesEF);
                SaveAndClear();
                return reservatie;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AddReservatie", ex);
            }
        }

        public void CancelReservatie(Reservatie reservatie)
        {
            try
            {
                ctx.Reservatie.Remove(new ReservatiesEF() { ReservatieNummer = reservatie.ReservatieNr });
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("CancelReservatie", ex);
            }
        }

        public List<Reservatie> GetReservatiesByDateRange(DateTime Datum)
        {
            try
            {
                return ctx.Reservatie.Where(x => x.Datum == Datum).Select(x => MapReservatiesEF.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetReservatiesByDate", ex);
            }
        }

        public List<Reservatie> GetReservatiesGebruiker(int klantenNr)
        {
            try
            {
                return ctx.Reservatie.Where(x => x.Gebruiker.KlantenNummer == klantenNr).Select(x => MapReservatiesEF.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetReservatiesGebruiker", ex);
            }
        }

        public bool ReservatieExists(int reservatieNr)
        {
            try
            {
                return ctx.Reservatie.Any(x => x.ReservatieNummer == reservatieNr);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("ReservatieExists", ex);
            }
        }

        public void UpdateReservatie(Reservatie reservatie)
        {
            try
            {
                ctx.Reservatie.Update(MapReservatiesEF.MapToDB(reservatie, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateReservatie", ex);
            }
        }

        public List<Reservatie> GetReservatiesByDateAndRestaurantNaam(DateTime Datum, string RestaurantNaam)
        {
            try
            {
                return ctx.Reservatie.Where(x => x.Datum == Datum).Where(x => x.Restaurant.Naam == RestaurantNaam).Select(x => MapReservatiesEF.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetReservatiesByDateAndRestaurantNaam", ex);
            }
        }
    }
}
