using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TjCaptureCtrlVB.Utils;
using TjCaptureCtrlVB.visual;

namespace TjCaptureCtrlVB.userControl
{
    public partial class ShowFrame : Form
    {
        #region 初始化成员变量
        private static ShowFrame inst;
        private Bitmap imagesource = null;
        private string videoFileName;
        private ShapeType currentfun;
        private LineColor currentColor;
        private LineWidth currentWidth;
        private Point startPoint = Point.Empty;
        private Point endPoint = Point.Empty;
        private ThumbnailViewerPanel frameFromVideoContainer;
        private string currentImageID;
        private bool isVideo;
        private List<DrawVisual> visualList = new List<DrawVisual>();

        private Bitmap m_background;
        private Bitmap m_backBuffer;
        private Brush m_blackBrush;
        private Pen m_blackPen = new Pen(Brushes.Black);
        #endregion


        #region  根据要显示的是视频还是图像，设置窗体的可用控件
        public static ShowFrame GetForm(Bitmap image, string currentImageID, bool isVideo = false, string videofilename = "", ThumbnailViewerPanel frameFromVideoContainer = null)
        {
            //frame must contain image
            if (isVideo == false && image == null)
            {
                return null;
            }
            //video must contain filename
            if (isVideo == true && (videofilename == null || videofilename == ""))
            {
                return null;
            }

            if (inst == null || inst.IsDisposed)
            {
                inst = new ShowFrame();
                inst.FormClosing += inst_FormClosing;
            }
            inst.currentImageID = currentImageID;
            inst.isVideo = isVideo;
            inst.visualList.Clear();
            inst.currentDrawVisual = null;
            if (isVideo == false)
            {

                inst.imagesource = (Bitmap)image.Clone();
                inst.pictureBox1.Paint += pictureBox1_Paint;
                inst.DoubleBuffered = true;
                inst.frameFromVideoContainer = frameFromVideoContainer;
                inst.SetStyle(System.Windows.Forms.ControlStyles.UserPaint |
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                    System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                    true);
                if (inst.m_backBuffer == null)
                {
                    inst.m_backBuffer = new Bitmap(inst.imagesource.Width, inst.imagesource.Height);
                }

                inst.m_background = new Bitmap(inst.imagesource);
                //g = inst.pictureBox1.CreateGraphics();
                using (var g = Graphics.FromImage(inst.m_background))
                {
                    // draw in a static background here
                    g.DrawRectangle(inst.m_blackPen, 0, 0, inst.imagesource.Width, inst.imagesource.Height);
                    // etc.
                }
                inst.DoFrameShow();
            }
            else
            {
                inst.videoFileName = videofilename;
                inst.frameFromVideoContainer = frameFromVideoContainer;
                inst.DoVideoShow();
            }

            inst.KeyDown += inst_KeyDown;
            inst.KeyPreview = true;
            inst.FormClosing += inst_FormClosing;


            return inst;
        }

        #endregion


        #region 图像重绘时的事件,需要绘制标注
        static void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using (var g = Graphics.FromImage(inst.m_backBuffer))
            {
                // use appropriate back color
                // only necessary if the m_background doesn't fill the entire image
                g.Clear(Color.White);

                // draw in the static background
                g.DrawImage(inst.m_background, 0, 0);

                // draw in dynamic items here
                //g.DrawLine(m_blackPen, ...);
                if (inst.currentDrawVisual != null)
                {
                    (inst.currentDrawVisual).setGraphics(g);
                    inst.currentDrawVisual.render();
                }

                if (inst.visualList.Count > 0)
                {
                    //inst.visualList.RemoveAll(null);
                    foreach (DrawVisual item in inst.visualList)
                    {

                        item.setGraphics(g);
                        item.render();
                    }
                }
            }

            e.Graphics.DrawImage(inst.m_backBuffer, 0, 0);
        }
        #endregion

