using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALCodeOwner
    {
        public static MoonCakeCodeSampleEntities context;
        public static string GetAliasByCode(int id,string type)
        {
            context = new MoonCakeCodeSampleEntities();
            var ownerEntity = context.CodeOwnerships.Where(c => c.FkId == id&&c.Type==type).OrderByDescending(p=>p.LogAt).FirstOrDefault();
            if (ownerEntity != null)
            {
                return ownerEntity.support_alias;
            }
            else {
                return null;
            }
        }

        public static void AddCodeOwner(CodeOwnership codeowner)
        {
            codeowner.LogAt = DateTime.UtcNow;
            context = new MoonCakeCodeSampleEntities();
            context.CodeOwnerships.Add(codeowner);
        }

    }
}