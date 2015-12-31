using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailCustomer.DAL
{
    public class ForExpiryAlerts
    {
        int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        int sum;

        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        private DateTime expiry;

        public DateTime Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }
    }
}