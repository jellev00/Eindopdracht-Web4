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
    public class MapTafelEF
    {
        public static Tafel MapToDomain(TafelsEF db)
        {
            try
            {
                Locatie locatie = new Locatie(db.Restaurant.Postcode, db.Restaurant.GemeenteNaam, db.Restaurant.StraatNaam, db.Restaurant.HuisNummerLabel);
                Contactgegevens contactgegevens = new Contactgegevens(db.Restaurant.TelefoonNummer, db.Restaurant.Email);

                BL.Models.Restaurant restaurant =  new BL.Models.Restaurant(db.Restaurant.Naam, locatie, db.Restaurant.Keuken, contactgegevens, db.Restaurant.Status);

                return new BL.Models.Tafel(db.Id, db.AantalPlaatsen, restaurant);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapTafelEF - MapToDomain", ex);
            }
        }
        public static TafelsEF MapToDB(int restaurantId, int tafelId, Tafel t, RestaurantContext ctx)
        {
            try
            {
                TafelsEF tafel = ctx.Tafels.Find(tafelId);
                RestaurantEF restaurant = ctx.Restaurant.Find(restaurantId);

                if (restaurant == null)
                {
                    throw new MapperException($"Restaurant met ID {restaurantId} bestaat niet!");
                }

                if (tafel == null)
                {
                    tafel = new TafelsEF(t.AantalPlaatsen, restaurant);
                }
                else
                {
                    if (t.AantalPlaatsen != 0)
                    {
                        tafel.AantalPlaatsen = t.AantalPlaatsen;
                    }
                }

                return tafel;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapTafelEF - MapToDB", ex);
            }
        }
    }
}
