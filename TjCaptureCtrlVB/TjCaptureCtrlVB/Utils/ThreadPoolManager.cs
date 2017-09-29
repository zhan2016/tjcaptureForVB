using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace TjCaptureCtrlVB.Utils
{
    class ThreadPoolManager
    {
        private static bool IsInit = false;
        public  static void initPool()
        {
            if (IsInit == false)
            {
                ThreadPool.SetMaxThreads(5, 5);
                IsInit = true;
            }
            else
            {
                return;
            }
            
        }
        
        public static void saveBitmapWork(Bitmap imagesource, string filename)
        {
            initPool();
            workParameters workparameter = new workParameters(imagesource, filename);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveFile.saveBitmap), workparameter);
        }

        public static void saveDicomWork(Bitmap imagesource, string filename,string dicomInfo)
        {
            initPool();
            workParameters workparameter = new workParameters(imagesource, "",filename,"",dicomInfo);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveFile.saveDicom), workparameter);
        }

        public static void saveFrameWork(Bitmap imagesource, string videofilename, string filename)
        {
            initPool();
            workParameters workparameter = new workParameters(imagesource, "", filename, videofilename);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveFile.saveDicomFrame), workparameter);
        }

        public static void saveFile(Bitmap imagesource, string bitmapfilename, string dicomfilename,string dicomInfo)
        {
            initPool();
            workParameters workparameter = new workParameters(imagesource, bitmapfilename, dicomfilename,"", dicomInfo);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveFile.saveFile), workparameter);
        }
    }
}
