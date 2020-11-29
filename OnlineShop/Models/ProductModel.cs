using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ProductModel
    {
        
        public string ProductID { get; set; }
        
        public string ProductName { get; set; }
        
        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal? PromotionPrice { get; set; }
        
        public string ShowImage_1 { get; set; }
       
        public string ShowImage_2 { get; set; }

        public int? ProductStock { get; set; }

        public bool? ProductStatus { get; set; }

        public int? ViewCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CategoryID { get; set; }

        public List<string> SizeID { get; set; }
        ProductModel(string ProductID,string ProductName,string ProductDescription,decimal ProductPrice,decimal PromotionPrice,string ShowImage_1,string ShowImage_2,int ProductStock, bool ProductStatus, int ViewCount,DateTime CreatedDate,string CategoryID, List<string> SizeID) 
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.ProductDescription = ProductDescription;
            this.ProductPrice = ProductPrice;
            this.PromotionPrice = PromotionPrice;
            this.ShowImage_1 = ShowImage_1;
            this.ShowImage_2 = ShowImage_2;
            this.ProductStock = ProductStock;
            this.ProductStatus = ProductStatus;
            this.ViewCount = ViewCount;
            this.CreatedDate = CreatedDate;
            this.CategoryID = CategoryID;
            this.SizeID = SizeID;
            
        }

        public ProductModel(string productID, string productName, string productDescription, decimal productPrice, decimal? promotionPrice, string showImage_1, string showImage_2, int? productStock, bool? productStatus, string v, DateTime? createdDate, string categoryID)
        {
            ProductID = productID;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            PromotionPrice = promotionPrice;
            ShowImage_1 = showImage_1;
            ShowImage_2 = showImage_2;
            ProductStock = productStock;
            ProductStatus = productStatus;
            CreatedDate = createdDate;
            CategoryID = categoryID;
        }

        public ProductModel(string productID, string productName, string productDescription, decimal productPrice, decimal? promotionPrice, string showImage_1, string showImage_2, int? productStock, bool? productStatus, string v1, DateTime? createdDate, string categoryID, string v2)
        {
            ProductID = productID;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            PromotionPrice = promotionPrice;
            ShowImage_1 = showImage_1;
            ShowImage_2 = showImage_2;
            ProductStock = productStock;
            ProductStatus = productStatus;
            CreatedDate = createdDate;
            CategoryID = categoryID;
        }

        public ProductModel()
        {
        }
    }
}