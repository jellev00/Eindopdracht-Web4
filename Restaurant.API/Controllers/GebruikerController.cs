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

        private GebruikerManager _gebruikerManager;
        private ReservatieManager _reservatieManager;
        private RestaurantManager _restaurantManager;

        public GebruikerController(GebruikerManager gebruikerManager, ReservatieManager reservatieManager, RestaurantManager restaurantManager)
        {
            _gebruikerManager = gebruikerManager;
            _reservatieManager = reservatieManager;
            _restaurantManager = restaurantManager;
        }

        // User

        [HttpPost("AddGebruiker")]
        public ActionResult<GebruikerRESToutputDTO> AddGebruiker([FromBody] GebruikerRESTinputDTO inputDTO)
        {
            try
            {
                BL.Models.Gebruiker g = _gebruikerManager.AddGebruiker(MapToDomain.MapToGebruikerDomain(inputDTO));
                return CreatedAtAction(nameof(GetGebruikerByEmail), new { Email = g.Email }, MapFromDomain.MapFromGebruikerDomain(URL, g, _reservatieManager));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGebruikerByEmail/{email}")]
        public ActionResult<BL.Models.Gebruiker> GetGebruikerByEmail(string email)
        {
            try
            {
                BL.Models.Gebruiker gebruiker = _gebruikerManager.GetGebruikerByEmail(email);
                return Ok(MapFromDomain.MapFromGebruikerDomain(URL, gebruiker, _reservatieManager));
            }
            catch (Exception ex)
            {
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

                return Ok("Gebruiker updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteGebruiker/{email}")]
        public ActionResult DeleteGebruiker(string email)
        {
            try
            {
                if (!_gebruikerManager.ExistsGebruiker(email))
                {
                    return NotFound();
                }
                else
                {
                    _gebruikerManager.DeleteGebruiker(email);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Reservaties

        [HttpPost("{klantenNr}/Restaurant/{restaurantId}/Reservaties/AddReservatie")]
        public ActionResult<GebruikerRESToutputDTO> AddReservatie(int klantenNr, int restaurantId, [FromBody] ReservatieRESTInputDTO inputDTO)
        {
            try
            {
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

                return CreatedAtAction(nameof(GetAllReservaties), new { KlantenNr = klantenNr }, MapFromDomain.MapFromReservatieDomain(URL, r));
            }
            catch (Exception ex)
            {
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
                    return NotFound("Geen reserveringen gevonden voor de opgegeven criteria.");
                }

                List<ReservatieRESTOutputDTO> reservationDTOs = MapFromDomain.MapFromReservatiesDomain(URL, reservations);
                return Ok(reservationDTOs);
            }
            catch (GebruikerManagerException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
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

                return Ok("Reservatie updated successfully");
            }
            catch (Exception ex)
            {
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
                    return NotFound();
                }
                else
                {
                    _reservatieManager.DeleteReservatie(klantenNr, reservatieNr);
                    return Ok("Reservatie geanuleerd");
                }
            }
            catch (Exception ex)
            {
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
                return Ok(MapFromDomain.MapFromRestaurantDomain(URLRestaurant, restaurant));
            }
            catch (Exception ex)
            {
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
                    return NotFound("Geen reserveringen gevonden voor de opgegeven criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URLRestaurant, restaurants);
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
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
                    return NotFound("Geen reserveringen gevonden voor de opgegeven criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URLRestaurant, restaurants);
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAvailableRestaurants/{datum}/{aantalplaatsen}")]
        public ActionResult<List<RestaurantRESTOutputDTO>> GetAvailableRestaurants(DateTime datum, int aantalplaatsen)
        {
            try
            {
                List<BL.Models.Restaurant> availableRestaurants = _restaurantManager.GetAvailableRestaurants(datum, aantalplaatsen);

                if (availableRestaurants.Count == 0)
                {
                    return NotFound("Geen restaurant gevonden voor de opgegeven criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URLRestaurant, availableRestaurants);
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
