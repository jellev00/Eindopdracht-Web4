using Restaurant.API.Exceptions;
using Restaurant.API.Models.Input;
using Restaurant.API.Models.Output;
using Restaurant.BL.Models;
using Restaurant.Gebruiker.API.Models.Input;
using System.Net.Mail;

namespace Restaurant.API.Mappers
{
    public class MapToDomain
    {
        public static BL.Models.Gebruiker MapToGebruikerDomain(GebruikerRESTinputDTO dto)
        {
            try
            {
                Locatie locatie = new Locatie(dto.locatie.Postcode, dto.locatie.Gemeentenaam, dto.locatie.Straatnaam, dto.locatie.Huisnummerlabel);
                return new BL.Models.Gebruiker(dto.Naam, dto.Email, dto.TelefoonNummer, locatie);
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromGebruikerDomain", ex);
            }
        }

        public static Reservatie MapToReservatieDomain(ReservatieRESTInputDTO dto)
        {
            try
            {
                return new Reservatie(dto.AantalPlaatsen, dto.DatumUur);
            }
            catch (Exception ex)
            {
                throw new MapException("MapToReservatieDomain", ex);
            }
        }

        public static BL.Models.Restaurant MapToRestaurantDomain(RestaurantRESTInputDTO dto)
        {
            try
            {
                Locatie locatie = new Locatie(dto.locatie.Postcode, dto.locatie.Gemeentenaam, dto.locatie.Straatnaam, dto.locatie.Huisnummerlabel);
                Contactgegevens contectgegevens = new Contactgegevens(dto.contactgegevens.TelefoonNr, dto.contactgegevens.Email);
                return new BL.Models.Restaurant(dto.Naam, locatie, dto.Keuken, contectgegevens, dto.Status);
            }
            catch (Exception ex)
            {
                throw new MapException("MapToRestaurantDomain", ex);
            }
        }

        public static Tafel MapToTafelDomain(TafelRESTInputDTO dto)
        {
            try
            {
                return new Tafel(dto.AantalPlaatsen);
            }
            catch (Exception ex)
            {
                throw new MapException("MapToTafelDomain", ex);
            }
        }
    }
}
