using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.Utils
{
    class MyTraceListener :TraceListener
    {
        public override void Write(string message)
        {
            File.AppendAllText("c:\\1.log", message);
        }

        public override void WriteLine(string message)
        {
            File.AppendAllText("c:\\1.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + message + Environment.NewLine);
        }
    }
}
