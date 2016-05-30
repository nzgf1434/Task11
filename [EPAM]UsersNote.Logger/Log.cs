using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UsersNote.Logger
{
    public static class Log
    {
        public static void ToLog(string s, Exception e)
        {
            using (StreamWriter str = File.AppendText(s))
            {
                str.WriteLine("{0}-{1}-{2}", e.Message, e.Source, e.TargetSite.Name);
            }
        }
    }
}
