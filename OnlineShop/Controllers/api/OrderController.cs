using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Models.Dao;
using Models.Ef;
using System.Data.Entity;
using OnlineShop.Models;

namespace OnlineShop.Controllers.api
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public async Task<List<ORDER>> ListAll()
        {
            return await new OrderDao().ListAll();
        }

        [HttpPut]
        public async Task<int> ChangeOrder(ORDER order)
        {
            return await new OrderDao().ChangeOrder(order.OrderID, order.OrderStatusID);
        }

        [HttpGet]
        [Route("api/addliststatus/{id}")]
        public async Task<List<ORDERSTATU>> OrderStatus(int id)
        {
            return await new OrderStatusDao().OrderStatusListAsync(id);
        }

        [HttpPut]
        [Route("api/cancelorder/{orderID}")]
        public async Task<int> CancelOrder(int orderID)
        {
            return await new OrderDao().CancelOrderProc(orderID);
        }

        [HttpGet]
        [Route("api/statuslist")]
        public async Task<List<ORDERSTATU>> GetListStatus()
        {
            return await new OrderStatusDao().LoadStatusProc();
        }


        [HttpGet]
        [Route("api/loadorder/{customerID}")]
        public async Task<List<OrderModel>> LoadOrder(int customerID)
        {
            return await new OrderDao().LoadOrder<OrderModel>(customerID);
        }

        [HttpPost]
        [Route("api/addorderdetail")]
        public async Task<int> AddOrderDetail(int OrderID, string ProductID, int SizeID, int Quanity)
        {
            return await new OrderDetailDao().AddOrderDetailProc(OrderID, ProductID, SizeID, Quanity);
        }

        [HttpPost]
        [Route("api/addorder")]
        public async Task<int> AddOrder(ORDER order)
        {
            return await new OrderDao().AddOrderProc(order);
        }
    }
}
