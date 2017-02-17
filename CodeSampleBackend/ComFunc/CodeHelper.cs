using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class CodeHelper
    {
        public string Body { get; set; }
        public List<Code> codes { get; set; }

        public CodeHelper GetAcomRequestBody(string url)
        {
            url = HttpUtility.UrlDecode(url);
            string body = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    body = reader.ReadToEnd();
                }

                response.Close();
                this.Body = body;
            }
            catch (Exception e)
            {
                ErrorLog.WriteError(e.Message, "GetHttpRequestBody");
                throw;
            }
            return this;
        }
        public CodeHelper ConvertBodyToCode(string body)
        {
            if (body.Contains("unable to find any sample"))
            {
                this.codes = null;
                return this;
            }
            List<Code> codes = new List<Code>();
            try
            {
                HtmlDocument hdoc = new HtmlDocument();
                hdoc.LoadHtml(body);
                HtmlNodeCollection nodes = hdoc.DocumentNode.SelectNodes("//div[@class='card card-resource']");
                if (nodes == null) return null;
                foreach (var node in nodes)
                {
                    Code code = new Code();
                    code.Title = node.SelectSingleNode(".//a").InnerText;
                    code.Description = node.SelectSingleNode(".//p[@class='sd-truncateText text-mini']").InnerText;
                    code.Link = (node.SelectSingleNode(".//a")).Attributes["href"].Value;
                    code.Author = node.SelectSingleNode(".//div[@class='meta']//span//a").InnerText;
                    code.SyncDate = DateTime.UtcNow;
                    code.GitHubUrl = HttpHelper.GetGitHubURL(code.Link);
                    var tempUpdate = (node.SelectSingleNode(".//div[@class='meta']//span").InnerText);
                    code.LastUpdateDate = Convert.ToDateTime(tempUpdate.Substring(tempUpdate.IndexOf(":") + 1));
                    var products = node.SelectNodes(".//ul[@class='tags']//a[@class='service-label']");
                    if (products != null)
                    {
                        foreach (var product in products)
                        {
                            code.Products += product.InnerText + ";";
                        }
                    }
                    var platforms = node.SelectNodes(".//ul[@class='tags']//a[@class='platform-label']");
                    if (platforms != null)
                    {
                        foreach (var platform in platforms)
                        {
                            code.Platform += platform.InnerText + ";";
                        }
                    }
                    codes.Add(code);
                    //add sample code pull request
                    //sample.GitHubPullRequests = MooncakeTool.Common.GitHubDeveloper.GetGitHubPullEntity(sample.GitResourceUrl);
                    // sample.GitHubIssues= MooncakeTool.Common.GitHubDeveloper.GetGitHubIssuesEntity(sample.GitResourceUrl);
                    // sample.GitHubCommits = MooncakeTool.Common.GitHubDeveloper.GetGitHubCommitsEntity(sample.GitResourceUrl);
                }
                this.codes = codes;
            }
            catch (Exception e)
            {
                ErrorLog.WriteError(e.Message, "ConvertBodyToCode");
                throw;
            }
            return this;
        }
        public CodeHelper InsertToDatabase(List<Code> codes, BasicCRUD dal)
        {
            if (codes != null)
            {
                foreach (var item in codes)
                {
                    item.SyncDate = DateTime.UtcNow;
                    dal.AddOrUpdate<Code>(item, c => c.Title == item.Title, Basic.ToDictionary<Code>(item));
                }
            }
            return this;
        }
    }
}