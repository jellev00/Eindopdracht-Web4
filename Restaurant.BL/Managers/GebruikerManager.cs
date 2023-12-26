using Restaurant.BL.Interfaces;
using Restaurant.BL.Models;
using Restaurant.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Managers
{
    public class GebruikerManager
    {
        private IGebruikerRepo _gebruikerRepo;

        public GebruikerManager(IGebruikerRepo gebruikerRepo)
        {
            _gebruikerRepo = gebruikerRepo;
        }

        public Gebruiker AddGebruiker(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker == null)
                {
                    throw new GebruikerManagerException($"AddGebruiker - De gebruiker mag niet null zijn!");
                }

                if (_gebruikerRepo.ExistsGebruiker(gebruiker.Email))
                {
                    throw new GebruikerManagerException($"AddGebruiker - De gebruiker bestaat al!");
                }

                return _gebruikerRepo.AddGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("AddGebruiker", ex);
            }
        }

        public void UpdateGebruiker(int klantenNr, Gebruiker gebruiker)
        {
            try
            {
                _gebruikerRepo.UpdateGebruiker(klantenNr, gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("UpdateCustomer", ex);
            }
        }

        public void DeleteGebruiker(string email)
        {
            try
            {
                if (!_gebruikerRepo.ExistsGebruiker(email))
                {
                    throw new GebruikerManagerException($"DeleteGebruiker - De gebruiker bestaat niet");
                }

                _gebruikerRepo.DeleteGebruiker(email);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("DeleteGebruiker", ex);
            }
        }

        public Gebruiker GetGebruikerByEmail(string email)
        {
            try
            {
                if (!_gebruikerRepo.ExistsGebruiker(email))
                {
                    throw new GebruikerManagerException($"GetGebruikerByEmail - De gebruiker bestaat niet");
                }

                return _gebruikerRepo.GetGebruikerByEmail(email);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("GetGebruikerByEmail", ex);
            }
        }

        public bool ExistsGebruiker(string email)
        {
            try
            {
                return _gebruikerRepo.ExistsGebruiker(email);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("GebruikerExists", ex);
            }
        }
    }
}
