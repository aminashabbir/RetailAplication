using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace  RetailCustomer
{
    public class OrderReciveStructure
    {
        int? productSuppliedId;

        public int? ProductSuppliedId
        {
            get { return productSuppliedId; }
            set { productSuppliedId = value; }
        }
       
        string ProductName;
        public string ProductName1
        {
            get { return ProductName; }
            set { ProductName = value; }
        }
     
        int? quantityRecived;
        public int? QuantityRecived
        {
            get { return quantityRecived; }
            set { quantityRecived = value; }
        }
       
        DateTime? orderRecivingDateD;
        public DateTime? OrderRecivingDateD
        {
            get { return orderRecivingDateD; }
            set { orderRecivingDateD = value; }
        }

        double? pricePrItem;
        public double? PricePrItem
        {
            get { return pricePrItem; }
            set { pricePrItem = value; }
        }

        double? discountPercentage;
        public double? DiscountPercentage
        {
            get { return discountPercentage; }
            set { discountPercentage = value; }
        }

        int? packSize;
        public int? PackSize
        {
            get { return packSize; }
            set { packSize = value; }
        }

        int? stockId;
        public int? StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        int? quantity;
        public int? QuantityAcepted
        {
            get { return quantity; }
            set { quantity = value; }
        }

        string batchNO;
        public string BatchNO
        {
            get { return batchNO; }
            set { batchNO = value; }
        }

        DateTime expiryDateD;
        public DateTime ExpiryDateD
        {
            get { return expiryDateD; }
            set { expiryDateD = value; }
        }

        double sellingPricePrItem;
        public double SellingPricePrItem
        {
            get { return sellingPricePrItem; }
            set { sellingPricePrItem = value; }
        }

        int? itemSold;
        public int? ItemSold
        {
            get { return itemSold; }
            set { itemSold = value; }
        }

       
    }
}