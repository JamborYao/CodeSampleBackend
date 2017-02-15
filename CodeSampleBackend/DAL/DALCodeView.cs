using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALCodeView
    {
        public static PageCodeView GetCodeView(List<Code> codes,int page,int limit)
        {
            BasicCRUD dal = new BasicCRUD();
            PageCodeView pageview = new PageCodeView();
            pageview.Total = codes.Count();
            List<CodeView> views = new List<CodeView>();
            if (page == -1)
            {
                page = 1;limit = codes.Count();
            }
            CodeView view = new CodeView();
            foreach (var item in codes.Skip((page-1)*limit).Take(limit))
            {
                view = new CodeView();
                view.ID = item.id;
                view.Author = item.Author;
                view.Description = item.Description;
                view.GitHubUrl = item.GitHubUrl;
                view.LastUpateDate = item.LastUpdateDate;
                view.Link = item.Link;
                var takeTime = DALCodeOwner.GetTakenTime(item.id, "code");
                view.NewCommit =Basic.ConvertCommitToCommitView(dal.GetEntities<Commit>(c => c.GitHubUrl == item.GitHubUrl && c.CreateAt >= takeTime),dal);// ( DAL.DALCommit.GetNewCommitsView(item.GitHubUrl,DALCodeOwner.GetTakenTime(item.id,"code"));
                view.NewIssue = DAL.DALIssueView.GetNewIssueView(item.id, DALCodeOwner.GetTakenTime(item.id, "code"));
                view.Platforms = ConvertPlatformProductToList(item.Platform);
                view.Products = ConvertPlatformProductToList(item.Products);
                view.SyncDate = item.SyncDate;
                view.Title = item.Title;
                view.Alias = DAL.DALCodeOwner.GetAlias(item.id,"Code");
                view.Process = DAL.DALProcessLog.GetLatestProcess(item.id,"Code");
                views.Add(view);
            }
            pageview.Views = views;

           return pageview;
        }
        public static List<string> ConvertPlatformProductToList(string platform)
        {
            
            if (platform == null) return null;
            return platform.Split(';').ToList();
            //string[] platforms = platform.Split(';').ToList();

            //List<string> plat = new List<string>();
            //for (int i = 0; i < platforms.Length; i++)
            //{
            //    if (platforms[i].ToString() != "")
            //    {
            //        plat.Add(platforms[i]);
            //    }

            //}
            //return plat;
        }
    }
}