using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UI.DTO
{
    public class GebruikerDTO
    {
        public GebruikerDTO(int klantenNummer, string naam, string email, string telefoonNummer, string locatie)
        {
            KlantenNummer = klantenNummer;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Locatie = locatie;
        }

        public int KlantenNummer { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public string Locatie { get; set; }
    }
}
