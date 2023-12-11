using Restaurant.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Interfaces
{
    public interface IReservatieRepo
    {
        List<Reservatie> GetReservatiesByDateAndRestaurantNaam(DateTime Datum, string RestaurantNaam);
        List<Reservatie> GetReservatiesByDateRange(DateTime Datum);
        List<Reservatie> GetReservatiesGebruiker(int klantenNr);
        Reservatie AddReservatie(Reservatie reservatie);
        void CancelReservatie(Reservatie reservatie);
        void UpdateReservatie(Reservatie reservatie);
        bool ReservatieExists(int reservatieNr);
    }
}
