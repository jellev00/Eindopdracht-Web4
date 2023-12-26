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
        Reservatie AddReservatie(int klantenNr, int restaurantId, Reservatie reservatie);
        void UpdateReservatie(int klantenNr, int restaurantId, int reservatieNr, Reservatie reservatie);
        void DeleteReservatie(int klantenNr, int reservatieNr);
        List<Reservatie> GetAllReservationsByKlantenNr(int klantenNr, DateTime? beginDatum, DateTime? eindDatum);
        Reservatie GetReservationsByReservatieNr(int reservatieNr);
        List<Reservatie> GetAllReservationsByRestauranNaam(string restauranNaam, DateTime? beginDatum, DateTime? eindDatum);
        bool ExistsResertvatie(int reservatieNr);

    }
}
