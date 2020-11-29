using Models.Ef;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class OrderDetailDao
    {
        OnlineShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new OnlineShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }
        public async Task<int> AddOrderDetailProc(int OrderID, string ProductID, int SizeID, int Quanity)
        {
            try
            {
                var orderid = new SqlParameter("@orderID", OrderID);
                var prodID = new SqlParameter("@prodID", ProductID);
                var sizeID = new SqlParameter("@sizeID", SizeID);
                var quantity = new SqlParameter("@quantity", Quanity);
                var res = await db.Database.ExecuteSqlCommandAsync("Add_OrderDetail @orderID,@prodID,@sizeID,@quantity", orderid, prodID, sizeID, quantity);
                return res;
            }
            catch
            {
                return 0;
            }
        }
        public List<ORDERDETAIL> LoadOrderDetail(int OrderID)
        {
            return db.ORDERDETAILs.AsNoTracking().Where(x => x.OrderID == OrderID).ToList();
        }

        public List<ORDERDETAIL> LoadOrderDetailProc(int OrderID)
        {
            var param = new SqlParameter("@orderID", OrderID);
            return db.Database.SqlQuery<ORDERDETAIL>("LoadOrderDetail @orderID", param).ToList();

            //return db.ORDERDETAILs.AsNoTracking().Where(x => x.OrderID == OrderID).ToList();
        }
    }
}
