using FrontEndApp.Models;
using FrontEndApp.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndApp.Services
{
    interface ILoginService
    {
        Task<string> LoginUser(LoginDto loginDto);
    }
    class LoginService : ILoginService
    {
        public async Task<string> LoginUser(LoginDto loginDto)
        {
            using (HttpClient client = new HttpClient())
            {
                const string requestUri = @"api/account/login";
                HttpResponseMessage response = HelperHttpClient.PostHttp(client, loginDto, requestUri);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return responseBody;
                }
                else
                {
                    int startIndex = responseBody.IndexOf("\"errors\"", StringComparison.OrdinalIgnoreCase);
                    if (startIndex != -1) responseBody = responseBody.Substring(startIndex);
                    Xceed.Wpf.Toolkit.MessageBox.Show("Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + "\nErrors: " + responseBody);
                }
                return "";
            }

        }
    }
}
