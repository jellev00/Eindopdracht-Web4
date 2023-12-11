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

        //public GebruikerRESTinputDTO(string naam, string email, string telefoonNummer, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel)
        //{
        //    Naam = naam;
        //    Email = email;
        //    TelefoonNummer = telefoonNummer;
        //    Postcode = postcode;
        //    GemeenteNaam = gemeenteNaam;
        //    StraatNaam = straatNaam;
        //    HuisNummerLabel = huisNummerLabel;
        //}

        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public Locatie locatie { get; set; }
        //public string Postcode { get; set; }
        //public string GemeenteNaam { get; set; }
        //public string StraatNaam { get; set; }
        //public string HuisNummerLabel { get; set; }
    }
}
