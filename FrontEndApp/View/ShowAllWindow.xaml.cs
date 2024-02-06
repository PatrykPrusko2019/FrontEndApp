using FrontEndApp.Models.ShowAll;
using FrontEndApp.Models;
using FrontEndApp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FrontEndApp.Models.BasicQuery;

namespace FrontEndApp.View
{
    /// <summary>
    /// Interaction logic for ShowAllWindow.xaml
    /// </summary>
    public partial class ShowAllWindow : Window
    {
        ObservableCollection<PageResult<AllProductDto>> productPaginationResults;
        ObservableCollection<AllProductDto> products;

        ObservableCollection<PageResult<AllInventoryDto>> inventoryPaginationResults;
        ObservableCollection<AllInventoryDto> inventories;

        ObservableCollection<PageResult<AllPriceDto>> pricesPaginationResults;
        ObservableCollection<AllPriceDto> prices;
        public Query Query { get; set; }
        
        public ChooseShow ChooseShow { get; set; }
        public ShowAllWindow()
        {
            InitializeComponent();
            products = new ObservableCollection<AllProductDto>();
            productPaginationResults = new ObservableCollection<PageResult<AllProductDto>>();

            inventoryPaginationResults =new ObservableCollection<PageResult<AllInventoryDto>>();
            inventories = new ObservableCollection<AllInventoryDto>();

            pricesPaginationResults = new ObservableCollection<PageResult<AllPriceDto>>();
            prices = new ObservableCollection<AllPriceDto>();
        }

        public async void GetAllProducts()
        {
            products.Clear();
            productPaginationResults.Clear();

            IProductService productService = new ProductService();
            Query.SearchWord = SearchWord.Text;

            if (Query.PageNumber == 0) Query.PageNumber = 1;
            if (Query.PageSize == 0) Query.PageSize = 5;
            var productsResult = await productService.GetAll((ProductQuery) Query);
            if (productsResult == null || productsResult.Items == null || productsResult.Items.Count() == 0) 
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("products not found ! Please use the search button again !!!"); 
                Query.PageNumber = 0; 
                Query.PageSize = 0;
                Query.SearchWord = "";
                Query.SortBy = "";
                Query.SortDirection = Models.SortDirection.NULL;
                return; 
            }
            productPaginationResults.Add(productsResult);
            DataGridResults.DataContext = productPaginationResults;

            string[] strings = CurrentPage.Text.Split(" ");
            strings[2] = $"{productsResult.TotalPages}";
            CurrentPage.Text = string.Join(" ", strings);
            RecordsPerPage.Text = Query.PageSize.ToString();

            foreach (var product in productsResult.Items)
            {
                products.Add(product);
            }

            DataGridProducts.DataContext = products;
        }

        public async void GetAllInventories()
        {
            inventories.Clear();
            inventoryPaginationResults.Clear();

            IInventoryService inventoryService = new InventoryService();
            Query.SearchWord = SearchWord.Text;

            if (Query.PageNumber == 0) Query.PageNumber = 1;
            if (Query.PageSize == 0) Query.PageSize = 5;

            var inventoriesResult = await inventoryService.GetAll((InventoryQuery) Query);
            if (inventoriesResult == null || inventoriesResult.Items == null || inventoriesResult.Items.Count() == 0) 
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("inventories not found ! Please use the search button again !!!");
                Query.PageNumber = 0;
                Query.PageSize = 0;
                Query.SearchWord = "";
                Query.SortBy = "";
                Query.SortDirection = Models.SortDirection.NULL;
                return;
            }
            inventoryPaginationResults.Add(inventoriesResult);
            DataGridResults.DataContext = inventoryPaginationResults;

            string[] strings = CurrentPage.Text.Split(" ");
            strings[2] = $"{inventoriesResult.TotalPages}";
            CurrentPage.Text = string.Join(" ", strings);
            RecordsPerPage.Text = Query.PageSize.ToString();

