using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.Utils
{
    class LockFlag
    {
        public static object lockflag = new object();
        public static object AddToTablePanelLockFlag = new object();
        public static object lockFrame = new Object();
    }
}
