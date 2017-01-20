﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALProcessLog
    {
        public static MoonCakeCodeSampleEntities context;
        public static string GetLatestProcess(int codeID,string type)
        {
            context = new MoonCakeCodeSampleEntities();
            var processLog = context.ProcessViews.Where(c => c.FkId == codeID&&c.Type==type).OrderByDescending(p => p.LogAT).FirstOrDefault();
            if (processLog != null)
            {
                return processLog.name;
            }
            else
            {
                return null;
            }
        }

        public static List<Process> GetAllProcess()
        {
            context = new MoonCakeCodeSampleEntities();
            return context.Processes.ToList<Process>();

        }
    }
}