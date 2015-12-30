using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailCustomer.DAL
{

    public class SaleWithInStockItem
    {
        int pdID;

        public int ProductDetailID
        {
            get { return pdID; }
            set { pdID = value; }
        }

        int soldItemId;
        public int SoldItemId
        {
            get { return soldItemId; }
            set { soldItemId = value; }
        }

        int soldStockId;
        public int SoldStockId
        {
            get { return soldStockId; }
            set { soldStockId = value; }
        }

        double? amount;
        public double? priceUnit
        {
            get { return amount; }
            set { amount = value; }
        }

        int? quantity;
        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        int inStockItem;
        public int InStockItem
        {
            get { return inStockItem; }
            set { inStockItem = value; }
        }


        string pname;
        public string Pname
        {
            get { return pname; }
            set { pname = value; }
        }

        string cname;
        public string Cname
        {
            get { return cname; }
            set { cname = value; }
        }

        string cunit;
        public string Cunit
        {
            get { return cunit; }
            set { cunit = value; }
        }

        double? psize;
        public double? Psize
        {
            get { return psize; }
            set { psize = value; }
        }

    }
}

