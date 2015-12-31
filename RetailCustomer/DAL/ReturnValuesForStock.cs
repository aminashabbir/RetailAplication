using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailCustomer.DAL
{
    public class ReturnValuesForStock
    {
        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}