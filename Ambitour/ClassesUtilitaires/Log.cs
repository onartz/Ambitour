using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ambitour
{
    public static class Log
    {
        public static void Write(string Message)
        {
           StreamWriter sw= File.AppendText("c:\\ambitour\\ambitour.log");
           TextWriter tw = (TextWriter)sw;
            tw.WriteLine(DateTime.Now.ToShortTimeString());
            tw.WriteLine(Message);
            tw.Write(tw.NewLine);
            tw.Close();
        }
    }
}
