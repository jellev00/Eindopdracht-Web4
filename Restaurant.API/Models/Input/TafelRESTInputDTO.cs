namespace Restaurant.API.Models.Input
{
    public class TafelRESTInputDTO
    {
        public TafelRESTInputDTO(int aantalPlaatsen)
        {
            AantalPlaatsen = aantalPlaatsen;
        }

        public int AantalPlaatsen { get; set; }
    }
}
