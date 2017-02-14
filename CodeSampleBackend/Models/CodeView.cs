using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class PageCodeView
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
        [JsonProperty(PropertyName = "result")]
        public List<CodeView> Views { get; set; }
    }
    public class CodeView
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string  Link { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "last_update")]
        public DateTime? LastUpateDate { get; set; }

        [JsonProperty(PropertyName = "products")]
        public List<string> Products { get; set; }
        [JsonProperty(PropertyName = "platforms")]
        public List<string> Platforms { get; set; }

        [JsonProperty(PropertyName = "sync_date")]
        public DateTime? SyncDate { get; set; }

        [JsonProperty(PropertyName = "process")]
        public string Process { get; set; }

        [JsonProperty(PropertyName = "github_url")]
        public string GitHubUrl { get; set; }

      
        [JsonProperty(PropertyName = "new_commit")]
        public List<Commit> NewCommit { get; set; }

        [JsonProperty(PropertyName = "new_issue")]
        public List<IssueView> NewIssue { get; set; }

        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }
    }
}