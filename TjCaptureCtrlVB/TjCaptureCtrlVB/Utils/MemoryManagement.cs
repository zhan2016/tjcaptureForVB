using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.Utils
{
    class MemoryManagement
    {
        private  static int width;
        private  static int height;
        private  static Bitmap PlayFrameFlush;
        private static  Bitmap PlayVideoFlush;  //对应播放窗口的视频内存
        public static bool IsClassInit = false;

        public static  void Init(int widthParameter, int heightParameter)
        {
            if (IsClassInit == false)
            {
                width = widthParameter;
                height = heightParameter;

                PlayFrameFlush = new Bitmap(widthParameter, heightParameter);
                PlayVideoFlush = new Bitmap(widthParameter, heightParameter);
                IsClassInit = true;
            }
            else
            {
                return;
            
            }



        }

       
        public static  Bitmap getPlayFrameFlush()
        {
            if (IsClassInit == true)
            {
                return PlayFrameFlush;
            }
            else
            {
                throw (new Exception());
            }
        }
        public  static Bitmap getPlayVideoFlush()
        {
            if (IsClassInit == true)
            {
                return PlayVideoFlush;
            }
            else
            {
                throw (new Exception());
            }
        }
    }
}
