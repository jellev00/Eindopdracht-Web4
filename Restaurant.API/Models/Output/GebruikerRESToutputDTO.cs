namespace Restaurant.API.Models.Output
{
    public class GebruikerRESToutputDTO
    {
        public GebruikerRESToutputDTO(int klantenNummer, string naam, string email, string telefoonNummer, string postcode, string gemeenteNaam, string straatNaam, string huisNummerLabel)
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

        public int KlantenNummer { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public string Postcode { get; set; }
        public string GemeenteNaam { get; set; }
        public string StraatNaam { get; set; }
        public string HuisNummerLabel { get; set; }
    }
}
