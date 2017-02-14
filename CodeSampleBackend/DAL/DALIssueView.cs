using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALIssueView
    {
        public static MoonCakeCodeSampleEntities context;
        public static List<IssueView> GetNewIssueView(int codeID, DateTime? takeTime)
        {
            context = new MoonCakeCodeSampleEntities();
            return context.IssueViews.Where(c => c.CodeID == codeID && c.CreateAt >= takeTime).ToList<IssueView>();
        }
    }
}