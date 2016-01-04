using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailCustomer
{
   static  class SessionData
    {
        private static string _userName = "";

        public static string UserName
        {
            get { return SessionData._userName; }
            set { SessionData._userName = value; }
        }


    }
}
