using Admin.Model;
using Models.Ef;
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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        public string baseAddress = "https://localhost:44337/api/";
        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            USER user = new USER();

            user.UserUsername= txtTenDN.Text;
            user.UserPassword = txtMatKhau.Text;   
           var responce = await Login.Logins(user);
            if(responce==true)
            {
                frmMain frm = new frmMain();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("username or password is incorrect");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
