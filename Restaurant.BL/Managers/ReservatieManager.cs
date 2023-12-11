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
    public class ReservatieManager
    {
        private IReservatieRepo _reservatieRepo;

        public ReservatieManager(IReservatieRepo reservatieRepo)
        {
            _reservatieRepo = reservatieRepo;
        }

        public List<Reservatie> GetReservatiesByDateAndRestaurantNaam(DateTime Datum, string RestaurantNaam)
        {
            try
            {
                List<Reservatie> reservaties = _reservatieRepo.GetReservatiesByDateAndRestaurantNaam(Datum, RestaurantNaam);

                return reservaties;
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GetReservatiesByDateAndRestaurantNaam", ex);
            }
        }

        public List<Reservatie> GetReservatiesByDateRange(DateTime Datum)
        {
            try
            {
                List<Reservatie> reservaties = _reservatieRepo.GetReservatiesByDateRange(Datum);

                return reservaties;
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GetReservatiesByDateRange", ex);
            }
        }

        public List<Reservatie> GetReservatiesGebruiker(int klantenNr)
        {
            try
            {
                return _reservatieRepo.GetReservatiesGebruiker(klantenNr);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GetReservatiesGebruiker", ex);
            }
        }

        public void AddReservaties(Reservatie reservatie)
        {
            try
            {
                if (!_reservatieRepo.ReservatieExists(reservatie.ReservatieNr))
                {
                    _reservatieRepo.AddReservatie(reservatie);
                }
                else
                {
                    throw new ReservatieManagerException($"AddReservaties - De reservatie bestaat al");
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("", ex);
            }
        }

        public void DeleteReservaties(Reservatie reservatie)
        {
            try
            {
                if (_reservatieRepo.ReservatieExists(reservatie.ReservatieNr))
                {
                    _reservatieRepo.CancelReservatie(reservatie);
                }
                else
                {
                    throw new ReservatieManagerException($"DeleteReservaties - De reservatie bestaat niet");
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("", ex);
            }
        }

        public void UpdateReservaties(Reservatie reservatie)
        {
            try
            {
                _reservatieRepo.UpdateReservatie(reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("", ex);
            }
        }

        public bool ReservatiesExists(int reservatieNr)
        {
            try
            {
                return _reservatieRepo.ReservatieExists(reservatieNr);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ReservatiesExists", ex);
            }
        }
    }
}
