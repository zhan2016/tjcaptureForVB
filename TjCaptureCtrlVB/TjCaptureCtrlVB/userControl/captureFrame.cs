using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TjCaptureCtrlVB.Utils;
using AForge.Video.DirectShow;
using System.IO;
using AForge.Video.FFMPEG;
using AForge.Controls;
using TjCaptureCtrlVB.Common;

namespace TjCaptureCtrlVB.userControl
{
    public partial class captureFrame : UserControl
    {
        #region 初始化成员变量
        private ThumbnailViewerPanel parentContainer;
        private int deviceId;
        private MemoryManagement memorymanagement;
        private bool IsEnable;
        private Bitmap frameData;
        private Bitmap videoData;
        private VideoSourcePlayer videoSource;
        private Button SingleFrame1;
        private Button startVideoFrame1;
        private Button stopVideoFram1;
        private Button deviceStateCtrl1;
        private Label patientInfo; //用来展示病人的信息
        #endregion

        #region 控件初始化
        public captureFrame(VideoCaptureDevice videoplayer, ThumbnailViewerPanel showContainer,int left,int top, int width, int height)
        {
            InitializeComponent();
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.Height = height;
            this.Dock = DockStyle.Fill;

            #region 初始化病人信息展示框
            this.patientInfo = new Label();
            this.patientInfo.Left = 0;
            this.patientInfo.Top = 5;
            this.patientInfo.Width = 500;
            this.patientInfo.Height = 25;
            this.patientInfo.BackColor = Color.Transparent;
            this.patientInfo.ForeColor = Color.Green;
            this.patientInfo.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            //this.patientInfo.ForeColor = Color.Green;
            this.patientInfo.Text = GlobalAttribute.getPatientInfo();
            this.Controls.Add(this.patientInfo);
            
            #endregion
            #region 初始化视频源控件
            this.videoSource = new VideoSourcePlayer();
            videoSource.Left = 0;
            videoSource.Top = 0;
            videoSource.Width = this.Width;
            videoSource.Height = this.Height - 40;
            //this.videoSource = new VideoSourcePlayer();
            this.videoSource.VideoSource = videoplayer;
            this.videoSource.NewFrame += videoSource_NewFrame;
            this.parentContainer = showContainer;
            this.videoSource.Start();
            this.Controls.Add(this.videoSource);
            #endregion
            #region 初始化所有的按钮,按钮之间间隔1/2宽度,按钮高度固定在30
            int btnwidth = this.Width * 2 / 13;
            int btnInterval = btnwidth / 2;
            int btnheight = 30;
            int heightOffeset = this.Height - 40 + 5;

            this.SingleFrame1 = new Button();
            this.SingleFrame1.Left = btnInterval;
            this.SingleFrame1.Top = heightOffeset;
            this.SingleFrame1.Width = btnwidth;
            this.SingleFrame1.Height = btnheight;
            this.SingleFrame1.Text = "单帧采集";
            this.SingleFrame1.Click += SingleFrame_Click;
            this.Controls.Add(this.SingleFrame1);

            this.startVideoFrame1 = new Button();
            this.startVideoFrame1.Left = 1 * btnwidth + 2 * btnInterval;
            this.startVideoFrame1.Top = heightOffeset;
            this.startVideoFrame1.Width = btnwidth;
            this.startVideoFrame1.Height = btnheight;
            this.startVideoFrame1.Click += startVideoFrame_Click;
            this.startVideoFrame1.Text = "启动视频采集";
            this.Controls.Add(this.startVideoFrame1);

            this.stopVideoFram1 = new Button();
            this.stopVideoFram1.Left = 2 * this.Width * 2 / 13 + 3 * btnInterval;
            this.stopVideoFram1.Top = heightOffeset;
            this.stopVideoFram1.Width = btnwidth;
            this.stopVideoFram1.Height = btnheight;
            this.stopVideoFram1.Click += stopVideoFram_Click;
            this.stopVideoFram1.Text = "停止视频采集";
            this.Controls.Add(this.stopVideoFram1);

            this.deviceStateCtrl1 = new Button();
            this.deviceStateCtrl1.Left = 3 * this.Width * 2 / 13 + 4 * btnInterval;
            this.deviceStateCtrl1.Top = heightOffeset;
            this.deviceStateCtrl1.Width = btnwidth;
            this.deviceStateCtrl1.Height = btnheight;
            this.deviceStateCtrl1.Click += deviceStateCtrl_Click;
            this.deviceStateCtrl1.Text = "启动/停止设备";
            this.Controls.Add(this.deviceStateCtrl1);
            #endregion

            this.patientInfo.Parent = this.videoSource;
            this.stopVideoFram1.Enabled = false;
            this.IsEnable = true;
            this.KeyDown += captureFrame_KeyDown;
            this.memorymanagement = new MemoryManagement();
        }
#endregion