        #region 针对视频回放采集的脚踏板快捷键
        static void inst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (inst.isVideo == true)
                {
                    inst.FrameCaptureFromVideo_Click(sender, e);
                }
            }
        }
        #endregion

        #region 窗体关闭时
        static void inst_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (inst != null)
            {
                if (inst.isVideo == true)
                {
                    inst.videoSource.SignalToStop();
                    inst.videoSource.WaitForStop();
                }
                inst.Dispose();
                inst = null;
            }
        }
        #endregion

        #region 基础初始化
        public ShowFrame()
        {
            InitializeComponent();

        }
        #endregion

        #region 显示单帧图像设置
        private void DoFrameShow()
        {
            inst.videoSource.Hide();
            inst.videoSource.SignalToStop();
            inst.videoSource.WaitForStop();
            saveMarkPic.Enabled = true;
            clearMark.Enabled = true;
            inst.menuStrip1.Enabled = true;
            inst.FrameCaptureFromVideo.Enabled = false;
            inst.startPoint = Point.Empty;
            inst.endPoint = Point.Empty;
            inst.pictureBox1.Image = inst.imagesource;
            inst.pictureBox1.MouseDown += pictureBox1_MouseDown;
            inst.pictureBox1.MouseMove += pictureBox1_MouseMove;
            inst.pictureBox1.Paint += pictureBox1_Paint;
            inst.pictureBox1.Enabled = true;
            //inst.pictureBox2.Enabled = false;
            //inst.pictureBox2.Hide();
            inst.pictureBox1.Show();
            inst.funcPanel.Show();
        }
        #endregion


        #region 显示视频设置
        private void DoVideoShow()
        {
            inst.videoSource.Show();
            inst.currentfun = ShapeType.none;
            inst.menuStrip1.Enabled = false;
            inst.pictureBox1.Enabled = false;
            saveMarkPic.Enabled = false;
            clearMark.Enabled = false;
            inst.pictureBox1.Hide();
            //inst.pictureBox2.Show();
            inst.pictureBox1.MouseDown -= pictureBox1_MouseDown;
            inst.pictureBox1.MouseMove -= pictureBox1_MouseMove;
            inst.pictureBox1.Paint -= pictureBox1_Paint;
            inst.FrameCaptureFromVideo.Enabled = true;
            inst.funcPanel.Hide();
            VideoFileSource filesource1 = new VideoFileSource(inst.videoFileName);
            inst.videoSource.VideoSource = filesource1;
            inst.videoSource.VideoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();


        }

        #endregion

        #region 播放视频时的单帧回调事件
        void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            MemoryManagement.Init(eventArgs.Frame.Width, eventArgs.Frame.Height);

            using (Graphics g = Graphics.FromImage(MemoryManagement.getPlayVideoFlush()))
            {
                g.DrawImage(eventArgs.Frame, 0, 0);

            }
            // inst.pictureBox2.Image = MemoryManagement.getPlayVideoFlush();
        }
        #endregion

        #region 鼠标事件
        private DrawVisual currentDrawVisual;

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (inst.currentfun == ShapeType.none)
            {
                return;
            }
            else
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    //inst.pictureBox1.Update();
                    switch (inst.currentfun)
                    {

                        case ShapeType.line:

                            if (currentDrawVisual != null)
                            {
                                (currentDrawVisual as lineVisual).EndPoint = e.Location;
                                inst.pictureBox1.Invalidate();
                            }
                            break;
                        case ShapeType.arrow:

                            if (currentDrawVisual != null)
                            {
                                (currentDrawVisual as lineVisual).EndPoint = e.Location;
                                inst.pictureBox1.Invalidate();
                            }

                            break;
                        case ShapeType.RoundRect:

                            if (currentDrawVisual != null)
                            {
                                (currentDrawVisual as rectangleVisual).EndPoint = e.Location;
                                inst.pictureBox1.Invalidate();
                            }

                            break;
                        case ShapeType.SquareRect:

                            if (currentDrawVisual != null)
                            {
                                (currentDrawVisual as rectangleVisual).EndPoint = e.Location;
                                inst.pictureBox1.Invalidate();
                            }

                            break;
                        default:
                            return;
                    }
                }


                else
                {
                    return;
                }
            }
        }


        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (inst.currentfun == ShapeType.none)
            {
                return;
            }
            else
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    switch (inst.currentfun)
                    {
                        case ShapeType.line:
                            lineVisual linevisual = new lineVisual();
                            linevisual.LineCol = currentColor;
                            linevisual.LineWid = currentWidth;
                            linevisual.LineTy = currentfun;
                            currentDrawVisual = linevisual;
                            linevisual.StartPoint = e.Location;
                            inst.pictureBox1.Invalidate();
                            inst.visualList.Add(currentDrawVisual);
                            break;
                        case ShapeType.arrow:
                            lineVisual arrowVisual = new lineVisual();
                            arrowVisual.LineCol = currentColor;
                            arrowVisual.LineWid = currentWidth;
                            arrowVisual.LineTy = currentfun;
                            currentDrawVisual = arrowVisual;
                            arrowVisual.StartPoint = e.Location;
                            inst.visualList.Add(currentDrawVisual);
                            break;
                        case ShapeType.RoundRect:
                            rectangleVisual rectVisual = new rectangleVisual();
                            rectVisual.LineCol = currentColor;
                            rectVisual.LineWid = currentWidth;
                            rectVisual.RectTy = currentfun;
                            currentDrawVisual = rectVisual;
                            rectVisual.StartPoint = e.Location;
                            inst.visualList.Add(currentDrawVisual);
                            break;
                        case ShapeType.SquareRect:
                            rectangleVisual rectVisual2 = new rectangleVisual();
                            rectVisual2.LineCol = currentColor;
                            rectVisual2.LineWid = currentWidth;
                            rectVisual2.RectTy = currentfun;
                            currentDrawVisual = rectVisual2;
                            rectVisual2.StartPoint = e.Location;
                            inst.visualList.Add(currentDrawVisual);
                            break;
                        case ShapeType.circle:
                            circleVisual circlevisual = new circleVisual();
                            circlevisual.LineCol = currentColor;
                            circlevisual.LineWid = currentWidth;
                            currentDrawVisual = circlevisual;
                            circlevisual.EndPoint = e.Location;
                            inst.pictureBox1.Invalidate();
                            inst.visualList.Add(currentDrawVisual);
                            currentDrawVisual = null;
                            break;
                        case ShapeType.text:
                            string inputString = Microsoft.VisualBasic.Interaction.InputBox("文本输入", "InputBox", "", e.Location.X, e.Location.Y);
                            if (inputString == null || inputString == "")
                            {
                                return;
                            }
                            TextVisual textvisual = new TextVisual(inputString);
                            textvisual.LineCol = currentColor;
                            textvisual.LineWid = currentWidth;
                            currentDrawVisual = textvisual;
                            textvisual.EndPoint = e.Location;
                            inst.pictureBox1.Invalidate();
                            inst.visualList.Add(inst.currentDrawVisual);
                            inst.currentDrawVisual = null;
                            MenuItems_Click(inst.光标, new EventArgs()); //恢复为光标状态
                            break;

                    }

                }
                else
                {
                    return;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (inst.currentDrawVisual != null)
            {
                inst.visualList.Add(inst.currentDrawVisual);
                inst.currentDrawVisual = null;
                MenuItems_Click(inst.光标, new EventArgs()); //恢复为光标状态
            }
        }
        #endregion

        #region 针对菜单栏的选中函数
        // Uncheck all menu items in this menu except checked_item.
        private void CheckMenuItem(ToolStripMenuItem mnu,
            ToolStripMenuItem checked_item)
        {
            // Uncheck the menu items except checked_item.
            foreach (ToolStripItem item in mnu.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menu_item =
                        item as ToolStripMenuItem;
                    menu_item.Checked = (menu_item == checked_item);
                }
            }
        }
        #endregion

        #region  视频回放的单帧采集
        private void FrameCaptureFromVideo_Click(object sender, EventArgs e)
        {
            inst.frameFromVideoContainer.setimage(new Bitmap(MemoryManagement.getPlayVideoFlush()), false, "视频采集测试.bmp");

        }
        #endregion


        #region 菜单栏选项
        private void MenuItems_Click(object sender, EventArgs e)
        {
            // Check the menu item.
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CheckMenuItem(ShapeItems, item);
            switch (item.Name)
            {
                case "光标":
                    currentfun = ShapeType.none;
                    break;
                case "直线":
                    currentfun = ShapeType.line;
                    break;
                case "箭头":
                    currentfun = ShapeType.arrow;
                    break;
                case "方角矩形":
                    currentfun = ShapeType.SquareRect;
                    break;
                case "圆角矩形":
                    currentfun = ShapeType.RoundRect;
                    break;
                case "圆形":
                    currentfun = ShapeType.circle;
                    break;
                case "文字":
                    currentfun = ShapeType.text;
                    break;
                default:
                    currentfun = ShapeType.none;
                    break;
            }


        }
        private void ColorItems_Click(object sender, EventArgs e)
        {
            // Check the menu item.
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CheckMenuItem(ColorItems, item);

            switch (item.Name)
            {
                case "黑色":
                    currentColor = LineColor.black;
                    break;
                case "蓝色":
                    currentColor = LineColor.blue;
                    break;
                case "白色":
                    currentColor = LineColor.white;
                    break;
                case "黄色":
                    currentColor = LineColor.yellow;
                    break;
                case "红色":
                    currentColor = LineColor.red;
                    break;
                case "绿色":
                    currentColor = LineColor.green;
                    break;
                default:
                    currentColor = LineColor.black;
                    break;
            }
        }

        private void LineWidthItems_Click(object sender, EventArgs e)
        {
            // Check the menu item.
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CheckMenuItem(LineWidthItems, item);

            switch (item.Name)
            {
                case "磅2":
                    currentWidth = LineWidth.width2p;
                    break;
                case "磅4":
                    currentWidth = LineWidth.width4p;
                    break;
                case "磅6":
                    currentWidth = LineWidth.width6p;
                    break;
                default:
                    currentWidth = LineWidth.width2p;
                    break;
            }

        }
        #endregion

        #region 保存/删除标注
        private void saveMarkPic_Click(object sender, EventArgs e)
        {
            if (inst.isVideo == false)
            {
                if (inst.visualList.Count < 1)
                {
                    MessageBox.Show("未发现标注");
                    return;
                }
                inst.m_backBuffer.Save("marktest.bmp");
                inst.frameFromVideoContainer.updateImage(this.currentImageID, inst.m_backBuffer);

            }
        }

        private void clearMark_Click(object sender, EventArgs e)
        {
            if (inst.visualList.Count == 0)
            {
                return;
            }
            inst.visualList.Clear();
            inst.m_background.Dispose();
            inst.m_background = new Bitmap(inst.imagesource);
            inst.pictureBox1.Invalidate();
        }
        #endregion

        #region 标注的快捷按钮
        private void Line_Click(object sender, EventArgs e)
        {
            this.currentfun = ShapeType.line;
        }

        private void Arrow_Click(object sender, EventArgs e)
        {
            this.currentfun = ShapeType.arrow;
        }

        private void squareRect_Click(object sender, EventArgs e)
        {
            this.currentfun = ShapeType.SquareRect;
        }

        private void RoundRect_Click(object sender, EventArgs e)
        {
            this.currentfun = ShapeType.RoundRect;
        }

        private void Text_Click(object sender, EventArgs e)
        {
            this.currentfun = ShapeType.text;
        }
        #endregion

        #region 翻动图像
        private void PrevImage_btn_Click(object sender, EventArgs e)
        {
            imageItems imageitems = this.frameFromVideoContainer.getPrevImageItems(this.currentImageID);
            if (imageitems != null)
            {
                GetForm(imageitems.imageitem,imageitems.saveFileName,imageitems.isVideo,imageitems.videoPath, this.frameFromVideoContainer);
            }
        }

        private void NextImage_btn_Click(object sender, EventArgs e)
        {
            imageItems imageitems = this.frameFromVideoContainer.getNextImageItems(this.currentImageID);
            if (imageitems != null)
            {
                GetForm(imageitems.imageitem, imageitems.saveFileName, imageitems.isVideo, imageitems.videoPath, this.frameFromVideoContainer);
            }
        }
        #endregion

        

    }

}
