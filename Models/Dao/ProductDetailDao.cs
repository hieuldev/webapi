using Models.Ef;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductDetailDao
    {
        OnlineShopDbContext db = null;
        public ProductDetailDao()
        {
            db = new OnlineShopDbContext();
        }

        public async Task<bool> CreateProductDetail(string productID, List<string> sizeID)
        {
            try
            {
                foreach (var item in sizeID)
                {
                    _ = db.PRODUCTDETAILs.Add(new PRODUCTDETAIL { ProductID = productID, SizeID = int.Parse(item) });
                }
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductDetail(string prodID)
        {
            try
            {
                var prod = db.PRODUCTDETAILs.Where(x => x.ProductID == prodID).ToList();
                foreach (var item in prod)
                {
                    db.PRODUCTDETAILs.Remove(item);
                }
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PRODUCTDETAIL> LoadByProductID(string prodID)
        {
            return db.PRODUCTDETAILs.Where(x => x.ProductID == prodID).ToList();
        }

        public async Task<List<SIZE>> LoadSize(string prodID)
        {
            var list = new List<SIZE>();
            var dbs = new SizeDao();
            foreach (var item in LoadByProductID(prodID))
            {
                list.Add(await dbs.LoadByID(item.SizeID.Value));
            }
            return list;
        }
    }
}
