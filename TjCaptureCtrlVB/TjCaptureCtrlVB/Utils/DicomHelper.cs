using Dicom;
using Dicom.Imaging;
using Dicom.IO.Buffer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TjCaptureCtrlVB.Utils;

namespace TjCaptureCtrlVB
{
    /// <summary>
    /// this is a tool to convert file between dicom and other file.
    /// </summary>
    class DicomHelper
    {
        #region bitmap转dicom测试函数
        /// <summary>
        /// convert bitmap to dicom from bitmap file
        /// </summary>
        /// <param name="sourceFileName">source file </param>
        public static void ImportImage(string sourceFileName,string destinationFileName)
        {
            if (!File.Exists(sourceFileName))
            {
                return;
            }

            Bitmap bitmap = new Bitmap(sourceFileName);
            bitmap = GetValidImage(bitmap);
            int rows, columns;
            byte[] pixels = GetPixels(bitmap, out rows, out columns);
            MemoryByteBuffer buffer = new MemoryByteBuffer(pixels);
            DicomDataset dataset = new DicomDataset();
            FillDataset(dataset,"");
            dataset.Add(DicomTag.PhotometricInterpretation, PhotometricInterpretation.Rgb.Value);
            dataset.Add(DicomTag.Rows, (ushort)rows);
            dataset.Add(DicomTag.Columns, (ushort)columns);
            DicomPixelData pixelData = DicomPixelData.Create(dataset, true);
            pixelData.BitsStored = 8;
            pixelData.BitsAllocated = 8;
            pixelData.SamplesPerPixel = 3;
            pixelData.HighBit = 7;
            pixelData.PixelRepresentation = 0;
            pixelData.PlanarConfiguration = 0;
            pixelData.AddFrame(buffer);

            DicomFile dicomfile = new DicomFile(dataset);
            dicomfile.Save(destinationFileName);
            bitmap.Dispose();
            bitmap = null;
        }
        #endregion

        #region bitmap转dicom，附带有头信息
        public static void ImportImage(Bitmap imageSource, string destinationFileName, string dicominfo)
        {
            if (destinationFileName == null || destinationFileName == "")
            {
                return;
            }
            Bitmap bitmap = imageSource;
            bitmap = GetValidImage(bitmap);
            int rows, columns;
            byte[] pixels = GetPixels(bitmap, out rows, out columns);
            MemoryByteBuffer buffer = new MemoryByteBuffer(pixels);
            DicomDataset dataset = new DicomDataset();
            FillDataset(dataset, dicominfo);
            dataset.Add(DicomTag.PhotometricInterpretation, PhotometricInterpretation.Rgb.Value);
            dataset.Add(DicomTag.Rows, (ushort)rows);
            dataset.Add(DicomTag.Columns, (ushort)columns);
            DicomPixelData pixelData = DicomPixelData.Create(dataset, true);
            pixelData.BitsStored = 8;
            pixelData.BitsAllocated = 8;
            pixelData.SamplesPerPixel = 3;
            pixelData.HighBit = 7;
            pixelData.PixelRepresentation = 0;
            pixelData.PlanarConfiguration = 0;
            pixelData.AddFrame(buffer);
           // pixelData.AddFrame(buffer);

            DicomFile dicomfile = new DicomFile(dataset);
            dicomfile.Save(destinationFileName);
            bitmap.Dispose();
            bitmap = null;
        }
        #endregion

        #region 导出视频为dicom文件
        public static void importMutilFrame(string sourceFileName, string destinationFileName, string dicominfo)
        {
            if (!File.Exists(sourceFileName))
            {
                return;
            }
            VideoStreamManager videomanager = new VideoStreamManager(sourceFileName);
            videomanager.readOpen();


            Bitmap bitmap = new Bitmap(videomanager.readTheFirstbitmap());
            bitmap = GetValidImage(bitmap);
            int rows, columns;
            byte[] pixels = GetPixels(bitmap, out rows, out columns);
            MemoryByteBuffer buffer = new MemoryByteBuffer(pixels);
            DicomDataset dataset = new DicomDataset();
            FillDataset(dataset, dicominfo);
            dataset.Add(DicomTag.PhotometricInterpretation, PhotometricInterpretation.Rgb.Value);
            dataset.Add(DicomTag.Rows, (ushort)rows);
            dataset.Add(DicomTag.Columns, (ushort)columns);
            DicomPixelData pixelData = DicomPixelData.Create(dataset, true);
            pixelData.BitsStored = 8;
            pixelData.BitsAllocated = 8;
            pixelData.SamplesPerPixel = 3;
            pixelData.HighBit = 7;
            pixelData.PixelRepresentation = 0;
            pixelData.PlanarConfiguration = 0;
            //pixelData.NumberOfFrames = 2; // Don't set the number of Frames. This will generate a bug! zwj 20170810
            pixelData.AddFrame(buffer);
            //pixelData.AddFrame(buffer);
            bitmap.Dispose();

            //add mutil frame continul
            
            while(videomanager.hasNextFrame())
            {
                Bitmap newFrame = new Bitmap(videomanager.Next());
                newFrame = GetValidImage(newFrame);
                pixels = GetPixels(newFrame, out rows, out columns);
                buffer = new MemoryByteBuffer(pixels);
                pixelData.AddFrame(buffer);
                newFrame.Dispose();
            }

            DicomFile dicomfile = new DicomFile(dataset);
            dicomfile.Save(destinationFileName);
            videomanager.closeStream();
        }
        #endregion

