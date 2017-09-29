using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TjCaptureCtrlVB.Utils;

namespace TjCaptureCtrlVB.userControl
{
    public partial class ThumbnailViewItem : UserControl
    {
        #region 初始化成员
        //图像容器
        private PictureBox picShow;
        //播放按钮
        private Button playBtn;
        private Panel playbtn_container;
        //当前控件的位置
        private int index;
        //视频文件的路径
        private string videoFilePath;
        //是否为视频控件
        private bool isVideoCtrl;
        //该控件当前的唯一路径标志
        private string savefilepath;

        #endregion

        #region 初始化控件的宽度和高度
        public ThumbnailViewItem(int left, int top, int width, int height, int index)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.Height = height;
            this.index = index;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            InitializeComponent();

           
            

            //初始化显示控件
            this.picShow = new PictureBox();
            this.picShow.Left = 0;
            this.picShow.Top = 0;
            this.picShow.Width = width;
            this.picShow.Height = height;
            this.picShow.BackColor = Color.Black;
            this.picShow.SizeMode = PictureBoxSizeMode.Zoom;
            this.picShow.MouseDoubleClick += picShow_MouseDoubleClick;
            this.picShow.MouseClick += picShow_MouseClick;
            this.picShow.Paint += picShow_Paint;
            this.picShow.Tag = Color.Red;
            this.Controls.Add(this.picShow);

            //初始化播放按钮
            this.playBtn = new Button();
            this.playBtn.BackgroundImage = global::TjCaptureCtrlVB.Properties.Resources.movie;
            this.playBtn.BackgroundImageLayout = ImageLayout.Zoom;
            //this.playBtn.BackColor = Color.Transparent;
            //this.playBtn.Image = global::测试.Properties.Resources.movie;
            this.playBtn.Left = this.Width / 2 - 10;
            this.playBtn.Top = this.Height / 2 - 10;
            this.playBtn.Width = 20;
            this.playBtn.Height = 20;
            this.playBtn.Visible = true;
            this.Controls.Add(playBtn);
            this.playBtn.BringToFront();

    

        }

       
        
 #endregion

        #region 鼠标双击事件
        void picShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.isVideoCtrl == false)
            {
                if ((sender as PictureBox).Image != null)
                {
                    (ShowFrame.GetForm((Bitmap)((sender as PictureBox).Image), this.SaveFilePath, false, "", ((this.Parent as Panel).Parent as ThumbnailViewerPanel))).Show();
                }
            }
            else
            {
                (ShowFrame.GetForm(null, this.savefilepath, true, this.videoFilePath, ((this.Parent as Panel).Parent as ThumbnailViewerPanel))).Show();
            }

        }
        #endregion

        #region  鼠标单击事件
        void picShow_MouseClick(object sender, MouseEventArgs e)
        {
            ((this.Parent).Parent as ThumbnailViewerPanel).unSelectAll();
            this.picShow.Tag = Color.Yellow; 
            this.picShow.Refresh();

            if (this.picShow.Image != null)
            {
                ((((this.Parent).Parent).Parent) as UserControl1).ImageViewerChanged((Bitmap)this.picShow.Image.Clone());
            }
        }
        #endregion

        #region picturebox重绘事件
        void picShow_Paint(object sender, PaintEventArgs e)
        {
           // this.playbtn_container.BringToFront();
            ControlPaint.DrawBorder(e.Graphics, this.picShow.ClientRectangle, (Color)this.picShow.Tag, ButtonBorderStyle.Solid);
            if (this.isVideoCtrl == false)
            {
                this.playBtn.Hide();
                this.picShow.BringToFront();
            }
            else
            {
                this.playBtn.BringToFront();
                this.playBtn.Show();
            }
           // MessageBox.Show("do redraw");
            //this.playBtn.Invalidate();
        }

        #endregion

        #region 设置图像
        public void setimage(imageItems image)
        {
            this.picShow.Image = image.imageitem;
            this.playBtn.Visible = image.isVideo;
            //this.playBtn.
            this.videoFilePath = image.videoPath;
            this.isVideoCtrl = image.isVideo;
            this.savefilepath = image.saveFileName;
            if (this.isVideoCtrl == false)
            {
                this.picShow.AllowDrop = true;
                this.picShow.DragEnter += pictureBox1_DragEnter;
                this.picShow.DragDrop += pictureBox1_DragDrop;
                this.picShow.MouseMove += picShow_MouseMove;
            }
            else
            {
                
                this.picShow.DragEnter -= pictureBox1_DragEnter;
                this.picShow.DragDrop -= pictureBox1_DragDrop;
                //this.Invalidate();
            }
        }

        
        #endregion

        #region 将该控件初始化
        public void reset()
        {
            this.picShow.Image = null;
            this.picShow.Tag = Color.Red;
            this.isVideoCtrl = false;
            this.playBtn.Visible = false;
            this.savefilepath = "";
            //this.picShow.AllowDrop = false;
        }
        #endregion

        #region 重置控件的选中状态
        public void unSelected()
        {
            this.picShow.Tag = Color.Red;
            this.picShow.Refresh();
        }
        #endregion

        #region 获取当前保存路径的唯一标识
        public string SaveFilePath
        {
            get
            {
                return this.savefilepath;
            }
        }
        #endregion

        #region 拖拽事件方法组
        //drag drop functions 
        void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            this.picShow.Image = bmp;
        }

        void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
        }

        

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                var img = this.picShow.Image;
                if (img == null) return;
                if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
                {
                    //pictureBox1.Image = null;
                }
            }
        }
        void picShow_MouseMove(object sender, MouseEventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                var img = this.picShow.Image;
                if (img == null) return;
                if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
                {
                    //pictureBox1.Image = null;
                }
            }
        }



        #endregion
    }
}
