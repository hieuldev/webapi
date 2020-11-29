using Models.Dao;
using Models.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View();
        }

       [HttpGet]
        public ActionResult CustomerList()
        {
            IEnumerable<CUSTOMER> customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                //HTTP GET
                var putTask = client.GetAsync("customer");
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CUSTOMER>>();
                    readTask.Wait();
                    customer = readTask.Result;
                    return PartialView(customer);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            
        }
    }
}