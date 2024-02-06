using FrontEndApp.Models.ShowAll;
using FrontEndApp.Models;
using FrontEndApp.Utilites;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services
{
    public interface IProductService
    {
        Task<string> GetDetails(string SkuNumber);
        Task<PageResult<AllProductDto>> GetAll(ProductQuery searchQuery);
    }
    public class ProductService : IProductService
    {
        public async Task<string> GetDetails(string SkuNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/file/details";

                var response = await HelperHttpClient.SecondGetHttp(client, SkuNumber, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var detailsProduct = await response.Content.ReadFromJsonAsync<string>();
                    if (detailsProduct != null) return detailsProduct;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }

        public async Task<PageResult<AllProductDto>> GetAll(ProductQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/product";

                var response = await HelperHttpClient.SecondGetHttp(client, searchQuery, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<PageResult<AllProductDto>>();
                    if (products != null && products.Items != null && products.Items.Count() > 0)
                    {
                        return products;
                    }
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }

    }
}
