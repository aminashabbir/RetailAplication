using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailCustomer.DAL
{
    public class StockType
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        double size;

        public double Size
        {
            get { return size; }
            set { size = value; }
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
        int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        int itemSold;

        public int ItemSold
        {
            get { return itemSold; }
            set { itemSold = value; }
        }
        int stockId;

        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }
        DateTime expiry;

        public DateTime Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }
        int packSize;

        public int PackSize
        {
            get { return packSize; }
            set { packSize = value; }
        }
        double disc;

        public double Disc
        {
            get { return disc; }
            set { disc = value; }
        }
        double priceRecived;

        public double PriceRecived
        {
            get { return priceRecived; }
            set { priceRecived = value; }
        }
        double sellingPrice;

        public double SellingPrice
        {
            get { return sellingPrice; }
            set { sellingPrice = value; }
        }
    }
}