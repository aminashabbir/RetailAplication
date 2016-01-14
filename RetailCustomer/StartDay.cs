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
        DAL.PharmacyDAL pdal= new DAL.PharmacyDAL();
        StartDay s = new StartDay();
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

        private void StartDay_Load(object sender, EventArgs e)
        {
             try
            {
              
                int EmpID = int.Parse(SessionData.EmpID.ToString());
                int SaleDayId = int.Parse(SessionData.ThisSaleDayId.ToString());
                int? GetAnyPreviousDayOpen = pdal.CheckPreviousDayOpen(SaleDayId,EmpID);
                if (GetAnyPreviousDayOpen !=null)
                {   // check time  if > cutoff time                 
                   
                   // return RedirectToAction("closeThisDaySale", new { preId= GetAnyPreviousDayOpen});
                
                }
                bool chkchk = pdal.CheckSalesmanDayOpen(EmpID, SaleDayId);
                if (chkchk)
                { 
                    SessionData.SalesmanDayId = pdal.GetSalesmanDayOpenID(EmpID, SaleDayId);
                    s.ShowDialog();
                }
                else
                {
                    s.ShowDialog();
                }
            }
            catch {
                 s.Hide();
                Login l = new Login();
                l.ShowDialog();
            }
        }
        
    }
}
