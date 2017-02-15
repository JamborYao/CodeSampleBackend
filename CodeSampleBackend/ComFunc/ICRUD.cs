using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public interface ICRUD
    {
        void Add<T>(T obj) where T : class;
        void AddOrUpdate<T>(T obj, Expression<Func<T, bool>> where, Dictionary<string, object> dic) where T : class;
        List<T> GetAll<T>() where T : class;       
        void Delete<T>(List<T> obj) where T : class;
        List<T> GetEntities<T>(Expression<Func<T, bool>> where) where T : class;
        void Update<T>(Expression<Func<T, bool>> where,Dictionary<string,object> dic) where T : class;
    }
}