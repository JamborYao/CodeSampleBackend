using CodeSampleBackend.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class HttpHelper
    {
        public static string GetHttpRequestBody(string url)
        {
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
                return body;
            }
            catch (Exception e)
            {
                ErrorLog.WriteError(e.Message, "GetHttpRequestBody");
                throw;
            }


        }
        /// <summary>
        /// get git url from the sample code hyper link
        /// </summary>
        /// <param name="url">url that used to redirect to code sample detailed page</param>
        /// <returns></returns>
        public static string GetGitHubURL(string url)
        {
            string body = string.Empty, gitUrl = string.Empty;
            try
            {
                body = GetHttpRequestBody(Constants.SampleCodeDomain + url);

                HtmlDocument hdoc = new HtmlDocument();
                hdoc.LoadHtml(body);
                var link = hdoc.DocumentNode.SelectSingleNode("//a[@id='samples-browse-code']");
                gitUrl = link.Attributes["href"].Value;
                return gitUrl;
            }
            catch (Exception e)
            {
                ErrorLog.WriteError(e.Message, "GetGitHubURL");
                throw;
            }
        }

        public static List<Code> ConvertBodyToCode(string body)
        {

            List<Code> codes = new List<Code>();

            try
            {
                HtmlDocument hdoc = new HtmlDocument();
                hdoc.LoadHtml(body);
                HtmlNodeCollection nodes = hdoc.DocumentNode.SelectNodes("//div[@class='card card-resource']");
                foreach (var node in nodes)
                {
                    Code code = new Code();
                    code.Products = new List<string>();
                    code.Platform = new List<string>();
                    code.Title = node.SelectSingleNode(".//a").InnerText;
                    code.Description = node.SelectSingleNode(".//p[@class='sd-truncateText text-mini']").InnerText;
                    code.Link = (node.SelectSingleNode(".//a")).Attributes["href"].Value;
                    code.Author = node.SelectSingleNode(".//div[@class='meta']//span//a").InnerText;
                    code.SyncDate = DateTime.UtcNow;
                    code.GitHubUrl = HttpHelper.GetGitHubURL(code.Link);
                    var tempUpdate = (node.SelectSingleNode(".//div[@class='meta']//span").InnerText);
                    code.LastUpateDate = Convert.ToDateTime(tempUpdate.Substring(tempUpdate.IndexOf(":") + 1));
                    var products = node.SelectNodes(".//ul[@class='tags']//a[@class='service-label']");
                    if (products != null)
                    {
                        foreach (var product in products)
                        {
                            code.Products.Add(product.InnerText);
                        }
                    }
                    var platforms = node.SelectNodes(".//ul[@class='tags']//a[@class='platform-label']");
                    if (platforms != null)
                    {
                        foreach (var platform in platforms)
                        {
                            code.Platform.Add(platform.InnerText);
                        }
                    }
                    codes.Add(code);
                    //add sample code pull request
                    //sample.GitHubPullRequests = MooncakeTool.Common.GitHubDeveloper.GetGitHubPullEntity(sample.GitResourceUrl);
                    // sample.GitHubIssues= MooncakeTool.Common.GitHubDeveloper.GetGitHubIssuesEntity(sample.GitResourceUrl);
                   // sample.GitHubCommits = MooncakeTool.Common.GitHubDeveloper.GetGitHubCommitsEntity(sample.GitResourceUrl);
                }
                return codes;
            }
            catch (Exception e)
            {
                ErrorLog.WriteError(e.Message, "ConvertBodyToCode");
                throw;
            }
           
        
        }

    }
}