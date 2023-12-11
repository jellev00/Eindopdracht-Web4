using Restaurant.EF.Models;
using Restaurant.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Restaurant.EF.Exceptions;

namespace Restaurant.EF.Mappers
{
    public class MapReservatiesEF
    {
        public static Reservatie MapToDomain(ReservatiesEF db)
        {
            try
            {
                Contactgegevens contactgegevens = new Contactgegevens(db.Restaurant.TelefoonNummer, db.Restaurant.Email);
                Locatie locatieRestaurant = new Locatie(db.Restaurant.Postcode, db.Restaurant.GemeenteNaam, db.Restaurant.StraatNaam, db.Restaurant.HuisNummerLabel);
                BL.Models.Restaurant restaurant = new BL.Models.Restaurant(db.Restaurant.Naam, locatieRestaurant, db.Restaurant.Keuken, contactgegevens);

                Locatie locatieGebruiker = new Locatie(db.Gebruiker.Postcode, db.Gebruiker.GemeenteNaam, db.Gebruiker.StraatNaam, db.Gebruiker.HuisNummerLabel);
                Gebruiker gebruiker = new Gebruiker(db.Gebruiker.KlantenNummer, db.Gebruiker.Naam, db.Gebruiker.Email, db.Gebruiker.TelefoonNummer, locatieGebruiker);

                return new Reservatie(db.ReservatieNummer, restaurant, gebruiker, db.AantalPlaatsen, db.Datum, db.Uur, db.TafelNummer);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapReservatiesEF - MapToDomain", ex);
            }
        }

        public static ReservatiesEF MapToDB(Reservatie r, RestaurantContext ctx)
        {
            try
            {
                RestaurantEF restaurant = ctx.Restaurant.Find(r.RestaurantInfo.Naam);
                GebruikerEF gebruiker = ctx.Gebruiker.Find(r.Gebruiker.KlantenNr);

                if (restaurant == null)
                {
                    restaurant = MapRestaurantEF.MapToDB(r.RestaurantInfo, ctx);
                }
                if (gebruiker == null)
                {
                    gebruiker = MapGebruikerEF.MapToDB(r.Gebruiker, ctx);
                }

                return new ReservatiesEF(r.ReservatieNr, restaurant, gebruiker, r.AantalPlaatsen, r.Datum, r.Uur, r.TafelNr);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapReservatiesEF - MapToDomain", ex);
            }
        }
    }
}
