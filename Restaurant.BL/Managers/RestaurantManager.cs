using Restaurant.BL.Interfaces;
using Restaurant.BL.Models;
using Restaurant.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Managers
{
    public class RestaurantManager
    {
        private IRestaurantRepo _beheerderRepo;

        public RestaurantManager(IRestaurantRepo beheerderRepo)
        {
            _beheerderRepo = beheerderRepo;
        }

        public List<Models.Restaurant> GetRestaurants()
        {
            try
            {
                return _beheerderRepo.GetRestaurants();
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetRestaurants", ex);
            }
        }
        public void AddRestaurant(Models.Restaurant restaurant)
        {
            try
            {
                if (!_beheerderRepo.RestaurantExists(restaurant.Contactgegevens.Email))
                {
                    _beheerderRepo.AddRestaurant(restaurant);
                }
                else
                {
                    throw new RestaurantManagerException("AddRestaurant - Beheerder bestaat al");
                }
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("AddRestaurant", ex);
            }
        }

        public bool RestaurantExists(string naam)
        {
            try
            {
                return _beheerderRepo.RestaurantExists(naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("RestaurantExists", ex);
            }
        }
    }
}
