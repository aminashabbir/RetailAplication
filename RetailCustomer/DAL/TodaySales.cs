using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailCustomer.DAL
{
    public class TodaysSales
    {
        Int32 salesnumber;

        public Int32 SalesNumber
        {
            get { return salesnumber; }
            set { salesnumber = value; }
        }
        double? netamount;

        public double? NetAmount
        {
            get { return netamount; }
            set { netamount = value; }
        }
        string pname;

        public string PName
        {
            get { return pname; }
            set { pname = value; }
        }
    }
}
