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
        List<Reservatie> GetReservaties(string filterPostcode, string filterKeuken, int filterAantalPlaatsen);
        List<Reservatie> GetReservatiesGebruiker(int klantenNr);
        void AddReservaties(Reservatie reservatie);
        void DeleteReservaties(Reservatie reservatie);
        void UpdateReservaties(Reservatie reservatie);
        bool ReservatiesExists(int reservatieNr);
    }
}
