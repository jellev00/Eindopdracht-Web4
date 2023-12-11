using Restaurant.API.Exceptions;
using Restaurant.API.Models.Input;
using Restaurant.BL.Models;
using System.Net.Mail;

namespace Restaurant.API.Mappers
{
    public class MapToDomain
    {
        public static Gebruiker MapToGebruikerDomain(GebruikerRESTinputDTO dto)
        {
            try
            {
                Locatie locatie = new Locatie(dto.locatie.Postcode, dto.locatie.Gemeentenaam, dto.locatie.Straatnaam, dto.locatie.Huisnummerlabel);

                return new Gebruiker(dto.Naam , dto.Email, dto.TelefoonNummer, locatie);
            }
            catch (Exception ex)
            {
                throw new MapException("maptogemeentedomain", ex);
            }
        }
    }
}
