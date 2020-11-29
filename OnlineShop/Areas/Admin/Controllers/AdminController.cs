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
    public class AdminController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidateAdmin(USER model)
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
                        Session["AdminLogin"] = model;
                        ViewBag.AdminName = model.UserName;
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

        public ActionResult Logout()
        {
            Session["AdminLogin"] = null;
            return RedirectToAction("Login");
        }

    }
}