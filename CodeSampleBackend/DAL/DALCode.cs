using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALCode
    {
        public static List<Code> GetAllCode()
        {
            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();
            return context.Codes.ToList<Code>();
        }
    }
}