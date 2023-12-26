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
        Models.Restaurant AddRestaurant(Models.Restaurant restaurant);
        void UpdateRestaurant(int Id, Models.Restaurant restaurant);
        void DeleteRestaurant(string naam);
        List<Models.Restaurant> GetAllRestaurants();
        Models.Restaurant GetRestaurantByNaam(string naam);
        bool ExistsRestaurant(string naam);

        // Additional methods for searching restaurants
        List<Models.Restaurant> SearchRestaurants(string postcode, string keuken);
        List<Models.Restaurant> GetAvailableRestaurants(DateTime datum, int aantalplaatsen);


        // Tafel

        Tafel AddTafel(int restaurantId, Tafel tafel);
        Tafel GetTafel(int Id);
        List<Tafel> GetTafels(int restaurantId);
        void UpdateTafel(int restaurantId, int Id, Tafel tafel);
        void DeleteTafel(int restaurantId, int Id);
        bool ExistsTafel(int Id);
    }
}
