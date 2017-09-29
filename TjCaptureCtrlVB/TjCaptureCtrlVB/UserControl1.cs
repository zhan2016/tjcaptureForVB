using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using System.Diagnostics;
using TjCaptureCtrlVB.Utils;
using TjCaptureCtrlVB.userControl;
using AForge.Controls;
using TjCaptureCtrlVB.Common;
using AForge.Video.FFMPEG;

namespace TjCaptureCtrlVB
{
    public partial class UserControl1: UserControl
    {
        #region 提供一系列属性，以供vb初始化
        public string DicomInfo
        {
            set
            {
                if (dicominfo != value)
                {
                    dicominfo = value;
                    setDicomInfo(value);
                    if (this.thumnailviewerpanel != null)
                    {
                        this.thumnailviewerpanel.resetAll();
                    }
                }
            }
        }

        public void setDicomInfo(string dicomInfo)
        {
            GlobalAttribute.setDicomInfo(dicomInfo);
        }

        public string getinfo()
        {
            return GlobalAttribute.getValue("PID");
        }
        public string LayOut
        {
            set
            {
                if (value != GlobalAttribute.LayOutInfo)
                {
                    this.layoutInfo = value;
                    GlobalAttribute.LayOutInfo = value;
                    if (this.thumnailviewerpanel != null)
                    {
                        this.thumnailviewerpanel.resetAll();
                    }
                    
                }

            }
        }

        public string  CapturePos
        {
            set
            {
                if (value != this.capturepos)
                {
                    this.capturepos = value;
                    GlobalAttribute.CaptureCtrlPos = value;
                    int[] pos = GlobalAttribute.getCaptureCtrlPos();
                    this.capturecontainer = new Panel();
                    this.capturecontainer.Left = pos[0];
                    this.capturecontainer.Top = pos[1];
                    this.capturecontainer.Width = pos[2];
                    this.capturecontainer.Height = pos[3];
                    this.Controls.Add(this.capturecontainer);

                }
            }
        }

        public string ThumnailPos
        {
            set
            {
                if (value != GlobalAttribute.ThumbnailCtrlPos)
                {
                    this.thumnailpos = value;
                    GlobalAttribute.ThumbnailCtrlPos = value;
                    int[] pos = GlobalAttribute.getThumbnailCtrlPos();
                    this.thumnailviewerpanel = new ThumbnailViewerPanel(pos[0], pos[1], pos[2], pos[3]);
                    this.thumnailviewerpanel.Left = pos[0];
                    this.thumnailviewerpanel.Top = pos[1];
                    this.thumnailviewerpanel.Width = pos[2];
                    this.thumnailviewerpanel.Height = pos[3];
                    StartCameras();
                    InitImageViewer();
                    this.Controls.Add(this.thumnailviewerpanel);
                    this.thumnailviewerpanel.Visible = true;
                }
            }
        }


        public string hotKeyCapture
        {
            set
            {
                KeyEventArgs e = new KeyEventArgs(Keys.F12);
                MainForm_KeyDown(this, e);
            }
        }
        #endregion

        #region 初始化成员变量
        // list of video devices
        FilterInfoCollection videoDevices;
        // stop watch for measuring fps
        private Stopwatch stopWatch = null;
        private  string dicominfo = "";
        private string capturepos;
        private string thumnailpos;
        private ThumbnailViewerPanel thumnailviewerpanel;
        private Panel imageViewerPanel;
        private System.Windows.Forms.PictureBox imageViewerPicturebox;
        private Panel capturecontainer;
        private string layoutInfo;
        #endregion 
       
        #region 初始化 窗口
        public UserControl1()
        {
            InitializeComponent();
            // show device list
            Trace.Listeners.Clear();  //清除系统监听器 (就是输出到Console的那个)
            Trace.Listeners.Add(new MyTraceListener()); //添加MyTraceListener实例
            //缩略图窗口
            //int[] pos = GlobalAttribute.getThumbnailCtrlPos();
            //MessageBox.Show(this.thumnailpos);

            // show device list
            //MessageBox.Show("初始化成功了");
            try
            {
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    throw new Exception();
                }

                for (int i = 1, n = videoDevices.Count; i <= n; i++)
                {
                    string cameraName = i + " : " + videoDevices[i - 1].Name;
                   LoggerUtil.LoggerInfo("Find devices:" + videoDevices[i - 1].Name);
                }
            }
            catch (Exception e)
            {
                LoggerUtil.LoggerError(e.Message);
            }
            //this.captureContainer.AutoScroll = true;
           
