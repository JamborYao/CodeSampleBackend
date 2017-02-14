using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
  
    public class DALUT
    {
        public static MoonCakeCodeSampleEntities context;
        public static List<UTLog> GetUT(int id,string type)
        {
            context = new MoonCakeCodeSampleEntities();
            List<UTLog> uts = new List<UTLog>();
            var entities = context.UTLogs.Where(c => c.FkId == id&&c.Type==type);
            if (entities != null)
            {
                uts= entities.ToList<UTLog>();
            }
            return uts;
             
        }

        public static void AddUT(UTLog ut)
        {
            context = new MoonCakeCodeSampleEntities();
            ut.LogAt = DateTime.UtcNow;
            context.UTLogs.Add(ut);
            context.SaveChanges();
        }
    }
}