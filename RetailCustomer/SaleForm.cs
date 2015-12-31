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
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void lbl_name_Click(object sender, EventArgs e)
        {

        }

        private void lbl_subtotal_Click(object sender, EventArgs e)
        {


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void txt_search1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_amount_TextChanged(object sender, EventArgs e)
        {
            int anInteger;
            anInteger = Convert.ToInt32(txt_amount.Text);
            anInteger = int.Parse(txt_amount.Text);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                this.ActiveControl = txt_amount;
                return true;
            }
            if (keyData == (Keys.Control | Keys.P))
            {
                this.ActiveControl = txt_search2;
                return true;
            }
                if (keyData == (Keys.Control | Keys.D))
                {
                    this.ActiveControl = txt_discountinrupees;
                return true;
            }
            if (keyData == (Keys.Control | Keys.X))
            {
                this.ActiveControl = txt_discount;
                return true;
            }
            if (keyData == (Keys.Control | Keys.Z))
            {
                this.ActiveControl = txt_Name;
                return true;
            }
            if (keyData == (Keys.Control | Keys.Q))
            {
                this.ActiveControl = cmb_payment;
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                this.ActiveControl = txt_search1;
                return true;
            }
            if (keyData == (Keys.Control | Keys.N))
            {
                this.ActiveControl = btn_suspend;
                return true;
            }
            if (keyData == (Keys.Control | Keys.C))
            {
                this.ActiveControl = btn_cancel;
                return true;
            }
            if (keyData == (Keys.Control | Keys.W))
            {
                this.ActiveControl = btn_saleandprint;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void btn_saleandprint_Click(object sender, EventArgs e)
        {

        }

        private void txt_search2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_payment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SaleForm_Load(object sender, EventArgs e)
        {

            
        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            int anInteger;
            anInteger = Convert.ToInt32(txt_discountinrupees.Text);
            anInteger = int.Parse(txt_discountinrupees.Text);
        }

        private void txt_discountinrupees_TextChanged(object sender, EventArgs e)
        {
            int anInteger;
            anInteger = Convert.ToInt32(txt_discount.Text);
            anInteger = int.Parse(txt_discount.Text);
        }

        private void rdb_noprint_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void list_suspendedsale_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
      
    }
}
