using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALIssueStatusLog
    {
        public static MoonCakeCodeSampleEntities context;
        public static void AddIssueStatus(IssueStatusLog statusLog)
        {
            context = new MoonCakeCodeSampleEntities();

            var entity = context.IssueStatusLogs.Where(c => c.IssueID == statusLog.IssueID).FirstOrDefault();
            if (entity!=null)
            {
                entity.IssueStatusID = statusLog.IssueStatusID;
                entity.LogAt= DateTime.UtcNow;
                context.SaveChanges();
            }
            else
            {
                statusLog.LogAt = DateTime.UtcNow;
                context.IssueStatusLogs.Add(statusLog);
                context.SaveChanges();
            }
         
        }
    }
}