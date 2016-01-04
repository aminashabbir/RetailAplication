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
    public partial class StartDay : Form
    {
        public StartDay()
        {
            InitializeComponent();
            lbl_username.Text = "Welcome "+SessionData.UserName;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaleForm s = new SaleForm();
            s.ShowDialog();
        }
        private void btn_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_startday.PerformClick();
            }

        }
    }
}