            foreach (var inventory in inventoriesResult.Items)
            {
                inventories.Add(inventory);
            }

            DataGridInventories.DataContext = inventories;
        }

        public async void GetAllPrices()
        {
            prices.Clear();
            pricesPaginationResults.Clear();

            IPriceService priceService = new PriceService();
            Query.SearchWord = SearchWord.Text;
            if (Query.PageNumber == 0) Query.PageNumber = 1;
            if (Query.PageSize == 0) Query.PageSize = 5;

            var pricesResult = await priceService.GetAll((PriceQuery)Query);
            if (pricesResult == null || pricesResult.Items == null || pricesResult.Items.Count() == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("prices not found ! Please use the search button again !!!");
                Query.PageNumber = 0;
                Query.PageSize = 0;
                Query.SearchWord = "";
                Query.SortBy = "";
                Query.SortDirection = Models.SortDirection.NULL;
                return;
            }
            pricesPaginationResults.Add(pricesResult);
            DataGridResults.DataContext = pricesPaginationResults;

            string[] strings = CurrentPage.Text.Split(" ");
            strings[2] = $"{pricesResult.TotalPages}";
            CurrentPage.Text = string.Join(" ", strings);
            RecordsPerPage.Text = Query.PageSize.ToString();

            foreach (var price in pricesResult.Items)
            {
                prices.Add(price);
            }

            DataGridPrices.DataContext = prices;
        }

        private void Button_ReturnToProductStore(object sender, RoutedEventArgs e)
        {
            ProductStoreWindow productStore = new ProductStoreWindow();
            this.Visibility = Visibility.Hidden;
            productStore.Show();
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            UpdateRecords();
        }

        private void UpdateRecords()
        {
            switch (ChooseShow)
            {
                case ChooseShow.PRODUCT:
                    GetAllProducts();
                    break;
                case ChooseShow.INVENTORY:
                    GetAllInventories();
                    break;
                case ChooseShow.PRICE:
                    GetAllPrices();
                    break;
                default:
                    break;
            }
        }


        //5, 10, 15 -> records
        private void ComboBox_ChangeRecordsPerPage(object sender, SelectionChangedEventArgs e)
        {
            if (RecordsPerPage.SelectedIndex == 0) Query.PageSize = 5;
            else if (RecordsPerPage.SelectedIndex == 1) Query.PageSize = 10;
            else if (RecordsPerPage.SelectedIndex == 2) Query.PageSize = 15;
        }

        private void Button_PreviousPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);

            if (currentPage > 1) currentPage--;
            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            Query.PageNumber = currentPage;
            UpdateRecords();
        }

        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);
            int totalPage = int.Parse(strings[2]);

            if (currentPage < totalPage) currentPage++;

            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            Query.PageNumber = currentPage;
            UpdateRecords();
        }

        private void Button_FirstPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);

            if (currentPage > 1) currentPage = 1;
            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            Query.PageNumber = currentPage;
            UpdateRecords();
        }

        private void Button_LastPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);
            int totalPage = int.Parse(strings[2]);

            if (currentPage < totalPage) currentPage = totalPage;

            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            Query.PageNumber = currentPage;
            UpdateRecords();
        }

        private void ComboBox_ChangeSortDirection(object sender, SelectionChangedEventArgs e)
        {
            if (SortDirection.SelectedIndex == 0) Query.SortDirection = Models.SortDirection.NULL;
            else if (SortDirection.SelectedIndex == 1) Query.SortDirection = Models.SortDirection.ASC;
            else if (SortDirection.SelectedIndex == 2) Query.SortDirection = Models.SortDirection.DESC;
        }

        private void ComboBox_ChangeSortBy(object sender, SelectionChangedEventArgs e)
        {
            if (SortBy.SelectedIndex == 0) Query.SortBy = "";
            else if (SortBy.SelectedIndex == 1) Query.SortBy = "Id";
        }
    }
}
