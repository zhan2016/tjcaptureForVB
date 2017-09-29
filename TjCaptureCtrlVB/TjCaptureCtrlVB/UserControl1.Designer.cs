using System.Windows.Forms;
using TjCaptureCtrlVB.userControl;
namespace TjCaptureCtrlVB
{
    partial class UserControl1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                // your clean up code here
                if (this.capturecontainer.Controls.Count >= 1)
                {
                    foreach (Control ctrl in this.capturecontainer.Controls)
                    {
                        (ctrl as captureFrame).releaseVideo();
                    }
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.opendicomfileTest = new System.Windows.Forms.Button();
            this.openVedio_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // opendicomfileTest
            // 
            this.opendicomfileTest.Location = new System.Drawing.Point(266, 14);
            this.opendicomfileTest.Name = "opendicomfileTest";
            this.opendicomfileTest.Size = new System.Drawing.Size(85, 38);
            this.opendicomfileTest.TabIndex = 18;
            this.opendicomfileTest.Text = "打开一张dicom图";
            this.opendicomfileTest.UseVisualStyleBackColor = true;
            this.opendicomfileTest.Click += new System.EventHandler(this.opendicomfileTest_Click);
            // 
            // openVedio_btn
            // 
            this.openVedio_btn.Location = new System.Drawing.Point(377, 12);
            this.openVedio_btn.Name = "openVedio_btn";
            this.openVedio_btn.Size = new System.Drawing.Size(128, 39);
            this.openVedio_btn.TabIndex = 19;
            this.openVedio_btn.Text = "打开一个采集的视频";
            this.openVedio_btn.UseVisualStyleBackColor = true;
            this.openVedio_btn.Click += new System.EventHandler(this.openVedio_btn_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.openVedio_btn);
            this.Controls.Add(this.opendicomfileTest);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(917, 566);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Button opendicomfileTest;
        private Button openVedio_btn;

    }
}
