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
    
    public partial class borrowlendstatus
    {
        public borrowlendstatus()
        {
            this.borrowlends = new HashSet<borrowlend>();
        }
    
        public int borrowLendStatusId { get; set; }
        public string statusName { get; set; }
        public string discription { get; set; }
    
        public virtual ICollection<borrowlend> borrowlends { get; set; }
    }
}
