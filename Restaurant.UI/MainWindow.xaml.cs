using Restaurant.UI.DTO;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly APIClient _apiClient;

        public MainWindow()
        {
            InitializeComponent();

            _apiClient = new APIClient("http://localhost:5212/api/Gebruiker");
        }


        private async void SearchRestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get input values from UI controls
                DateTime datum = Datum.SelectedDate ?? DateTime.Now;
                int aantalPlaatsen = int.Parse(Aantalplaatsen.Text);

                if (datum < DateTime.Now)
                {
                    MessageBox.Show($"Datum mag niet kleiner zijn dan de datum van vandaag!");
                }

                // Make API request to search restaurants based on postCode and keuken
                List<RestaurantDTO> availableRestaurants = await _apiClient.GetAvailableRestaurants(datum, aantalPlaatsen);

                // Display the result in the DataGrid
                RestaurantsDataGrid.ItemsSource = availableRestaurants;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //private async void GetAvailableRestaurantsButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        // Get input values from UI controls
        //        DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;
        //        int aantalPlaatsen = int.Parse(AantalPlaatsenTextBox.Text);

        //        // Make API request
        //        List<RestaurantDTO> availableRestaurants = await _apiClient.GetAvailableRestaurants(selectedDate, aantalPlaatsen);

        //        // Display the result in a ListBox or any other control
        //        AvailableRestaurantsListBox.ItemsSource = availableRestaurants;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private async void AddRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (RestaurantsDataGrid.SelectedItem == null) MessageBox.Show("Restaurant niet geselecteerd");
            else
            {
                RestaurantDTO selectedRestaurant = (RestaurantDTO)RestaurantsDataGrid.SelectedItem;
                Registartie w = new Registartie(selectedRestaurant);
                w.ShowDialog();
            }
        }
    }
}
