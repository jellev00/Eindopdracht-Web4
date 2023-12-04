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
        void AddRestaurant(Models.Restaurant Restaurant);
        bool RestaurantExists(string naam);

        //void DeleteGebruiker(Beheerder beheerder);
        //void UpdateCustomer(Beheerder beheerder);
    }
}
