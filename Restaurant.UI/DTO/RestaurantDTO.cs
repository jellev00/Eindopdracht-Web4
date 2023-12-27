using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UI.DTO
{
    public class RestaurantDTO
    {
        public RestaurantDTO(int id, string naam, string locatie, string keuken, string telefoonNr, string email)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            TelefoonNr = telefoonNr;
            Email = email;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Locatie { get; set; }
        public string Keuken { get; set; }
        public string TelefoonNr { get; set; }
        public string Email { get; set; }
    }
}