            this.KeyDown += MainForm_KeyDown;
            //this.Disposed += UserControl1_Disposed;
            
        }
        private void InitImageViewer()
        {
            //初始化浏览窗口，但是不显示
            this.imageViewerPanel = new Panel();
            this.imageViewerPanel.Left = this.capturecontainer.Left;
            this.imageViewerPanel.Top = this.capturecontainer.Top;
            this.imageViewerPanel.Width = this.capturecontainer.Width;
            this.imageViewerPanel.Height = this.capturecontainer.Height;
            this.imageViewerPicturebox = new System.Windows.Forms.PictureBox();
            this.imageViewerPicturebox.Dock = DockStyle.Fill;
            this.imageViewerPicturebox.BackColor = Color.Black;
            this.imageViewerPicturebox.SizeMode = PictureBoxSizeMode.Zoom;
            this.imageViewerPanel.Controls.Add(this.imageViewerPicturebox);
            this.imageViewerPicturebox.MouseUp += imageViewerPanel_MouseUp;
            //this.imageViewerPanel.MouseUp += imageViewerPanel_MouseUp;
            this.imageViewerPanel.Visible = false;
            this.Controls.Add(this.imageViewerPanel);
        }

        



        #endregion
        
       
        #region 按钮按下事件
        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                MessageBox.Show("F12按下啦");
                foreach (Control ctr in this.capturecontainer.Controls)
                {
                    (ctr as captureFrame).captureFrame_KeyDown(sender, e);
                }
            }
        }

        void imageViewerPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
               
                this.imageViewerPicturebox.Image = null;
                this.imageViewerPanel.Visible = false;
                 this.capturecontainer.Visible = true;
            }
        }
        #endregion

        #region 启动采集摄像头控件
        // Start cameras
        private void StartCameras()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            int i = 0;
            if (videoDevices.Count == 0)
            {
                return;
            }
            if (videoDevices.Count == 1)
            {
                VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.DesiredFrameRate = 100;

                int width = this.capturecontainer.Width;
                int height = this.capturecontainer.Height;
                int left = 0;
                int top = 0;

                captureFrame captureframe = new captureFrame(videoSource, this.thumnailviewerpanel, left, top, width, height);
                //captureframe.Width = this.ViewerContainer.Width;
                //captureContainer.Height = this.ViewerContainer.Height;
                this.capturecontainer.Controls.Add(captureframe);
            }
            else
            {
                int captureDeviceCount = videoDevices.Count;
                int deviceIteor = 0;
                foreach (FilterInfo s in videoDevices)
                {
                    VideoCaptureDevice videoSource = new VideoCaptureDevice(s.MonikerString);
                    videoSource.DesiredFrameRate = 100;
                    int capturewidth = this.capturecontainer.Width / captureDeviceCount;
                    int captureheight = this.capturecontainer.Height;

                    int width = capturewidth > captureheight ? captureheight : capturewidth;
                    int height = width;
                    int left = (this.capturecontainer.Width - width) / 2 + width * deviceIteor++;
                    int top = (this.capturecontainer.Height - height) / 2;

                    captureFrame captureframe = new captureFrame(videoSource, this.thumnailviewerpanel, left, top, width, height);
                    //captureframe.Width = this.ViewerContainer.Width;
                    //captureContainer.Height = this.ViewerContainer.Height;
                    this.capturecontainer.Controls.Add(captureframe);

                }
            }
        }
        #endregion

        #region 切换为浏览窗口
        public void ImageViewerChanged(Bitmap imageFrame)
        {
            this.capturecontainer.Visible = false;
            this.imageViewerPanel.Show();
            this.imageViewerPicturebox.Image = imageFrame;
        }
        #endregion

        #region 一系列的测试函数
        private void opendicomfileTest_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = @"C:\Users\cs\Desktop\personalProgrammer\other\aforgo\Tjcapture\bin\x86\Debug";
            this.openFileDialog1.Title = "打开dicom文件";
            //this.openFileDialog1.ShowDialog();
            string filename = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
            }

            if (filename != "")
            {
                Bitmap imageframe = DicomHelper.dcm2bmp(filename);
                this.thumnailviewerpanel.setimage(imageframe, false, filename);
            }

        }

        VideoFileSource filesource1;
        string videofilePath;
        private void openVedio_btn_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = @"C:\Users\cs\Desktop\personalProgrammer\other\aforgo\Tjcapture\bin\x86\Debug";
            this.openFileDialog1.Title = "打开dicom文件";
            //this.openFileDialog1.ShowDialog();
            string filename = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
            }
            LoggerUtil.LoggerInfo("尝试打开文件:" + filename);
            if (filename != "")
            {
                VideoFileSource filesource1 = new VideoFileSource(filename);
                videofilePath = filename;
                filesource1.NewFrame += filesource1_NewFrame;
                filesource1.Start();
                System.Threading.Thread.Sleep(200);
                
            }
        }

        void filesource1_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
           // Bitmap imageframe = eventArgs.Frame;
            this.thumnailviewerpanel.setimage(new Bitmap(eventArgs.Frame), true, videofilePath, videofilePath);
            filesource1.NewFrame -= filesource1_NewFrame;
            filesource1.WaitForStop();
            
        }
#endregion

        
    }
}
