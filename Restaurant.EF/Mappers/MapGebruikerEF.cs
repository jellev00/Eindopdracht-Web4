using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class MapGebruikerEF
    {
        public static Gebruiker MapToDomain(GebruikerEF db)
        {
            try
            {
                List<Reservatie> reservaties = new List<Reservatie>();

                if (db.Reservaties != null)
                {
                    foreach (ReservatiesEF r in db.Reservaties)
                    {
                        Locatie locatieRestaurant = new Locatie(r.Restaurant.Postcode, r.Restaurant.GemeenteNaam, r.Restaurant.StraatNaam, r.Restaurant.HuisNummerLabel);
                        Contactgegevens contactgegevens = new Contactgegevens(r.Restaurant.TelefoonNummer, r.Restaurant.Email);
                        BL.Models.Restaurant restaurant = new BL.Models.Restaurant(r.Restaurant.Id, r.Restaurant.Naam, locatieRestaurant, r.Restaurant.Keuken, contactgegevens, r.Restaurant.Status);

                        Locatie locatieGebruiker = new Locatie(r.Gebruiker.Postcode, r.Gebruiker.GemeenteNaam, r.Gebruiker.StraatNaam, r.Gebruiker.HuisNummerLabel);
                        Gebruiker g = new Gebruiker(r.Gebruiker.KlantenNummer, r.Gebruiker.Naam, r.Gebruiker.Email, r.Gebruiker.TelefoonNummer, locatieGebruiker);

                        Reservatie reservatie = new Reservatie(r.ReservatieNummer, restaurant, g, r.AantalPlaatsen, r.DatumUur, r.TafelNummer);

                        reservaties.Add(reservatie);
                    }
                }

                Locatie locatie = new Locatie(db.Postcode, db.GemeenteNaam, db.StraatNaam, db.HuisNummerLabel);

                return new Gebruiker(db.KlantenNummer, db.Naam, db.Email, db.TelefoonNummer, locatie, reservaties);
            } catch (Exception ex)
            {
                throw new MapperException("MapGebruikerEF - MapToDomain", ex);
            }
        }
        public static GebruikerEF MapToDB(int klantenNr, Gebruiker g, RestaurantContext ctx)
        {
            try
            {

                GebruikerEF gebruiker = ctx.Gebruiker.Find(klantenNr);

                List<ReservatiesEF> reservaties = new List<ReservatiesEF>();

                if (g.Reservatie != null)
                {
                    foreach (Reservatie reservatie in g.Reservatie.ToList())
                    {
                        ReservatiesEF reservatiesEF = MapReservatiesEF.MapToDB(klantenNr, reservatie.RestaurantInfo.Id, reservatie.ReservatieNr, reservatie, ctx);
                        reservaties.Add(reservatiesEF);
                    }
                }

                if (gebruiker == null)
                {
                    gebruiker = new GebruikerEF(g.Naam, g.Email, g.TelefoonNr, g.Locatie.Postcode, g.Locatie.Gemeentenaam, g.Locatie.Straatnaam, g.Locatie.Huisnummerlabel);
                }
                else
                {
                    if (g.Naam != "string")
                    {
                        gebruiker.Naam = g.Naam;
                    }

                    if (g.Email != "string")
                    {
                        gebruiker.Email = g.Email;
                    }

                    if (g.TelefoonNr != "string")
                    {
                        gebruiker.TelefoonNummer = g.TelefoonNr;
                    }

                    if (g.Locatie != null)
                    {
                        if (g.Locatie.Postcode != "string")
                        {
                            gebruiker.Postcode = g.Locatie.Postcode;
                        }

                        if (g.Locatie.Gemeentenaam != "string")
                        {
                            gebruiker.GemeenteNaam = g.Locatie.Gemeentenaam;
                        }

                        if (g.Locatie.Straatnaam != "string")
                        {
                            gebruiker.StraatNaam = g.Locatie.Straatnaam;
                        }

                        if (g.Locatie.Huisnummerlabel != "string")
                        {
                            gebruiker.HuisNummerLabel = g.Locatie.Huisnummerlabel;
                        }
                    }

                    if (reservaties != null)
                    {
                        gebruiker.Reservaties = reservaties;
                    }
                }

                return gebruiker;
            }
            catch (Exception ex)
            {
                throw new MapperException("MapGebruikerEF - MapToDB", ex);
            }
        }
    }
}
