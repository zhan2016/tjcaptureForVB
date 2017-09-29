using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TjCaptureCtrlVB.userControl
{
    public enum thumType
    {
        frame,
        video
    }
    public partial class thumViewer : UserControl
    {
        private thumType thumbtype;

        private Bitmap frame;
        private string videoFilename;
        private string bitmapFileName;

        /// <summary>
        /// 缩略图的内容样式 video or frame
        /// </summary>
        public thumType ThumbType
        {
            get { return thumbtype; }
            set { this.thumbtype = value; }
        }

        /// <summary>
        /// init a thumbnail control.The image source is from bitmap
        /// </summary>
        /// <param name="thumbtype">video or single frame</param>
        /// <param name="image">image for show</param>
        /// <param name="vedioFilename">if this is a video control, we have to attach video file to control</param>
        public thumViewer(thumType thumbtype, Bitmap image, string videoFilename = "")
        {
            InitializeComponent();
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.thumbtype = thumbtype;
            this.button1.Hide();
            frame = new Bitmap(image);
            // a play button special for video
            if (thumbtype == thumType.video)
            {
                this.videoFilename = videoFilename;
                //this.BackColor = Color.Red;
                this.button1.Parent = this.pictureBox1;

                //this.button1.
                this.button1.Show();
            }
            show();
            //saveFile();
            this.pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
            // for image viewer open drag drop
            if (thumbtype == thumType.frame)
            {
                this.pictureBox1.AllowDrop = true;
                this.pictureBox1.MouseDown += pictureBox1_MouseDown;
                this.pictureBox1.DragEnter += pictureBox1_DragEnter;
                this.pictureBox1.DragDrop += pictureBox1_DragDrop;
            }

        }


        #region 拖拽事件方法组
        //drag drop functions 
        void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            this.pictureBox1.Image = bmp;
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
                var img = this.pictureBox1.Image;
                if (img == null) return;
                if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
                {
                    //pictureBox1.Image = null;
                }
            }
        }




        #endregion


        /// <summary>
        /// init a thumnail control.the image  source is from file.
        /// </summary>
        /// <param name="thumbtype">video or single frame</param>
        /// <param name="bitmapFileName">image path for show</param>
        /// <param name="videoFilename">if this is a video control, we have to attach video file to control</param>
        public thumViewer(thumType thumbtype, string bitmapFileName, string videoFilename = "")
        {

            if (!File.Exists(bitmapFileName))
            {
                LoggerUtil.LoggerError("can not find bitmap file:" + bitmapFileName);
                return;
            }

            Bitmap toShowBitmap = new Bitmap(bitmapFileName);
            InitializeComponent();
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Width = this.Width;
            this.pictureBox1.Height = this.Height;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.thumbtype = thumbtype;
            this.button1.Hide();
            frame = (Bitmap)toShowBitmap.Clone();
            // a play button special for video
            if (thumbtype == thumType.video)
            {
                this.videoFilename = videoFilename;
                //this.BackColor = Color.Red;
                this.button1.Parent = this.pictureBox1;

                //this.button1.
                this.button1.Show();
            }
            show();
            this.pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;

        }

        public void refreshPictureBox(Bitmap bitmapSource)
        {
            if (this.pictureBox1.Image != null && bitmapSource != null)
            {
                this.frame.Dispose();
                this.frame = new Bitmap(bitmapSource);
                this.pictureBox1.Image = (Image)this.frame.Clone();
            }
        }

        void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.thumbtype == thumType.frame)
            {
                if ((sender as PictureBox).Image != null)
                {
                   // (ShowFrame.GetForm((Bitmap)((sender as PictureBox).Image), this)).Show();
                }
            }
            if (this.thumbtype == thumType.video)
            {
                if ((sender as PictureBox).Image != null)
                {
                    //(ShowFrame.GetForm(null, this, true, videoFilename, (this.Parent as Panel))).Show();
                }
            }
        }

        private void show()
        {
            this.pictureBox1.Width = this.Width;
            this.pictureBox1.Height = this.Height;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.Image = (Bitmap)this.frame.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // (ShowFrame.GetForm(null, this, true, videoFilename, (this.Parent as Panel))).Show();
        }

        public void saveFile()
        {
            if (this.frame == null)
            {
                return;
            }
            string bitmapPath = "test" + ".bmp";
            this.frame.Save(bitmapPath);
        }

    }
}
