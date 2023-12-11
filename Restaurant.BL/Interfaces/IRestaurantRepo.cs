using Restaurant.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Interfaces
{
    public interface IRestaurantRepo
    {
        List<Models.Restaurant> GetRestaurants();
        Models.Restaurant AddRestaurant(Models.Restaurant restaurant);
        bool RestaurantExists(string naam);
        void DeleteRestaurant(Models.Restaurant restaurant);
        void UpdateRestaurant(Models.Restaurant restaurant);

        // Additional methods for searching restaurants
        List<Models.Restaurant> SearchRestaurantsByPostcode(string postcode);
        List<Models.Restaurant> SearchRestaurantsByKeuken(string keuken);
        List<Models.Restaurant> SearchRestaurantsByLocationAndKeuken(string postcode, string keuken);

        // Method for getting an overview of available tables for a specific date
        List<Models.Restaurant> GetAvailableTables(DateTime datum, int aantalPlaatsen);

        // Additional method for getting an overview with location and cuisine filters
        List<Models.Restaurant> GetAvailableTables(DateTime datum, int aantalPlaatsen, string postcode, string keuken);
    }
}
