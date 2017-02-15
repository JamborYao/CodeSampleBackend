using CodeSampleBackend.DAL;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class Basic
    {
        public static Dictionary<string, object> ToDictionary<T>(T list)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Type type = typeof(T);
            List<PropertyInfo> propertyList = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();

            foreach (PropertyInfo propertyInfo in propertyList)
            {
                if (propertyInfo.Name == "id") continue;
                dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(list, null));
            }

            return dictionary;
        } public static List<CommitView> ConvertCommitToCommitView(List<Commit> issues, BasicCRUD dal)
        {
            List<CommitView> views = new List<CommitView>();
            foreach (var item in issues)
            {
                CommitView view = new CommitView();
                view.id = item.id;

                view.CreateAt = item.CreateAt;
                view.Author = item.Author;
                view.CodeID = item.CodeID;
                view.GitHubUrl = item.GitHubUrl;
                view.id = item.id;
                view.IsNew = item.IsNew;
                view.Message = item.Message;
                var process = DALProcessLog.GetLatestProcess(item.id, "commit");
                view.Process = process;
                view.PSha = item.PSha;
                view.Sha = item.Sha;
                view.Type = item.Author;
                view.URL = item.URL;
                var uts = dal.GetEntities<UTLog>(c => c.FkId == item.id && c.Type == "commit");// context.UTLogs.Where(c => c.FkId == item.id && c.Type == "commit");
                int? utValue = 0;
                foreach (var ut in uts)
                {
                    utValue += ut.UT;
                }
                view.UT = utValue;
                var aliasEntity = dal.GetEntities<CodeOwnership>(c => c.FkId == item.id && c.Type == "commit").OrderByDescending(p => p.LogAt).FirstOrDefault();
                if (aliasEntity != null)
                {
                    view.Alias = aliasEntity.support_alias;
                }
                views.Add(view);
            }

            return views;
        }
    }
}