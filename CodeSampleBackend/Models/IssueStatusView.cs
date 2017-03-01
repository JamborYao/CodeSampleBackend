using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class IssueStatusView
    {
        public int id { get; set; }
        public Nullable<int>  IssueID { get; set; }
        public string IssueStatusName { get; set; }
        public Nullable<System.DateTime> LogAt { get; set; }
    }
}