using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using FrontEndApp.View;
using System.Net.Http.Json;
using FrontEndApp.Models;
using FrontEndApp.Models.BasicQuery;

namespace FrontEndApp.Utilites
{
    static class HelperHttpClient
    {

        private const string uri = @"https://localhost:7113/"; // local connection

        private static string GetTokenJWT()
        {
            string tokenJWT = "";
            if (ProductStoreWindow.DetailsUser != null)
            {
                tokenJWT = $@"{ProductStoreWindow.DetailsUser.TokenJWT}";
                tokenJWT = tokenJWT.Substring(1, tokenJWT.Count() - 2);
                return tokenJWT;
            }
            return null;
        }

        public async static Task<HttpResponseMessage> SecondGetHttp<T>(HttpClient client, T value, string requestUri)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value.GetType() == typeof(ProductQuery) || value.GetType() == typeof(InventoryQuery) || value.GetType() == typeof(PriceQuery))
            {
                Query query = value as Query;
                requestUri += @$"?SearchWord={query.SearchWord}&PageSize={query.PageSize}&PageNumber={query.PageNumber}&SortDirection={query.SortDirection}&SortBy={query.SortBy}";
            }
            else
            {
                requestUri += @$"?SKU={value}";
            }
            
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            var response = await client.GetAsync(@$"{requestUri}");

            return response;
        }

        public static HttpResponseMessage PostHttp<T>(HttpClient client, T modelDto, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            HttpResponseMessage response = client.PostAsJsonAsync(requestUri, modelDto).Result;
            return response;
        }

        public async static Task<HttpResponseMessage> GetHttp<T>(HttpClient client, T value, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            var response = await client.GetAsync(@$"{requestUri}/{value}");
            return response;
        }

        public async static Task<HttpResponseMessage> GetHttp(HttpClient client, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            var response = await client.GetAsync(@$"{requestUri}");
            return response;
        }

        public async static void GetResponseBodyOk(HttpResponseMessage response, string extendText)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Xceed.Wpf.Toolkit.MessageBox.Show($"{extendText}\nStatus Code: " + (int)response.StatusCode + " -> " + response.StatusCode + (responseBody != null ? "\nResponse Body: " + responseBody : ""));
        }
        public async static void GetResponseBodyError(HttpResponseMessage response)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            int startIndex = responseBody.IndexOf("\"errors\"", StringComparison.OrdinalIgnoreCase);
            if (startIndex != -1) responseBody = responseBody.Substring(startIndex);
            Xceed.Wpf.Toolkit.MessageBox.Show("Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + (responseBody != null ? "\nErrors: " + responseBody : ""));
        }
    }
}
