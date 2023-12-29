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
using Restaurant.EF.Models;

namespace Restaurant.API.Tests
{
    public class GebruikerControllerTest
    {
        private readonly GebruikerController _gebruikerController;
        private readonly GebruikerManager _gebruikerManager;
        private readonly ReservatieManager _reservatieManager;
        private readonly RestaurantManager _restaurantManager;
        private readonly Mock<IGebruikerRepo> _gebruikerRepo;
        private readonly Mock<IReservatieRepo> _reservatieRepo;
        private readonly Mock<IRestaurantRepo> _restaurantRepo;
        private readonly ILoggerFactory _loggerFactory;

        public GebruikerControllerTest()
        {
            // Initialize mock objects
            _gebruikerRepo = new Mock<IGebruikerRepo>();
            _reservatieRepo = new Mock<IReservatieRepo>();
            _restaurantRepo = new Mock<IRestaurantRepo>();

            // Initialize managers with mock objects
            _gebruikerManager = new GebruikerManager(_gebruikerRepo.Object);
            _reservatieManager = new ReservatieManager(_reservatieRepo.Object);
            _restaurantManager = new RestaurantManager(_restaurantRepo.Object);

            _loggerFactory = new LoggerFactory();

            // Initialize the controller with the managers and logger factory
            _gebruikerController = new GebruikerController(_gebruikerManager, _reservatieManager, _restaurantManager, _loggerFactory);
        }

        // Gebruiker Tests

