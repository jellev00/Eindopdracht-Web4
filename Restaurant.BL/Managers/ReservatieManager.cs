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


        public Reservatie AddReservatie(int klantenNr, int restaurantId, Reservatie reservatie)
        {
            try
            {
                if (reservatie == null)
                {
                    throw new ReservatieManagerException($"AddReservatie - De reservatie mag niet null zijn!");
                }

                return _reservatieRepo.AddReservatie(klantenNr, restaurantId, reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("AddReservatie", ex);
            }
        }
        public void UpdateReservatie(int klantenNr, int restaurantId, int reservatieNr, Reservatie reservatie)
        {
            try
            {
                _reservatieRepo.UpdateReservatie(klantenNr, restaurantId, reservatieNr, reservatie);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("UpdateReservatie", ex);
            }
        }

        public void DeleteReservatie(int klantenNr, int reservatieNr)
        {
            try
            {
                if (!_reservatieRepo.ExistsResertvatie(reservatieNr))
                {
                    throw new ReservatieManagerException($"DeleteReservatie - De reservatie bestaat niet");
                }

                _reservatieRepo.DeleteReservatie(klantenNr, reservatieNr);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("DeleteReservatie", ex);
            }
        }

        public List<Reservatie> GetAllReservationsByKlantenNr(int klantenNr, DateTime? beginDatum, DateTime? eindDatum)
        {
            try
            {
                return _reservatieRepo.GetAllReservationsByKlantenNr(klantenNr, beginDatum, eindDatum);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GetAllReservationsByDate", ex);
            }
        }

        public Reservatie GetReservationsByReservatieNr(int reservatieNr)
        {
            try
            {
                return _reservatieRepo.GetReservationsByReservatieNr(reservatieNr);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GetReservationsByReservatieNr", ex);
            }
        }

        public List<Reservatie> GetAllReservationsByRestauranNaam(string restauranNaam, DateTime? beginDatum, DateTime? eindDatum)
        {
            try
            {
                return _reservatieRepo.GetAllReservationsByRestauranNaam(restauranNaam, beginDatum, eindDatum);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GetAllReservationsByRestauranNaam", ex);
            }
        }

        public bool ExistsResertvatie(int reservatieNr)
        {
            try
            {
                return _reservatieRepo.ExistsResertvatie(reservatieNr);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ExistsResertvatie", ex);
            }
        }
    }
}
