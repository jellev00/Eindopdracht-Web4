using Microsoft.EntityFrameworkCore;
using Restaurant.BL.Exceptions;
using Restaurant.BL.Interfaces;
using Restaurant.BL.Models;
using Restaurant.EF.Exceptions;
using Restaurant.EF.Mappers;
using Restaurant.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Repositories
{
    public class RepoRestaurantEF : IRestaurantRepo
    {
        private readonly RestaurantContext ctx;

        public RepoRestaurantEF(string connectionString)
        {
            this.ctx = new RestaurantContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public BL.Models.Restaurant AddRestaurant(BL.Models.Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Add(MapRestaurantEF.MapToDB(0, restaurant, ctx));
                SaveAndClear();
                return restaurant;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AddRestaurant", ex);
            }
        }

        public void DeleteRestaurant(string naam)
        {
            try
            {
                var restaurantToDelete = ctx.Restaurant.FirstOrDefault(x => x.Naam == naam);

                if (restaurantToDelete != null)
                {
                    restaurantToDelete.Status = false;
                    SaveAndClear();
                }
                else
                {
                    throw new RepositoryException($"Restaurant with name {naam} not found");
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("DeleteRestaurant", ex);
            }
        }

        public bool ExistsRestaurant(string naam)
        {
            try
            {
                return ctx.Restaurant.Any(x => x.Naam == naam);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("ExistsRestaurant", ex);
            }
        }

        public List<BL.Models.Restaurant> GetAllRestaurants()
        {
            try
            {
                List<RestaurantEF> restaurantEntities = ctx.Restaurant
                    .Where(x => x.Status == true)
                    .Include(x => x.Tafels)
                    .Include(x => x.Reservaties)
                    .ThenInclude(x => x.Gebruiker)
                    .AsNoTracking()
                    .ToList();

                return MapRestaurantEF.MapToDomain(restaurantEntities);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetAllRestaurants", ex);
            }
        }

        public List<BL.Models.Restaurant> GetAvailableRestaurants(DateTime Datum, int aantalplaatsen)
        {
            try
            {
                var beschikbareRestaurants = ctx.Restaurant
                    .Where(r => r.Status == true)
                    .Include(x => x.Tafels)
                    .Include(x => x.Reservaties)
                    .ThenInclude(x => x.Gebruiker)
                    .AsNoTracking()
                    .ToList();

                // Filter restaurants based on availability
                var filteredRestaurants = beschikbareRestaurants
                    .Where(r =>
                        r.Tafels.Any(t =>
                            !IsTableOccupied(t.Id, Datum, Datum.AddHours(1.5)) &&
                            t.AantalPlaatsen >= aantalplaatsen))
                    .ToList();

                return MapRestaurantEF.MapToDomain(filteredRestaurants);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetAvailableRestaurants", ex);
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

        public BL.Models.Restaurant GetRestaurantByNaam(string naam)
        {
            try
            {
                RestaurantEF restaurantEF = ctx.Restaurant.Where(x => x.Status == true && x.Naam == naam).Include(x => x.Tafels).Include(x => x.Reservaties).ThenInclude(x => x.Gebruiker).AsNoTracking().FirstOrDefault();

                if (restaurantEF == null)
                {
                    throw new RepositoryException($"GetRestaurantByNaam - Restaurant met naam {naam} bestaat niet");
                }

                return MapRestaurantEF.MapToDomain(restaurantEF);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetRestaurantByNaam", ex);
            }
        }

        public List<BL.Models.Restaurant> SearchRestaurants(string postcode, string keuken)
        {
            try
            {
                // Start with all restaurants
                IQueryable<RestaurantEF> query = ctx.Restaurant.Where(x => x.Status == true);

                // Apply filters based on user input
                if (!string.IsNullOrEmpty(postcode))
                {
                    query = query.Where(x => x.Postcode == postcode);
                }

                if (!string.IsNullOrEmpty(keuken))
                {
                    query = query.Where(x => x.Keuken == keuken);
                }

                // Execute the query and map the results to domain models
                List<BL.Models.Restaurant> restaurants = MapRestaurantEF.MapToDomain(
                    query.Include(x => x.Tafels)
                        .Include(x => x.Reservaties)
                        .ThenInclude(x => x.Gebruiker)
                        .AsNoTracking()
                        .ToList()
                );

                // Check if there are restaurants with the specified criteria
                if (restaurants == null || restaurants.Count == 0)
                {
                    throw new RestaurantManagerException("No restaurants found with the specified criteria.");
                }

                return restaurants;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("SearchRestaurants", ex);
            }
        }


        public void UpdateRestaurant(int Id, BL.Models.Restaurant restaurant)
        {
            try
            {
                RestaurantEF rEF = ctx.Restaurant.Find(Id);

                if (rEF == null)
                {
                    throw new RepositoryException($"Restaurant met ID {Id} is niet gevonden!");
                }
                else
                {
                    rEF = MapRestaurantEF.MapToDB(Id, restaurant, ctx);
                }


            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateRestaurant", ex);
            }
        }


        // Tafel

        public Tafel AddTafel(int restaurantId, Tafel tafel)
        {
            try
            {
                RestaurantEF rEF = ctx.Restaurant.Find(restaurantId);

                if (rEF == null)
                {
                    throw new RepositoryException($"Restaurant met ID {restaurantId} bestaat niet!");
                }

                TafelsEF tafelsEF = MapTafelEF.MapToDB(restaurantId, tafel.Id, tafel, ctx);

                rEF.Tafels.Add(tafelsEF);

                SaveAndClear();

                return tafel;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AddTafel", ex);
            }
        }

        public Tafel GetTafel(int Id)
        {
            try
            {
                var tafels = ctx.Tafels
                    .Where(t => t.Id == Id)
                    .Include(x => x.Restaurant)
                    .Select(t => MapTafelEF.MapToDomain(t))
                    .AsNoTracking()
                    .FirstOrDefault();

                return tafels;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetTafel", ex);
            }
        }

        public List<Tafel> GetTafels(int restaurantId)
        {
            try
            {
                var tafels = ctx.Tafels
                    .Where(t => t.Restaurant.Id == restaurantId)
                    .Include(x => x.Restaurant)
                    .Select(t => MapTafelEF.MapToDomain(t))
                    .ToList();

                return tafels;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetTafels", ex);
            }
        }

        public void UpdateTafel(int restaurantId, int Id, Tafel tafel)
        {
            try
            {
                TafelsEF tEF = ctx.Tafels.Find(Id);

                if (tEF == null)
                {
                    throw new RepositoryException($"Tafel met ID {Id} bestaat niet!");
                }
                else
                {
                    tEF = MapTafelEF.MapToDB(restaurantId, Id, tafel, ctx);
                }

                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateTafel", ex);
            }
        }

        public void DeleteTafel(int restaurantId, int Id)
        {
            try
            {
                var tafelToDelete = ctx.Tafels.SingleOrDefault(x => x.Restaurant.Id == restaurantId && x.Id == Id);

                if (tafelToDelete != null)
                {
                    ctx.Tafels.Remove(tafelToDelete);
                    SaveAndClear();
                }

            }
            catch (Exception ex)
            {
                throw new RepositoryException("DeleteTafel", ex);
            }
        }

        public bool ExistsTafel(int Id)
        {
            try
            {
                return ctx.Tafels.Any(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("ExistsTafel", ex);
            }
        }
    }
}
