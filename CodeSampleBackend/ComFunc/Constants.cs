using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CodeSampleBackend.ComFunc
{
    public class Constants
    {
        public static readonly string SampleCodeURL = $"{ConfigurationManager.AppSettings["SampleCodeURL"].ToString()}";
        public static readonly string SampleCodeDomain = ConfigurationManager.AppSettings["SampleCodeDomain"].ToString();
        public static readonly string LogFilePath = $"{HttpRuntime.AppDomainAppPath}error.txt";
        public static readonly string GitHubAccount = ConfigurationManager.AppSettings["GitHubAccount"].ToString();
        public static readonly string GitHubKey = ConfigurationManager.AppSettings["GitHubKey"].ToString();

    }
}