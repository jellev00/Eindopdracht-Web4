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
        public static List<BL.Models.Restaurant> MapToDomain(List<RestaurantEF> dbList)
        {
            try
            {
                List<BL.Models.Restaurant> restaurants = new List<BL.Models.Restaurant>();

                foreach (var db in dbList)
                {
                    Locatie locatie = new Locatie(db.Postcode, db.GemeenteNaam, db.StraatNaam, db.HuisNummerLabel);
                    Contactgegevens contactgegevens = new Contactgegevens(db.TelefoonNummer, db.Email);
                    BL.Models.Restaurant restaurant = new BL.Models.Restaurant(db.Id, db.Naam, locatie, db.Keuken, contactgegevens, db.Status);

                    List<Tafel> tafels = new List<Tafel>();

                    if (db.Tafels != null)
                    {
                        foreach (TafelsEF t in db.Tafels)
                        {
                            Tafel tafel = new Tafel(t.Id, t.AantalPlaatsen, restaurant);
                            tafels.Add(tafel);
                        }
                    }

                    List<Reservatie> reservaties = new List<Reservatie>();

                    if (db.Reservaties != null)
                    {
                        foreach (ReservatiesEF r in db.Reservaties)
                        {
                            Locatie locatieGebruiker = new Locatie(r.Gebruiker.Postcode, r.Gebruiker.GemeenteNaam, r.Gebruiker.StraatNaam, r.Gebruiker.HuisNummerLabel);
                            Gebruiker gebruiker = new Gebruiker(r.Gebruiker.KlantenNummer, r.Gebruiker.Naam, r.Gebruiker.Email, r.Gebruiker.TelefoonNummer, locatieGebruiker);

                            Reservatie reservatie = new Reservatie(r.ReservatieNummer, restaurant, gebruiker, r.AantalPlaatsen, r.DatumUur, r.TafelNummer);

                            reservaties.Add(reservatie);
                        }
                    }

                    restaurants.Add(new BL.Models.Restaurant(db.Id, db.Naam, locatie, db.Keuken, contactgegevens, db.Status, tafels, reservaties));
                }

                return restaurants;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapRestaurantEF - MapToDomain", ex);
            }
        }

        public static BL.Models.Restaurant MapToDomain(RestaurantEF db)
        {
            return MapToDomain(new List<RestaurantEF> { db }).FirstOrDefault();
        }

        public static RestaurantEF MapToDB(int Id, BL.Models.Restaurant r, RestaurantContext ctx)
        {
            try
            {
                RestaurantEF restaurant = ctx.Restaurant.Find(Id);


                List<TafelsEF> tafels = new List<TafelsEF>();

                if (r.Tafel != null)
                {
                    foreach (Tafel t in r.Tafel)
                    {
                        TafelsEF tafelEF = MapTafelEF.MapToDB(Id, t.Id, t, ctx);

                        tafels.Add(tafelEF);
                    }
                }

                List<ReservatiesEF> reservaties = new List<ReservatiesEF>();

                if (r.Reservatie != null)
                {
                    foreach (Reservatie reservatie in r.Reservatie)
                    {
                        ReservatiesEF reservatieEF = MapReservatiesEF.MapToDB(reservatie.Gebruiker.KlantenNr, r.Id, reservatie.ReservatieNr, reservatie, ctx);

                        reservaties.Add(reservatieEF);
                    }
                }

                if (restaurant == null)
                {
                    restaurant = new RestaurantEF(r.Naam, r.Locatie.Postcode, r.Locatie.Gemeentenaam, r.Locatie.Straatnaam, r.Locatie.Huisnummerlabel, r.Keuken, r.Contactgegevens.TelefoonNr, r.Contactgegevens.Email, r.Status, tafels, reservaties);
                }
                else
                {
                    if (r.Naam != "string")
                    {
                        restaurant.Naam = r.Naam;
                    }
                    if (r.Locatie.Postcode != "string")
                    {
                        restaurant.Postcode = r.Locatie.Postcode;
                    }
                    if (r.Locatie.Gemeentenaam != "string")
                    {
                        restaurant.GemeenteNaam = r.Locatie.Gemeentenaam;
                    }
                    if (r.Locatie.Straatnaam != "string")
                    {
                        restaurant.StraatNaam = r.Locatie.Straatnaam;
                    }
                    if (r.Locatie.Huisnummerlabel != "string")
                    {
                        restaurant.HuisNummerLabel = r.Locatie.Huisnummerlabel;
                    }
                    if (r.Keuken != "string")
                    {
                        restaurant.Keuken = r.Keuken;
                    }
                    if (r.Contactgegevens.TelefoonNr != "string")
                    {
                        restaurant.TelefoonNummer = r.Contactgegevens.TelefoonNr;
                    }
                    if (r.Contactgegevens.Email != "string")
                    {
                        restaurant.Email = r.Contactgegevens.Email;
                    }
                    restaurant.Status = r.Status;
                    if (tafels != null)
                    {
                        restaurant.Tafels = tafels;
                    }
                    if (reservaties != null)
                    {
                        restaurant.Reservaties = reservaties;
                    }
                }

                return restaurant;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapRestaurantEF - MapToDB", ex);
            }
        }
    }
}
