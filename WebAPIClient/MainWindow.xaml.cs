using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebAPIClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:3660/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void allproductButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Product> products = await GetProductsAsync(client.BaseAddress + "api/Products");
            productDataGrid.ItemsSource = products;
        }


        private async void idproductButton_Click(object sender, RoutedEventArgs e)
        {
            List<Products> products = new List<Products>();
            if (idproductTextBox.Text != String.Empty)
            {
                Products product = await GetProductAsync(client.BaseAddress + "api/Products/" +
                idproductTextBox.Text);
                if (product != null)
                    products.Add(product);
            }
            else
            {
                products = (List<Products>)await GetProductsAsync(client.BaseAddress + "api/Products");
            }
            productDataGrid.ItemsSource = products;
        }

        static async Task<IEnumerable<Products>> GetProductsAsync(string path)
        {
            IEnumerable<Products> products = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    products = await response.Content.ReadAsAsync<IEnumerable<Products>>();
                }
            }
            catch (Exception)
            {
            }
            return products;
        }

        static async Task<Products> GetProductAsync(string path)
        {
            Products product = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Products>();
                }
            }
            catch (Exception)
            {
               var msg = "Nothing";
            }
            return product;
        }

    }
}
