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

        public ReservatiesEF(RestaurantEF restaurant, GebruikerEF gebruiker, int aantalPlaatsen, DateTime datumUur, int tafelNummer)
        {
            Restaurant = restaurant;
            Gebruiker = gebruiker;
            AantalPlaatsen = aantalPlaatsen;
            DatumUur = datumUur;
            TafelNummer = tafelNummer;
        }

        public ReservatiesEF(int reservatieNummer, RestaurantEF restaurant, GebruikerEF gebruiker, int aantalPlaatsen, DateTime datumUur, int tafelNummer)
        {
            ReservatieNummer = reservatieNummer;
            Restaurant = restaurant;
            Gebruiker = gebruiker;
            AantalPlaatsen = aantalPlaatsen;
            DatumUur = datumUur;
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
        public DateTime DatumUur { get; set; }
        public int TafelNummer { get; set; }
    }
}
