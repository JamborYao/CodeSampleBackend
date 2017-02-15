using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class IssueView
    {

        public string Title { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<int> Number { get; set; }
        public string Url { get; set; }
        public Nullable<int> UnicodeId { get; set; }
        public Nullable<int> Replies { get; set; }
        public string Author { get; set; }
        public Nullable<int> CodeID { get; set; }
        public string Type { get; set; }
        public string alias { get; set; }
        public Nullable<int> UT { get; set; }
        public string Comment { get; set; }
        public int id { get; set; }
        public string process { get; set; }
    }

}