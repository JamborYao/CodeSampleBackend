//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeSampleBackend
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcessLog
    {
        public int id { get; set; }
        public Nullable<int> ProcessID { get; set; }
        public Nullable<int> FkId { get; set; }
        public Nullable<System.DateTime> LogAT { get; set; }
        public string Type { get; set; }
    }
}
