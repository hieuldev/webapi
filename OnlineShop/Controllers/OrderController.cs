using Models.Dao;
using Models.Ef;
using Newtonsoft.Json;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        [HttpPut]
        [Authorize]
        public JsonResult CancelOrder(ORDER order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var putTask = client.PutAsJsonAsync<ORDER>("cancelorder/"+order.OrderID, order);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();

                    int res = readTask.Result;
                    if (res == 1)
                    {
                        return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }
            }           
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> OrderList(int? StatusID)
        {
            var membername = HttpContext.User.Identity.Name;
            string url = GlobalVariable.url + "getcustomer/" + membername;
            string json = await new GlobalVariable().GetApiAsync(url);
            var customer = JsonConvert.DeserializeObject<CUSTOMER>(json);

            string urlloadorder = GlobalVariable.url + "loadorder/" + customer.CustomerID;
            string jsonloadorder = await new GlobalVariable().GetApiAsync(urlloadorder);
            var list = JsonConvert.DeserializeObject<List<OrderModel>>(jsonloadorder);
            return PartialView("OrderList", list);
        }

        public async Task<JsonResult> GetStatus()
        {
            string url = GlobalVariable.url + "statuslist";
            string json = await new GlobalVariable().GetApiAsync(url);
            var status = JsonConvert.DeserializeObject<List<ORDERSTATU>>(json);

            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}