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
    
    public partial class employee
    {
        public employee()
        {
            this.borrowlends = new HashSet<borrowlend>();
            this.salesmansaledays = new HashSet<salesmansaleday>();
            this.employee1 = new HashSet<employee>();
            this.employerstocks = new HashSet<employerstock>();
            this.orders = new HashSet<order>();
            this.orderpartials = new HashSet<orderpartial>();
            this.returnofborrows = new HashSet<returnofborrow>();
            this.sales = new HashSet<sale>();
        }
    
        public int empId { get; set; }
        public string firstName { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string remarks { get; set; }
        public Nullable<bool> deleted { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> joiningDate { get; set; }
        public int designationId { get; set; }
        public int departmentId { get; set; }
        public Nullable<int> managerId { get; set; }
        public string userName { get; set; }
    
        public virtual ICollection<borrowlend> borrowlends { get; set; }
        public virtual department department { get; set; }
        public virtual designation designation { get; set; }
        public virtual ICollection<salesmansaleday> salesmansaledays { get; set; }
        public virtual ICollection<employee> employee1 { get; set; }
        public virtual employee employee2 { get; set; }
        public virtual user user { get; set; }
        public virtual ICollection<employerstock> employerstocks { get; set; }
        public virtual ICollection<order> orders { get; set; }
        public virtual ICollection<orderpartial> orderpartials { get; set; }
        public virtual ICollection<returnofborrow> returnofborrows { get; set; }
        public virtual ICollection<sale> sales { get; set; }
    }
}
