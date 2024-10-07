using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NegoSoftShared.Models.Entities;
using Newtonsoft.Json;
namespace NegoSoftWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProductsFromApi();
        }
        private void MenuItem_Produits(object sender, RoutedEventArgs e)
        {
            LoadProductsFromApi();
        }
        private void MenuItem_Clients(object sender, RoutedEventArgs e)
        {
            
        }
        private void MenuItem_Fournisseurs(object sender, RoutedEventArgs e)
        {
            LoadSupplierFromApi();
        }
        private void MenuItem_Commandes(object sender, RoutedEventArgs e)
        {

        }
        private async void LoadProductsFromApi()
        {
            try
            {
                string apiUrl = "https://localhost:7101/api/product"; // Replace with your API URL

                // Use HttpClient to make the API call
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of objects
                        List<Product> data = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);

                        // Bind the data to the DataGrid
                        dataTab.ItemsSource = data;
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void LoadSupplierFromApi()
        {
            try
            {
                string apiUrl = "https://localhost:7101/api/Supplier"; // Replace with your API URL

                // Use HttpClient to make the API call
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of objects
                        List<Product> data = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);

                        // Bind the data to the DataGrid
                        dataTab.ItemsSource = data;
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void LoadClientsFromApi()
        {
            try
            {
                string apiUrl = "https://localhost:7101/api/Clients"; // Replace with your API URL

                // Use HttpClient to make the API call
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of objects
                        List<Product> data = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);

                        // Bind the data to the DataGrid
                        dataTab.ItemsSource = data;
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
