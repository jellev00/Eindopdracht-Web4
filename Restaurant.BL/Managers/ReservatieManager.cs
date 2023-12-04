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
        public List<Reservatie> GetReservaties(string filterPostcode, string filterKeuken, int filterAantalPlaatsen)
        {
            try
            {
                List<Reservatie> allReservaties = _reservatieRepo.GetReservaties("", "", 0);

                List<Reservatie> filteredReservaties = new List<Reservatie>();

                foreach (var reservatie in allReservaties)
                {
                    bool postcodeMatch = string.IsNullOrEmpty(filterPostcode) || reservatie.Gebruiker.Locatie.Postcode.ToString() == filterPostcode;
                    bool keukenMatch = string.IsNullOrEmpty(filterKeuken) || reservatie.Gebruiker.Locatie.Straatnaam == filterKeuken;
                    bool plaatsenMatch = filterAantalPlaatsen <= reservatie.AantalPlaatsen;

                    if (postcodeMatch && keukenMatch && plaatsenMatch)
                    {
                        filteredReservaties.Add(reservatie);
                    }
                }

                return filteredReservaties;
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("GetReservaties - Fout bij ophalen en filteren van reservaties.", ex);
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
                if (!_reservatieRepo.ReservatiesExists(reservatie.ReservatieNr))
                {
                    _reservatieRepo.AddReservaties(reservatie);
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
                if (_reservatieRepo.ReservatiesExists(reservatie.ReservatieNr))
                {
                    _reservatieRepo.DeleteReservaties(reservatie);
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
                _reservatieRepo.UpdateReservaties(reservatie);
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
                return _reservatieRepo.ReservatiesExists(reservatieNr);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ReservatiesExists", ex);
            }
        }
    }
}
