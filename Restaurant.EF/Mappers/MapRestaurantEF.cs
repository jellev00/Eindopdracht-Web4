using Restaurant.BL.Models;
using Restaurant.EF.Exceptions;
using Restaurant.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Mappers
{
    public class MapRestaurantEF
    {
        public static BL.Models.Restaurant MapToDomain(RestaurantEF db)
        {
            try
            {
                Locatie locatie = new Locatie(db.Postcode, db.GemeenteNaam, db.StraatNaam, db.HuisNummerLabel);
                Contactgegevens contactgegevens = new Contactgegevens(db.TelefoonNummer, db.Email);

                return new BL.Models.Restaurant(db.Naam, locatie, db.Keuken, contactgegevens);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapGebruikerEF - MapToDomain", ex);
            }
        }
        public static RestaurantEF MapToDB(BL.Models.Restaurant r, RestaurantContext ctx)
        {
            try
            {
                return new RestaurantEF(r.Naam, r.Locatie.Postcode, r.Locatie.Gemeentenaam, r.Locatie.Straatnaam, r.Locatie.Huisnummerlabel, r.Keuken, r.Contactgegevens.TelefoonNr, r.Contactgegevens.Email);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapGebruikerEF - MapToDB", ex);
            }
        }
    }
}
