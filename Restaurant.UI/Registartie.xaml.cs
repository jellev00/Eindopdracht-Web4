using Restaurant.UI.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant.UI
{
    /// <summary>
    /// Interaction logic for Registartie.xaml
    /// </summary>
    public partial class Registartie : Window
    {
        public RestaurantDTO restaurantDTO;
        private readonly APIClient _apiClient;

        public Registartie(RestaurantDTO restaurantDTO)
        {
            InitializeComponent();
            this.restaurantDTO = restaurantDTO;
            _apiClient = new APIClient("http://localhost:5212/api/Gebruiker");
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GebruikerDTO g = await _apiClient.GetGebruikerByEmail("jelle.vandriessche@gmail.com");


                int aantalPlaatsen = int.Parse(AantalPlaatsen.Text);
                if (aantalPlaatsen == null)
                {
                    MessageBox.Show($"aantalplaatsen mag niet null zijn");
                }

                DateTime date;
                if (!DateTime.TryParse(Datum.Text, out date))
                {
                    return;
                }

                string uur = Uur.Text;
                if (uur == null)
                {
                    MessageBox.Show($"uur mag niet null zijn");
                }

                string minuten = Minuten.Text;
                if (minuten == null)
                {
                    MessageBox.Show($"minuten mag niet null zijn");
                }

                DateTime combinedDateTime = date.AddHours(int.Parse(uur)).AddMinutes(int.Parse(minuten));

                if (combinedDateTime < DateTime.Now)
                {
                    MessageBox.Show($"Datum mag niet kleiner zijn dan de datum van vandaag!");
                }

                // Create a ReservatieDTO object based on the input values
                ReservatieDTO reservatieDTO = new ReservatieDTO(aantalPlaatsen, combinedDateTime);

                // Make API request
                HttpResponseMessage response = await _apiClient.AddReservatie(g.KlantenNummer, restaurantDTO.Id, reservatieDTO);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Reservatie added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}, {response.ReasonPhrase}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
