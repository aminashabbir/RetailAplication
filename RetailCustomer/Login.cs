using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailCustomer
{

    public partial class Login : Form
    {
        DAL.PharmacyDAL pdal;
        public Login()
        {
            InitializeComponent();
            pdal = new DAL.PharmacyDAL();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text;
            string password = txt_password.Text;

            string aut = pdal.Login(username, password);
            this.Hide();
            StartDay s = new StartDay();
            s.ShowDialog();
        }
        private void btn_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login.PerformClick();
            }
            
        }

    }
}
