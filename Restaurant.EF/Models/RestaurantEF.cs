using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Models
{
    public class RestaurantEF
    {
        public RestaurantEF()
        {
            // Default constructor
        }

        public RestaurantEF(string naam, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel, string keuken, int telefoonNummer, string email)
        {
            Naam = naam;
            Postcode = postcode;
            GemeenteNaam = gemeenteNaam;
            StraatNaam = straatNaam;
            HuisNummerLabel = huisNummerLabel;
            Keuken = keuken;
            TelefoonNummer = telefoonNummer;
            Email = email;
        }

        [Key]
        [Column(TypeName = "varchar(150)")]
        public string Naam { get; set; }

        // Locatie
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Postcode { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string GemeenteNaam { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string StraatNaam { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string HuisNummerLabel { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Keuken { get; set; }

        // ContactGegevens
        [Required]
        public int TelefoonNummer { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
