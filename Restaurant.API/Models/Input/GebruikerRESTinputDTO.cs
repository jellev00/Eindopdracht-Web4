using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Restaurant.BL.Models;

namespace Restaurant.API.Models.Input
{
    public class GebruikerRESTinputDTO
    {
        public GebruikerRESTinputDTO(string naam, string email, string telefoonNummer, Locatie locatie)
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            this.locatie = locatie;
        }

        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public Locatie locatie { get; set; }
    }
}
