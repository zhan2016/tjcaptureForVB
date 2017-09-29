using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB
{
    interface DrawVisual
    {
        void setGraphics(System.Drawing.Graphics g);
        void render();
    }
}
