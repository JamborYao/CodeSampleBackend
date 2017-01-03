using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class Constants
    {
        public const string SampleCodeURL = "https://azure.microsoft.com/en-us/resources/samples/samplesapi/?term=&sortType=0&service=&platform=&pageNumber={0}";
        public const string SampleCodeDomain = "https://azure.microsoft.com";
        public static readonly string LogFilePath = $"{HttpRuntime.AppDomainAppPath}error.txt";
        public const int LatestNumber = 5; //get the latest commits/issues/pull requests, only get 5 at default

    }
}