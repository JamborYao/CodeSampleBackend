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
        }
    }
}