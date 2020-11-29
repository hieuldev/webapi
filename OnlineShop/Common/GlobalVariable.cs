using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShop.Common
{
    public class GlobalVariable
    {
        public static string url = "https://localhost:44337/api/";
        public async Task<string> GetApiAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage Response = await client.GetAsync(url);
                if (Response.IsSuccessStatusCode)
                {
                    var responseJsonString = await Response.Content.ReadAsStringAsync();
                    return responseJsonString;
                }
                else
                {
                    return Response.ReasonPhrase;
                }
            }
        }
    }
}