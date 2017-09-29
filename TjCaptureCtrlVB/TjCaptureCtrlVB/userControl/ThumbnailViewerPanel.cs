using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TjCaptureCtrlVB.Utils;
using TjCaptureCtrlVB.Common;

namespace TjCaptureCtrlVB.userControl
{
    public partial class ThumbnailViewerPanel : UserControl
    {
        #region 初始化成员变量
        //AutoSizeCtrlClass asc = new AutoSizeCtrlClass();
        private ThumnailImageList thumnailImageList;
        private Button prev_btn;
        private Button Next_btn;
        private Panel container;
        private List<ThumbnailViewItem> thumnailPictureBoxList = new List<ThumbnailViewItem>();
        #endregion

        #region 构造函数
        public ThumbnailViewerPanel(int left, int top, int width, int height)
        {
            InitializeComponent();

        }

   

        #endregion

        #region 真正的初始化
        private void initThumnailPanel()
        {
            this.container = new Panel();
            this.container.Left = 0;
            this.container.Top = 0;
            this.container.Width = this.Width;
            this.container.Height = this.Height;
            //this.container.Dock = DockStyle.Fill;
            this.Controls.Add(this.container);
            //Panel skinPanel = this.container;

            int[] layoutinfo = GlobalAttribute.getLayOutInfo();

            int layoutRow = layoutinfo[0];
            int layoutCol = layoutinfo[1];
            //this.thumnailPictureBoxList.Clear();
            int btn_width = this.container.Width / 9; //width is 1/5 of height
            int btn_height = this.container.Height;
            int item_width = (this.container.Width - btn_width * 2) * 9 / (10 * layoutRow + 1);
            int item_height = (this.container.Height) * 9 / (10 * layoutCol + 1);
            int row_interval = item_width / 9;
            int col_interval = item_height / 9;

            //add prevPage button
            this.Next_btn = new Button();
            Next_btn.BackColor = Color.Transparent;
            // prev_btn.BaseColor = Color.Transparent;
            Next_btn.Left = 0;
            Next_btn.Top = 0;
            Next_btn.Width = btn_width;
            Next_btn.Height = btn_height;
            Next_btn.BackgroundImage = global::TjCaptureCtrlVB.Properties.Resources.分页_上一页;
            Next_btn.BackgroundImageLayout = ImageLayout.Zoom;
            Next_btn.Click += prev_btn_Click;
            this.container.Controls.Add(Next_btn);

            //add thumnail picturebox
            int i = 1;
            for (int row = 0; row < layoutRow; row++)
            {
                int baseHeightOffset = col_interval * (row + 1) + row * item_height;
                for (int col = 0; col < layoutCol; col++)
                {
                    int widthOffset = btn_width + row_interval * (col + 1) + col * item_width;
                    ThumbnailViewItem thumnailviewitem = new ThumbnailViewItem(widthOffset, baseHeightOffset, item_width, item_height, i++);
                    this.container.Controls.Add(thumnailviewitem);
                    thumnailPictureBoxList.Add(thumnailviewitem);
                }
            }

            //add nextPage button
            this.prev_btn = new Button();
            Next_btn.BackColor = Color.Transparent;
            //Next_btn.BaseColor = Color.Transparent;
            prev_btn.Left = this.container.Width - btn_width;
            prev_btn.Top = 0;
            prev_btn.Width = btn_width;
            prev_btn.Height = btn_height;
            prev_btn.BackgroundImage = global::TjCaptureCtrlVB.Properties.Resources.分页_下一页;
            prev_btn.BackgroundImageLayout = ImageLayout.Zoom;
            prev_btn.Click += Next_btn_Click;
            this.container.Controls.Add(prev_btn);
            thumnailImageList = new ThumnailImageList(layoutRow, layoutCol, thumnailPictureBoxList);


        }

        #endregion

        #region 翻页控制
        void prev_btn_Click(object sender, EventArgs e)
        {
            if (this.thumnailImageList == null || this.thumnailPictureBoxList == null)
            {
                return;
            }
            this.thumnailImageList.prevPage();
        }

        void Next_btn_Click(object sender, EventArgs e)
        {
            if (this.thumnailImageList == null || this.thumnailPictureBoxList == null)
            {
                return;
            }
            this.thumnailImageList.nextPage();
        }
        #endregion

        #region 加载时完成
        private void ThumbnailViewerPanel_Load(object sender, EventArgs e)
        {
            //asc.controllInitializeSize(this);
        }
        #endregion

        #region 控件大小改变时
        private void ThumbnailViewerPanel_SizeChanged(object sender, EventArgs e)
        {
            //asc.controlAutoSize(this);
        }
        #endregion

        #region 添加一张到缩略图
        public void setimage(Bitmap image, bool isVideo, string saveFileName, string videoFilePath = "")
        {
            if (this.thumnailImageList == null || this.thumnailPictureBoxList == null)
            {
                return;
            }
            this.thumnailImageList.addImageItem(image, isVideo, saveFileName, videoFilePath);
        }

        #endregion

        #region  更新一张缩略图  只针对图像窗口，视频窗口无效
        public void updateImage(string savepath, Bitmap newframe)
        {
            if (this.thumnailImageList == null)
            {
                return;
            }
            this.thumnailImageList.refereshThumnail(savepath, newframe);

        }
        #endregion

        #region 整个重置图像区域和图像列表区域
        public void resetAll()
        {
            if (this.thumnailImageList != null)
            {
                this.thumnailImageList.reset();
            }
            if (this.thumnailPictureBoxList != null)
            {
                this.thumnailPictureBoxList.Clear();
            }
            if (this.container != null)
            {
                this.container.Controls.Clear();
            }

            initThumnailPanel();
        }
        #endregion

        #region 重置所有的选中状态
        public void unSelectAll()
        {
            if (this.thumnailPictureBoxList == null)
            {
                return;
            }

            foreach (ThumbnailViewItem item in this.thumnailPictureBoxList)
            {
                item.unSelected();
            }
        }
        #endregion

        #region 获取list里面的上一张/下一张图像
        public imageItems getNextImageItems(string currentImageID)
        {
            return this.thumnailImageList.getNextImage(currentImageID);
        }

        public imageItems getPrevImageItems(string currentImageID)
        {
            return this.thumnailImageList.getPrevImage(currentImageID);
        }
        #endregion



    }
}
