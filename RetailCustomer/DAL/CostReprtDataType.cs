using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailCustomer.DAL
{
    public class CostReprtDataType
    {
        string Name;

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        string Unit;

        public string unit
        {
            get { return Unit; }
            set { Unit = value; }
        }
        double Size;

        public double size
        {
            get { return Size; }
            set { Size = value; }
        }
        double Pprice;

        public double pprice
        {
            get { return Pprice; }
            set { Pprice = value; }
        }
        double Sprice;

        public double sprice
        {
            get { return Sprice; }
            set { Sprice = value; }
        }
        int Quantity;

        public int quantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }
    }
}