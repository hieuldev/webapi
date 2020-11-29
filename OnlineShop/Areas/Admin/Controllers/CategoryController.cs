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

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateCategory(CATEGORY cate)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/createategory");

                var postTask = client.PostAsJsonAsync<CATEGORY>("createcategory", cate);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            //if (ModelState.IsValid)
            //{
            //    var x = new CategoryDao().CreateCategory(cate);
            //}
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult CategoryList()
        {
            //var url = GlobalVariable.url + "categorylist";
            //var json = await new GlobalVariable().GetApiAsync(url);
            //var list = JsonConvert.DeserializeObject<List<CATEGORY>>(json);

            IEnumerable<CATEGORY> categories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                //HTTP GET
                var responseTask = client.GetAsync("categorylist");

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CATEGORY>>();
                    readTask.Wait();
                    categories = readTask.Result;
                }
                else
                {
                    categories = Enumerable.Empty<CATEGORY>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return PartialView(categories);
        }

        [HttpDelete]
        public JsonResult DeleteCategory(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deletecategory/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPut]
        public JsonResult EditCategory(CATEGORY cate, string id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                //HTTP PUT
                var putTask = client.PutAsJsonAsync("editcategory/" + id, cate);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true, id }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false, id }, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }
}