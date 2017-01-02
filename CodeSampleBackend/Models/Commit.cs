using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class Commit
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public string Committer { get; set; }
        public string Message { get; set; }
        public string Sha { get; set; }
        public string PSha { get; set; }
        public Nullable<int> GitCodeId { get; set; }
        public string Html_Url { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public string From { get; set; } //Acom,Acn
    }
}