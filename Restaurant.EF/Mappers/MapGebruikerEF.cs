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
                Locatie locatie = new Locatie(db.Postcode, db.GemeenteNaam, db.StraatNaam, db.HuisNummerLabel);

                return new Gebruiker(db.KlantenNummer, db.Naam, db.Email, db.TelefoonNummer, locatie);
            } catch (Exception ex)
            {
                throw new MapperException("MapGebruikerEF - MapToDomain", ex);
            }
        }
        public static GebruikerEF MapToDB(Gebruiker g, RestaurantContext ctx)
        {
            try
            {
                return new GebruikerEF(g.Naam, g.Email, g.TelefoonNr, g.Locatie.Postcode, g.Locatie.Gemeentenaam, g.Locatie.Straatnaam, g.Locatie.Huisnummerlabel);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapGebruikerEF - MapToDB", ex);
            }
        }
    }
}
