using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.API.Controllers;
using Restaurant.BL.Interfaces;
using Restaurant.BL.Managers;
using Restaurant.API.Models.Input;
using Restaurant.API.Models.Output;
using Castle.Core.Resource;
using Restaurant.BL.Exceptions;
using Restaurant.Gebruiker.API.Models.Input;
using Restaurant.Gebruiker.API.Models.Output;
using Restaurant.BL.Models;

namespace Restaurant.API.Tests
{
    public class RestaurantControllerTest
    {
        private readonly RestaurantController _restaurantController;
        private readonly ReservatieManager _reservatieManager;
        private readonly RestaurantManager _restaurantManager;
        private readonly Mock<IReservatieRepo> _reservatieRepo;
        private readonly Mock<IRestaurantRepo> _restaurantRepo;
        private readonly ILoggerFactory _loggerFactory;

        public RestaurantControllerTest()
        {
            // Initialize mock objects
            _reservatieRepo = new Mock<IReservatieRepo>();
            _restaurantRepo = new Mock<IRestaurantRepo>();

            // Initialize managers with mock objects
            _reservatieManager = new ReservatieManager(_reservatieRepo.Object);
            _restaurantManager = new RestaurantManager(_restaurantRepo.Object);

            _loggerFactory = new LoggerFactory();

            // Initialize the controller with the managers and logger factory
            _restaurantController = new RestaurantController(_restaurantManager, _reservatieManager, _loggerFactory);
        }

        [Fact]
        public void AddRestaurant_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var inputDTO = new RestaurantRESTInputDTO("New Restaurant", new Locatie("1234", "Test City", "Test Street", "42"), "Test Cuisine", new Contactgegevens("1234567890", "test@example.com"), true);

            _restaurantRepo.Setup(r => r.AddRestaurant(It.IsAny<BL.Models.Restaurant>()))
                           .Returns((BL.Models.Restaurant restaurant) => restaurant);

