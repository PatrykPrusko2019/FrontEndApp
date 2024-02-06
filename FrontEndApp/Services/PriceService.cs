using FrontEndApp.Models;
using FrontEndApp.Models.ShowAll;
using FrontEndApp.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndApp.Services
{
    public interface IPriceService
    {
        Task<PageResult<AllPriceDto>> GetAll(PriceQuery searchQuery);
    }
    public class PriceService : IPriceService
    {
        public async Task<PageResult<AllPriceDto>> GetAll(PriceQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/price";

                var response = await HelperHttpClient.SecondGetHttp(client, searchQuery, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var prices = await response.Content.ReadFromJsonAsync<PageResult<AllPriceDto>>();
                    if (prices != null && prices.Items != null && prices.Items.Count() > 0)
                    {
                        return prices;
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
