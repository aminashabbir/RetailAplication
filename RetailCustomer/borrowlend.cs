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
    
    public partial class borrowlend
    {
        public borrowlend()
        {
            this.borrowlendstocks = new HashSet<borrowlendstock>();
            this.borrowreturns = new HashSet<borrowreturn>();
        }
    
        public int borrowLendId { get; set; }
        public Nullable<double> netAmount { get; set; }
        public Nullable<System.DateTime> lendingDate { get; set; }
        public string borrowerName { get; set; }
        public int empId { get; set; }
        public int borrowLendStatusId { get; set; }
        public string lendingNumber { get; set; }
        public Nullable<int> branchAsSupplierId { get; set; }
        public int orderId { get; set; }
    
        public virtual borrowlendstatus borrowlendstatus { get; set; }
        public virtual employee employee { get; set; }
        public virtual order order { get; set; }
        public virtual supplier supplier { get; set; }
        public virtual ICollection<borrowlendstock> borrowlendstocks { get; set; }
        public virtual ICollection<borrowreturn> borrowreturns { get; set; }
    }
}
