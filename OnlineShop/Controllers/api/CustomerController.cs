using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Models.Dao;
using Models.Ef;
namespace OnlineShop.Controllers.api
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        public async Task<List<CUSTOMER>> ListAll()
        {
            return await new CustomerDao().LoadCustomerAsync();
        }

        [HttpPost]
        [Route("api/validateuser")]
        public bool ValidateUser(CUSTOMER model)
        {
            return new CustomerDao().Login(model.CustomerUsername, model.CustomerPassword);
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<int> Register(CUSTOMER model)
        {
            return await new CustomerDao().Register(model);
        }

        [HttpGet]
        [Route("api/getcustomer/{username}")]
        public async Task<CUSTOMER> GetByUsername(string username)
        {
            return await new CustomerDao().LoadByUsername(username);
        }

        [HttpPut]
        [Route("api/editcustomer")]
        public int Edit(CUSTOMER customer)
        {
            return new CustomerDao().Edit(customer);
        }
    }
}
