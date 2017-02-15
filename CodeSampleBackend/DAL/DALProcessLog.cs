using CodeSampleBackend;
using CodeSampleBackend.ComFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALProcessLog: BasicCRUD
    {
        public static MoonCakeCodeSampleEntities context;
        public static string GetLatestProcess(int codeID,string type)
        {
            context = new MoonCakeCodeSampleEntities();
            var processLog = context.ProcessViews.Where(c => c.FkId == codeID&&c.Type==type).OrderByDescending(p => p.LogAT).FirstOrDefault();
            if (processLog != null)
            {
                return processLog.name;
            }
            else
            {
                return null;
            }
        }

        public static string GetLatestIssueProcess(int issueId)
        {
            context = new MoonCakeCodeSampleEntities();
            var processLog = context.IssueStatusLogs.Where(c => c.IssueID == issueId).OrderByDescending(p => p.LogAt).FirstOrDefault();
            if (processLog != null)
            {
                return context.IssueStatus.Where(c=>c.id== processLog.IssueStatusID).First().name;
            }
            else
            {
                return null;
            }
        }

      

        public static string GetIssueIdByName(int id)
        {
            context = new MoonCakeCodeSampleEntities();
            return context.IssueStatus.Where(c => c.id == id).First().name;
        }

        public static List<IssueStatu> GetAllIssueStatus()
        {
            context = new MoonCakeCodeSampleEntities();
            return context.IssueStatus.ToList<IssueStatu>();
        }
      
    }
}