namespace Restaurant.API.Models.Output
{
    public class TafelRESTOutputDTO
    {
        public TafelRESTOutputDTO(int aantalPlaatsen, string restaurant)
        {
            AantalPlaatsen = aantalPlaatsen;
            Restaurant = restaurant;
        }

        public TafelRESTOutputDTO(int id, int aantalPlaatsen, string restaurant)
        {
            Id = id;
            AantalPlaatsen = aantalPlaatsen;
            Restaurant = restaurant;
        }

        public int Id { get; set; }
        public int AantalPlaatsen { get; set; }
        public string Restaurant { get; set; }
    }
}
