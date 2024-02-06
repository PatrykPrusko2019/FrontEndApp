using FrontEndApp.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndApp.Services
{
    interface IFileService
    {
        void GetFilesCSV();
    }
    public class FileService : IFileService
    {
        public async void GetFilesCSV()
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = @"api/file";

                var response = await HelperHttpClient.GetHttp(client, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                
            }
        }
    }
}
