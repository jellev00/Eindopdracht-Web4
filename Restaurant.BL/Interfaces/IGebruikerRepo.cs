using Restaurant.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Interfaces
{
    public interface IGebruikerRepo
    {
        List<Gebruiker> GetGebruikers();
        Gebruiker GetGebruikersByKlantNr(int klantNr);
        Gebruiker GetGebruikersByEmail(string email);


        Gebruiker AddGebruiker(Gebruiker gebruiker);
        void DeleteGebruiker(Gebruiker gebruiker);
        void UpdateGebruiker(Gebruiker gebruiker);
        bool GebruikerExists(string email);

        //Gebruiker GetCustomerById(int id);
    }
}
