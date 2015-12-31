using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailCustomer.DAL
{
    public class ReturnOfSalesStructure
    {
        int salesID;

        public int SalesID
        {
            get { return salesID; }
            set { salesID = value; }
        }
        int soldItemID;

        public int SoldItemID
        {
            get { return soldItemID; }
            set { soldItemID = value; }
        }
        int stockID;

        public int StockID
        {
            get { return stockID; }
            set { stockID = value; }
        }
        int quantitySold;

        public int QuantitySold
        {
            get { return quantitySold; }
            set { quantitySold = value; }
        }
        
        int quantityReturn;

        public int QuantityReturn
        {
            get { return quantityReturn; }
            set { quantityReturn = value; }
        }
        double amountSold;

        public double AmountSold
        {
            get { return amountSold; }
            set { amountSold = value; }
        }
        
        double amountReturn;

        public double AmountReturn
        {
            get { return amountReturn; }
            set { amountReturn = value; }
        }
        string batchNo;

        public string BatchNo
        {
            get { return batchNo; }
            set { batchNo = value; }
        }
        DateTime expiryDate;

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }
        string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

    }
}