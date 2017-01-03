using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class ErrorLog
    {
        public static void WriteError(string errorMessage,string method)
        {
            try
            {
                string path = Constants.LogFilePath;
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("\r\nLog Entry : "+ method);
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    w.WriteLine(errorMessage);
                    w.WriteLine("________________________________________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message, "WriteError");
            }
        }
    }
}