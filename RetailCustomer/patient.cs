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
    
    public partial class patient
    {
        public patient()
        {
            this.sales = new HashSet<sale>();
        }
    
        public int patientsId { get; set; }
        public string patientName { get; set; }
        public string patientFatherName { get; set; }
        public string contactNumber { get; set; }
        public string emailId { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> dateOfBirth { get; set; }
    
        public virtual ICollection<sale> sales { get; set; }
    }
}