            // Act
            var result = _restaurantController.AddRestaurant(inputDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtActionResult.Value);
            Assert.IsType<RestaurantRESTOutputDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void GetRestaurantByNaam_ReturnsOk()
        {
            // Arrange
            var restaurantNaam = "TestRestaurant";
            var restaurant = new BL.Models.Restaurant(restaurantNaam, new Locatie("1234", "Test City", "Test Street", "42"), "Test Cuisine", new Contactgegevens("1234567890", "test@example.com"), true);

            _restaurantRepo.Setup(r => r.GetRestaurantByNaam(restaurantNaam))
                           .Returns(restaurant);

            // Act
            var result = _restaurantController.GetRestaurantByNaam(restaurantNaam);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllRestaurants_ReturnsOk()
        {
            // Arrange
            var restaurants = new List<BL.Models.Restaurant>
            {
                new BL.Models.Restaurant("Restaurant1", new Locatie("1234", "City1", "Street1", "42"), "Cuisine1", new Contactgegevens("1234567890", "restaurant1@example.com"), true),
                new BL.Models.Restaurant("Restaurant2", new Locatie("5678", "City2", "Street2", "21"), "Cuisine2", new Contactgegevens("9876543210", "restaurant2@example.com"), true),
            };

            _restaurantRepo.Setup(r => r.GetAllRestaurants())
                           .Returns(restaurants);

            // Act
            var result = _restaurantController.GetAllRestaurants();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateRestaurant_ValidInput_ReturnsOk()
        {
            // Arrange
            var restaurantId = 1;
            var inputDTO = new RestaurantRESTInputDTO("UpdatedRestaurant", new Locatie("4321", "UpdatedCity", "UpdatedStreet", "21"), "UpdatedCuisine", new Contactgegevens("9876543210", "updated@example.com"), true);

            _restaurantRepo.Setup(r => r.ExistsRestaurant(It.IsAny<string>())).Returns(false);
            _restaurantRepo.Setup(r => r.UpdateRestaurant(It.IsAny<int>(), It.IsAny<BL.Models.Restaurant>()));

            // Act
            var result = _restaurantController.UpdateRestaurant(restaurantId, inputDTO);

            // Assert
            Assert.IsType<ActionResult<BL.Models.Restaurant>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteRestaurant_ExistingRestaurant_ReturnsNoContent()
        {
            // Arrange
            var restaurantNaam = "ExistingRestaurant";
            _restaurantRepo.Setup(r => r.ExistsRestaurant(restaurantNaam)).Returns(true);

            // Act
            var result = _restaurantController.DeleteRestaurant(restaurantNaam);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteRestaurant_NonExistingRestaurant_ReturnsNotFound()
        {
            // Arrange
            var restaurantNaam = "NonExistingRestaurant";
            _restaurantRepo.Setup(r => r.ExistsRestaurant(restaurantNaam)).Returns(false);

            // Act
            var result = _restaurantController.DeleteRestaurant(restaurantNaam);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Tafels

        [Fact]
        public void AddTafel_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var restaurantName = "TestRestaurant";
            var inputDTO = new TafelRESTInputDTO(4);

            _restaurantRepo.Setup(r => r.GetRestaurantByNaam(restaurantName))
                           .Returns(new BL.Models.Restaurant { Id = 1 }); // Provide a sample restaurant for testing

            _restaurantRepo.Setup(r => r.AddTafel(It.IsAny<int>(), It.IsAny<Tafel>()))
                           .Returns((int restaurantId, Tafel tafel) => tafel);

            // Act
            var result = _restaurantController.AddTafel(restaurantName, inputDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtActionResult.Value);
            Assert.IsType<TafelRESTOutputDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void GetTafels_ReturnsOk()
        {
            // Arrange
            var restaurantId = 1;
            var tafels = new List<Tafel>
            {
                new Tafel(4),
                new Tafel(2)
            };

            _restaurantRepo.Setup(r => r.GetTafels(restaurantId))
                           .Returns(tafels);

            // Act
            var result = _restaurantController.GetTafels(restaurantId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateTafel_ValidInput_ReturnsOk()
        {
            // Arrange
            var restaurantId = 1;
            var tafelId = 1;
            var inputDTO = new TafelRESTInputDTO(6);

            _restaurantRepo.Setup(r => r.ExistsTafel(It.IsAny<int>())).Returns(false);
            _restaurantRepo.Setup(r => r.UpdateTafel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Tafel>()));

            // Act
            var result = _restaurantController.UpdateTafel(restaurantId, tafelId, inputDTO);

            // Assert
            Assert.IsType<ActionResult<Tafel>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteTafel_ExistingTafel_ReturnsNoContent()
        {
            // Arrange
            var restaurantId = 1;
            var tafelId = 1;
            _restaurantRepo.Setup(r => r.ExistsTafel(tafelId)).Returns(true);

            // Act
            var result = _restaurantController.DeleteTafel(restaurantId, tafelId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteTafel_NonExistingTafel_ReturnsNotFound()
        {
            // Arrange
            var restaurantId = 1;
            var tafelId = 2;
            _restaurantRepo.Setup(r => r.ExistsTafel(tafelId)).Returns(false);

            // Act
            var result = _restaurantController.DeleteTafel(restaurantId, tafelId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Reservatie

        [Fact]
        public void GetAllReservaties_ReturnsOk()
        {
            // Arrange
            var restaurantName = "TestRestaurant";
            var beginDatum = DateTime.Now.AddDays(5);
            var eindDatum = DateTime.Now.AddDays(10);

            _reservatieRepo.Setup(r => r.GetAllReservationsByRestauranNaam(restaurantName, beginDatum, eindDatum))
                           .Returns(new List<Reservatie>()); // Provide a sample list of reservations for testing

            // Act
            var result = _restaurantController.GetAllReservaties(restaurantName, beginDatum, eindDatum);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllReservaties_NoReservations_ReturnsNotFound()
        {
            // Arrange
            var restaurantName = "TestRestaurant";
            var beginDatum = DateTime.Now.AddDays(5);
            var eindDatum = DateTime.Now.AddDays(10);

            _reservatieRepo.Setup(r => r.GetAllReservationsByRestauranNaam(restaurantName, beginDatum, eindDatum))
                           .Returns(new List<Reservatie>()); // Empty list to simulate no reservations

            // Act
            var result = _restaurantController.GetAllReservaties(restaurantName, beginDatum, eindDatum);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
