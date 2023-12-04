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

        public List<Gebruiker> GetGebruikers()
        {
            try
            {
                return _gebruikerRepo.GetGebruikers();
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("GetGebruikers", ex);
            }
        }
        public void AddGebruiker(Gebruiker gebruiker)
        {
            try
            {
                if (!_gebruikerRepo.GebruikerExists(gebruiker.Email))
                {
                    _gebruikerRepo.AddGebruiker(gebruiker);
                }
                else
                {
                    throw new GebruikerManagerException($"AddGebruiker - De gebruiker bestaat al");
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("AddGebruiker", ex);
            }
        }
        public void DeleteGebruiker(Gebruiker gebruiker)
        {
            try
            {
                if (_gebruikerRepo.GebruikerExists(gebruiker.Email))
                {
                    _gebruikerRepo.DeleteGebruiker(gebruiker);
                }
                else
                {
                    throw new GebruikerManagerException($"DeleteGebruiker - De gebruiker bestaat niet");
                }
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("DeleteGebruiker", ex);
            }
        }
        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                _gebruikerRepo.UpdateGebruiker(gebruiker);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("UpdateCustomer", ex);
            }
        }

        public bool GebruikerExists(string email)
        {
            try
            {
                return _gebruikerRepo.GebruikerExists(email);
            }
            catch (Exception ex)
            {
                throw new GebruikerManagerException("GebruikerExists", ex);
            }
        }
    }
}
