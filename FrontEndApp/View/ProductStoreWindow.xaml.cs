using FrontEndApp.Models;
using FrontEndApp.Services;
using FrontEndApp.View.Details;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace FrontEndApp.View
{
    /// <summary>
    /// Interaction logic for ProductStoreWindow.xaml
    /// </summary>
    public partial class ProductStoreWindow : Window
    {
        public static UserDto? DetailsUser;

        public ProductStoreWindow()
        {
            InitializeComponent();

            DescriptionProductStore.Text += $" {DetailsUser.FirstName}";
        }

        private void Button_SingOut(object sender, RoutedEventArgs e)
        {
            MainLoginWindow mainLoginWindow = new MainLoginWindow();
            this.Visibility = Visibility.Hidden;
            mainLoginWindow.Show();
        }

        private void Button_UserDetails(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowProducts(object sender, RoutedEventArgs e)
        {
            ShowAllWindow showAllProductsWindow = new ShowAllWindow();
            showAllProductsWindow.DescriptionShowAll.Text = "You are in the Show All Products section";
            showAllProductsWindow.ChooseShow = ChooseShow.PRODUCT;
            showAllProductsWindow.DataGridInventories.Visibility = Visibility.Collapsed;
            showAllProductsWindow.DataGridPrices.Visibility = Visibility.Collapsed;
            showAllProductsWindow.DataGridProducts.Visibility = Visibility.Visible;
            showAllProductsWindow.Query = new ProductQuery();
            showAllProductsWindow.GetAllProducts();
            this.Visibility = Visibility.Hidden;
            showAllProductsWindow.Show();
        }

        private void Button_ShowInventories(object sender, RoutedEventArgs e)
        {
            ShowAllWindow showAllInventoriesWindow = new ShowAllWindow();
            showAllInventoriesWindow.DescriptionShowAll.Text = "You are in the Show All Inventories section";
            showAllInventoriesWindow.ChooseShow = ChooseShow.INVENTORY;
            showAllInventoriesWindow.DataGridInventories.Visibility = Visibility.Visible;
            showAllInventoriesWindow.DataGridPrices.Visibility = Visibility.Collapsed;
            showAllInventoriesWindow.DataGridProducts.Visibility = Visibility.Collapsed;
            showAllInventoriesWindow.Query = new InventoryQuery();
            showAllInventoriesWindow.GetAllInventories();
            this.Visibility = Visibility.Hidden;
            showAllInventoriesWindow.Show();
        }

        private void Button_ShowPrices(object sender, RoutedEventArgs e)
        {
            ShowAllWindow showAllPricesWindow = new ShowAllWindow();
            showAllPricesWindow.DescriptionShowAll.Text = "You are in the Show All Prices section";
            showAllPricesWindow.ChooseShow = ChooseShow.PRICE;
            showAllPricesWindow.DataGridInventories.Visibility = Visibility.Collapsed;
            showAllPricesWindow.DataGridPrices.Visibility = Visibility.Visible;
            showAllPricesWindow.DataGridProducts.Visibility = Visibility.Collapsed;
            showAllPricesWindow.Query = new PriceQuery();
            showAllPricesWindow.GetAllPrices();
            this.Visibility = Visibility.Hidden;
            showAllPricesWindow.Show();
        }

        private void Button_getFiles(object sender, RoutedEventArgs e)
        {
            IFileService fileService = new FileService();
            fileService.GetFilesCSV();
        }

        private async void Button_GetDetails(object sender, RoutedEventArgs e)
        {
            string skuNumber = DetailsSKUNumber.Text;
            DetailsSKUNumber.Text = "";
            IProductService productService = new ProductService();
            var detailsProduct = await productService.GetDetails(skuNumber);

            var array = detailsProduct.Split(';').ToList();
            if (array.Count != 10) { Xceed.Wpf.Toolkit.MessageBox.Show(detailsProduct); return; }

            ShowProductDto showProductDto = new ShowProductDto()
            {
                Name = array[0],
                EAN = array[1],
                ProducerName = array[2],
                Category = array[3],
                DefaultImage = array[4],
                Available = array[5],
                SKU = array[6],
                ShippingCost = double.Parse(array[7]),
                NettProductPrice = double.Parse(array[8]),
                NettProductPriceAfterDiscountForProductLogisticUnit = double.Parse(array[9])
            };

            DetailsProductWindow detailsProductWindow = new DetailsProductWindow();
            detailsProductWindow.FillDetailsArray(showProductDto);
            this.Visibility = Visibility.Hidden;
            detailsProductWindow.Show();


        }
    }
}
