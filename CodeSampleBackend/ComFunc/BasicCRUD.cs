using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;

namespace CodeSampleBackend.ComFunc
{
    public class BasicCRUD : ICRUD 
    {
      
        public BasicCRUD() {
            context = new MoonCakeCodeSampleEntities();
        }
        public MoonCakeCodeSampleEntities context { get; set; }
        public virtual void Add<T>(T obj) where T : class
        {
            this.context.Entry<T>(obj).State = EntityState.Added;
            context.SaveChanges();
        }
        public virtual void AddOrUpdate<T>(T obj, Expression<Func<T, bool>> where, Dictionary<string, object> dic) where T : class
        {
            IEnumerable<T> result = context.Set<T>().Where(where).ToList();
            if (result.Count() != 0)
            {
                Update<T>(where, dic);
            }
            else
            {
                Add<T>(obj);
            }
            context.SaveChanges();
        }
        public virtual void Delete<T>(List<T> objs) where T : class
        {
            foreach (var item in objs)
            {
                context.Entry<T>(item).State = EntityState.Deleted;
            }
           
            context.SaveChanges();
        }

        public virtual List<T> GetAll<T>() where T : class
        {
           var entities = context.Set<T>().ToList();
            return entities;
        }
        public virtual List<T> GetEntities<T>(Expression<Func<T,bool>> where) where T : class
        {
            return context.Set<T>().Where(where).ToList();
           
        }

        public void Update<T>(Expression<Func<T, bool>> where, Dictionary<string, object> dic) where T : class
        {
            IEnumerable<T> result = context.Set<T>().Where(where).ToList();
            Type type = typeof(T);
            List<PropertyInfo> propertyList = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();
            foreach (T entity in result)
            {
                foreach (PropertyInfo propertyInfo in propertyList)
                {
                    string propertyName = propertyInfo.Name;
                   
                    if (dic.ContainsKey(propertyName))
                    {
                        propertyInfo.SetValue(entity, dic[propertyName], null);
                    }
                }
            }
            context.SaveChanges();
        }

      
    }
}