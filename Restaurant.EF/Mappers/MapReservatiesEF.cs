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
        public static List<Reservatie> MapToDomain(List<ReservatiesEF> dbList)
        {
            try
            {
                List<Reservatie> reservaties = new List<Reservatie>();

                foreach (var db in dbList)
                {
                    Contactgegevens contactgegevens = new Contactgegevens(db.Restaurant.TelefoonNummer, db.Restaurant.Email);
                    Locatie locatieRestaurant = new Locatie(db.Restaurant.Postcode, db.Restaurant.GemeenteNaam, db.Restaurant.StraatNaam, db.Restaurant.HuisNummerLabel);
                    BL.Models.Restaurant restaurant = new BL.Models.Restaurant(db.Restaurant.Id, db.Restaurant.Naam, locatieRestaurant, db.Restaurant.Keuken, contactgegevens, db.Restaurant.Status);

                    Locatie locatieGebruiker = new Locatie(db.Gebruiker.Postcode, db.Gebruiker.GemeenteNaam, db.Gebruiker.StraatNaam, db.Gebruiker.HuisNummerLabel);
                    Gebruiker gebruiker = new Gebruiker(db.Gebruiker.KlantenNummer, db.Gebruiker.Naam, db.Gebruiker.Email, db.Gebruiker.TelefoonNummer, locatieGebruiker);

                    reservaties.Add(new Reservatie(db.ReservatieNummer, restaurant, gebruiker, db.AantalPlaatsen, db.DatumUur, db.TafelNummer));
                }

                return reservaties;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapReservatiesEF - MapToDomain", ex);
            }
        }

        public static Reservatie MapToDomain(ReservatiesEF db)
        {
            return MapToDomain(new List<ReservatiesEF> { db }).FirstOrDefault();
        }

        public static ReservatiesEF MapToDB(int klantenNr, int restaurantId, int ReservatieNr, Reservatie r, RestaurantContext ctx)
        {
            try
            {
                ReservatiesEF reservatie = ctx.Reservatie.Find(ReservatieNr);
                RestaurantEF restaurant = ctx.Restaurant.Find(restaurantId);
                GebruikerEF gebruiker = ctx.Gebruiker.Find(klantenNr);

                if (restaurant == null)
                {
                    throw new MapperException($"Restaurant met naam {r.RestaurantInfo.Naam} bestaat niet!");
                }
                if (gebruiker == null)
                {
                    throw new MapperException($"Gebruiker met klantenNummer {r.Gebruiker.KlantenNr} bestaat niet!");
                }

                if (reservatie == null)
                {
                    reservatie = new ReservatiesEF(restaurant, gebruiker, r.AantalPlaatsen, r.DatumUur, r.TafelNr);
                }
                else
                {
                    if (r.DatumUur != null)
                    {
                        reservatie.DatumUur = r.DatumUur;
                    }
                    if (r.AantalPlaatsen != null)
                    {
                        reservatie.AantalPlaatsen = r.AantalPlaatsen;
                    }
                }

                return reservatie;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapReservatiesEF - MapToDomain", ex);
            }
        }
    }
}
