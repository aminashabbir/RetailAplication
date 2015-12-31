using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailCustomer.DAL
{
    public class OrderPlaceTable
    {
        int indexNumber;

        public int IndexNumber
        {
            get { return indexNumber; }
            set { indexNumber = value; }
        }

        int orderPlaceId;

        public int OrderPlaceId
        {
            get { return orderPlaceId; }
            set { orderPlaceId = value; }
        }
        string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        int? quantityOrderd;

        public int? QuantityOrderd
        {
            get { return quantityOrderd; }
            set { quantityOrderd = value; }
        }
        int? packSize;

        public int? PackSize
        {
            get { return packSize; }
            set { packSize = value; }
        }
        double? ratePrUnit;

        public double? RatePrUnit
        {
            get { return ratePrUnit; }
            set { ratePrUnit = value; }
        }

    }
}