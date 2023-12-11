using Microsoft.EntityFrameworkCore;
using Restaurant.BL.Interfaces;
using Restaurant.BL.Models;
using Restaurant.EF.Exceptions;
using Restaurant.EF.Mappers;
using Restaurant.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                RestaurantEF restaurantEF = MapRestaurantEF.MapToDB(restaurant, ctx);
                ctx.Restaurant.Add(restaurantEF);
                SaveAndClear();
                return restaurant;
            } catch (Exception ex)
            {
                throw new RepositoryException("AddRestaurant", ex);
            }
        }

        // Kijken hoe deze lijsten moet worden opgehaald.

        public List<BL.Models.Restaurant> GetAvailableTables(DateTime datum, int aantalPlaatsen)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetAvailableTables", ex);
            }
        }

        public List<BL.Models.Restaurant> GetAvailableTables(DateTime datum, int aantalPlaatsen, string postcode, string keuken)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetAvailableTables", ex);
            }
        }

        public List<BL.Models.Restaurant> GetRestaurants()
        {
            try
            {
                return ctx.Restaurant.Select(x => MapRestaurantEF.MapToDomain(x)).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetRestaurants", ex);
            }
        }

        public bool RestaurantExists(string naam)
        {
            try
            {
                return ctx.Restaurant.Any(x => x.Naam == naam);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("RestaurantExists", ex);
            }
        }

        public List<BL.Models.Restaurant> SearchRestaurantsByKeuken(string keuken)
        {
            try
            {
                return ctx.Restaurant.Where(x => x.Keuken == keuken).Select(x => MapRestaurantEF.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("SearchRestaurantsByKeuken", ex);
            }
        }

        public List<BL.Models.Restaurant> SearchRestaurantsByLocationAndKeuken(string postcode, string keuken)
        {
            try
            {
                return ctx.Restaurant.Where(x => x.Keuken == keuken).Where(x => x.Postcode == postcode).Select(x => MapRestaurantEF.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("SearchRestaurantsByLocationAndKeuken", ex);
            }
        }

        public List<BL.Models.Restaurant> SearchRestaurantsByPostcode(string postcode)
        {
            try
            {
                return ctx.Restaurant.Where(x => x.Postcode == postcode).Select(x => MapRestaurantEF.MapToDomain(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("SearchRestaurantsByPostcode", ex);
            }
        }

        public void DeleteRestaurant(BL.Models.Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Remove(new RestaurantEF() { Naam = restaurant.Naam });
                SaveAndClear();

            }
            catch (Exception ex)
            {
                throw new RepositoryException("DeleteRestaurant", ex);
            }
        }

        public void UpdateRestaurant(BL.Models.Restaurant restaurant)
        {
            try
            {
                ctx.Restaurant.Update(MapRestaurantEF.MapToDB(restaurant, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateRestaurant", ex);
            }
        }
    }
}
