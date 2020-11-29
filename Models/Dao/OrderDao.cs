using Models.Ef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class OrderDao
    {
        OnlineShopDbContext db = null;
        public OrderDao()
        {
            db = new OnlineShopDbContext();
        }

        public async Task<List<ORDER>> ListAll()
        {
            return await db.ORDERs.Include(s => s.ORDERSTATU).Include(x=>x.CUSTOMER).AsNoTracking().ToListAsync();
        }
        public async Task<int> ChangeOrder(int OrderID, int? StatusID)
        {
            try
            {
                var order = db.ORDERs.Find(OrderID);
                order.OrderStatusID = StatusID;
                await db.SaveChangesAsync();
                return OrderID;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddOrderProc(ORDER order)
        {
            try
            {
                var o = new ORDER()
                {
                    OrderDate = DateTime.Now,
                    CustomerID = order.CustomerID,
                    OrderStatusID = 1,
                    Total = order.Total,
                    DeliveryDate = DateTime.Now
                };
                db.ORDERs.Add(o);
                await db.SaveChangesAsync();
                return order.OrderID; 
            }
            catch
            {
                return 0;
            }
        }

        public async Task<List<ORDER>> LoadOrder(int CustomerID)
        {
            return await db.ORDERs.Where(x=>x.CustomerID==CustomerID).Include(s=>s.ORDERSTATU).ToListAsync();
        }

        public async Task<List<T>> LoadOrder<T>(int CustomerID)
        {
            var Param = new SqlParameter("@CustomerID", CustomerID);

            return await new OnlineShopDbContext().Database.SqlQuery<T>("SelectOrder @CustomerID", Param).ToListAsync();
        }

        public async Task<List<T>> LoadProductOrder<T>(int OrderID)
        {
            var Param = new SqlParameter("@OrderID", OrderID);

            return await new OnlineShopDbContext().Database
                 .SqlQuery<T>("SelectOrderProduct @OrderID", Param)
                 .ToListAsync();
        }

        public async Task<int> CancelOrderProc(int OrderID)
        {
            try
            {
                var ID = new SqlParameter("@orderID", OrderID);
                return await db.Database.ExecuteSqlCommandAsync("Cancel_Order @orderID", ID);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> ChangeOrderProc(int OrderID, int StatusID)
        {
            try
            {
                var orderID = new SqlParameter("@orderID", OrderID);
                var statusID = new SqlParameter("@statusID", StatusID);
                return await db.Database.ExecuteSqlCommandAsync("Change_Order @orderID,@statusID", orderID, statusID);
            }
            catch
            {
                return 0;
            }
        }
    }
}
