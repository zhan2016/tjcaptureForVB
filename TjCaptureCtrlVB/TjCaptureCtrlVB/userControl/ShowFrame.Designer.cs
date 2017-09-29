namespace TjCaptureCtrlVB.userControl
{
    partial class ShowFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Textbtn = new System.Windows.Forms.Button();
            this.RoundRect = new System.Windows.Forms.Button();
            this.squareRect = new System.Windows.Forms.Button();
            this.Line = new System.Windows.Forms.Button();
            this.funcPanel = new System.Windows.Forms.Panel();
            this.Arrow = new System.Windows.Forms.Button();
            this.clearMark = new System.Windows.Forms.Button();
            this.saveMarkPic = new System.Windows.Forms.Button();
            this.磅6 = new System.Windows.Forms.ToolStripMenuItem();
            this.磅4 = new System.Windows.Forms.ToolStripMenuItem();
            this.磅2 = new System.Windows.Forms.ToolStripMenuItem();
            this.LineWidthItems = new System.Windows.Forms.ToolStripMenuItem();
            this.绿色 = new System.Windows.Forms.ToolStripMenuItem();
            this.红色 = new System.Windows.Forms.ToolStripMenuItem();
            this.黄色 = new System.Windows.Forms.ToolStripMenuItem();
            this.白色 = new System.Windows.Forms.ToolStripMenuItem();
            this.蓝色 = new System.Windows.Forms.ToolStripMenuItem();
            this.黑色 = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorItems = new System.Windows.Forms.ToolStripMenuItem();
            this.部位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圆形 = new System.Windows.Forms.ToolStripMenuItem();
            this.文字 = new System.Windows.Forms.ToolStripMenuItem();
            this.圆角矩形 = new System.Windows.Forms.ToolStripMenuItem();
            this.方角矩形 = new System.Windows.Forms.ToolStripMenuItem();
            this.箭头 = new System.Windows.Forms.ToolStripMenuItem();
            this.直线 = new System.Windows.Forms.ToolStripMenuItem();
            this.光标 = new System.Windows.Forms.ToolStripMenuItem();
            this.ShapeItems = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameCaptureFromVideo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.videoSource = new AForge.Controls.VideoSourcePlayer();
            this.NextImage_btn = new System.Windows.Forms.Button();
            this.PrevImage_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.funcPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Textbtn
            // 
            this.Textbtn.Location = new System.Drawing.Point(414, 12);
            this.Textbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Textbtn.Name = "Textbtn";
            this.Textbtn.Size = new System.Drawing.Size(75, 22);
            this.Textbtn.TabIndex = 4;
            this.Textbtn.Text = "文字";
            this.Textbtn.UseVisualStyleBackColor = true;
            this.Textbtn.Click += new System.EventHandler(this.Text_Click);
            // 
            // RoundRect
            // 
            this.RoundRect.Location = new System.Drawing.Point(316, 12);
            this.RoundRect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RoundRect.Name = "RoundRect";
            this.RoundRect.Size = new System.Drawing.Size(75, 22);
            this.RoundRect.TabIndex = 3;
            this.RoundRect.Text = "圆角矩形";
            this.RoundRect.UseVisualStyleBackColor = true;
            this.RoundRect.Click += new System.EventHandler(this.RoundRect_Click);
            // 
            // squareRect
            // 
            this.squareRect.Location = new System.Drawing.Point(215, 12);
            this.squareRect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.squareRect.Name = "squareRect";
            this.squareRect.Size = new System.Drawing.Size(75, 22);
            this.squareRect.TabIndex = 2;
            this.squareRect.Text = "方角矩形";
            this.squareRect.UseVisualStyleBackColor = true;
            this.squareRect.Click += new System.EventHandler(this.squareRect_Click);
            // 
            // Line
            // 
            this.Line.Location = new System.Drawing.Point(19, 12);
            this.Line.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(75, 22);
            this.Line.TabIndex = 0;
            this.Line.Text = "直线";
            this.Line.UseVisualStyleBackColor = true;
            this.Line.Click += new System.EventHandler(this.Line_Click);
            // 
            // funcPanel
            // 
            this.funcPanel.Controls.Add(this.Textbtn);
            this.funcPanel.Controls.Add(this.RoundRect);
            this.funcPanel.Controls.Add(this.squareRect);
            this.funcPanel.Controls.Add(this.Arrow);
            this.funcPanel.Controls.Add(this.Line);
            this.funcPanel.Location = new System.Drawing.Point(38, 475);
            this.funcPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.funcPanel.Name = "funcPanel";
            this.funcPanel.Size = new System.Drawing.Size(651, 39);
            this.funcPanel.TabIndex = 20;
            // 
            // Arrow
            // 
            this.Arrow.Location = new System.Drawing.Point(117, 12);
            this.Arrow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Arrow.Name = "Arrow";
            this.Arrow.Size = new System.Drawing.Size(75, 22);
            this.Arrow.TabIndex = 1;
            this.Arrow.Text = "箭头";
            this.Arrow.UseVisualStyleBackColor = true;
            this.Arrow.Click += new System.EventHandler(this.Arrow_Click);
            // 
            // clearMark
            // 
            this.clearMark.Location = new System.Drawing.Point(369, 528);
            this.clearMark.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearMark.Name = "clearMark";
            this.clearMark.Size = new System.Drawing.Size(109, 22);
            this.clearMark.TabIndex = 19;
            this.clearMark.Text = "清除所有标注";
            this.clearMark.UseVisualStyleBackColor = true;
            this.clearMark.Click += new System.EventHandler(this.clearMark_Click);
            // 
            // saveMarkPic
            // 
            this.saveMarkPic.Location = new System.Drawing.Point(240, 528);
            this.saveMarkPic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveMarkPic.Name = "saveMarkPic";
            this.saveMarkPic.Size = new System.Drawing.Size(109, 22);
            this.saveMarkPic.TabIndex = 18;
            this.saveMarkPic.Text = "保存标注图像";
            this.saveMarkPic.UseVisualStyleBackColor = true;
            this.saveMarkPic.Click += new System.EventHandler(this.saveMarkPic_Click);
            // 
            // 磅6
            // 
            this.磅6.Name = "磅6";
            this.磅6.Size = new System.Drawing.Size(108, 26);
            this.磅6.Text = "6磅";
            this.磅6.Click += new System.EventHandler(this.LineWidthItems_Click);
            // 
            // 磅4
            // 
            this.磅4.Name = "磅4";
            this.磅4.Size = new System.Drawing.Size(108, 26);
            this.磅4.Text = "4磅";
            this.磅4.Click += new System.EventHandler(this.LineWidthItems_Click);
            // 
            // 磅2
            // 
            this.磅2.Name = "磅2";
            this.磅2.Size = new System.Drawing.Size(108, 26);
            this.磅2.Text = "2磅";
            this.磅2.Click += new System.EventHandler(this.LineWidthItems_Click);
            // 
            // LineWidthItems
            // 
            this.LineWidthItems.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.磅2,
            this.磅4,
            this.磅6});
            this.LineWidthItems.Name = "LineWidthItems";
            this.LineWidthItems.Size = new System.Drawing.Size(81, 24);
            this.LineWidthItems.Text = "标记线条";
            // 
            // 绿色
            // 
            this.绿色.Name = "绿色";
            this.绿色.Size = new System.Drawing.Size(114, 26);
            this.绿色.Text = "绿色";
            this.绿色.Click += new System.EventHandler(this.ColorItems_Click);
            // 
            // 红色
            // 
            this.红色.Name = "红色";
            this.红色.Size = new System.Drawing.Size(114, 26);
            this.红色.Text = "红色";
            this.红色.Click += new System.EventHandler(this.ColorItems_Click);
            // 
            // 黄色
            // 
            this.黄色.Name = "黄色";
            this.黄色.Size = new System.Drawing.Size(114, 26);
            this.黄色.Text = "黄色";
            this.黄色.Click += new System.EventHandler(this.ColorItems_Click);
            // 
            // 白色
            // 
            this.白色.Name = "白色";
            this.白色.Size = new System.Drawing.Size(114, 26);
            this.白色.Text = "白色";
            this.白色.Click += new System.EventHandler(this.ColorItems_Click);
            // 
            // 蓝色
            // 
            this.蓝色.Name = "蓝色";
            this.蓝色.Size = new System.Drawing.Size(114, 26);
            this.蓝色.Text = "蓝色";
            this.蓝色.Click += new System.EventHandler(this.ColorItems_Click);
            // 
            // 黑色
            // 
            this.黑色.Name = "黑色";
            this.黑色.Size = new System.Drawing.Size(114, 26);
            this.黑色.Text = "黑色";
            this.黑色.Click += new System.EventHandler(this.ColorItems_Click);
            // 
            // ColorItems
            // 
            this.ColorItems.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.黑色,
            this.蓝色,
            this.白色,
            this.黄色,
            this.红色,
            this.绿色});
            this.ColorItems.Name = "ColorItems";
            this.ColorItems.Size = new System.Drawing.Size(81, 24);
            this.ColorItems.Text = "标记颜色";
            // 
            // 部位ToolStripMenuItem
            // 
            this.部位ToolStripMenuItem.Name = "部位ToolStripMenuItem";
            this.部位ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.部位ToolStripMenuItem.Text = "部位";
            this.部位ToolStripMenuItem.Visible = false;
            this.部位ToolStripMenuItem.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // 圆形
            // 
            this.圆形.Name = "圆形";
            this.圆形.Size = new System.Drawing.Size(144, 26);
            this.圆形.Text = "圆形";
            this.圆形.Visible = false;
            this.圆形.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // 文字
            // 
            this.文字.Name = "文字";
            this.文字.Size = new System.Drawing.Size(144, 26);
            this.文字.Text = "文字";
            this.文字.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // 圆角矩形
            // 
            this.圆角矩形.Name = "圆角矩形";
            this.圆角矩形.Size = new System.Drawing.Size(144, 26);
            this.圆角矩形.Text = "圆角矩形";
            this.圆角矩形.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // 方角矩形
            // 
            this.方角矩形.Name = "方角矩形";
            this.方角矩形.Size = new System.Drawing.Size(144, 26);
            this.方角矩形.Text = "方角矩形";
            this.方角矩形.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // 箭头
            // 
            this.箭头.Name = "箭头";
            this.箭头.Size = new System.Drawing.Size(144, 26);
            this.箭头.Text = "箭头";
            this.箭头.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // 直线
            // 
            this.直线.Name = "直线";
            this.直线.Size = new System.Drawing.Size(144, 26);
            this.直线.Text = "直线";
            this.直线.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // 光标
            // 
            this.光标.Name = "光标";
            this.光标.Size = new System.Drawing.Size(144, 26);
            this.光标.Text = "光标";
            this.光标.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // ShapeItems
            // 
            this.ShapeItems.CheckOnClick = true;
            this.ShapeItems.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.光标,
            this.直线,
            this.箭头,
            this.方角矩形,
            this.圆角矩形,
            this.文字,
            this.圆形,
            this.部位ToolStripMenuItem});
            this.ShapeItems.Name = "ShapeItems";
            this.ShapeItems.Size = new System.Drawing.Size(81, 24);
            this.ShapeItems.Text = "标记图形";
            // 
            // FrameCaptureFromVideo
            // 
            this.FrameCaptureFromVideo.Location = new System.Drawing.Point(502, 528);
            this.FrameCaptureFromVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FrameCaptureFromVideo.Name = "FrameCaptureFromVideo";
            this.FrameCaptureFromVideo.Size = new System.Drawing.Size(112, 22);
            this.FrameCaptureFromVideo.TabIndex = 15;
            this.FrameCaptureFromVideo.Text = "录像单帧采集";
            this.FrameCaptureFromVideo.UseVisualStyleBackColor = true;
            this.FrameCaptureFromVideo.Click += new System.EventHandler(this.FrameCaptureFromVideo_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShapeItems,
            this.ColorItems,
            this.LineWidthItems});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(712, 28);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // videoSource
            // 
            this.videoSource.BackColor = System.Drawing.SystemColors.ControlDark;
            this.videoSource.ForeColor = System.Drawing.Color.White;
            this.videoSource.Location = new System.Drawing.Point(38, 30);
            this.videoSource.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.videoSource.Name = "videoSource";
            this.videoSource.Size = new System.Drawing.Size(632, 392);
            this.videoSource.TabIndex = 16;
            this.videoSource.VideoSource = null;
            this.videoSource.Visible = false;
            // 
            // NextImage_btn
            // 
            this.NextImage_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.NextImage_btn.BackgroundImage = global::TjCaptureCtrlVB.Properties.Resources.next;
            this.NextImage_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.NextImage_btn.Location = new System.Drawing.Point(670, 144);
            this.NextImage_btn.Name = "NextImage_btn";
            this.NextImage_btn.Size = new System.Drawing.Size(38, 173);
            this.NextImage_btn.TabIndex = 22;
            this.NextImage_btn.UseVisualStyleBackColor = false;
            this.NextImage_btn.Click += new System.EventHandler(this.NextImage_btn_Click);
            // 
            // PrevImage_btn
            // 
            this.PrevImage_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.PrevImage_btn.BackgroundImage = global::TjCaptureCtrlVB.Properties.Resources.last;
            this.PrevImage_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PrevImage_btn.Location = new System.Drawing.Point(0, 144);
            this.PrevImage_btn.Name = "PrevImage_btn";
            this.PrevImage_btn.Size = new System.Drawing.Size(38, 173);
            this.PrevImage_btn.TabIndex = 21;
            this.PrevImage_btn.UseVisualStyleBackColor = false;
            this.PrevImage_btn.Click += new System.EventHandler(this.PrevImage_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(38, 30);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(632, 429);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // ShowFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 561);
            this.Controls.Add(this.NextImage_btn);
            this.Controls.Add(this.PrevImage_btn);
            this.Controls.Add(this.funcPanel);
            this.Controls.Add(this.clearMark);
            this.Controls.Add(this.saveMarkPic);
            this.Controls.Add(this.FrameCaptureFromVideo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.videoSource);
            this.Name = "ShowFrame";
            this.Text = "浏览图像";
            this.funcPanel.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Textbtn;
        private System.Windows.Forms.Button RoundRect;
        private System.Windows.Forms.Button squareRect;
        private System.Windows.Forms.Button Line;
        private System.Windows.Forms.Panel funcPanel;
        private System.Windows.Forms.Button Arrow;
        private System.Windows.Forms.Button clearMark;
        private System.Windows.Forms.Button saveMarkPic;
        private System.Windows.Forms.ToolStripMenuItem 磅6;
        private System.Windows.Forms.ToolStripMenuItem 磅4;
        private System.Windows.Forms.ToolStripMenuItem 磅2;
        private System.Windows.Forms.ToolStripMenuItem LineWidthItems;
        private System.Windows.Forms.ToolStripMenuItem 绿色;
        private System.Windows.Forms.ToolStripMenuItem 红色;
        private System.Windows.Forms.ToolStripMenuItem 黄色;
        private System.Windows.Forms.ToolStripMenuItem 白色;
        private System.Windows.Forms.ToolStripMenuItem 蓝色;
        private System.Windows.Forms.ToolStripMenuItem 黑色;
        private System.Windows.Forms.ToolStripMenuItem ColorItems;
        private System.Windows.Forms.ToolStripMenuItem 部位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圆形;
        private System.Windows.Forms.ToolStripMenuItem 文字;
        private System.Windows.Forms.ToolStripMenuItem 圆角矩形;
        private System.Windows.Forms.ToolStripMenuItem 方角矩形;
        private System.Windows.Forms.ToolStripMenuItem 箭头;
        private System.Windows.Forms.ToolStripMenuItem 直线;
        private System.Windows.Forms.ToolStripMenuItem 光标;
        private System.Windows.Forms.ToolStripMenuItem ShapeItems;
        private System.Windows.Forms.Button FrameCaptureFromVideo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private AForge.Controls.VideoSourcePlayer videoSource;
        private System.Windows.Forms.Button PrevImage_btn;
        private System.Windows.Forms.Button NextImage_btn;
    }
}