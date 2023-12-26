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
        // Gebruiker
        Gebruiker AddGebruiker(Gebruiker gebruiker);
        void UpdateGebruiker(int klantenNr, Gebruiker gebruiker);
        void DeleteGebruiker(string email);
        Gebruiker GetGebruikerByEmail(string email);
        bool ExistsGebruiker(string email);
    }
}
