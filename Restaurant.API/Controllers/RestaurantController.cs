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

        private readonly ILogger<RestaurantController> _logger;
        private ReservatieManager _reservatieManager;
        private RestaurantManager _restaurantManager;

        public RestaurantController(RestaurantManager restaurantManager, ReservatieManager reservatieManager, ILogger<RestaurantController> logger)
        {
            _restaurantManager = restaurantManager;
            _reservatieManager = reservatieManager;
            _logger = logger;
        }

        // Restaurant

        [HttpPost("AddRestaurant")]
        public ActionResult<RestaurantRESTOutputDTO> AddRestaurant([FromBody] RestaurantRESTInputDTO inputDTO)
        {
            try
            {
                BL.Models.Restaurant r = _restaurantManager.AddRestaurant(MapToDomain.MapToRestaurantDomain(inputDTO));
                _logger.LogInformation($"Restaurant toegevoegd: {r.Naam}");
                return CreatedAtAction(nameof(GetRestaurantByNaam), new { Naam = r.Naam }, MapFromDomain.MapFromRestaurantDomain(URL, r));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het toevoegen van het restaurant");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRestaurantByNaam/{naam}")]
        public ActionResult<BL.Models.Restaurant> GetRestaurantByNaam(string naam)
        {
            try
            {
                BL.Models.Restaurant restaurant = _restaurantManager.GetRestaurantByNaam(naam);
                _logger.LogInformation($"Restaurant opgehaald met naam {naam}");
                return Ok(MapFromDomain.MapFromRestaurantDomain(URL, restaurant));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij het ophalen van het restaurant met naam {naam}");
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
                    _logger.LogInformation("Geen restaurants gevonden.");
                    return NotFound("No reservations found for the specified criteria.");
                }

                List<RestaurantRESTOutputDTO> restaurantDTOs = MapFromDomain.MapFromRestaurantsDomain(URL, restaurants);
                _logger.LogInformation("Alle restaurants opgehaald.");
                return Ok(restaurantDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het ophalen van alle restaurants.");
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

                _logger.LogInformation($"Restaurant geüpdatet met ID {Id}");

                return Ok("Restaurant updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het updaten van het restaurant");
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
                    _logger.LogInformation($"Restaurant met naam {naam} niet gevonden voor verwijdering.");
                    return NotFound();
                }
                else
                {
                    _restaurantManager.DeleteRestaurant(naam);
                    _logger.LogInformation($"Restaurant met naam {naam} verwijderd.");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij het verwijderen van het restaurant met naam {naam}");
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

                _logger.LogInformation($"Tafel toegevoegd aan restaurant: {restaurantName}, TafelId: {t.Id}");

                return CreatedAtAction(nameof(GetTafels), new { restaurantId = restaurant.Id }, MapFromDomain.MapFromTafelDomain(URL, t, restaurant));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het toevoegen van een tafel");
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
                    _logger.LogInformation($"Geen tafels gevonden voor restaurant met ID: {restaurantId}");
                    return NotFound("Geen tafel gevonden");
                }

                List<TafelRESTOutputDTO> tafelDTOs = MapFromDomain.MapFromTafelsDomain(URL, tafels);
                _logger.LogInformation($"Tafels opgehaald voor restaurant met ID: {restaurantId}");
                return Ok(tafelDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het ophalen van tafels");
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

                _logger.LogInformation($"Tafel geüpdatet voor restaurant met ID: {restaurantId}, TafelId: {tafelId}");

                return Ok("Tafel updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het updaten van een tafel");
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
                    _logger.LogInformation($"Tafel met ID: {tafelId} niet gevonden voor verwijdering in restaurant met ID: {restaurantId}");
                    return NotFound();
                }
                else
                {
                    _restaurantManager.DeleteTafel(restaurantId, tafelId);
                    _logger.LogInformation($"Tafel met ID: {tafelId} verwijderd in restaurant met ID: {restaurantId}");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het verwijderen van een tafel");
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
                    _logger.LogInformation($"Geen reserveringen gevonden voor restaurant: {restaurantName}, beginDatum: {beginDatum}, eindDatum: {eindDatum}");
                    return NotFound("Geen reserveringen gevonden voor de opgegeven criteria.");
                }

                List<ReservatieRESTOutputDTO> reservationDTOs = MapFromDomain.MapFromReservatiesDomain(URL, reservations);
                _logger.LogInformation($"Reserveringen opgehaald voor restaurant: {restaurantName}, beginDatum: {beginDatum}, eindDatum: {eindDatum}");
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
    }
}
