using Models.Ef;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<CATEGORY> ListAll()
        {
            return db.CATEGORies.ToList();
        }
        
        public string CreateCategory(CATEGORY cate)
        {
            try
            {
                var category = new CATEGORY()
                {
                    CategoryID = cate.CategoryID,
                    CategoryName = cate.CategoryName,
                    CreatedDate = DateTime.Now,
                    MetaKeyword = SlugGenerator.SlugGenerator.GenerateSlug(cate.CategoryName)
                };
                db.CATEGORies.Add(category);
                db.SaveChanges();
                return category.CategoryID;
            }
            catch
            {
                return null;
            }
            
        }

        public bool EditCategory(CATEGORY cate, string id)
        {
            try
            {
                var x = db.CATEGORies.Find(id);
                if (x != null)
                {
                    x.CategoryName = cate.CategoryName;
                    x.MetaKeyword = SlugGenerator.SlugGenerator.GenerateSlug(cate.CategoryName);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        public bool DeleteCategory(string id)
        {
            try
            {
                var cate = db.CATEGORies.Find(id);
                db.CATEGORies.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CATEGORY> GetByID(string id)
        {
            return await db.CATEGORies.FirstOrDefaultAsync(x => x.CategoryID == id);
        }
    }
}
