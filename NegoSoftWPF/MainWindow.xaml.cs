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
        //Attributs
        private string apiUrl;
        private System.Type dataGridType;
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
            LoadClientsFromApi();
        }
        private void MenuItem_Fournisseurs(object sender, RoutedEventArgs e)
        {
            LoadSupplierFromApi();
        }
        private void MenuItem_Commandes(object sender, RoutedEventArgs e)
        {
            LoadOrdersFromApi();
        }
        private async void LoadProductsFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/product"; // Replace with your API URL

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
                        dataGridType = typeof(Product);
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
                apiUrl = "https://localhost:7101/api/Supplier"; // Replace with your API URL

                // Use HttpClient to make the API call
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of objects
                        List<Supplier> data = JsonConvert.DeserializeObject<List<Supplier>>(jsonResponse);

                        // Bind the data to the DataGrid
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(Supplier);
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
                apiUrl = "https://localhost:7101/api/Customer"; // Replace with your API URL

                // Use HttpClient to make the API call
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of objects
                        List<Customer> data = JsonConvert.DeserializeObject<List<Customer>>(jsonResponse);

                        // Bind the data to the DataGrid
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(Customer);
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
        private async void LoadOrdersFromApi()
        {
            try
            {
                apiUrl = "https://localhost:7101/api/CustomerOrder"; // Replace with your API URL

                // Use HttpClient to make the API call
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a List of objects
                        List<CustomerOrder> data = JsonConvert.DeserializeObject<List<CustomerOrder>>(jsonResponse);

                        // Bind the data to the DataGrid
                        dataTab.ItemsSource = data;
                        dataGridType = typeof(CustomerOrder);
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
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var columnsToShow = new List<string> {"Supplier", "Type", "SupplierOrderDetails", "CustomerOrderDetails", "AlcoholProduct", "DefaultAddress", "CustomerOrders", "SupplierOrders", "Products", "Customer", "Address"};

            if (columnsToShow.Contains(e.PropertyName))
            {
                e.Cancel = true;
            }
        }
        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {

        }
        private void Button_ClickRefresh(object sender, RoutedEventArgs e)
        {
            switch (dataGridType)
            {
                case System.Type t when t == typeof(CustomerOrder):
                    LoadOrdersFromApi();
                    break;
                case System.Type t when t == typeof(Customer):
                    LoadClientsFromApi();
                    break;
                case System.Type t when t == typeof(Supplier):
                    LoadSupplierFromApi();
                    break;
                case System.Type t when t == typeof(Product):
                    LoadProductsFromApi();
                    break;

            }
        }
        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {

        }
        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {

        }


    }
}

