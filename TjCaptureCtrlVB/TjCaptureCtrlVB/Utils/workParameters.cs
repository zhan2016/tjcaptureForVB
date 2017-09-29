using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.Utils
{
    class workParameters
    {
        public Bitmap imagesource; 
        public string bitmapFileName;
        public string dicomFileName;
        public string videoName;
        public string dicomInfo;
        

        public workParameters(Bitmap imagesource, string bitmapfilename = "", string dicomfilename = "", string videoName="", string dicominfo = "")
        {
            this.imagesource = imagesource;
            this.bitmapFileName = bitmapfilename;
            this.dicomFileName = dicomfilename;
            this.videoName = videoName;
            this.dicomInfo = dicominfo;

        }
    }
}