        #region dicom文件转bitmap
        public static Bitmap dcm2bmp(string dicomfile)
        {
            if (dicomfile == null || dicomfile == "")
            {
                LoggerUtil.LoggerError("未找到dicom文件:" + dicomfile);
                return null;
            }

            Bitmap bmp;
            try
            {
                var image = new DicomImage(dicomfile);
                bmp = new Bitmap(image.RenderImage());
                return bmp;
                //bmp.Save("mybmptest.bmp");
            }
            catch(Exception e)
            {
                LoggerUtil.LoggerError(e.Message);
                return null;
            }

            
        }
        #endregion

        #region  以指定的头信息填充dicomDataset

        private static void FillDataset(DicomDataset dataset, string dicomInfo)
        {
            DicomInfoItems dicominfoItems = new DicomInfoItems();
            XMLParse.parseDicomItem(dicomInfo, ref dicominfoItems);
            //type 1 attributes.
            dataset.Add(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage);
            dataset.Add(DicomTag.StudyInstanceUID, GenerateUid());
            dataset.Add(DicomTag.SeriesInstanceUID, GenerateUid());
            dataset.Add(DicomTag.SOPInstanceUID, GenerateUid());
            dataset.Add(DicomTag.PatientID, dicominfoItems.getValue("PID"),"");

            //support for chinese character(this can solve all dicom encoding problems)
            DicomItem di = new DicomShortText(DicomTag.PatientName, DicomEncoding.GetEncoding("GB18030"), dicominfoItems.getValue("NAME"));
            dataset.Add(di);

            dataset.Add(DicomTag.PatientBirthDate, dicominfoItems.getValue("BIRTHDAY"));
            di = new DicomShortText(DicomTag.PatientSex, DicomEncoding.GetEncoding("GB18030"), dicominfoItems.getValue("SEX"));
            dataset.Add(di);
            dataset.Add(DicomTag.StudyDate, dicominfoItems.getValue("EXAM_DATE"));
            dataset.Add(DicomTag.StudyTime, dicominfoItems.getValue("EXAM_TIME"));
            dataset.Add(DicomTag.AccessionNumber, dicominfoItems.getValue("ACCESSION_NUMBER"));
            dataset.Add(DicomTag.ReferringPhysicianName, string.Empty);
            dataset.Add(DicomTag.StudyID, dicominfoItems.getValue("STUDY_ID"));
            //dataset.Add(DicomTag.SeriesNumber, "1");
            //dataset.Add(DicomTag.ModalitiesInStudy, "CR");
            dataset.Add(DicomTag.Modality, dicominfoItems.getValue("MODALITY"));
            dataset.Add(DicomTag.NumberOfStudyRelatedInstances, "1");
            dataset.Add(DicomTag.NumberOfStudyRelatedSeries, "1");
            dataset.Add(DicomTag.NumberOfSeriesRelatedInstances, "1");
            dataset.Add(DicomTag.PatientOrientation, "F/A");
            dataset.Add(DicomTag.ImageLaterality, "U");
            dataset.Add(DicomTag.DeviceID, dicominfoItems.getValue("DEVICE"));

        }
        #endregion

        #region  生成唯一标识
        private static DicomUID GenerateUid()
        {
            StringBuilder uid = new StringBuilder();
            uid.Append("1.08.1982.10121984.2.0.07").Append('.').Append(DateTime.UtcNow.Ticks);
            return new DicomUID(uid.ToString(), "SOP Instance UID", DicomUidType.SOPInstance);
        }
        #endregion

        #region bitmap图像的像素进行必要的处理
        private static Bitmap GetValidImage(Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
            {
                Bitmap old = bitmap;
                using (old)
                {
                    bitmap = new Bitmap(old.Width, old.Height, PixelFormat.Format24bppRgb);
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(old, 0, 0, old.Width, old.Height);
                    }
                }
            }
            return bitmap;
        }
        private static byte[] GetPixels(Bitmap image, out int rows, out int columns)
        {
            rows = image.Height;
            columns = image.Width;

            if (rows % 2 != 0 && columns % 2 != 0)
                --columns;

            BitmapData data = image.LockBits(new Rectangle(0, 0, columns, rows), ImageLockMode.ReadOnly, image.PixelFormat);
            IntPtr bmpData = data.Scan0;
            try
            {
                int stride = columns * 3;
                int size = rows * stride;
                byte[] pixelData = new byte[size];
                for (int i = 0; i < rows; ++i)
                    Marshal.Copy(new IntPtr(bmpData.ToInt64() + i * data.Stride), pixelData, i * stride, stride);

                //swap BGR to RGB
                SwapRedBlue(pixelData);
                return pixelData;
            }
            finally
            {
                image.UnlockBits(data);
            }
        }
        private static void SwapRedBlue(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 3)
            {
                byte temp = pixels[i];
                pixels[i] = pixels[i + 2];
                pixels[i + 2] = temp;
            }
        }
        #endregion
    }
}
