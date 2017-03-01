using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class ProcessView
    {

        public string ProcessName { get; set; }
        //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public Nullable<int> FkId { get; set; }
        public Nullable<System.DateTime> LogAT { get; set; }
        public string Type { get; set; }

    }
}