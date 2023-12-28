using Restaurant.API.Exceptions;
using Restaurant.API.Models.Input;
using Restaurant.API.Models.Output;
using Restaurant.BL.Managers;
using Restaurant.BL.Models;
using Restaurant.EF.Models;
using Restaurant.Gebruiker.API.Models.Output;
using System;

namespace Restaurant.API.Mappers
{
    public class MapFromDomain
    {
        public static GebruikerRESToutputDTO MapFromGebruikerDomain(string url, BL.Models.Gebruiker g, ReservatieManager reservatieManager)
        {
            try 
            {
                string reservatiesURL;

                if (g.Reservatie == null || g.Reservatie.Count == 0)
                {
                    reservatiesURL = null;
                } else
                {
                    reservatiesURL = $"{url}/{g.KlantenNr}/Reservaties/GetAllReservaties";
                }

                //List<string> reservaties = reservatieManager.GetAllReservationsByKlantenNr(g.KlantenNr, null, null).Select(x => gebruikerURL + $"/reservatie/{x.ReservatieNr}").ToList();

                string locatie = $"{g.Locatie.Straatnaam} {g.Locatie.Huisnummerlabel}, {g.Locatie.Postcode} {g.Locatie.Gemeentenaam}";

                GebruikerRESToutputDTO dto = new GebruikerRESToutputDTO(g.KlantenNr, g.Naam, g.Email, g.TelefoonNr, locatie, reservatiesURL);

                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromGebruikerDomain", ex);
            }
        }

        public static List<ReservatieRESTOutputDTO> MapFromReservatiesDomain(string url, List<Reservatie> reservaties)
        {
            try
            {
                List<ReservatieRESTOutputDTO> reservatieDTOs = new List<ReservatieRESTOutputDTO>();

                foreach (var r in reservaties)
                {
                    string encodedEmail = Uri.EscapeDataString(r.Gebruiker.Email);
                    string gebruikerURL = $"{url}/GetGebruikerByEmail/{encodedEmail}";
                    string encodedRestaurantNaam = Uri.EscapeDataString(r.RestaurantInfo.Naam);
                    string restaurantURL = $"{url}/GetRestaurantByNaam/{encodedRestaurantNaam}";

                    ReservatieRESTOutputDTO dto = new ReservatieRESTOutputDTO(r.ReservatieNr, gebruikerURL, restaurantURL, r.AantalPlaatsen, r.DatumUur, r.TafelNr);
                    reservatieDTOs.Add(dto);
                }

                return reservatieDTOs;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromReservatiesDomain", ex);
            }
        }

        public static ReservatieRESTOutputDTO MapFromReservatieDomain(string url, Reservatie r)
        {
            try
            {
                string encodedEmail = Uri.EscapeDataString(r.Gebruiker.Email);
                string gebruikerURL = $"{url}/GetGebruikerByEmail/{encodedEmail}";
                string encodedRestaurantNaam = Uri.EscapeDataString(r.RestaurantInfo.Naam);
                string restaurantURL = $"{url}/GetRestaurantByNaam/{encodedRestaurantNaam}";

                ReservatieRESTOutputDTO dto = new ReservatieRESTOutputDTO(r.ReservatieNr, gebruikerURL, restaurantURL, r.AantalPlaatsen, r.DatumUur, r.TafelNr);

                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromReservatieDomain", ex);
            }
        }

        public static RestaurantRESTOutputDTO MapFromRestaurantDomain(string url, BL.Models.Restaurant r)
        {
            try
            {
                string reservatiesURL;
                string tafelsURL;

                string locatie = $"{r.Locatie.Straatnaam} {r.Locatie.Huisnummerlabel}, {r.Locatie.Postcode} {r.Locatie.Gemeentenaam}";
                string telefoonNr = $"{r.Contactgegevens.TelefoonNr}";
                string email = $"{r.Contactgegevens.Email}";

                if (r.Reservatie == null || r.Reservatie.Count == 0)
                {
                    reservatiesURL = null;
                }
                else
                {
                    string encodedRestaurantNaam = Uri.EscapeDataString(r.Naam);
                    reservatiesURL = $"{url}/{encodedRestaurantNaam}/Reservaties/GetAllReservaties";
                }

                if (r.Tafel == null || r.Tafel.Count == 0)
                {
                    tafelsURL = null;
                }
                else
                {
                    tafelsURL = $"{url}/{r.Id}/Tafels/GetTafels";
                }

                RestaurantRESTOutputDTO dto = new RestaurantRESTOutputDTO(r.Id, r.Naam, locatie, r.Keuken, telefoonNr, email, tafelsURL, reservatiesURL);

                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromRestaurantDomain", ex);
            }
        }

        public static List<RestaurantRESTOutputDTO> MapFromRestaurantsDomain(string url, List<BL.Models.Restaurant> restaurants)
        {
            try
            {
                List<RestaurantRESTOutputDTO> restaurantDTOs = new List<RestaurantRESTOutputDTO>();
                string reservatiesURL;
                string tafelsURL;

                foreach (var r in restaurants)
                {
                    string locatie = $"{r.Locatie.Straatnaam} {r.Locatie.Huisnummerlabel}, {r.Locatie.Postcode} {r.Locatie.Gemeentenaam}";
                    string telefoonNr = $"{r.Contactgegevens.TelefoonNr}";
                    string email = $"{r.Contactgegevens.Email}";

                    if (r.Reservatie == null || r.Reservatie.Count == 0)
                    {
                        reservatiesURL = null;
                    }
                    else
                    {
                        string encodedRestaurantNaam = Uri.EscapeDataString(r.Naam);
                        reservatiesURL = $"{url}/{encodedRestaurantNaam}/Reservaties/GetAllReservaties";
                    }

                    if (r.Tafel == null || r.Tafel.Count == 0)
                    {
                        tafelsURL = null;
                    }
                    else
                    {
                        tafelsURL = $"{url}/{r.Id}/Tafels/GetTafels";
                    }

                    RestaurantRESTOutputDTO dto = new RestaurantRESTOutputDTO(r.Id, r.Naam, locatie, r.Keuken, telefoonNr, email, tafelsURL, reservatiesURL);
                    restaurantDTOs.Add(dto);
                }

                return restaurantDTOs;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromRestaurantDomain", ex);
            }
        }

        public static List<TafelRESTOutputDTO> MapFromTafelsDomain(string url, List<Tafel> tafels)
        {
            try
            {
                List<TafelRESTOutputDTO> tafelDTOs = new List<TafelRESTOutputDTO>();

                foreach (var t in tafels)
                {
                    string encodedRestaurantNaam = Uri.EscapeDataString(t.Restaurant.Naam);
                    string restaurantURL = $"{url}/GetRestaurantByNaam/{encodedRestaurantNaam}";
                    TafelRESTOutputDTO dto = new TafelRESTOutputDTO(t.Id, t.AantalPlaatsen, restaurantURL);

                    tafelDTOs.Add(dto);
                }

                return tafelDTOs;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromTafelsDomain", ex);
            }
        }

        public static TafelRESTOutputDTO MapFromTafelDomain(string url, Tafel t, BL.Models.Restaurant r)
        {
            try
            {
                string encodedRestaurantNaam = Uri.EscapeDataString(r.Naam);
                string restaurantURL = $"{url}/GetRestaurantByNaam/{encodedRestaurantNaam}";
                TafelRESTOutputDTO dto = new TafelRESTOutputDTO(t.AantalPlaatsen, restaurantURL);

                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromTafelDomain", ex);
            }
        }
    }
}
