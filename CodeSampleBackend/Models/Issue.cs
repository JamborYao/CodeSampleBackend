using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public Nullable<int> GitCodeId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string IssueUser { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public string State { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<System.DateTime> CloseAt { get; set; }
        public Nullable<System.DateTime> UpdateAt { get; set; }
        public string Html_Url { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public string From { get; set; } //Acom,Acn
    }
}