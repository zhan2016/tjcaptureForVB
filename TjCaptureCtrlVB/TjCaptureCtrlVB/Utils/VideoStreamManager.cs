using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TjCaptureCtrlVB.Utils;

namespace TjCaptureCtrlVB
{
    class VideoStreamManager
    {
        private int initWidth;
        private int initHeight;
        private int frameRate;
        private long frameCount;
        private VideoCodec codec;
        private string aviFileName;
        private VideoFileWriter writer;
        private VideoFileReader reader;
        private int currentReadPos;

        #region write video
        public VideoStreamManager(string aviFileName, int initWidth, int initHeight, int frameRate, VideoCodec codec)
        {
            this.initHeight = initHeight;
            this.initWidth = initWidth;
            this.frameRate = frameRate;
            this.codec = codec;
            this.aviFileName = aviFileName;
        }
        public void startStream()
        {
            writer = new VideoFileWriter();
            if (!File.Exists(this.aviFileName))
            {
                File.Create(this.aviFileName).Close();
            }

            writer.Open(this.aviFileName, this.initWidth, this.initHeight, this.frameRate, this.codec);
        }

        public void AddFrame(Bitmap imageSource)
        {
           
                if (imageSource == null)
                {
                    return;
                }
                if (writer.IsOpen == true)
                {
                   // Bitmap frameData = new Bitmap(imageSource);
                    writer.WriteVideoFrame(imageSource);
                  //  frameData.Dispose();
                }
                
               // imageSource.Dispose();
            
        }

        public void closeStream()
        {
            if (writer != null)
            {
                this.writer.Close();
            }
            if (reader != null)
            {
                this.reader.Close();
            }
        }
        #endregion write video

        #region read video
        public VideoStreamManager(string aviFileName)
        {
            this.aviFileName = aviFileName;
            this.currentReadPos = 0;
            this.reader = new VideoFileReader();
        }

        public void readOpen()
        {
            if (File.Exists(this.aviFileName))
            {
                this.reader.Open(this.aviFileName);
                this.frameCount = this.reader.FrameCount;
                this.frameRate = this.reader.FrameRate;
            }
        }

        public bool hasNextFrame()
        {
            return (this.currentReadPos++ < this.frameCount);
        }

        public Bitmap Next()
        {
            return this.reader.ReadVideoFrame();
        }

        public Bitmap readTheFirstbitmap()
        {
            this.currentReadPos++;
            return this.reader.ReadVideoFrame();
        }

        public long getFrameCount()
        {
            return this.frameCount;
        }
        #endregion 
    }
}
