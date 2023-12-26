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
        private IRestaurantRepo _restaurantRepo;

        public RestaurantManager(IRestaurantRepo beheerderRepo)
        {
            _restaurantRepo = beheerderRepo;
        }


        public BL.Models.Restaurant AddRestaurant(BL.Models.Restaurant restaurant)
        {
            try
            {
                if (restaurant == null)
                {
                    throw new RestaurantManagerException($"AddRestaurant - De Restaurant mag niet null zijn!");
                }

                if (_restaurantRepo.ExistsRestaurant(restaurant.Naam))
                {
                    throw new RestaurantManagerException($"AddRestaurant - Het Restaurant bestaat al!");
                }

                return _restaurantRepo.AddRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("AddRestaurant", ex);
            }
        }

        public void DeleteRestaurant(string naam)
        {
            try
            {
                if (!_restaurantRepo.ExistsRestaurant(naam))
                {
                    throw new RestaurantManagerException($"DeleteRestaurant - Het Restaurant bestaat niet");
                }

                _restaurantRepo.DeleteRestaurant(naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("DeleteRestaurant", ex);
            }
        }

        public bool ExistsRestaurant(string naam)
        {
            try
            {
                return _restaurantRepo.ExistsRestaurant(naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("ExistsRestaurant", ex);
            }
        }

        public List<BL.Models.Restaurant> GetAllRestaurants()
        {
            try
            {
                return _restaurantRepo.GetAllRestaurants();
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetAllRestaurants", ex);
            }
        }

        public List<BL.Models.Restaurant> GetAvailableRestaurants(DateTime Datum, int aantalplaatsen)
        {
            try
            {
                return _restaurantRepo.GetAvailableRestaurants(Datum, aantalplaatsen);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetAvailableRestaurants", ex);
            }
        }

        public BL.Models.Restaurant GetRestaurantByNaam(string naam)
        {
            try
            {
                if (!_restaurantRepo.ExistsRestaurant(naam))
                {
                    throw new RestaurantManagerException($"GetRestaurantByNaam - Het Restaurant bestaat niet");
                }

                return _restaurantRepo.GetRestaurantByNaam(naam);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetRestaurantByNaam", ex);
            }
        }
        public List<BL.Models.Restaurant> SearchRestaurants(string postcode, string keuken)
        {
            try
            {
                List<BL.Models.Restaurant> restaurants = _restaurantRepo.SearchRestaurants(postcode, keuken);

                if (restaurants == null || restaurants.Count == 0)
                {
                    throw new RestaurantManagerException($"Geen restaurants gevonden met postcode: {postcode} of keuken: {keuken}");
                }

                return restaurants;
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("SearchRestaurants", ex);
            }
        }

        public void UpdateRestaurant(int Id, BL.Models.Restaurant restaurant)
        {
            try
            {
                _restaurantRepo.UpdateRestaurant(Id, restaurant);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("UpdateRestaurant", ex);
            }
        }

        // Tafel

        public Tafel AddTafel(int restaurantId, Tafel tafel)
        {
            try
            {
                return _restaurantRepo.AddTafel(restaurantId, tafel);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("AddTafel", ex);
            }
        }

        public Tafel GetTafel(int Id)
        {
            try
            {
                return _restaurantRepo.GetTafel(Id);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetTafel", ex);
            }
        }

        public List<Tafel> GetTafels(int restaurantId)
        {
            try
            {
                return _restaurantRepo.GetTafels(restaurantId);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("GetTafels", ex);
            }
        }

        public void UpdateTafel(int restaurantId, int Id, Tafel tafel)
        {
            try
            {
                _restaurantRepo.UpdateTafel(restaurantId, Id, tafel);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("UpdateTafel", ex);
            }
        }

        public void DeleteTafel(int restaurantId, int Id)
        {
            try
            {
                _restaurantRepo.DeleteTafel(restaurantId, Id);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("DeleteTafel", ex);
            }
        }

        public bool ExistsTafel(int Id)
        {
            try
            {
                return _restaurantRepo.ExistsTafel(Id);
            }
            catch (Exception ex)
            {
                throw new RestaurantManagerException("ExistsTafel", ex);
            }
        }
    }
}
