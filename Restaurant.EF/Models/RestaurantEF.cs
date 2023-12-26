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

        public RestaurantEF(string naam, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel, string keuken, string telefoonNummer, string email, bool status)
        {
            Naam = naam;
            Postcode = postcode;
            GemeenteNaam = gemeenteNaam;
            StraatNaam = straatNaam;
            HuisNummerLabel = huisNummerLabel;
            Keuken = keuken;
            TelefoonNummer = telefoonNummer;
            Email = email;
            Status = status;
        }

        public RestaurantEF(int id, string naam, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel, string keuken, string telefoonNummer, string email, bool status)
        {
            Id = id;
            Naam = naam;
            Postcode = postcode;
            GemeenteNaam = gemeenteNaam;
            StraatNaam = straatNaam;
            HuisNummerLabel = huisNummerLabel;
            Keuken = keuken;
            TelefoonNummer = telefoonNummer;
            Email = email;
            Status = status;
        }

        public RestaurantEF(string naam, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel, string keuken, string telefoonNummer, string email, bool status, List<TafelsEF> tafels, List<ReservatiesEF> reservaties) : this(naam, postcode, gemeenteNaam, straatNaam, huisNummerLabel, keuken, telefoonNummer, email, status)
        {
            Tafels = tafels;
            Reservaties = reservaties;
        }

        public RestaurantEF(int id, string naam, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel, string keuken, string telefoonNummer, string email, bool status, List<TafelsEF> tafels, List<ReservatiesEF> reservaties) : this(id, naam, postcode, gemeenteNaam, straatNaam, huisNummerLabel, keuken, telefoonNummer, email, status)
        {
            Tafels = tafels;
            Reservaties = reservaties;
        }

        [Key]
        public int Id { get; set; }

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
        public string TelefoonNummer { get; set; }

        [Required]
        public string Email { get; set; }

        public bool Status { get; set; }

        public List<TafelsEF> Tafels { get; set; } = new List<TafelsEF>();

        public List<ReservatiesEF> Reservaties { get; set; } = new List<ReservatiesEF>();
    }
}
