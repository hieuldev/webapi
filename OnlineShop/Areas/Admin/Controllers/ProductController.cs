using Models.Ef;
using Newtonsoft.Json;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using System.Net.Http;
using Models.Dao;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        public async Task<ActionResult> CreateProduct()
        {
            string urlcate = GlobalVariable.url + "categorylist";
            string jsoncate = await new GlobalVariable().GetApiAsync(urlcate);
            var category = JsonConvert.DeserializeObject<List<CATEGORY>>(jsoncate);
            ViewBag.Cate = category;

            string url = GlobalVariable.url + "size";
            string json = await new GlobalVariable().GetApiAsync(url);
            var size = JsonConvert.DeserializeObject<List<SIZE>>(json);
            ViewBag.Size = size;

            return View();
        }

        [HttpPost]
        public JsonResult CreateProduct(ProductModel prod)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var postTask = client.PostAsJsonAsync<ProductModel>("createproduct", prod);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true, id = 1 }, JsonRequestBehavior.AllowGet);
                }
            }
            
            return Json(new { Success = false, id = 1 }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ProductList()
        {
            string urlcate = GlobalVariable.url + "productlist";
            string jsoncate = await new GlobalVariable().GetApiAsync(urlcate);
            var product = JsonConvert.DeserializeObject<List<PRODUCT>>(jsoncate);

            return View(product);
        }

        [HttpDelete]
        public JsonResult DeleteProduct(string ProductID)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var deleteTask = client.DeleteAsync("delete/" +ProductID);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true, id = 1 }, JsonRequestBehavior.AllowGet);
                }
            }
            
            return Json(new { Success = false, id = 1 }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> EditProduct(string id)
        {
            string urlcate = GlobalVariable.url + "categorylist";
            string jsoncate = await new GlobalVariable().GetApiAsync(urlcate);
            var category = JsonConvert.DeserializeObject<List<CATEGORY>>(jsoncate);
            ViewBag.Cate = category;

            string url = GlobalVariable.url + "size";
            string json = await new GlobalVariable().GetApiAsync(url);
            var size = JsonConvert.DeserializeObject<List<SIZE>>(json);
            ViewBag.Size = size;

            string urlproduct = GlobalVariable.url + "product/"+id;
            string jsonproduct = await new GlobalVariable().GetApiAsync(urlproduct);
            var product = JsonConvert.DeserializeObject<PRODUCT>(jsonproduct);

            return View(product);

        }

        [HttpPut]
        public JsonResult EditProduct(ProductModel prod)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var editTask = client.PutAsJsonAsync<ProductModel>("editproduct/" + prod.ProductID,prod);
                editTask.Wait();

                var result = editTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true, id = 1 }, JsonRequestBehavior.AllowGet);
                }
            }
            
            return Json(new { Success = false, id = 1 }, JsonRequestBehavior.AllowGet);
        }


        [ActionName("Size")]
        public ActionResult CreateSize()
        {
            return View();
        }

        [HttpPost]
        public  JsonResult CreateSize(SIZE model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");

                var postTask = client.PostAsJsonAsync<SIZE>("createsize", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public async Task<ActionResult> SizeList()
        {
            string url = GlobalVariable.url + "size";
            string json = await new GlobalVariable().GetApiAsync(url);
            var size = JsonConvert.DeserializeObject<List<SIZE>>(json);

            return PartialView("SizeList", size);
        }

        [HttpDelete]
        public  JsonResult DeleteSize(int id)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44337/api/");

                    var deleteTask = client.DeleteAsync("deletesize/" + id);
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
            else
            {
                return Json(new { Success = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPut]
        public JsonResult EditSize(SIZE model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44337/api/");

                    var postTask = client.PutAsJsonAsync<SIZE>("editsize", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Json(new { Success = false, model.SizeID }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { Success = false, model.SizeID }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, model.SizeID }, JsonRequestBehavior.AllowGet);
            }
            //if (ModelState.IsValid)
            //{
            //    int result = await new SizeDao().EditSizeProc(model, id);
            //    if (result == 0)
            //    {
            //        return Json(new { Success = false, id }, JsonRequestBehavior.AllowGet);
            //    }
            //    return Json(new { Success = true, id }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { Success = false, id }, JsonRequestBehavior.AllowGet);
        }
    }
}