        #region 脚踏板快捷键采集
        public void captureFrame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (this.Enabled == true)
                {
                    SingleFrame_Click(sender, e);
                }
                
            }
            
        }
        #endregion

        #region 释放摄像头设备
        public  void releaseVideo()
        {
            this.videoSource.SignalToStop();
            this.videoSource.WaitForStop();
            this.videoSource.Stop();
        }
        #endregion

        #region 捕获摄像头图像
        void videoSource_NewFrame(object sender, ref Bitmap image)
        {
            //memorymanagement.Init(image.Width, image.Height);
            if (frameData == null || videoData == null)
            {
                frameData = new Bitmap(image.Width, image.Height);
                videoData = new Bitmap(image.Width, image.Height);
            }
            using (Graphics g = Graphics.FromImage(frameData))
           {
               g.DrawImage(image,0,0);
               //g.ReleaseHdc();
           }
        }
        #endregion

        #region 采集录像时的处理
        void videoSource_NewFrameSeques(object sender, ref Bitmap image)
        {
            using (Graphics g = Graphics.FromImage(frameData))
            {
                  g.DrawImage(image, 0, 0);
            }

            videomanager.AddFrame(image);
        }
        #endregion

        #region 单帧采集
        /// <summary>
        /// capture single frame toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleFrame_Click(object sender, EventArgs e)
        {
  
                //直接更新UI界面 需要加锁 zwj 20170821
                if (System.Threading.Monitor.TryEnter(LockFlag.AddToTablePanelLockFlag, 500))
                {
                    //thumViewer thumviewer = new thumViewer(thumType.frame, frameData);
                    this.parentContainer.setimage(new Bitmap(frameData), false, System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".dcm");
                    string dicominfo = @"<?xml version=""1.0"" encoding=""GB2312""?><XML><INFO><RIS_NO>111200020</RIS_NO><PID>11000255</PID><NAME>测试2</NAME><AGE>23</AGE><SEX>男</SEX><BIRTHDAY>1988-12-15</BIRTHDAY><PATIENT_LOCAL_ID>110128</PATIENT_LOCAL_ID><EXAM_SUB_CLASS>腹部</EXAM_SUB_CLASS><CLIN_SYMP></CLIN_SYMP><MODALITY>US</MODALITY><EXAM_DATE></EXAM_DATE><EXAM_TIME></EXAM_TIME><DEVICE>philips</DEVICE><STUDY_ID></STUDY_ID><ACCESSION_NUMBER>110128</ACCESSION_NUMBER></INFO></XML>";

                    ThreadPoolManager.saveDicomWork(new Bitmap(frameData), System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".dcm", dicominfo);
                }
               
        }
        #endregion

        #region 采集视频的控制函数
        private VideoStreamManager videomanager;
        /// <summary>
        /// toggle video capture start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startVideoFrame_Click(object sender, EventArgs e)
        {
            //create video file stream
            vedioFileName = "test_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".avi";
            if (!File.Exists(vedioFileName))
            {
                File.Create(vedioFileName).Close(); //需要加上close关闭句柄
            }
            LoggerUtil.LoggerInfo("Create VideoFile:" + vedioFileName);
            //Bitmap Initdata = (Bitmap)this.VideoSequence.Image.Clone();
            videomanager = new VideoStreamManager(vedioFileName, videoData.Width, videoData.Height, 10, VideoCodec.H264);
            videomanager.startStream();
            this.videoSource.NewFrame += videoSource_NewFrameSeques;
            this.videoSource.NewFrame -= videoSource_NewFrame;
            
            this.startVideoFrame1.Enabled = false;
            this.stopVideoFram1.Enabled = true;
            //timeStart();

        }


 
       
        /// <summary>
        /// toggle video capture stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopVideoFram_Click(object sender, EventArgs e)
        {


            videomanager.closeStream();
            this.videoSource.NewFrame -= videoSource_NewFrameSeques;
            this.videoSource.NewFrame += videoSource_NewFrame;
            
            //timeStop();
            

            if (System.Threading.Monitor.TryEnter(LockFlag.AddToTablePanelLockFlag, 500))
            {
                this.parentContainer.setimage(new Bitmap(frameData), true, vedioFileName, vedioFileName);
                this.startVideoFrame1.Enabled = true;
                this.stopVideoFram1.Enabled = false;
                
            }
            
            //ThreadPoolManager.saveFrameWork(null, vedioFileName, "dicomtest.dcm");
            //DicomHelper.importMutilFrame(vedioFileName);   //可以直接保存多帧视频 zwj 20170815
        }

        private String vedioFileName = "";
        #endregion


        #region 启动/停止设备
        /// <summary>
        /// manager the state of this capture device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deviceStateCtrl_Click(object sender, EventArgs e)
        {
            this.IsEnable = !this.IsEnable;
            if (this.IsEnable == true)
            {
                this.startVideoFrame1.Enabled = true;
                //this.stopVideoFram.Enabled = true;
                this.SingleFrame1.Enabled = true;
                this.videoSource.NewFrame += videoSource_NewFrame;
                this.videoSource.Start();
            }
            else
            {
                this.startVideoFrame1.Enabled = false;
                this.stopVideoFram1.Enabled = false;
                this.SingleFrame1.Enabled = false;
                this.videoSource.NewFrame -= videoSource_NewFrame;
                this.videoSource.SignalToStop();
                this.videoSource.WaitForStop();
            }
        }
        #endregion


    }
}
