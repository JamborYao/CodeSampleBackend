using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models.ValidModel
{
    [MetadataType(typeof(ProcessLogMetadata))]
    public partial class PartialProcessLog
    {
    }
    public class ProcessLogMetadata
    {
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public Nullable<int> ProcessID { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public Nullable<int> FkId { get; set; }
        [StringLength(50, ErrorMessage = "Max 50 character")]
        public string Type { get; set; }
        [StringLength(50, ErrorMessage = "Max 50 character")]
        public string LogBy { get; set; }
    }
}