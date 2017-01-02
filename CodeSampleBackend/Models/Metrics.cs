using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class Metrics
    {
        public string ID { get; set; }
        public string Alias { get; set; }
        public int Labor { get; set; }
        public string LaborComment { get; set; }
        public string CodeID { get; set; }

        public DateTime LogAt { get; set; }



    }
}