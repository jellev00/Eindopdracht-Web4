using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Restaurant.UI.DTO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Restaurant.BL.Models;

namespace Restaurant.UI
{
    public class APIClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAPIURL;

        public APIClient(string baseAPIURL)
        {
            _httpClient = new HttpClient();
            _baseAPIURL = baseAPIURL;
        }

        public async Task<List<RestaurantDTO>> GetAvailableRestaurants(DateTime d, int aantalPlaatsen)
        {
            string datum = $"{d.Year}-{d.Month}-{d.Day}";

            string apiURL = $"{_baseAPIURL}/GetAvailableRestaurants/{datum}/{aantalPlaatsen}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiURL);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Manually deserialize the string content into a list of RestaurantDTO
                List<RestaurantDTO> restaurantList = DeserializeRestaurantList(responseContent);

                return restaurantList;
            }

            throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        private List<RestaurantDTO> DeserializeRestaurantList(string content)
        {

            // Deserialize the JSON string into a list of RestaurantDTO objects
            List<RestaurantDTO> restaurantList = JsonConvert.DeserializeObject<List<RestaurantDTO>>(content);

            if (restaurantList != null)
            {
                return restaurantList;
            }

            // This is a placeholder, replace it with your actual logic.
            throw new NotImplementedException("Implement your custom deserialization logic here.");
        }

        public async Task<HttpResponseMessage> AddReservatie(int klantenNr, int restaurantId, ReservatieDTO reservatieDTO)
        {
            string apiURL = $"{_baseAPIURL}/{klantenNr}/Restaurant/{restaurantId}/Reservaties/AddReservatie";

            var content = new ObjectContent<ReservatieDTO>(reservatieDTO, new JsonMediaTypeFormatter());

            return await _httpClient.PostAsync(apiURL, content);
        }

        public async Task<GebruikerDTO> GetGebruikerByEmail(string email)
        {
            try
            {
                string encodedEmail = Uri.EscapeDataString(email);
                string apiURL = $"{_baseAPIURL}/GetGebruikerByEmail/{encodedEmail}";

                HttpResponseMessage response = await _httpClient.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GebruikerDTO>(content);
                }

                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                // Handle exceptions here or propagate them
                throw new Exception($"Error while getting Gebruiker: {ex.Message}");
            }
        }
    }
}
