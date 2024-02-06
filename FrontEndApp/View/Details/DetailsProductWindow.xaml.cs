using FrontEndApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace FrontEndApp.View.Details
{
    /// <summary>
    /// Interaction logic for DetailsProductWindow.xaml
    /// </summary>
    public partial class DetailsProductWindow : Window
    {
        ObservableCollection<ShowProductDto> detailsProduct;
        public DetailsProductWindow()
        {
            InitializeComponent();
            detailsProduct = new ObservableCollection<ShowProductDto>();
        }


        public void FillDetailsArray(ShowProductDto product)
        {
            detailsProduct.Add(product);
            DataGridDetailsProduct.DataContext = detailsProduct;
        }

        private void Button_ReturnToProductStore(object sender, RoutedEventArgs e)
        {
            ProductStoreWindow productStoreWindow = new ProductStoreWindow();
            this.Visibility = Visibility.Hidden;
            productStoreWindow.Show();
        }
    }
}
