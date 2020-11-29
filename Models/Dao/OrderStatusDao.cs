using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Ef;
namespace Models.Dao
{
    public class OrderStatusDao
    {
        OnlineShopDbContext db = null;
        public OrderStatusDao()
        {
            db = new OnlineShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }
        public async Task<List<ORDERSTATU>> StatusList()
        {
            return await db.ORDERSTATUS.AsNoTracking().ToListAsync();
        }

        public async Task<List<ORDERSTATU>> LoadStatusProc()
        {
            return await db.ORDERSTATUS.SqlQuery("LoadOrderStatus").AsNoTracking().ToListAsync();
        }

        public async Task<List<ORDERSTATU>> OrderStatusListAsync(int id)
        {

            if (id == 2)
            {
                var list = new List<ORDERSTATU>();
                list.Add(db.ORDERSTATUS.Where(x => x.StatusID == 3).FirstOrDefault());
                list.Add(db.ORDERSTATUS.Where(x => x.StatusID == 4).FirstOrDefault());

                return list;
            }
            else if (id == 1)
            {
                var list = new List<ORDERSTATU>();
                foreach (var item in await StatusList())
                {
                    if (item.StatusID != 1)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            return null;
        }

    }
}
