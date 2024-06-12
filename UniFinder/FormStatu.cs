//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniFinder
{
    using System;
    using System.Collections.Generic;
    
    public partial class FormStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormStatu()
        {
            this.ApplicationManagements = new HashSet<ApplicationManagement>();
            this.PaymentToUnis = new HashSet<PaymentToUni>();
        }
    
        public int FormID { get; set; }
        public Nullable<int> CID { get; set; }
        public Nullable<int> UniID { get; set; }
        public string FormCode { get; set; }
        public Nullable<int> Payment { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationManagement> ApplicationManagements { get; set; }
        public virtual UniversityTbl UniversityTbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentToUni> PaymentToUnis { get; set; }
    }
}