using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.Utils
{
    class DicomInfoItems
    {
        private List<KeyVal> dicomInfoItems = new List<KeyVal>();

        public DicomInfoItems()
        {
            dicomInfoItems.Add(new KeyVal("RIS_NO", ""));
            dicomInfoItems.Add(new KeyVal("PID", ""));
            dicomInfoItems.Add(new KeyVal("NAME", ""));
            dicomInfoItems.Add(new KeyVal("AGE", ""));
            dicomInfoItems.Add(new KeyVal("SEX", ""));
            dicomInfoItems.Add(new KeyVal("BIRTHDAY", ""));
            dicomInfoItems.Add(new KeyVal("PATIENT_LOCAL_ID", ""));
            dicomInfoItems.Add(new KeyVal("EXAM_SUB_CLASS", ""));
            dicomInfoItems.Add(new KeyVal("CLIN_SYMP", ""));
            dicomInfoItems.Add(new KeyVal("MODALITY", "US"));
            dicomInfoItems.Add(new KeyVal("EXAM_DATE", ""));
            dicomInfoItems.Add(new KeyVal("EXAM_TIME", ""));
            dicomInfoItems.Add(new KeyVal("DEVICE", ""));
            dicomInfoItems.Add(new KeyVal("STUDY_ID", ""));
            dicomInfoItems.Add(new KeyVal("ACCESSION_NUMBER", ""));
        }

        public void addItem(string key, string value)
        {
            KeyVal item = new KeyVal(key, value);
            if (!dicomInfoItems.Contains(item))
            {
                dicomInfoItems.Add(item);
            }
            else
            {
                //int index = dicomInfoItems.IndexOf(item);
                //dicomInfoItems[index] = item;
                return;
            }
        }

        public void removeItem(string key)
        {
            foreach (KeyVal item in dicomInfoItems)
            {
                if (item.Id.ToLower() == key.ToLower())
                {
                    dicomInfoItems.Remove(item);
                }
            }
        }

        public List<KeyVal> getList()
        {
            return dicomInfoItems;
        }

        public string getValue(string key)
        {
            foreach (KeyVal item in dicomInfoItems)
            {
                if (item.Id.ToLower() == key.ToLower())
                {
                    return item.Text;
                }
            }
            return "";
        }



    }
}
