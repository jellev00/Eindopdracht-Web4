using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Mappers;
using Restaurant.API.Models.Input;
using Restaurant.API.Models.Output;
using Restaurant.BL.Exceptions;
using Restaurant.BL.Managers;
using Restaurant.BL.Models;
using Restaurant.Gebruiker.API.Models.Output;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private string URL = "http://localhost:5212/api/Restaurant";
        private ReservatieManager _reservatieManager;
        private RestaurantManager _restaurantManager;

        public RestaurantController(RestaurantManager restaurantManager, ReservatieManager reservatieManager)
        {
            _restaurantManager = restaurantManager;
            _reservatieManager = reservatieManager;
        }

        // Restaurant

        [HttpPost("AddRestaurant")]
        public ActionResult<RestaurantRESTOutputDTO> AddRestaurant([FromBody] RestaurantRESTInputDTO inputDTO)
        {
            try
            {
                BL.Models.Restaurant r = _restaurantManager.AddRestaurant(MapToDomain.MapToRestaurantDomain(inputDTO));
                return CreatedAtAction(nameof(GetRestaurantByNaam), new { Naam = r.Naam }, MapFromDomain.MapFromRestaurantDomain(URL, r));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRestaurantByNaam/{naam}")]
        public ActionResult<BL.Models.Restaurant> GetRestaurantByNaam(string naam)
        {
            try
            {
                BL.Models.Restaurant restaurant = _restaurantManager.GetRestaurantByNaam(naam);
                return Ok(MapFromDomain.MapFromRestaurantDomain(URL, restaurant));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllRestaurants")]
        public ActionResult<BL.Models.Restaurant> GetAllRestaurants()
        {
            try
            {
                List<BL.Models.Restaurant> restaurants = _restaurantManager.GetAllRestaurants();

                if (restaurants.Count == 0)
                {
                    return NotFound("No reservations found for the specified criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URL, restaurants);
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("UpdateRestaurant/{Id}")]
        public ActionResult<BL.Models.Restaurant> UpdateGebruiker(int Id, [FromBody] RestaurantRESTInputDTO rI)
        {
            try
            {
                Locatie locatie = new Locatie(rI.locatie.Postcode, rI.locatie.Gemeentenaam, rI.locatie.Straatnaam, rI.locatie.Huisnummerlabel);
                Contactgegevens contactgegevens = new Contactgegevens(rI.contactgegevens.TelefoonNr, rI.contactgegevens.Email);
                BL.Models.Restaurant restaurant = new BL.Models.Restaurant(rI.Naam, locatie, rI.Keuken, contactgegevens, rI.Status);

                _restaurantManager.UpdateRestaurant(Id, restaurant);

                return Ok("Restaurant updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRestaurant/{naam}")]
        public ActionResult DeleteGebruiker(string naam)
        {
            try
            {
                if (!_restaurantManager.ExistsRestaurant(naam))
                {
                    return NotFound();
                }
                else
                {
                    _restaurantManager.DeleteRestaurant(naam);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Tafels

        [HttpPost("{restaurantName}/Tafels/AddTafel")]
        public ActionResult<TafelRESTOutputDTO> AddTafel(string restaurantName, [FromBody] TafelRESTInputDTO inputDTO)
        {
            try
            {
                BL.Models.Restaurant restaurant = _restaurantManager.GetRestaurantByNaam(restaurantName);

                Tafel t = _restaurantManager.AddTafel(restaurant.Id, MapToDomain.MapToTafelDomain(inputDTO));

                return CreatedAtAction(nameof(GetTafels), new { restaurantId = restaurant.Id }, MapFromDomain.MapFromTafelDomain(URL, t, restaurant));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{restaurantId}/Tafels/GetTafels")]
        public ActionResult<Tafel> GetTafels(int restaurantId)
        {
            try
            {
                List<Tafel> tafels = _restaurantManager.GetTafels(restaurantId);

                if (tafels.Count == 0)
                {
                    return NotFound("Geen tafel gevonden");
                }

                List<TafelRESTOutputDTO> tafelDTOs = MapFromDomain.MapFromTafelsDomain(URL, tafels);
                return Ok(tafelDTOs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{restaurantId}/Tafels/{tafelId}/UpdateTafel")]
        public ActionResult<Tafel> UpdateTafel(int restaurantId, int tafelId, [FromBody] TafelRESTInputDTO rI)
        {
            try
            {
                Tafel tafel = new Tafel(rI.AantalPlaatsen);

                _restaurantManager.UpdateTafel(restaurantId, tafelId, tafel);

                return Ok("Tafel updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{restaurantId}/Tafels/{tafelId}/DeleteTafel")]
        public ActionResult DeleteTafel(int restaurantId, int tafelId)
        {
            try
            {
                if (!_restaurantManager.ExistsTafel(tafelId))
                {
                    return NotFound();
                }
                else
                {
                    _restaurantManager.DeleteTafel(restaurantId, tafelId);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Reservaties

        [HttpGet("{restaurantName}/Reservaties/GetAllReservaties")]
        public ActionResult<List<ReservatieRESTOutputDTO>> GetAllReservaties(string restaurantName, [FromQuery] DateTime? beginDatum, [FromQuery] DateTime? eindDatum)
        {
            // The {klantenNr} parameter is bound from the route, while beginDatum and eindDatum are bound from the query string.
            try
            {
                List<Reservatie> reservations = _reservatieManager.GetAllReservationsByRestauranNaam(restaurantName, beginDatum, eindDatum);

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
    }
}
