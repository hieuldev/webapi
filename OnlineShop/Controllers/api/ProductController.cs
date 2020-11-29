using Models.Dao;
using Models.Ef;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineShop.Controllers.api
{
    public class ProductController : ApiController
    {
        public ProductController() { }

        [Route("api/productlist")]
        public async Task<IEnumerable<PRODUCT>> GetAllProduct()
        {
            return await new ProductDao().ListAll();
        }

        public async Task<PRODUCT> GetByID(string id)
        {
            return await new ProductDao().GetByID(id);
        }


        [Route("api/categoryid/{cate}")]
        public async Task<List<PRODUCT>> GetByCate(string cate)
        {
            return await new ProductDao().GetByCate(cate);
        }

        [HttpGet]
        [Route("api/size")]
        public async Task<List<SIZE>> ListSize()
        {
            return await new SizeDao().ListAll();
        }

        [HttpPost]
        [Route("api/createproduct")]
        public async Task<string> CreateProduct(ProductModel prod)
        {
            var product = new PRODUCT()
            {
                ProductID = prod.ProductID,
                ProductName = prod.ProductName,
                ProductDescription = prod.ProductDescription,
                ProductPrice = prod.ProductPrice,
                PromotionPrice = prod.PromotionPrice,
                ProductStock = prod.ProductStock,
                CategoryID = prod.CategoryID,
                MetaKeyword = SlugGenerator.SlugGenerator.GenerateSlug(prod.ProductName),
                ShowImage_1 = prod.ShowImage_1,
                ShowImage_2 = prod.ShowImage_2,
                ProductStatus = prod.ProductStatus,
                CreatedDate = DateTime.Now
            };
            await new ProductDao().CreateProduct(product);
            await new ProductDetailDao().CreateProductDetail(prod.ProductID, prod.SizeID);
            return product.ProductID;
        }

        [HttpDelete]
        [Route("api/delete/{ID}")]
        public bool DeleteProduct(string ID)
        {
            return new ProductDao().DeleteProduct(ID);
        }

        [HttpPut]
        [Route("api/editproduct/{prod.ProductID}")]
        public async Task<string> EditProduct(ProductModel prod)
        {
            var product = new PRODUCT()
            {
                ProductID = prod.ProductID,
                ProductName = prod.ProductName,
                ProductPrice = prod.ProductPrice,
                PromotionPrice = prod.PromotionPrice,
                ProductStock = prod.ProductStock,
                CategoryID = prod.CategoryID,
                MetaKeyword = SlugGenerator.SlugGenerator.GenerateSlug(prod.ProductName),
                ShowImage_1 = prod.ShowImage_1,
                ShowImage_2 = prod.ShowImage_2,
                ProductStatus = prod.ProductStatus,
                CreatedDate = DateTime.Now
            };
            var check = await new ProductDetailDao().DeleteProductDetail(prod.ProductID);
            await new ProductDao().EditProduct(product);
            await new ProductDetailDao().CreateProductDetail(prod.ProductID, prod.SizeID);
            return prod.ProductID;
        }


        [HttpGet]
        [Route("api/getproductsize/{id}")]
        public async Task<List<SIZE>> GetProductBySize(string id)
        {
            return await new ProductDetailDao().LoadSize(id);
        }

        [HttpGet]
        [Route("api/size/{id}")]
        public async Task<SIZE> GetSizeByID(int id)
        {
            return await new SizeDao().LoadByID(id);
        }

        [HttpPost]
        [Route("api/createsize")]
        public async Task<int> CreteSize(SIZE size)
        {
            return await new SizeDao().CreateSizeProc(size);
        }

        [HttpPut]
        [Route("api/editsize")]
        public async Task<int> EditSize(SIZE size)
        {
            return await new SizeDao().EditSizeProc(size);
        }

        [HttpDelete]
        [Route("api/deletesize/{sizeID}")]
        public async Task<bool> DeleteSize(int sizeID)
        {
            return await new SizeDao().DeleteSizeProc(sizeID);
        }
    }
}
