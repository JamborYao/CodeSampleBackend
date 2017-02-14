using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class Basic
    {
        /// <summary>
        /// if first <= second then return true else return false
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CompareDateTime(DateTime? createTime, DateTime? TakenTime)
        {
            if ((createTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Value.Seconds >= (TakenTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Value.Seconds)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}