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
    
    public partial class ProgramTbl
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string Degree { get; set; }
        public Nullable<int> TradeID { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public string Picture { get; set; }
        public Nullable<int> UniID { get; set; }
    
        public virtual Tradetbl Tradetbl { get; set; }
        public virtual UniversityTbl UniversityTbl { get; set; }
    }
}