using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.Utils
{
    class KeyVal
    {
        public string Id { get; set; }
        public string  Text { get; set; }

        public KeyVal() { }

        public KeyVal(string key, string val)
        {
            this.Id = key;
            this.Text = val;
        }
    }
}
