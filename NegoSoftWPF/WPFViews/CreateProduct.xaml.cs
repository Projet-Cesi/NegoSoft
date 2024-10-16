using Microsoft.Win32;
using NegoSoftShared.Models.Entities;
using Newtonsoft.Json;
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
using System.Windows.Shapes;
using System;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Xml.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace NegoSoftWPF.WPFViews
{
    /// <summary>
    /// Logique d'interaction pour CreateProduct.xaml
    /// </summary>
    public partial class CreateProduct : Window
    {
        //attributes
        private string selectedPicPath;
        private static readonly HttpClient client = new HttpClient();
        public CreateProduct()
        {
            InitializeComponent();
            LoadSuppliersAsync();
            LoadTypesAsync();

        }
        public async Task LoadSuppliersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Supplier";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonResponse);
                        suppliersBox.ItemsSource = suppliers;
                        suppliersBox.DisplayMemberPath = "SupName";
                        suppliersBox.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des fournisseurs.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }
        public async Task LoadTypesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7101/api/Type";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<NegoSoftShared.Models.Entities.Type> types = JsonConvert.DeserializeObject<List<NegoSoftShared.Models.Entities.Type>>(jsonResponse);
                        typeBox.ItemsSource = types;
                        typeBox.DisplayMemberPath = "TypLibelle";
                        typeBox.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération des données des types.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }
        }
        private void pickProdPic(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedPicPath = openFileDialog.FileName;
            }
            IFormFile picForm = GetFormFileFromPath(selectedPicPath);
            var image = ConvertIFormFileToBitmapImage(picForm);
            MyImageControl.Source = image;
        }
        private BitmapImage ConvertIFormFileToBitmapImage(IFormFile formFile)
        {
            BitmapImage bitmap = new BitmapImage();

            using (var stream = formFile.OpenReadStream()) // Ouvrir un flux à partir de IFormFile
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Charger immédiatement l'image dans la mémoire
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }

            return bitmap;
        }
        private async void CreateProductAPIRequest(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != "" &&
                DescriptionBox.Text != "" &&
                BoxPriceBox.Text != "" &&
                StockBox.Text != "" &&
                selectedPicPath != "")
            {
                Supplier selectedSupplier = (Supplier)suppliersBox.SelectedValue;
                NegoSoftShared.Models.Entities.Type selectedType = (NegoSoftShared.Models.Entities.Type)typeBox.SelectedValue;
                string supplierId = selectedSupplier.SupId.ToString();
                string typeId = selectedType.TypId.ToString();
                string request = "https://localhost:7101/api/Product";
                //IFormFile prodPic = GetFormFileFromPath(selectedPicPath);
                var product = new NegoSoftWeb.Models.ViewModels.ProductViewModel
                {
                    ProName = NameBox.Text,
                    ProDescription = DescriptionBox.Text,
                    ProSupplierId = Guid.Parse(supplierId),
                    ProPrice = float.Parse(PriceBox.Text),
                    ProBoxPrice = float.Parse(BoxPriceBox.Text),
                    ProTypeId = Guid.Parse(typeId),
                    ProStock = int.Parse(StockBox.Text),
                    ProPictureName = NameBox.Text
                    //ProImageFile = prodPic
                };

                var rnr = JsonContent.Create(product);

                //request + checking
                HttpResponseMessage response = await client.PostAsync(request, rnr);
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responseBody);
            }
        }
        
        private void DecimalTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextDecimal((sender as TextBox).Text + e.Text);
        }

        private bool IsTextDecimal(string text)
        {
            return decimal.TryParse(text, out _);
        }
        private void NumericTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));

                if (!IsTextDecimal(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
        public static IFormFile GetFormFileFromPath(string filePath)
        {
            // Lire le fichier depuis le disque
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Obtenir les informations sur le fichier
            var fileInfo = new FileInfo(filePath);

            // Utiliser un MemoryStream pour représenter les données
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            memoryStream.Position = 0;  // Remettre le curseur au début du stream

            // Créer un FormFile, qui implémente IFormFile
            IFormFile formFile = new FormFile(memoryStream, 0, memoryStream.Length, fileInfo.Name, fileInfo.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"  // Type MIME du fichier
            };

            // Fermer le fileStream (il n'est plus nécessaire)
            fileStream.Close();

            return formFile;
        }
    }
}
