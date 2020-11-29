using Models.Dao;
using Models.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineShop.Controllers.api
{
    public class CategoryController : ApiController
    {
        [HttpPost]
        [Route("api/createcategory")]
        public IHttpActionResult CreateCategory(CATEGORY cate)
        {
            var c = new CategoryDao().CreateCategory(cate);
            return Ok();
        }

        [HttpGet]
        [Route("api/categorylist")]
        public List<CATEGORY> ListAll()
        {
            return new CategoryDao().ListAll();
        }
        

        [HttpDelete]
        [Route("api/deletecategory/{id}")]
        public bool DeleteCategory(string id)
        {
            return new CategoryDao().DeleteCategory(id);
        }

        [HttpGet]
        [Route("api/category/{id}")]
        public async Task<CATEGORY > GetByID(string id)
        {
            return await new CategoryDao().GetByID(id);
        }

        [HttpPut]
        [Route("api/editcategory/{id}")]
        public bool EditCategoy(CATEGORY cate, string id)
        {
            return new CategoryDao().EditCategory(cate,id);
        }
    }
}
