using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Mappers;
using Restaurant.API.Models.Input;
using Restaurant.API.Models.Output;
using Restaurant.BL.Exceptions;
using Restaurant.BL.Managers;
using Restaurant.BL.Models;
using Restaurant.Gebruiker.API.Models.Input;
using Restaurant.Gebruiker.API.Models.Output;
using System;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private string URL = "http://localhost:5212/api/Gebruiker";
        private string URLRestaurant = "http://localhost:5212/api/Restaurant";

        private readonly ILogger _logger;
        private GebruikerManager _gebruikerManager;
        private ReservatieManager _reservatieManager;
        private RestaurantManager _restaurantManager;

        public GebruikerController(GebruikerManager gebruikerManager, ReservatieManager reservatieManager, RestaurantManager restaurantManager, ILoggerFactory loggerFactory)
        {
            _gebruikerManager = gebruikerManager;
            _reservatieManager = reservatieManager;
            _restaurantManager = restaurantManager;
            _logger = loggerFactory.AddFile("logs/GebruikerLogs.txt").CreateLogger("Gebruiker");
        }

        // User

        [HttpPost("AddGebruiker")]
        public ActionResult<GebruikerRESToutputDTO> AddGebruiker([FromBody] GebruikerRESTinputDTO inputDTO)
        {
            try
            {
                BL.Models.Gebruiker g = _gebruikerManager.AddGebruiker(MapToDomain.MapToGebruikerDomain(inputDTO));
                _logger.LogInformation("Gebruiker succesvol toegevoegd");
                return CreatedAtAction(nameof(GetGebruikerByEmail), new { Email = g.Email }, MapFromDomain.MapFromGebruikerDomain(URL, g, _reservatieManager));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het toevoegen van de gebruiker");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGebruikerByEmail/{email}")]
        public ActionResult<BL.Models.Gebruiker> GetGebruikerByEmail(string email)
        {
            try
            {
                _logger.LogInformation($"Gebruiker ophalen op basis van e-mail: {email}");
                BL.Models.Gebruiker gebruiker = _gebruikerManager.GetGebruikerByEmail(email);
                _logger.LogInformation("Gebruiker succesvol opgehaald");
                return Ok(MapFromDomain.MapFromGebruikerDomain(URL, gebruiker, _reservatieManager));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij het ophalen van de gebruiker op basis van e-mail: {email}");
                return NotFound(ex.Message);
            }
        }

        [HttpPut("UpdateGebruiker/{klantenNr}")]
        public ActionResult<BL.Models.Gebruiker> UpdateGebruiker(int klantenNr, [FromBody] GebruikerRESTinputDTO gI)
        {
            try
            {
                Locatie locatie = new Locatie(gI.locatie.Postcode, gI.locatie.Gemeentenaam, gI.locatie.Straatnaam, gI.locatie.Huisnummerlabel);
                BL.Models.Gebruiker gebruiker = new BL.Models.Gebruiker(gI.Naam, gI.Email, gI.TelefoonNummer, locatie);

                _gebruikerManager.UpdateGebruiker(klantenNr, gebruiker);

                _logger.LogInformation($"Gebruiker met klantenNr {klantenNr} succesvol bijgewerkt");

                return Ok("Gebruiker updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij het bijwerken van de gebruiker met klantenNr {klantenNr}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteGebruiker/{email}")]
        public ActionResult DeleteGebruiker(string email)
        {
            try
            {
                _logger.LogInformation($"Gebruiker verwijderen met e-mail: {email}");

                if (!_gebruikerManager.ExistsGebruiker(email))
                {
                    _logger.LogWarning($"Gebruiker met e-mail {email} niet gevonden voor verwijdering");
                    return NotFound();
                }
                else
                {
                    _gebruikerManager.DeleteGebruiker(email);
                    _logger.LogInformation($"Gebruiker met e-mail {email} succesvol verwijderd");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij het verwijderen van de gebruiker met e-mail: {email}");
                return BadRequest(ex.Message);
            }
        }

        // Reservaties

        [HttpPost("{klantenNr}/Restaurant/{restaurantId}/Reservaties/AddReservatie")]
        public ActionResult<GebruikerRESToutputDTO> AddReservatie(int klantenNr, int restaurantId, [FromBody] ReservatieRESTInputDTO inputDTO)
        {
            try
            {
                if (inputDTO.DatumUur < DateTime.Now)
                {
                    _logger.LogError("Fout bij het invullen van de datum");
                    return BadRequest("Datum mag niet kleiner zijn dan de datum van vandaag!");
                }

                Reservatie reservatie = _reservatieManager.AddReservatie(klantenNr, restaurantId, MapToDomain.MapToReservatieDomain(inputDTO));
                List<Reservatie> reservaties = _reservatieManager.GetAllReservationsByKlantenNr(klantenNr, null, null);
                
                Reservatie r = null;

                foreach (Reservatie reservation in reservaties)
                {
                    if (reservation.RestaurantInfo.Id == restaurantId)
                    {
                        r = reservation;
                    }
                }

                _logger.LogInformation($"Reservatie toegevoegd voor klantenNr {klantenNr}, restaurantId {restaurantId}");

                return CreatedAtAction(nameof(GetAllReservaties), new { KlantenNr = klantenNr }, MapFromDomain.MapFromReservatieDomain(URL, r));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het toevoegen van de reservatie");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{klantenNr}/Reservaties/GetAllReservaties")]
        public ActionResult<List<ReservatieRESTOutputDTO>> GetAllReservaties(int klantenNr, [FromQuery] DateTime? beginDatum, [FromQuery] DateTime? eindDatum)
        {
            // The {klantenNr} parameter is bound from the route, while beginDatum and eindDatum are bound from the query string.
            try
            {
                List<Reservatie> reservations = _reservatieManager.GetAllReservationsByKlantenNr(klantenNr, beginDatum, eindDatum);

                if (reservations.Count == 0)
                {
                    _logger.LogInformation($"Geen reserveringen gevonden voor klantenNr {klantenNr} en opgegeven criteria.");
                    return NotFound("Geen reserveringen gevonden voor de opgegeven criteria.");
                }

                List<ReservatieRESTOutputDTO> reservationDTOs = MapFromDomain.MapFromReservatiesDomain(URL, reservations);
                _logger.LogInformation($"Reserveringen opgehaald voor klantenNr {klantenNr}");
                return Ok(reservationDTOs);
            }
            catch (GebruikerManagerException ex)
            {
                _logger.LogError(ex, "Fout bij het ophalen van reserveringen");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Interne serverfout bij het ophalen van reserveringen");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{klantenNr}/Restaurant/{restaurantId}/Reservaties/{reservatieNr}/UpdateReservatie")]
        public ActionResult<GebruikerRESToutputDTO> UpdateReservatie(int klantenNr, int restaurantId, int reservatieNr, [FromBody] ReservatieRESTInputDTO inputDTO)
        {
            try
            {
                Reservatie reservatie = new Reservatie(inputDTO.AantalPlaatsen, inputDTO.DatumUur);

                _reservatieManager.UpdateReservatie(klantenNr, restaurantId, reservatieNr, reservatie);

                _logger.LogInformation($"Reservatie bijgewerkt voor klantenNr {klantenNr}, restaurantId {restaurantId}, reservatieNr {reservatieNr}");

                return Ok("Reservatie updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het bijwerken van de reservatie");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{klantenNr}/Reservaties/{reservatieNr}/DeleteReservatie")]
        public ActionResult DeleteReservatie(int klantenNr, int reservatieNr)
        {
            try
            {
                if (!_reservatieManager.ExistsResertvatie(reservatieNr))
                {
                    _logger.LogWarning($"Reservatie met nummer {reservatieNr} niet gevonden voor verwijdering");
                    return NotFound();
                }
                else
                {
                    _reservatieManager.DeleteReservatie(klantenNr, reservatieNr);
                    _logger.LogInformation($"Reservatie met nummer {reservatieNr} succesvol geannuleerd");
                    return Ok("Reservatie geanuleerd");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het annuleren van de reservatie");
                return BadRequest(ex.Message);
            }
        }

        // Restaurant

        [HttpGet("GetRestaurantByNaam/{naam}")]
        public ActionResult<BL.Models.Restaurant> GetRestaurantByNaam(string naam)
        {
            try
            {
                BL.Models.Restaurant restaurant = _restaurantManager.GetRestaurantByNaam(naam);
                _logger.LogInformation($"Restaurant opgehaald met naam {naam}");
                return Ok(MapFromDomain.MapFromRestaurantDomain(URLRestaurant, restaurant));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij het ophalen van het restaurant met naam {naam}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetRestaurantByPostcodeOrKeuken")]
        public ActionResult<List<BL.Models.Restaurant>> GetRestaurantByPostcodeOrKeuken(string? postcode, string? keuken)
        {
            try
            {
                List<BL.Models.Restaurant> restaurants = _restaurantManager.SearchRestaurants(postcode, keuken);

                if (restaurants.Count == 0)
                {
                    _logger.LogInformation($"Geen restaurants gevonden voor de opgegeven criteria. Postcode: {postcode}, Keuken: {keuken}");
                    return NotFound("Geen reserveringen gevonden voor de opgegeven criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URLRestaurant, restaurants);
                _logger.LogInformation($"Restaurants opgehaald voor criteria. Postcode: {postcode}, Keuken: {keuken}");
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij het ophalen van restaurants voor criteria. Postcode: {postcode}, Keuken: {keuken}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllRestaurants")]
        public ActionResult<List<BL.Models.Restaurant>> GetAllRestaurants()
        {
            try
            {
                List<BL.Models.Restaurant> restaurants = _restaurantManager.GetAllRestaurants();

                if (restaurants.Count == 0)
                {
                    _logger.LogInformation("Geen restaurants gevonden.");
                    return NotFound("Geen reserveringen gevonden voor de opgegeven criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URLRestaurant, restaurants);
                _logger.LogInformation("Alle restaurants opgehaald.");
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het ophalen van alle restaurants.");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAvailableRestaurants/{datum}/{aantalplaatsen}")]
        public ActionResult<List<RestaurantRESTOutputDTO>> GetAvailableRestaurants(DateTime datum, int aantalplaatsen)
        {
            try
            {
                if (datum < DateTime.Now)
                {
                    _logger.LogError("Fout bij het invullen van de datum");
                    return BadRequest("Datum mag niet kleiner zijn dan de datum van vandaag!");
                }

                List<BL.Models.Restaurant> availableRestaurants = _restaurantManager.GetAvailableRestaurants(datum, aantalplaatsen);

                if (availableRestaurants.Count == 0)
                {
                    _logger.LogInformation($"Geen beschikbare restaurants gevonden voor de opgegeven criteria. Datum: {datum}, Aantalplaatsen: {aantalplaatsen}");
                    return NotFound("Geen restaurant gevonden voor de opgegeven criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URLRestaurant, availableRestaurants);
                _logger.LogInformation($"Beschikbare restaurants opgehaald voor criteria. Datum: {datum}, Aantalplaatsen: {aantalplaatsen}");
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het ophalen van beschikbare restaurants.");
                return BadRequest(ex.Message);
            }
        }
    }
}
