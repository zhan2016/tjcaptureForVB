using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB
{
    class LoggerUtil
    {
        public static void LoggerError(string s)
        {
            Trace.TraceError("-----Error: " + s);
            //Trace.WriteLine("-----Error: " + s);
            
        }

        public static void LoggerWarning(string s)
        {
            Trace.TraceWarning("warning: " + s);
           // Trace.WriteLine("warning: " + s);
        }

        public static void LoggerInfo(string s)
        {
            Trace.TraceWarning("Info:  " + s);
            //Trace.WriteLine("Info:  " + s);
        }
    }
}
