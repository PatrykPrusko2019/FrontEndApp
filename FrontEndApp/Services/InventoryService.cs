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
    public interface IInventoryService
    {
        Task<PageResult<AllInventoryDto>> GetAll(InventoryQuery searchQuery);
    }
    public class InventoryService : IInventoryService
    {
        public async Task<PageResult<AllInventoryDto>> GetAll(InventoryQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/inventory";

                var response = await HelperHttpClient.SecondGetHttp(client, searchQuery, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var inventories = await response.Content.ReadFromJsonAsync<PageResult<AllInventoryDto>>();
                    if (inventories != null && inventories.Items != null && inventories.Items.Count() > 0)
                    {
                        return inventories;
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
