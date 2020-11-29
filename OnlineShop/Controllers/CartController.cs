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
    public class CartController : Controller
    {
        // GET: Cart
        [AllowAnonymous]
        [Route("cart")]
        public async Task<ActionResult> Cart()
        {
            return View(await GetCartItem());
        }

        [Authorize]
        [Route("checkout")]
        public async Task<ActionResult> Checkout()
        {
            if (Session["cart"] == null)
            {
                return RedirectToAction("Cart", "Cart");
            }
            var customer = HttpContext.User.Identity.Name;
            CUSTOMER item = null;
            if (!string.IsNullOrEmpty(customer))
            {
                string url = GlobalVariable.url + "getcustomer/" + customer;
                string json = await new GlobalVariable().GetApiAsync(url);
                item = JsonConvert.DeserializeObject<CUSTOMER>(json);
            }
            ViewData["Customer"] = item;
            return View(await GetCartItem());
        }

        [HttpPost]
        public JsonResult OrderNow(string prodId, int sizeId, int quantity)
        {
            if (Session["cart"] == null)
            {
                var cart = new List<CartSession>();
                cart.Add(new CartSession(prodId, sizeId, quantity));
                Session["cart"] = cart;

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var cart = (List<CartSession>)Session["cart"];
                int index = IsExist(prodId);
                if (index == -1)
                {
                    cart.Add(new CartSession(prodId, sizeId, quantity));
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    cart[index].Quantity += quantity;
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Delete(string id)
        {
            int index = IsExist(id);
            List<CartSession> cart = (List<CartSession>)Session["cart"];
            cart.RemoveAt(index);
            if (cart.Count == 0)
            {
                Session["cart"] = null;
            }
            return RedirectToAction("Cart", "Cart");
        }

        private int IsExist(string id)
        {
            var cart = (List<CartSession>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ProductID == id)
                    return i;
            return -1;
        }

        public async Task<ActionResult> CartPartial()
        {
            return PartialView("_Cart", await GetCartItem());
        }

        public async Task<List<CartItemModel>> GetCartItem()
        {
            var list = new List<CartItemModel>();
            var session = (List<CartSession>)Session["cart"];
            if (session != null)
            {
                foreach (var item in session)
                {
                    string url = GlobalVariable.url + "product/" + item.ProductID;
                    string json = await new GlobalVariable().GetApiAsync(url);
                    var product = JsonConvert.DeserializeObject<PRODUCT>(json);

                    string urlsize = GlobalVariable.url + "size/" + item.SizeID;
                    string jsonsize = await new GlobalVariable().GetApiAsync(urlsize);
                    var size = JsonConvert.DeserializeObject<SIZE>(jsonsize);

                    list.Add(new CartItemModel(
                         product,
                         size,
                        item.Quantity));
                }
            }
            return list;
        }
        public async Task<JsonResult> SubmitCheckout()
        {
            try
            {
                string url = GlobalVariable.url + "getcustomer/" + HttpContext.User.Identity.Name;
                string json = await new GlobalVariable().GetApiAsync(url);
                var customer = JsonConvert.DeserializeObject<CUSTOMER>(json);

                var order = new ORDER()
                {
                    CustomerID = customer.CustomerID,
                    Total = await GetTotal()
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44337/api/");

                    var postTask = client.PostAsJsonAsync<ORDER>("addorder", order);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var orderdetail = new OrderDetailDao();
                        foreach (var item in (List<CartSession>)Session["cart"])
                        {
                            _ = await orderdetail.AddOrderDetailProc(order.OrderID, item.ProductID, item.SizeID, item.Quantity);
                        }
                        Session["cart"] = null;
                        return Json(new { Success = true, ID = order }, JsonRequestBehavior.AllowGet);

                    }
                }
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<decimal?> GetTotal()
        {
            decimal total = 0;

            var cart = (List<CartSession>)Session["cart"];
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    string url = GlobalVariable.url + "product/" + item.ProductID;
                    string json = await new GlobalVariable().GetApiAsync(url);
                    var product = JsonConvert.DeserializeObject<PRODUCT>(json);

                    if (product.PromotionPrice.HasValue)
                    {
                        total += product.PromotionPrice.Value * item.Quantity;
                    }
                    else
                    {
                        total += product.ProductPrice * item.Quantity;
                    }
                }
            }
            return total;
        }
    }
}