using Restaurant.BL.Models;

namespace Restaurant.API.Models.Output
{
    public class RestaurantRESTOutputDTO
    {
        public RestaurantRESTOutputDTO(int id, string naam, string locatie, string keuken, string telefoonNr, string email)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
            Keuken = keuken;
            TelefoonNr = telefoonNr;
            Email = email;
        }

        public RestaurantRESTOutputDTO(int id, string naam, string locatie, string keuken, string telefoonNr, string email, string tafels, string reservaties) : this(id, naam, locatie, keuken, telefoonNr, email)
        {
            Tafels = tafels;
            Reservaties = reservaties;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Locatie { get; set; }
        public string Keuken { get; set; }
        public string TelefoonNr { get; set; }
        public string Email { get; set; }
        public string Tafels { get; set; }
        public string Reservaties { get; set; }
    }
}
