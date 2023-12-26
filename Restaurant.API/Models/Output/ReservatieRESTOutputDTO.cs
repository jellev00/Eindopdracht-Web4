namespace Restaurant.Gebruiker.API.Models.Output
{
    public class ReservatieRESTOutputDTO
    {
        public ReservatieRESTOutputDTO(int reservatieNr, string gebruiker, string restaurant, int aantalPlaatsen, DateTime datum, int tafelNr)
        {
            ReservatieNr = reservatieNr;
            Gebruiker = gebruiker;
            Restaurant = restaurant;
            AantalPlaatsen = aantalPlaatsen;
            Datum = datum;
            TafelNr = tafelNr;
        }

        public int ReservatieNr { get; set; }
        public string Gebruiker { get; set; }
        public string Restaurant { get; set; }
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
        public int TafelNr { get; set; }
    }
}
