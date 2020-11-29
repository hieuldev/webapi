using Models.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Common;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.SqlServer.Server;
using Models.Dao;

namespace OnlineShop.Controllers
{
    public class ShopController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            string url = GlobalVariable.url + "productlist";
            string json = await new GlobalVariable().GetApiAsync(url);
            var products = JsonConvert.DeserializeObject<List<PRODUCT>>(json);

            string urlcate = GlobalVariable.url + "categorylist";
            string jsoncate = await new GlobalVariable().GetApiAsync(urlcate);
            var category = JsonConvert.DeserializeObject<List<CATEGORY>>(jsoncate);
            ViewBag.Cate = category;

            return View(products);
        }

        [HttpPost]
        public async Task<ActionResult> Index(string searchString, string id)
        {
            var products = await new ProductDao().Search(searchString, id);
            string urlcate = GlobalVariable.url + "categorylist";
            string jsoncate = await new GlobalVariable().GetApiAsync(urlcate);
            var category = JsonConvert.DeserializeObject<List<CATEGORY>>(jsoncate);
            ViewBag.Cate = category;
                
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(string id)
        {

            string url = GlobalVariable.url + "product/{0}";
            url = string.Format(url, id);
            string json = await new GlobalVariable().GetApiAsync(url);
            var product = JsonConvert.DeserializeObject<PRODUCT>(json);

            product.ViewCount = await new ProductDao().UpdateView(id);
 
            string urlsize = GlobalVariable.url + "getproductsize/{0}";
            urlsize = string.Format(urlsize, id);
            string jsonsize = await new GlobalVariable().GetApiAsync(urlsize);
            var size = JsonConvert.DeserializeObject<List<SIZE>>(jsonsize);
            ViewBag.Size = size;

            return View(product);
        }

        
        public async Task<ActionResult> GetProductByCategory(string id)
        {
            string urlcate = GlobalVariable.url + "categorylist";
            string jsoncate = await new GlobalVariable().GetApiAsync(urlcate);
            var category = JsonConvert.DeserializeObject<List<CATEGORY>>(jsoncate);
            ViewBag.Cate = category;

            string url = GlobalVariable.url + "categoryid/" + id;
            string json = await new GlobalVariable().GetApiAsync(url);
            var product = JsonConvert.DeserializeObject<List<PRODUCT>>(json);
            return View(product);
        }

        [HttpGet]
        public async Task<ActionResult> SelectTop()
        {
            int top = 6;
            var list = await new ProductDao().SelectTop(top);
            return PartialView(list);
        }


        //[HttpGet]
        //[Route("cate/{url}-{id:int}")]
        //public async Task<ActionResult> ShopCategory(string id, string url, string sort)
        //{
        //    var cate = await new CategoryDao().GetByID(id);
        //    if (cate == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.meta = cate;
        //    ViewBag.sort = sort;
        //    return View();
        //}
    }
}
