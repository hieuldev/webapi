using Models.Dao;
using Models.Ef;
using Newtonsoft.Json;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Controllers
{
    public class CustomerController : Controller
    {
        [Route("login")]
        public ActionResult Login()
        {
            var name = HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(name))
            {
                return RedirectToAction("CustomerProfile", "Customer", new { username = name });
            }
            return View();
        }

        [HttpPost]
        public JsonResult ValidateUser(CUSTOMER model, bool RememberMe)
        {
            bool res = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                var postTask = client.PostAsJsonAsync("validateuser", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<bool>();
                    readTask.Wait();

                    res = readTask.Result;
                    if (res)
                    {
                        FormsAuthentication.SetAuthCookie(model.CustomerUsername, RememberMe);
                        return Json(new { Success = true, Username = model.CustomerUsername }, JsonRequestBehavior.AllowGet);
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


        [HttpPost]
        public JsonResult RegisterCustomer(CUSTOMER model)
        {
            if(!new CustomerDao().CheckUser(model.CustomerUsername))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44337/api/");

                    var postTask = client.PostAsJsonAsync<CUSTOMER>("register", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Json(new { ReturnID = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { ReturnID = 2 }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new { ReturnID = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPut]
        public JsonResult Edit(CUSTOMER cus)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var postTask = client.PutAsJsonAsync<CUSTOMER>("editcustomer", cus);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [Authorize]
        [ActionName("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Customer");
        }

        [Authorize]
        [Route("profile/{username}")]
        public async Task<ActionResult> CustomerProfile(string username)
        {
            string url = GlobalVariable.url + "getcustomer/" + username;
            string json = await new GlobalVariable().GetApiAsync(url);
            var user = JsonConvert.DeserializeObject<CUSTOMER>(json);

            var membername = HttpContext.User.Identity.Name;
            if (!membername.Equals(username))
            {
                return View(user);
            }
            return View(user);
        }
    }
}