//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Threading.Tasks;
//using Moq;
//using Newtonsoft.Json;
//using Restaurant.API.Controllers;
//using Restaurant.API.Models.Input;
//using Restaurant.API.Models.Output;
//using Restaurant.BL.Interfaces;

//namespace Restaurant.API.Test
//{
//    public class GebruikerControllerTest
//    {
//        //private readonly Mock<IGebruikerRepo> mockRepo;
//        //private readonly GebruikerController gebruikerController;

//        //public GebruikerControllerTest()
//        //{
//        //    mockRepo = new Mock<IGebruikerRepo>();
//        //    gebruikerController = new GebruikerController(mockRepo.Object);
//        //}

//        private readonly WebApplicationFactory<GebruikerController> _factory;

//        public GebruikerControllerTests(WebApplicationFactory<YourStartupClass> factory)
//        {
//            _factory = factory;
//        }

//        [Fact]
//        public async Task AddGebruiker_ReturnsCreated()
//        {
//            // Arrange
//            var client = _factory.CreateClient();
//            var inputDTO = new GebruikerRESTinputDTO
//            {
//                // Initialize with test data
//            };
//            var content = new StringContent(JsonConvert.SerializeObject(inputDTO), Encoding.UTF8, "application/json");

//            // Act
//            var response = await client.PostAsync("/api/Gebruiker/AddGebruiker", content);

//            // Assert
//            response.EnsureSuccessStatusCode();
//            var result = JsonConvert.DeserializeObject<GebruikerRESToutputDTO>(await response.Content.ReadAsStringAsync());
//            Assert.NotNull(result);
//            // Add more assertions based on your response structure and expectations
//        }

//        // Add similar tests for other API endpoints...

//        [Fact]
//        public async Task GetGebruikerByEmail_ReturnsOk()
//        {
//            // Arrange
//            var client = _factory.CreateClient();

//            // Act
//            var response = await client.GetAsync("/api/Gebruiker/GetGebruikerByEmail/test@example.com");

//            // Assert
//            response.EnsureSuccessStatusCode();
//            var result = JsonConvert.DeserializeObject<BL.Models.Gebruiker>(await response.Content.ReadAsStringAsync());
//            Assert.NotNull(result);
//            // Add more assertions based on your response structure and expectations
//        }

//        // Add similar tests for other API endpoints...

//        [Fact]
//        public async Task GetAllReservaties_ReturnsNotFound()
//        {
//            // Arrange
//            var client = _factory.CreateClient();

//            // Act
//            var response = await client.GetAsync("/api/Gebruiker/1/Reservaties/GetAllReservaties");

//            // Assert
//            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
//            // Add more assertions based on your response structure and expectations
//        }

//        // Add similar tests for other API endpoints...

//    }
//}
