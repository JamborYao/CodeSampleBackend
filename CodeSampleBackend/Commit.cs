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
    
    public partial class Commit
    {
        public int id { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public string Sha { get; set; }
        public string PSha { get; set; }
        public string GitHubUrl { get; set; }
        public string URL { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public string Type { get; set; }
        public Nullable<int> CodeID { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
    }
}
