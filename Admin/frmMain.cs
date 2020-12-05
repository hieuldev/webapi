using Admin.Model;
using Models.Ef;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    public partial class frmMain : Form
    {

        Dictionary<int, string> ListDictionary = new Dictionary<int, string>();
        Dictionary<int, string> ListDictionary1 = new Dictionary<int, string>();
        Dictionary<int, string> ListDictionary2 = new Dictionary<int, string>();
        Dictionary<int, string> ListDictionary3 = new Dictionary<int, string>();
        Dictionary<int, string> ListDictionary4 = new Dictionary<int, string>();
        Bitmap bitmap;
        bool Kt = false;
        bool Kt2 = false;
        bool Kt3 = false;
        string p1;
        string p2;
        public frmMain()
        {
            InitializeComponent();
            loadData();
        }
        public string baseAddress = "http://localhost:50673/api/";
        List<Product> listProduct = null;
        List<Model.Size> listSize = null;
        List<Category> listCategory = null;
        List<Customer> listCustomer = null;
        List<Order> listOrder = null;
        int x;
        public void error()
        {

            if (txtID.Text == "")
            {
                MessageBox.Show("ID not null");
            }

            else if (txtName.Text == "")
            {
                MessageBox.Show("Name not null");
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("Price not null");
            }

            else if (cboCategory.Text == "")
            {
                MessageBox.Show("Category not null");
            }
            else if (cboSize.Text == "")
            {
                MessageBox.Show("SizeID not null");
            }
            else
            {
                this.Kt = true;
            }

        }
        public void error2()
        {

            if (txtIDSize.Text == "")
            {
                MessageBox.Show("ID not null");
            }

            else if (txtSizeName.Text == "")
            {
                MessageBox.Show("Name not null");
            }

            else
            {
                this.Kt2 = true;
            }

        }
        public void error3()
        {

            if (txtCategoryID.Text == "")
            {
                MessageBox.Show("ID not null");
            }

            else if (txtCategoryName.Text == "")
            {
                MessageBox.Show("Name not null");
            }

            else
            {
                this.Kt3 = true;
            }

        }
        private void loadData()
        {
            listProduct = loadProcduct();
            if (listProduct != null)
                dataGridView1.DataSource = listProduct;
            listSize = loadSize();
            if (listSize != null)
                dataGridView2.DataSource = listSize;
            loadCategoryProduct();
            listCategory = loadCategory();
            if (listCategory != null)
                dataGridView3.DataSource = listCategory;

            loadSizeProduct();
            listCustomer = loadCustomer();
            if (listCustomer != null)
                dataGridView5.DataSource = listCustomer;
            listOrder = loadOrder();
            if (listCustomer != null)
                dataGridView4.DataSource = listOrder;

        }
        private List<Product> loadProcduct()
        {
            List<Product> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("productlist");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Product>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            return list;
        }
        private List<Model.Size> loadSize()
        {
            List<Model.Size> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("size");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Model.Size>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            return list;
        }
        private List<Customer> loadCustomer()
        {
            List<Customer> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("customer");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Customer>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            return list;
        }
        private List<Order> loadOrder()
        {
            List<ORDER> list = null;
            List<Order> list1 = new List<Order>();
            /*List<Order> list1 = null;*/
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("order");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ORDER>>();
                    readTask.Wait();
                    list = readTask.Result;

                    foreach (var item in list)
                    {

                        list1.Add(new Order() { OrderID = item.OrderID, CustomerName = item.CUSTOMER.CustomerName, StatusName = item.ORDERSTATU.StatusName, OrderDate = item.OrderDate, DeliveryDate = item.DeliveryDate, Total = item.Total });

                    }

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            return list1;
        }
        private List<Category> loadCategory()
        {
            List<Category> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("categorylist");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Category>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            return list;
        }

        private void loadCategoryProduct()
        {
            List<CATEGORY> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("categorylist");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<CATEGORY>>();
                    readTask.Wait();

                    list = readTask.Result;
                    foreach (var item in list)
                    {
                        ListDictionary.Add(int.Parse(item.CategoryID), item.CategoryName);
                    }
                    cboCategory.DataSource = new BindingSource(ListDictionary, null);
                    cboCategory.DisplayMember = "Value";
                    cboCategory.ValueMember = "Key";



                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }



        }
        private void loadCategoryByID(string id)
        {
            List<CATEGORY> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("categorylist");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<CATEGORY>>();
                    readTask.Wait();

                    list = readTask.Result;
                    foreach (var item in list)
                    {
                        ListDictionary2.Add(int.Parse(item.CategoryID), item.CategoryName);
                    }
                    cboCategory.DataSource = new BindingSource(ListDictionary2, null);
                    cboCategory.DisplayMember = "Value";
                    cboCategory.ValueMember = "Key";



                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }

            CATEGORY list1 = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("category/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CATEGORY>();
                    readTask.Wait();

                    list1 = readTask.Result;
                    cboCategory.SelectedIndex = cboCategory.FindStringExact(list1.CategoryName);
                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
        }
        /* private void loadSizeByID(string id)
         {
             SIZE list = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(baseAddress);
                 //HTTP GET
                 var responseTask = client.GetAsync("getproductsize/" + id);
                 responseTask.Wait();

                 var result = responseTask.Result;
                 if (result.IsSuccessStatusCode)
                 {
                     var readTask = result.Content.ReadAsAsync<SIZE>();
                     readTask.Wait();

                     list = readTask.Result;

                     ListDictionary3.Add(list.SizeID, list.Size1);
                     cboCategory.DataSource = new BindingSource(ListDictionary3, null);
                     cboCategory.DisplayMember = "Value";
                     cboCategory.ValueMember = "Key";



                 }
                 else //web api sent error response 
                 {
                     //log response status here..    

                 }
             }
         }*/
        private void loadSizeProduct()
        {
            List<SIZE> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("size");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SIZE>>();
                    readTask.Wait();

                    list = readTask.Result;
                    foreach (var item in list)
                    {
                        ListDictionary1.Add(item.SizeID, item.Size1);
                    }
                    cboSize.DataSource = new BindingSource(ListDictionary1, null);
                    cboSize.DisplayMember = "Value";
                    cboSize.ValueMember = "Key";




                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            error();
            if (Kt == true)
            {
                string ProductID = txtID.Text;

                string ProductName = txtName.Text;

                string ProductDescription = txtDecription.Text;

                decimal ProductPrice = Decimal.Parse(txtPrice.Text);

                decimal? PromotionPrice = Decimal.Parse(txtPP.Text);

                string ShowImage_1 = p1;

                string ShowImage_2 = p2;

                int? ProductStock = int.Parse(txtStock.Text);

                bool? ProductStatus = bool.Parse(cboStatus.Text);

                string SizeID = cboSize.SelectedValue.ToString();
                string CategoryID = cboCategory.SelectedValue.ToString();
                DateTime? CreatedDate = DateTime.Now;
                ProductModel prod = new ProductModel();
                prod.ProductID = ProductID;
                prod.ProductName = ProductName;
                prod.ProductDescription = ProductDescription;
                prod.ProductPrice = ProductPrice;
                prod.PromotionPrice = PromotionPrice;
                prod.ShowImage_1 = ShowImage_1;
                prod.ShowImage_2 = ShowImage_2;
                prod.ProductStock = ProductStock;
                prod.ProductStatus = ProductStatus;
                prod.CategoryID = CategoryID;
                prod.CreatedDate = CreatedDate;
                List<string> Size = new List<string>();
                Size.Add(SizeID);
                prod.SizeID = Size;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.PutAsJsonAsync<ProductModel>("editproduct/" + prod.ProductID, prod);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Sửa sản phẩm thành công");
                        listProduct = loadProcduct();
                        if (listProduct != null)
                            dataGridView1.DataSource = listProduct;
                    }
                    else //web api sent error response 
                    {
                        MessageBox.Show("Sửa sản phẩm thất bại");

                    }
                }

            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            tabControl1.TabPages.Remove(tabPage1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bitmap = new Bitmap(open.FileName);
                pictureBox1.Image = bitmap;
                p1 = open.FileName;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bitmap = new Bitmap(open.FileName);
                pictureBox2.Image = bitmap;
                p2 = open.FileName;

            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (txtID.Text == "")
            {
                MessageBox.Show("ID not null");
            }
            else
            {
                int id = int.Parse(txtID.Text);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.DeleteAsync("delete/" + id);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Xóa sản phẩm thành công");
                            listProduct = loadProcduct();
                            if (listProduct != null)
                                dataGridView1.DataSource = listProduct;
                        }
                        else //web api sent error response 
                        {
                            MessageBox.Show("Xóa sản phẩm thất bại");

                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadCategoryByID(dataGridView1.CurrentRow.Cells[11].Value.ToString());
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[2].Value != null)
            {
                txtDecription.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }


            txtPrice.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[4].Value != null)
            {
                txtPP.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            if (dataGridView1.CurrentRow.Cells[5].Value != null)
            {
                pictureBox1.Image = Image.FromFile(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[6].Value != null)
            {
                pictureBox2.Image = Image.FromFile(dataGridView1.CurrentRow.Cells[6].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[7].Value != null)
            {
                txtStock.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
            if (dataGridView1.CurrentRow.Cells[8].Value != null)
            {
                cboStatus.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            }

            //cboCategory.Text= dataGridView1.CurrentRow.Cells[11].Value.ToString(); ;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            error();
            if (Kt == true)
            {
                string ProductID = txtID.Text;

                string ProductName = txtName.Text;

                string ProductDescription = txtDecription.Text;

                decimal ProductPrice = Decimal.Parse(txtPrice.Text);

                decimal? PromotionPrice = Decimal.Parse(txtPP.Text);

                string ShowImage_1 = p1;

                string ShowImage_2 = p2;

                int? ProductStock = int.Parse(txtStock.Text);

                bool? ProductStatus = bool.Parse(cboStatus.Text);

                string SizeID = cboSize.SelectedValue.ToString();
                string CategoryID = cboCategory.SelectedValue.ToString();
                DateTime? CreatedDate = DateTime.Now;
                ProductModel prod = new ProductModel();
                prod.ProductID = ProductID;
                prod.ProductName = ProductName;
                prod.ProductDescription = ProductDescription;
                prod.ProductPrice = ProductPrice;
                prod.PromotionPrice = PromotionPrice;
                prod.ShowImage_1 = ShowImage_1;
                prod.ShowImage_2 = ShowImage_2;
                prod.ProductStock = ProductStock;
                prod.ProductStatus = ProductStatus;
                prod.CategoryID = CategoryID;
                prod.CreatedDate = CreatedDate;
                List<string> Size = new List<string>();
                Size.Add(SizeID);
                prod.SizeID = Size;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.PostAsJsonAsync<ProductModel>("createproduct", prod);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Thêm sản phẩm thành công");
                        listProduct = loadProcduct();
                        if (listProduct != null)
                            dataGridView1.DataSource = listProduct;
                    }
                    else //web api sent error response 
                    {
                        MessageBox.Show("Thêm sản phẩm thất bại");

                    }
                }
            }
        }

        private void btnCreateSize_Click(object sender, EventArgs e)
        {
            error2();
            if (Kt2 == true)
            {
                string IDSize = txtIDSize.Text;

                string SizeName = txtSizeName.Text;

                SIZE model = new SIZE();
                model.SizeID = int.Parse(IDSize);
                model.Size1 = SizeName;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.PostAsJsonAsync<SIZE>("createsize", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Thêm size thành công");
                        listSize = loadSize();
                        if (listSize != null)
                            dataGridView2.DataSource = listSize;
                    }
                    else //web api sent error response 
                    {
                        MessageBox.Show("Thêm size thất bại");

                    }
                }
            }
        }

        private void btnEditSize_Click(object sender, EventArgs e)
        {
            error2();
            if (Kt2 == true)
            {
                string IDSize = txtIDSize.Text;

                string SizeName = txtSizeName.Text;

                SIZE model = new SIZE();
                model.SizeID = int.Parse(IDSize);
                model.Size1 = SizeName;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.PutAsJsonAsync<SIZE>("editsize", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Sửa size thành công");
                        listSize = loadSize();
                        if (listSize != null)
                            dataGridView2.DataSource = listSize;
                    }
                    else //web api sent error response 
                    {
                        MessageBox.Show("Sửa size thất bại");

                    }
                }
            }
        }

        private void btnDelSize_Click(object sender, EventArgs e)
        {
            if (txtIDSize.Text == "")
            {
                MessageBox.Show("ID not null");
            }
            else
            {
                string IDSize = txtIDSize.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.DeleteAsync("deletesize/" + IDSize);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Xóa size thành công");
                            listSize = loadSize();
                            if (listSize != null)
                                dataGridView2.DataSource = listSize;
                        }
                        else //web api sent error response 
                        {
                            MessageBox.Show("Xóa size thất bại");

                        }
                    }
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDSize.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();

            txtSizeName.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
        }

        private void CreateCategory_Click(object sender, EventArgs e)
        {
            error3();
            if (Kt3 == true)
            {
                string CategoryID = txtCategoryID.Text;

                string CategoryName = txtCategoryName.Text;

                CATEGORY model = new CATEGORY();
                model.CategoryID = CategoryID;
                model.CategoryName = CategoryName;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.PostAsJsonAsync<CATEGORY>("createcategory", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Thêm danh mục thành công");
                        listCategory = loadCategory();
                        if (listCategory != null)
                            dataGridView3.DataSource = listCategory;
                    }
                    else //web api sent error response 
                    {
                        MessageBox.Show("Thêm danh mục thất bại");

                    }
                }
            }
        }

        private void EditCategory_Click(object sender, EventArgs e)
        {
            error3();
            if (Kt3 == true)
            {
                string CategoryID = txtCategoryID.Text;

                string CategoryName = txtCategoryName.Text;

                CATEGORY model = new CATEGORY();
                model.CategoryID = CategoryID;
                model.CategoryName = CategoryName;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.PutAsJsonAsync<CATEGORY>("editcategory/" + model.CategoryID, model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Sửa danh mục thành công");
                        listCategory = loadCategory();
                        if (listCategory != null)
                            dataGridView3.DataSource = listCategory;
                    }
                    else //web api sent error response 
                    {
                        MessageBox.Show("Sửa danh mục thất bại");

                    }
                }
            }
        }

        private void DelCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryID.Text == "")
            {
                MessageBox.Show("ID not null");
            }
            else
            {
                string CategoryID = txtCategoryID.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.DeleteAsync("deletecategory/" + CategoryID);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Xóa danh mục thành công");
                            listCategory = loadCategory();
                            if (listCategory != null)
                                dataGridView3.DataSource = listCategory;
                        }
                        else //web api sent error response 
                        {
                            MessageBox.Show("Xóa danh mục thất bại");

                        }
                    }
                }
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryID.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();

            txtCategoryName.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
            this.Hide();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Show();
        }

        private void mânageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboStatusOder.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            if (cboStatusOder.Text == "Đang xử lý")
            {
                cboStatusOder.Items.Clear();
                cboStatusOder.Items.Add("Đang xử lý");
                cboStatusOder.Items.Add("Đang giao hàng");
                cboStatusOder.Items.Add("Đã giao hàng");
                cboStatusOder.Items.Add("Hàng có lỗi");
                cboStatusOder.Items.Add("Đã hủy");
                /* ListDictionary4.Add(1,"Đang xử lý");
                 ListDictionary4.Add(2, "Đang giao hàng");
                 ListDictionary4.Add(3, "Đã giao hàng");
                 ListDictionary4.Add(4, "Hàng có lỗi");
                 ListDictionary4.Add(5, "Đã hủy");
                 cboStatusOder.DataSource = new BindingSource(ListDictionary4, null);
                 cboStatusOder.DisplayMember = "Value";
                 cboStatusOder.ValueMember = "Key";*/
            }
            else if (cboStatusOder.Text == "Đang giao hàng")
            {
                cboStatusOder.Items.Clear();
                cboStatusOder.Items.Add("Đang giao hàng");
                cboStatusOder.Items.Add("Đã giao hàng");
                cboStatusOder.Items.Add("Hàng có lỗi");
                cboStatusOder.Items.Add("Đã hủy");
               /* cboStatusOder.Items.Clear();
                ListDictionary4.Add(2, "Đang giao hàng");
                ListDictionary4.Add(3, "Đã giao hàng");
                ListDictionary4.Add(4, "Hàng có lỗi");
                ListDictionary4.Add(5, "Đã hủy");
                cboStatusOder.DataSource = new BindingSource(ListDictionary4, null);
                cboStatusOder.DisplayMember = "Value";
                cboStatusOder.ValueMember = "Key";*/
          
            }
            else if(cboStatusOder.Text == "Đã giao hàng")
            {

                cboStatusOder.Items.Clear();
                cboStatusOder.Items.Add("Đã giao hàng");
            }   
            else if(cboStatusOder.Text == "Đã hủy")
            {
                cboStatusOder.Items.Clear();
                cboStatusOder.Items.Add("Đã hủy");
            }
            else if(cboStatusOder.Text == "Hàng có lỗi")
            {
                cboStatusOder.Items.Clear();
                cboStatusOder.Items.Add("Hàng có lỗi");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(cboStatusOder.Text == "Đang xử lý")
            {
                x = 1;
            }
            else if (cboStatusOder.Text == "Đang giao hàng")
            {
                x = 2;
            }
            else if (cboStatusOder.Text == "Đã giao hàng")
            {
                x = 3;
            }
            else if (cboStatusOder.Text == "Đã hủy")
            {
                x =5;
            }
            else if (cboStatusOder.Text == "Hàng có lỗi")
            {
                x = 4;
            }
            if (cboStatusOder.Text != "")
            {
                string OrderID = dataGridView4.CurrentRow.Cells[0].Value.ToString();

                int StatusID = x;

                ORDER model = new ORDER();
                model.OrderID = int.Parse(OrderID);
                model.OrderStatusID = StatusID;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    //HTTP GET
                    var postTask = client.PutAsJsonAsync<ORDER>("order", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Thành công");
                        listOrder = loadOrder();
                        if (listCustomer != null)
                            dataGridView4.DataSource = listOrder;
                    }
                    else //web api sent error response 
                    {
                        MessageBox.Show("thất bại");

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage1);
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
            this.Hide();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            tabControl1.Visible = true;
            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
            tabControl1.TabPages.Add(tabPage3);
            tabControl1.TabPages.Add(tabPage4);
            tabControl1.TabPages.Add(tabPage5);
        }

        private void statisticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add(tabPage6);
            tabControl1.TabPages.Add(tabPage7);
            tabControl1.TabPages.Add(tabPage8);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }
    }
}
