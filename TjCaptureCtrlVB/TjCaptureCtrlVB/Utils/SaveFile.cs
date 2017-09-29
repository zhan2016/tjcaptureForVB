using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TjCaptureCtrlVB.Utils;

namespace TjCaptureCtrlVB
{
    /*
     * this class is a tool to persist the bitmap to local file and write record to access DBA.
     * 
     * 
     * */
    class SaveFile
    {
        #region 仅保存bitmap文件
        public static void saveBitmap(object parameter)
        {
            workParameters workparameter = (workParameters)parameter;
            using (Bitmap sourcebitmap = new Bitmap(workparameter.imagesource))
            {
                string filename = (string)workparameter.bitmapFileName;

                if (sourcebitmap == null)
                {
                    return;
                }


                sourcebitmap.Save(filename);
                sourcebitmap.Dispose();
                workparameter.imagesource.Dispose();
                System.Threading.Thread.Sleep(100);
            }
        }
        #endregion

        #region 仅保存dicom文件
        public static void saveDicom(object parameter)
        {
            workParameters workparameter = (workParameters)parameter;
            using (Bitmap sourcebitmap = new Bitmap(workparameter.imagesource))
            {
                string filename = (string)workparameter.dicomFileName;
                if (sourcebitmap == null)
                {
                    return;
                }


                DicomHelper.ImportImage(sourcebitmap, filename, workparameter.dicomInfo);
                workparameter.imagesource.Dispose();
            }
        }

        #endregion

        #region 同时保存dicom和bitmap

        public static void saveFile(object parameter)
        {

            workParameters workparameter = (workParameters)parameter;
            using (Bitmap sourcebitmap = new Bitmap(workparameter.imagesource))
            {
                string filename = (string)workparameter.bitmapFileName;
                string dicomfilename = (string)workparameter.dicomFileName;
                if (sourcebitmap == null)
                {
                    return;
                }

                sourcebitmap.Save(filename);
                DicomHelper.ImportImage(sourcebitmap, dicomfilename, workparameter.dicomInfo);
                sourcebitmap.Dispose();
                workparameter.imagesource.Dispose();
            }

        }
        #endregion

        #region  保存视频为dicom文件
        public static void saveDicomFrame(object parameter)
        {
            workParameters workparameter = (workParameters)parameter;
            if (!File.Exists(workparameter.videoName))
            {
                return;
            }
            DicomHelper.importMutilFrame(workparameter.videoName, workparameter.dicomFileName, workparameter.dicomInfo);
        }
        #endregion
    }
}
