namespace Restaurant.Gebruiker.API.Models.Input
{
    public class ReservatieRESTInputDTO
    {
        public ReservatieRESTInputDTO(int aantalPlaatsen, DateTime datumUur)
        {
            AantalPlaatsen = aantalPlaatsen;
            DatumUur = datumUur;
        }

        public int AantalPlaatsen { get; set; }
        public DateTime DatumUur { get; set; }
    }
}
