using Models.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
   public static class Login
    {
        public static async Task<bool> Logins(USER model)
        {
            bool res = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var postTask = client.PostAsJsonAsync("validate", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<bool>();
                    readTask.Wait();

                    res = readTask.Result;
                    if (res)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
