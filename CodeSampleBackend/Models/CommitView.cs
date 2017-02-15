using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class CommitView
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
        public int? UT { get; set; }
        public string Alias { get; set; }
        public string Process { get; set; }
    }
}