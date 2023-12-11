using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Models
{
    public class ReservatiesEF
    {
        public ReservatiesEF()
        {
            // Default constructor
        }

        public ReservatiesEF(int reservatieNummer, RestaurantEF restaurant, GebruikerEF gebruiker, int aantalPlaatsen, DateTime datum, TimeSpan uur, int tafelNummer)
        {
            ReservatieNummer = reservatieNummer;
            Restaurant = restaurant;
            Gebruiker = gebruiker;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            Uur = uur;
            TafelNummer = tafelNummer;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservatieNummer { get; set; }

        // Restautant
        public RestaurantEF Restaurant { get; set; }

        // Gebruiker
        public GebruikerEF Gebruiker { get; set; }

        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Uur { get; set; }
        public int TafelNummer { get; set; }
    }
}
