using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TjCaptureCtrlVB.Utils;

namespace TjCaptureCtrlVB.Common
{
    class GlobalAttribute
    {
        #region dicom信息公共变量
        /// <summary>
        /// 原始dicom信息串
        /// </summary>
        private static string rawDicomInfo;
        private static DicomInfoItems dicominfoItems = new DicomInfoItems();

        public  static void setDicomInfo(string dicominfoParameter)
        {
            rawDicomInfo = dicominfoParameter;
            XMLParse.parseDicomItem(dicominfoParameter, ref dicominfoItems);
        }

        public static string getValue(string key)
        {
            return dicominfoItems.getValue(key);
        }

        public static string getPatientInfo()
        {
            if (rawDicomInfo == null || rawDicomInfo == "")
            {
                return "";
            }
            StringBuilder patientinfo = new StringBuilder();
            patientinfo.Append("检查号: ");
            patientinfo.Append(getValue("RIS_NO"));
            patientinfo.Append("  ");
            patientinfo.Append(getValue("NAME"));
            patientinfo.Append("  ");
            patientinfo.Append(getValue("SEX"));
            patientinfo.Append("  ");
            patientinfo.Append("检查项目: ");
            patientinfo.Append(getValue("EXAM_SUB_CLASS"));
            return patientinfo.ToString();
        }
        #endregion

        #region 缩略图窗口布局变量
        private static string layoutInfo = "1,1";
        public static string LayOutInfo
        {
            get
            {
                return layoutInfo;
            }
            set
            {
                layoutInfo = value;
            }
        }

        public static int[] getLayOutInfo()
        {
            int[] layoutinfoValue = new int[2];
            string[] layoutinfoStr = layoutInfo.Split(',');
            layoutinfoValue[0] = int.Parse(layoutinfoStr[0]);
            layoutinfoValue[1] = int.Parse(layoutinfoStr[1]);

            return layoutinfoValue;
        }
        #endregion

        #region 控件的位置初始化，字符串的格式统一按照 左，上，宽，高来指定
        private static string captureCtrlPos = "0,0,100,100";
        private static string thumbnailCtrlPos = "0,0,800,800";

        public static string CaptureCtrlPos
        {
            get
            {
                return captureCtrlPos;
            }
            set
            {
                captureCtrlPos = value;
            }
        }

        public static string ThumbnailCtrlPos
        {
            get
            {
                return thumbnailCtrlPos;
            }
            set
            {
                thumbnailCtrlPos = value;
            }
        }

        public static int[] getCaptureCtrlPos()
        {
            string[] posStr = captureCtrlPos.Split(',');
            int[] pos = new int[4];
            pos[0] = int.Parse(posStr[0]);
            pos[1] = int.Parse(posStr[1]);
            pos[2] = int.Parse(posStr[2]);
            pos[3] = int.Parse(posStr[3]);

            return pos;
        }

        public static int[] getThumbnailCtrlPos()
        {
            string[] posStr = ThumbnailCtrlPos.Split(',');
            int[] pos = new int[4];
            pos[0] = int.Parse(posStr[0]);
            pos[1] = int.Parse(posStr[1]);
            pos[2] = int.Parse(posStr[2]);
            pos[3] = int.Parse(posStr[3]);

            return pos;
        }
        #endregion

    }
}
