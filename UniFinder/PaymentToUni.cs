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
    
    public partial class PaymentToUni
    {
        public int UniTransID { get; set; }
        public Nullable<int> CID { get; set; }
        public Nullable<int> FormID { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<int> UniID { get; set; }
        public string PaymentOption { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        public virtual FormStatu FormStatu { get; set; }
        public virtual UniversityTbl UniversityTbl { get; set; }
    }
}
