using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALGenerateView
    {
        public static PageCodeView GetCodeView(List<Code> codes,int total)
        {
            PageCodeView pageview = new PageCodeView();
            BasicCRUD dal = new BasicCRUD();
            try
            {
                List<CodeView> views = new List<CodeView>();
                pageview.Total = total;
                CodeView view = new CodeView();
                foreach (var item in codes)

                {
                    view = new CodeView();
                    view.ID = item.id;
                    view.Author = item.Author;
                    view.Description = item.Description;
                    view.GitHubUrl = item.GitHubUrl;
                    view.LastUpateDate = item.LastUpdateDate;
                    view.Link = item.Link;
                    var takeTimeEntity = dal.GetEntities<CodeOwnership>(c => c.FkId == item.id && c.Type == "code").OrderByDescending(c => c.LogAt).FirstOrDefault();
                    if (takeTimeEntity != null)
                    {
                        var takeTime = takeTimeEntity.LogAt;
                        view.NewCommit = Basic.ConvertCommitToCommitView(dal.GetEntities<Commit>(c => c.GitHubUrl == item.GitHubUrl && c.CreateAt >= takeTime), dal);
                        var newissues = dal.GetEntities<Issue>(c => c.CodeID == item.id && c.CreateAt >= takeTime);
                        view.NewIssue = Basic.ConvertIssueToIssueView(newissues, dal,newissues.Count());
                    }
                    int? ut = 0;
                    foreach (var c in dal.GetEntities<UTLog>(c => c.Type == "code" && c.FkId == item.id))
                    {
                        ut += c.UT;
                    }
                    view.UT =ut ;
                    view.Platforms = Basic.stringToList(item.Platform);
                    view.Products = Basic.stringToList(item.Products);
                    view.SyncDate = item.SyncDate;
                    view.Title = item.Title;
                    var alias = dal.GetEntities<CodeOwnership>(c => c.FkId == item.id && c.Type == "code").ToList().FirstOrDefault();
                    view.Alias = alias != null ? alias.support_alias : null;

                    var process = dal.GetEntities<ProcessLog>(c => c.FkId == item.id && c.Type == "code").OrderByDescending(c => c.LogAT).FirstOrDefault();
                    if (process != null)
                    {
                        view.Process = dal.GetEntities<Process>(c => c.id == process.ProcessID).First().name;
                    }
                    else
                    {
                        view.Process = "Pending";
                    }

                    views.Add(view);
                }
                pageview.Views = views;
            }
            catch (Exception)
            {

                throw;
            }
           

           return pageview;
        }
    
    }
}