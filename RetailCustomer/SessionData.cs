using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailCustomer
{
    
    static class SessionData
    {
        
        private static string _userName = "";

        public static string UserName
        {
            get { return SessionData._userName; }
            set { SessionData._userName = value; }
        }
        private static string _firstName = "";

        public static string FirstName
        {
            get { return SessionData._firstName; }
            set { SessionData._firstName = value; }
        }
        private static string _Emp_ID = "";

        public static string Emp_ID
        {
            get { return SessionData._Emp_ID; }
            set { SessionData._Emp_ID = value; }
        }
        private static string _role = "";

        public static string Role
        {
            get { return SessionData._role; }
            set { SessionData._role = value; }
        }
        private static string _GuestId = "";

        public static string GuestId
        {
            get { return SessionData._GuestId; }
            set { SessionData._GuestId = value; }
        }
        private static string _authorityId = "";

        public static string AuthorityId
        {
            get { return SessionData._authorityId; }
            set { SessionData._authorityId = value; }
        }
        
        public static string IsLogedIn { get; set; }

        public static string userName { get; set; }

        public static string firstName { get; set; }

        public static string EmpID { get; set; }

        public static string _EmpID { get; set; }

        public static List<DAL.Roles> RolesData { get; set; }

        public static int? discount { get; set; }

        public static string pricing { get; set; }

        public static int alrtdaysexp { get; set; }

        public static string cutOffTime { get; set; }

        public static string CName { get; set; }

        public static string CLocation { get; set; }

        public static string CContactNo { get; set; }

        public static string CWebSite { get; set; }

        public static string FooterMessage { get; set; }

        public static string FooterMessageTwo { get; set; }

        public static string printerType { get; set; }

        public static int? ThisSaleDayId { get; set; }
        

        public static int SalesmanDayId { get; set; }
    }
}

