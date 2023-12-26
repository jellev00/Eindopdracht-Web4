using Restaurant.BL.Models;

namespace Restaurant.API.Models.Input
{
    public class RestaurantRESTInputDTO
    {
        public RestaurantRESTInputDTO(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens, bool status)
        {
            Naam = naam;
            this.locatie = locatie;
            Keuken = keuken;
            this.contactgegevens = contactgegevens;
            Status = status;
        }

        public string Naam { get; set; }
        public Locatie locatie { get; set; }
        public string Keuken { get; set; }
        public Contactgegevens contactgegevens { get; set; }
        public bool Status { get; set; }
    }
}