        [Fact]
        public void AddGebruikerTest()
        {
            // Arrange
            var inputDTO = new GebruikerRESTinputDTO("John Doe", "john.doe@example.com", "123456789", new BL.Models.Locatie("1234", "City", "Street", "123"));

            _gebruikerRepo.Setup(g => g.ExistsGebruiker(It.IsAny<string>())).Returns((string e) => false);
            _gebruikerRepo.Setup(g => g.AddGebruiker(It.IsAny<BL.Models.Gebruiker>())).Returns((BL.Models.Gebruiker g) => g);

            // Act
            var result = _gebruikerController.AddGebruiker(inputDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtActionResult.Value);
            Assert.IsType<GebruikerRESToutputDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void GetGebruikerByEmail_GebruikerBestaan()
        {
            // Arrange
            var email = "jelle.vandriessche@gmail.com";

            BL.Models.Gebruiker gebruiker = new BL.Models.Gebruiker("Jelle", "jelle.vandriessche@gmail.com", "0472533243", new Locatie("9750", "Kruisem", "Toekomststraat", "16"));
            GebruikerRESTinputDTO gebruikerIn = new GebruikerRESTinputDTO("Jelle", "jelle.vandriessche@gmail.com", "0472533243", new Locatie("9750", "Kruisem", "Toekomststraat", "16"));

            _gebruikerRepo.Setup(g => g.AddGebruiker(gebruiker))
                .Returns(gebruiker);

            _gebruikerRepo.Setup(g => g.GetGebruikerByEmail(email))
                .Returns(new BL.Models.Gebruiker("Jelle", "jelle.vandriessche@gmail.com", "0472533243", new Locatie("9750", "Kruisem", "Toekomststraat", "16")));

            // Act
            var testG = _gebruikerController.AddGebruiker(gebruikerIn);
            var result = _gebruikerController.GetGebruikerByEmail(email);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetGebruikerByEmail_GebruikerBestaatNiet()
        {
            // Arrange
            var email = "test@test.com";

            _gebruikerRepo.Setup(g => g.GetGebruikerByEmail(email))
                .Throws(new GebruikerManagerException("Gebruiker niet gevonden"));

            // Act
            var result = _gebruikerController.GetGebruikerByEmail(email);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateGebruiker_ValidInput_ReturnsOk()
        {
            // Arrange
            var klantenNr = 1;
            var inputDTO = new GebruikerRESTinputDTO("John Doe", "john.doe@example.com", "987654321", new BL.Models.Locatie("4321", "City", "Street", "321"));

            _gebruikerRepo.Setup(g => g.ExistsGebruiker(It.IsAny<string>())).Returns(false);
            _gebruikerRepo.Setup(g => g.UpdateGebruiker(It.IsAny<int>(), It.IsAny<BL.Models.Gebruiker>()));

            // Act
            var result = _gebruikerController.UpdateGebruiker(klantenNr, inputDTO);

            // Assert
            Assert.IsType<ActionResult<BL.Models.Gebruiker>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteGebruiker_ExistingEmail_ReturnsNoContent()
        {
            // Arrange
            var email = "john.doe@example.com";

            _gebruikerRepo.Setup(g => g.ExistsGebruiker(email)).Returns(true);

            // Act
            var result = _gebruikerController.DeleteGebruiker(email);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteGebruiker_NonExistingEmail_ReturnsNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";

            _gebruikerRepo.Setup(g => g.ExistsGebruiker(email)).Returns(false);

            // Act
            var result = _gebruikerController.DeleteGebruiker(email);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Reservatie Tests

        [Fact]
        public void AddReservatie_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var klantenNr = 1;
            var restaurantId = 1;
            var inputDTO = new ReservatieRESTInputDTO(4, DateTime.Now.AddDays(2));

            BL.Models.Restaurant restaurant = new BL.Models.Restaurant("Hof van Cleve", new Locatie("9770", "Kruisem", "Riemegemstraat", "1"), "culinaire", new Contactgegevens("093835848", "Hof-van-Cleve@info.be"), true);
            BL.Models.Gebruiker gebruiker = new BL.Models.Gebruiker("Jelle", "jelle.vandriessche@gmail.com", "0472533243", new Locatie("9750", "Kruisem", "Toekomststraat", "16"));

            _gebruikerRepo.Setup(g => g.ExistsGebruiker(It.IsAny<string>())).Returns((string e) => false);
            _reservatieRepo.Setup(r => r.AddReservatie(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<BL.Models.Reservatie>()))
                           .Returns(new BL.Models.Reservatie(restaurant, gebruiker, 4, DateTime.Now.AddDays(2), 4));

            // Act
            var result = _gebruikerController.AddReservatie(klantenNr, restaurantId, inputDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtActionResult.Value);
            Assert.IsType<ReservatieRESTOutputDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void GetAllReservaties_ReturnsOk()
        {
            // Arrange
            var klantenNr = 1;
            var beginDatum = DateTime.Now.AddDays(5);
            var eindDatum = DateTime.Now;

            BL.Models.Restaurant restaurant = new BL.Models.Restaurant("Hof van Cleve", new Locatie("9770", "Kruisem", "Riemegemstraat", "1"), "culinaire", new Contactgegevens("093835848", "Hof-van-Cleve@info.be"), true);

            BL.Models.Gebruiker gebruiker = new BL.Models.Gebruiker("Jelle", "jelle.vandriessche@gmail.com", "0472533243", new Locatie("9750", "Kruisem", "Toekomststraat", "16"));

            Reservatie reservatie1 = new Reservatie(restaurant, gebruiker, 4, new DateTime(2023, 12, 25, 18, 30, 0), 3);
            Reservatie reservatie2 = new Reservatie(restaurant, gebruiker, 2, new DateTime(2023, 12, 30, 18, 30, 0), 7);

            _reservatieRepo.Setup(r => r.GetAllReservationsByKlantenNr(It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .Returns(new List<BL.Models.Reservatie> { reservatie1, reservatie2 });


            // Act
            var result = _gebruikerController.GetAllReservaties(klantenNr, beginDatum, eindDatum);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateReservatie_ValidInput_ReturnsOk()
        {
            // Arrange
            var klantenNr = 1;
            var restaurantId = 1;
            var reservatieNr = 1;
            var inputDTO = new ReservatieRESTInputDTO(4, DateTime.Now.AddDays(2));

            _reservatieRepo.Setup(r => r.UpdateReservatie(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<BL.Models.Reservatie>()));

            // Act
            var result = _gebruikerController.UpdateReservatie(klantenNr, restaurantId, reservatieNr, inputDTO);

            // Assert
            Assert.IsType<ActionResult<GebruikerRESToutputDTO>>(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteReservatie_ExistingReservatie_ReturnsOk()
        {
            // Arrange
            var klantenNr = 1;
            var reservatieNr = 1;

            _reservatieRepo.Setup(r => r.ExistsResertvatie(It.IsAny<int>())).Returns(true);

            // Act
            var result = _gebruikerController.DeleteReservatie(klantenNr, reservatieNr);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteReservatie_NonExistingReservatie_ReturnsNotFound()
        {
            // Arrange
            var klantenNr = 1;
            var reservatieNr = 1;

            _reservatieRepo.Setup(r => r.ExistsResertvatie(It.IsAny<int>())).Returns(false);

            // Act
            var result = _gebruikerController.DeleteReservatie(klantenNr, reservatieNr);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Restaurant Tests

        [Fact]
        public void GetRestaurantByNaam_ReturnsOk()
        {
            // Arrange
            var restaurantNaam = "Le Comptoir";

            BL.Models.Restaurant restaurant1 = new BL.Models.Restaurant("Pasta Piccaso", new Locatie("9000", "Oudenaarde", "Nederstraat", "50"), "italiaans", new Contactgegevens("0487472075", "pasta-piccaso@info.be"), true);
            BL.Models.Restaurant restaurant2 = new BL.Models.Restaurant("Hof van Cleve", new Locatie("9770", "Kruisem", "Riemegemstraat", "1"), "culinaire", new Contactgegevens("093835848", "Hof-van-Cleve@info.be"), true);
            BL.Models.Restaurant restaurant3 = new BL.Models.Restaurant("Le Comptoir", new Locatie("1000", "Brussels", "Grand Place", "1"), "French cuisine", new Contactgegevens("0123456789", "info@restaurant-brussels.com"), true);

            _restaurantRepo.Setup(r => r.GetRestaurantByNaam(restaurantNaam)).Returns(restaurant3);

            // Act
            var result = _gebruikerController.GetRestaurantByNaam(restaurantNaam);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetRestaurantByNaam_ReturnsNotFound()
        {
            // Arrange
            var restaurantNaam = "NonExistentRestaurant";

            _restaurantRepo.Setup(r => r.GetRestaurantByNaam(restaurantNaam)).Throws(new Exception("Restaurant not found"));

            // Act
            var result = _gebruikerController.GetRestaurantByNaam(restaurantNaam);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetRestaurantByPostcodeOrKeuken_ReturnsOk()
        {
            // Arrange
            var postcode = "9770";
            var keuken = "culinaire";

            BL.Models.Restaurant restaurant1 = new BL.Models.Restaurant("Pasta Piccaso", new Locatie("9000", "Oudenaarde", "Nederstraat", "50"), "italiaans", new Contactgegevens("0487472075", "pasta-piccaso@info.be"), true);
            BL.Models.Restaurant restaurant2 = new BL.Models.Restaurant("Hof van Cleve", new Locatie("9770", "Kruisem", "Riemegemstraat", "1"), "culinaire", new Contactgegevens("093835848", "Hof-van-Cleve@info.be"), true);
            BL.Models.Restaurant restaurant3 = new BL.Models.Restaurant("Le Comptoir", new Locatie("1000", "Brussels", "Grand Place", "1"), "French cuisine", new Contactgegevens("0123456789", "info@restaurant-brussels.com"), true);

            var restaurants = new List<BL.Models.Restaurant> { restaurant1, restaurant2, restaurant3 };

            _restaurantRepo.Setup(r => r.SearchRestaurants(postcode, keuken)).Returns(restaurants);

            // Act
            var result = _gebruikerController.GetRestaurantByPostcodeOrKeuken(postcode, keuken);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetRestaurantByPostcodeOrKeuken_ReturnsNotFound()
        {
            // Arrange
            var postcode = "5678";
            var keuken = "Italian";

            _restaurantRepo.Setup(r => r.SearchRestaurants(postcode, keuken)).Returns(new List<BL.Models.Restaurant>());

            // Act
            var result = _gebruikerController.GetRestaurantByPostcodeOrKeuken(postcode, keuken);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllRestaurants_ReturnsOk()
        {
            // Arrange
            BL.Models.Restaurant restaurant1 = new BL.Models.Restaurant("Pasta Piccaso", new Locatie("9000", "Oudenaarde", "Nederstraat", "50"), "italiaans", new Contactgegevens("0487472075", "pasta-piccaso@info.be"), true);
            BL.Models.Restaurant restaurant2 = new BL.Models.Restaurant("Hof van Cleve", new Locatie("9770", "Kruisem", "Riemegemstraat", "1"), "culinaire", new Contactgegevens("093835848", "Hof-van-Cleve@info.be"), true);

            var restaurants = new List<BL.Models.Restaurant> { restaurant1, restaurant2 };

            _restaurantRepo.Setup(r => r.GetAllRestaurants()).Returns(restaurants);

            // Act
            var result = _gebruikerController.GetAllRestaurants();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllRestaurants_ReturnsNotFound()
        {
            // Arrange
            _restaurantRepo.Setup(r => r.GetAllRestaurants()).Returns(new List<BL.Models.Restaurant>());

            // Act
            var result = _gebruikerController.GetAllRestaurants();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetAvailableRestaurants_ReturnsOk()
        {
            // Arrange
            var datum = DateTime.Now.AddDays(7);
            var aantalPlaatsen = 4;
            BL.Models.Restaurant restaurant1 = new BL.Models.Restaurant("Pasta Piccaso", new Locatie("9000", "Oudenaarde", "Nederstraat", "50"), "italiaans", new Contactgegevens("0487472075", "pasta-piccaso@info.be"), true);
            BL.Models.Restaurant restaurant2 = new BL.Models.Restaurant("Hof van Cleve", new Locatie("9770", "Kruisem", "Riemegemstraat", "1"), "culinaire", new Contactgegevens("093835848", "Hof-van-Cleve@info.be"), true);

            var availableRestaurants = new List<BL.Models.Restaurant> { restaurant1, restaurant2 };

            _restaurantRepo.Setup(r => r.GetAvailableRestaurants(datum, aantalPlaatsen)).Returns(availableRestaurants);

            // Act
            var result = _gebruikerController.GetAvailableRestaurants(datum, aantalPlaatsen);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAvailableRestaurants_ReturnsBadRequest()
        {
            // Arrange
            var datum = DateTime.Now.AddDays(-1);
            var aantalPlaatsen = 2;

            // Act
            var result = _gebruikerController.GetAvailableRestaurants(datum, aantalPlaatsen);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

    }
}