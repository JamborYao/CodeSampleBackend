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
    
    public partial class Issue
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<int> Number { get; set; }
        public string Url { get; set; }
        public Nullable<int> UnicodeId { get; set; }
        public Nullable<int> Replies { get; set; }
        public string Author { get; set; }
        public Nullable<int> CodeID { get; set; }
        public string Type { get; set; }
    }
}