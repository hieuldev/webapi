using Models.Dao;
using Models.Ef;
using Newtonsoft.Json;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Http;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Order()
        {
            return View();
        }

        public async Task<ActionResult> OrderList()
        {
            string url = GlobalVariable.url + "order";
            string json = await new GlobalVariable().GetApiAsync(url);
            var order = JsonConvert.DeserializeObject<List<ORDER>>(json);
            return PartialView("OrderList", order);
        }

        [HttpPut]
        public JsonResult EditOrder(ORDER order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var putTask = client.PutAsJsonAsync<ORDER>("order", order);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true, order.OrderID }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = false, order.OrderID }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public async Task<JsonResult> StatusList(int id)
        {
            string url = GlobalVariable.url + "addliststatus/"+id;
            string json = await new GlobalVariable().GetApiAsync(url);
            var order = JsonConvert.DeserializeObject<List<ORDERSTATU>>(json);

            if (order!=null)
            {
                return Json(order, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<ORDERSTATU>(), JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}