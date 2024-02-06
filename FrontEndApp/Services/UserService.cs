using FrontEndApp.Models;
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
    interface IUserService
    {
        Task<UserDto> GetUserByEmail(string email);
    }
    class UserService : IUserService
    {
        public async Task<UserDto> GetUserByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/login/user";
                var response = await HelperHttpClient.GetHttp(client, email, requestUri);
                var user = await response.Content.ReadFromJsonAsync<UserDto>();

                if (response.IsSuccessStatusCode)
                {
                    return user;
                }
                return null;
            }
        }

    }
}
