 using Models.Ef;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CustomerDao
    {
        OnlineShopDbContext db = null;
        public CustomerDao()
        {
            db = new OnlineShopDbContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        public async Task<List<CUSTOMER>> LoadCustomerAsync()
        {
            return await db.CUSTOMERs.ToListAsync();
        }

        public async Task<bool> DeleteCustomer(int ID)
        {
            try
            {
                var cus = await db.CUSTOMERs.Where(x => x.CustomerID == ID).SingleOrDefaultAsync();
                db.CUSTOMERs.Remove(cus);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckUser(string username)
        {
            return db.CUSTOMERs.AsNoTracking().Any(x => x.CustomerUsername == username);
        }

        public async Task<CUSTOMER> LoadByUsername(string username)
        {
            return await db.CUSTOMERs.Where(x => x.CustomerUsername == username).SingleOrDefaultAsync();
        }

        public bool Login(string username, string password)
        {
            string encrypt = EncryptPassword(password);
            var result = db.CUSTOMERs.AsNoTracking().Count(x => x.CustomerUsername.Equals(username) && x.CustomerPassword.Equals(encrypt));
            if (result > 0)
                return true;
            else
                return false;
        }

        public string checknullphone(string phone)
        {
            if (phone == null) return ""; else return phone;
        }
        public string checknullmail(string mail)
        {
            if (mail == null) return ""; else return mail;
        }
        public async Task<int> Register(CUSTOMER cus)
        {
            cus.CustomerPassword = EncryptPassword(cus.CustomerPassword);
            var customer = new CUSTOMER()
            {
                CustomerUsername = cus.CustomerUsername,
                CustomerPassword = cus.CustomerPassword,
                CustomerName = cus.CustomerName,
                CustomerPhone = checknullphone(cus.CustomerPhone),
                CustomerEmail = checknullmail(cus.CustomerEmail),
                CreatedDate = DateTime.Now
            };
            db.CUSTOMERs.Add(customer);
            await db.SaveChangesAsync();
            return cus.CustomerID;
        }

        public CUSTOMER GetByID(int id)
        {
            return db.CUSTOMERs.Find(id);
        }
        public int Edit(CUSTOMER cus)
        {
            var customer = GetByID(cus.CustomerID);
            customer.CustomerName = cus.CustomerName;
            customer.CustomerEmail = cus.CustomerEmail;
            customer.CustomerPhone = cus.CustomerPhone;
            customer.CustomerAdress = cus.CustomerAdress;
            db.SaveChanges();
            return customer.CustomerID;
        }

        public static string EncryptPassword(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
