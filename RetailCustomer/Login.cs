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
            string password 
                = txt_password.Text;
            if (username == "" | password == "")
            {
                lbl_error.Text = "Please enter username or password";
            }
            else {
            SessionData.UserName = username;
            
            string returnvalue = pdal.Login(username,password);
            if (returnvalue == "invalid")
            {
                lbl_error.Text = "Invalid UserName or Password";

            }
            else
            {
                
            char[] delimiterChars = { ',' };
            

            string[] list = returnvalue.Split(delimiterChars);
            SessionData.IsLogedIn = "true";
            SessionData.userName = list[0];
            SessionData.firstName = list[1];
            SessionData.EmpID = list[2];
            SessionData.Role = list[3];
            SessionData.GuestId = list[4];
            SessionData.RolesData = pdal.GetRoles(int.Parse(list[5]));
            SessionData.discount = pdal.ReturnDiscountPr();
            SessionData.pricing = pdal.GetPriceingMethod();
            SessionData.alrtdaysexp = pdal.GetExpiryDays();
            SessionData.cutOffTime = pdal.GetCutOffTime();
            #region Company Info
            DAL.CompanyInfo cinfo = new DAL.CompanyInfo();
            companyinfo c = cinfo.GetCompanyInfo(1);
            SessionData.CName = c.compName;
            SessionData.CLocation = c.CompPrintAddress;
            SessionData.CContactNo = c.CompContactNo;
            SessionData.CWebSite = c.CompWebsite;
            SessionData.FooterMessage = c.CompFootNote1;
            SessionData.FooterMessageTwo = c.CompFootNote2;
            SessionData.printerType = c.prinerType;

            #endregion

            if (SessionData.Role.ToString() == "sales Person")
            {
                int cutofftime = int.Parse(SessionData.cutOffTime.ToString());
                bool chksaledaystart = pdal.CheckNewDayOpen(cutofftime);
                if (chksaledaystart)
                {
                    SessionData.ThisSaleDayId = pdal.GetTodayDayId(cutofftime);
                    SaleForm s = new SaleForm();
                    s.ShowDialog();
                }
                else
                {
                    SessionData.ThisSaleDayId = pdal.StartNewDay(cutofftime);
                    SaleForm s = new SaleForm();
                    s.ShowDialog();


                }
                
                 

            }
            this.Hide();
            StartDay d = new StartDay();
            d.ShowDialog();
            }
            }
            
        }
        private void btn_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login.PerformClick();
            }
            
        }



        public string UserName { get; set; }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
