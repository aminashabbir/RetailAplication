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
    
    public partial class authority
    {
        public authority()
        {
            this.roleandauthorities = new HashSet<roleandauthority>();
            this.users = new HashSet<user>();
        }
    
        public short authorityId { get; set; }
        public string authority1 { get; set; }
    
        public virtual ICollection<roleandauthority> roleandauthorities { get; set; }
        public virtual ICollection<user> users { get; set; }
    }
}
