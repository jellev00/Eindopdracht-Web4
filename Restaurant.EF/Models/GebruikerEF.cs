using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Models
{
    public class GebruikerEF
    {
        public GebruikerEF()
        {
            // Default constructor
        }

        public GebruikerEF(string naam, string email, string telefoonNummer, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel)
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Postcode = postcode;
            GemeenteNaam = gemeenteNaam;
            StraatNaam = straatNaam;
            HuisNummerLabel = huisNummerLabel;
        }

        public GebruikerEF(int klantenNummer, string naam, string email, string telefoonNummer, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel)
        {
            KlantenNummer = klantenNummer;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Postcode = postcode;
            GemeenteNaam = gemeenteNaam;
            StraatNaam = straatNaam;
            HuisNummerLabel = huisNummerLabel;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KlantenNummer { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Naam { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string TelefoonNummer { get; set; }

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

        public List<ReservatiesEF> Reservaties { get; set; } = new List<ReservatiesEF>();
    }
}
