//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetailCustomer
{
    using System;
    using System.Collections.Generic;
    
    public partial class borrowlendstock
    {
        public borrowlendstock()
        {
            this.borrowreturnitems = new HashSet<borrowreturnitem>();
            this.borrowstocks = new HashSet<borrowstock>();
        }
    
        public int borrowLendStockId { get; set; }
        public int borrowLendId { get; set; }
        public Nullable<int> quantityBorrowed { get; set; }
        public Nullable<double> amount { get; set; }
        public int productDetailId { get; set; }
    
        public virtual borrowlend borrowlend { get; set; }
        public virtual productdetail productdetail { get; set; }
        public virtual ICollection<borrowreturnitem> borrowreturnitems { get; set; }
        public virtual ICollection<borrowstock> borrowstocks { get; set; }
    }
}
