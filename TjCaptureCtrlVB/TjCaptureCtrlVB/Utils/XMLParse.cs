using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TjCaptureCtrlVB.Utils
{
    class XMLParse
    {
        public static void parseDicomItem(string xmlstr, ref DicomInfoItems dicominfo)
        {
            if (xmlstr == null || xmlstr == "")
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlstr);
            //XmlElement nodes = xmlDoc.DocumentElement;
            int itemIndex = 0;
            foreach (KeyVal item in dicominfo.getList())
            {
                XmlNode tempNode = xmlDoc.SelectSingleNode("//INFO/" + item.Id);
                if (tempNode.HasChildNodes && tempNode.FirstChild.InnerText != null)
                {
                    (dicominfo.getList())[itemIndex].Text = tempNode.FirstChild.InnerText;
                }
                itemIndex++;
            }
        }
    }
}
