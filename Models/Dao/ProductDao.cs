using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Models.Ef;

namespace Models.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public async Task<List<PRODUCT>> ListAll()
        {
            return await db.PRODUCTs.ToListAsync();
        }

        public async Task<PRODUCT> GetByID(string id)
        {
            return await db.PRODUCTs.FindAsync(id);
        }

        public async Task<List<PRODUCT>> GetByCate(string cate)
        {
            return await db.PRODUCTs.Where(x => x.CategoryID == cate).ToListAsync();
        }

        public async Task<List<PRODUCT>> Search(string searchString, string cateid)
        {
            if (!String.IsNullOrEmpty(cateid))
            {
                return await db.PRODUCTs.Where(x => x.ProductName.Contains(searchString) && x.CategoryID == cateid).ToListAsync();
            }
            return await db.PRODUCTs.Where(x => x.ProductName.Contains(searchString)).ToListAsync();
        }

        public bool DeleteProduct(string ProductID)
        {
            try
            {
                var prod = db.PRODUCTs.Find(ProductID);
                db.PRODUCTs.Remove(prod);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<string> CreateProduct(PRODUCT prod)
        {
            var product = new PRODUCT()
            {
                ViewCount = 0,
                ProductID = prod.ProductID,
                ProductName = prod.ProductName,
                ProductPrice = prod.ProductPrice,
                PromotionPrice = prod.PromotionPrice,
                ProductStock = prod.ProductStock,
                CategoryID = prod.CategoryID,
                MetaKeyword = prod.ProductName,
                ShowImage_1 = prod.ShowImage_1,
                ShowImage_2 = prod.ShowImage_2,
                ProductStatus = prod.ProductStatus,
                CreatedDate = DateTime.Now
            };

            db.PRODUCTs.Add(product);
            await db.SaveChangesAsync();
            return product.ProductID;
        }

        public async Task<string> EditProduct(PRODUCT prod)
        {
            try
            {
                var product = db.PRODUCTs.Find(prod.ProductID);
                product.ProductName = prod.ProductName;
                product.ProductPrice = prod.ProductPrice;
                product.PromotionPrice = prod.PromotionPrice;
                product.ProductStock = prod.ProductStock;
                product.CategoryID = prod.CategoryID;
                product.MetaKeyword = prod.MetaKeyword;
                product.ShowImage_1 = prod.ShowImage_1;
                product.ShowImage_2 = prod.ShowImage_2;
                product.CreatedDate = DateTime.Now;
                await db.SaveChangesAsync();
                return prod.ProductID;
            }
            catch
            {
                return null;
            }

        }

        public async Task<int> UpdateView(string prodID)
        {
            var id = new SqlParameter("@id", prodID);
            return await db.Database.SqlQuery<int>("Update_ViewCount @id", id).FirstOrDefaultAsync();
        }

        public async Task<List<PRODUCT>> SelectTop(int number)
        {
            var top = new SqlParameter("@topcount", number);
            return await db.Database.SqlQuery<PRODUCT>("Top_View @topcount", top).ToListAsync();
        }
    }
}
