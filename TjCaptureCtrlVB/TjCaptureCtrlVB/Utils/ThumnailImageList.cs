using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TjCaptureCtrlVB.userControl;

namespace TjCaptureCtrlVB.Utils
{
    public class imageItems
    {
        public Bitmap imageitem;
        public bool isVideo;
        public string videoPath;
        public string saveFileName;
        public Bitmap ImageItem
        {
            get
            {
                return imageitem;
            }
            set
            {
                imageitem = value;
                Raise(ImageItemChanged, this);
            }
        }

        #region 事件处理
        private static void Raise(EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        public event EventHandler ImageItemChanged;
        #endregion
    }
    class ThumnailImageList
    {
        #region 初始化成员
        //缩略图列数
        private int colCount;
        //缩略图行数
        private int rowCount;
        //缩略图窗口picturebox的数量
        private int thumnailCount;
        //图像对象
        private List<imageItems> imageItemsList = new List<imageItems>();
        //当前浏览到的页数 第1页开始
        private int currentPage;
        //总的页数 总第1页开始
        private int totalPage;
        //空白的thumbnail图像
        public static imageItems defaultImageItems = new imageItems();
        //picturebox 对象列表
        public List<ThumbnailViewItem> thumbnailviewitemList;
        #endregion

        #region 初始化管理对象
        public ThumnailImageList(int colcount, int rowcount, List<ThumbnailViewItem> thumbnailviewitemList)
        {
            this.rowCount = rowcount;
            this.colCount = colcount;
            this.currentPage = 1;
            this.totalPage = 1;
            this.thumnailCount = rowcount * colcount;
            defaultImageItems.imageitem = null;
            defaultImageItems.isVideo = false;
            defaultImageItems.videoPath = "";
            this.thumbnailviewitemList = thumbnailviewitemList;

        }
        #endregion

        #region 添加一个图像对象，自动加载到浏览控件中
        public void addImageItem(Bitmap image, bool isVideo, string saveFileName, string videopath = "")
        {

            imageItems imageitems = new imageItems();
            imageitems.ImageItem = image;
            imageitems.isVideo = isVideo;
            imageitems.videoPath = videopath;
            imageitems.saveFileName = saveFileName;
            imageItemsList.Add(imageitems);
            int index = imageItemsList.Count; //获取新加入对象的index
            if ((index - 1) % thumnailCount == 0 && index > 1) //自动加页
            {
                resetThumnail();
                this.currentPage++;
                this.totalPage++;   
            }
            if ((index >= (currentPage - 1) * thumnailCount) &&
                (index <= currentPage * thumnailCount))
            {
                //在显示范围内 显示该图像
                int thumnailIndex = (index - 1) % thumnailCount;
                thumbnailviewitemList[thumnailIndex].setimage(imageitems);
            }


        }
        #endregion

        #region 复位list状态
        public void reset()
        {
            this.imageItemsList.Clear();
            this.currentPage = 1;
            this.totalPage = 1;
        }
        #endregion

        #region 获取当前缩略图的第i个元素 从第1个元素开始
        public imageItems this[int i]
        {
            get
            {
                int imageIndex = i + (this.currentPage - 1) * this.colCount * this.rowCount;

                if (imageIndex > this.imageItemsList.Count() || this.imageItemsList.Count == 0)
                {
                    return defaultImageItems;
                }
                else
                {
                    return this.imageItemsList[i - 1];
                }

            }
        }
        #endregion

        #region 清空浏览控件的图像
        private void resetThumnail()
        {
            if (thumbnailviewitemList == null)
            {
                return;
            }
            foreach (ThumbnailViewItem thumbnailitem in thumbnailviewitemList)
            {
                thumbnailitem.reset();
            }
        }

        #endregion

        #region 下一页切换
        public void nextPage()
        {
            if (this.totalPage == 1 || this.currentPage == this.totalPage)
            {
                return;
            }
            int startIndex = this.currentPage * this.thumnailCount;

            this.resetThumnail();
            int j = 0;
            for (int i = startIndex; i < imageItemsList.Count && i < (this.currentPage + 1) * this.thumnailCount; i++)
            {
                thumbnailviewitemList[j++].setimage(imageItemsList[i]);
            }

            currentPage++;
        }
        #endregion

        #region 上一页切换
        public void prevPage()
        {
            if (this.totalPage == 1 || this.currentPage == 1)
            {
                return;
            }
            int startIndex = (this.currentPage - 2) * this.thumnailCount;

            this.resetThumnail();
            int j = 0;
            for (int i = startIndex; i < imageItemsList.Count && i < (this.currentPage - 1) * this.thumnailCount; i++)
            {
                thumbnailviewitemList[j++].setimage(imageItemsList[i]);
            }

            currentPage--;
        }
        #endregion

        #region 上一张/下一张切换
        public imageItems getPrevImage(string CurrentImageID)
        {
            if (this.imageItemsList.Count <= 0)
            {
                return null;
            }

            int currentIndex = getIndexOfImage(CurrentImageID);
            if (currentIndex == -1 || currentIndex == 0)
            {
                return null;
            }
            else
            {
                return this.imageItemsList[currentIndex - 1];
            }
           
        }

        public imageItems getNextImage(string CurrentImageID)
        {
            if (this.imageItemsList.Count <= 0)
            {
                return null;
            }

            int currentIndex = getIndexOfImage(CurrentImageID);
            if (currentIndex == -1 || currentIndex == this.imageItemsList.Count - 1)
            {
                return null;
            }
            else
            {
                return this.imageItemsList[currentIndex + 1];
            }

        }
        #endregion

        #region 获得某个图像在list中的索引
        private int getIndexOfImage(string imageID)
        {
            int currentIndex = 0;
            bool isInList = false;
            foreach (imageItems item in this.imageItemsList)
            {
                if (item.saveFileName == imageID)
                {
                    isInList = true;
                    break;
                }
                currentIndex++;
            }

            if (isInList == true)
            {
                return currentIndex;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region 更新某个缩略图的图像
        public void refereshThumnail(string saveFileName, Bitmap newframe)
        {
            foreach (imageItems item in this.imageItemsList)
            {
                if (item.saveFileName == saveFileName)
                {
                    item.imageitem.Dispose();
                    item.imageitem = new Bitmap(newframe);
                    foreach (ThumbnailViewItem thumitem in this.thumbnailviewitemList)
                    {
                        if (thumitem.SaveFilePath == saveFileName)
                        {
                            thumitem.setimage(item);
                        }
                    }
                }
            }
        }
        #endregion


    }
}
