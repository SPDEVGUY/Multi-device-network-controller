namespace NetworkController.Plugin.Kinect
{
    partial class KinectSkeleViewer
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
            this.frontViewPB = new System.Windows.Forms.PictureBox();
            this.sideViewPB = new System.Windows.Forms.PictureBox();
            this.topVewPB = new System.Windows.Forms.PictureBox();
            this.startRecordingB = new System.Windows.Forms.Button();
            this.stopRecordingB = new System.Windows.Forms.Button();
            this.startSensingB = new System.Windows.Forms.Button();
            this.stopSensingB = new System.Windows.Forms.Button();
            this.stopPlayingB = new System.Windows.Forms.Button();
            this.startPlayingB = new System.Windows.Forms.Button();
            this.loadRecordingB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.frontViewPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sideViewPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topVewPB)).BeginInit();
            this.SuspendLayout();
            // 
            // frontViewPB
            // 
            this.frontViewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frontViewPB.Location = new System.Drawing.Point(12, 12);
            this.frontViewPB.Name = "frontViewPB";
            this.frontViewPB.Size = new System.Drawing.Size(555, 664);
            this.frontViewPB.TabIndex = 0;
            this.frontViewPB.TabStop = false;
            // 
            // sideViewPB
            // 
            this.sideViewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sideViewPB.Location = new System.Drawing.Point(573, 12);
            this.sideViewPB.Name = "sideViewPB";
            this.sideViewPB.Size = new System.Drawing.Size(511, 330);
            this.sideViewPB.TabIndex = 1;
            this.sideViewPB.TabStop = false;
            // 
            // topVewPB
            // 
            this.topVewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topVewPB.Location = new System.Drawing.Point(573, 348);
            this.topVewPB.Name = "topVewPB";
            this.topVewPB.Size = new System.Drawing.Size(511, 328);
            this.topVewPB.TabIndex = 2;
            this.topVewPB.TabStop = false;
            // 
            // startRecordingB
            // 
            this.startRecordingB.Location = new System.Drawing.Point(12, 682);
            this.startRecordingB.Name = "startRecordingB";
            this.startRecordingB.Size = new System.Drawing.Size(128, 32);
            this.startRecordingB.TabIndex = 3;
            this.startRecordingB.Text = "Start Recording";
            this.startRecordingB.UseVisualStyleBackColor = true;
            this.startRecordingB.Click += new System.EventHandler(this.startRecordingB_Click);
            // 
            // stopRecordingB
            // 
            this.stopRecordingB.Enabled = false;
            this.stopRecordingB.Location = new System.Drawing.Point(146, 683);
            this.stopRecordingB.Name = "stopRecordingB";
            this.stopRecordingB.Size = new System.Drawing.Size(128, 32);
            this.stopRecordingB.TabIndex = 4;
            this.stopRecordingB.Text = "Stop Recording";
            this.stopRecordingB.UseVisualStyleBackColor = true;
            this.stopRecordingB.Click += new System.EventHandler(this.stopRecordingB_Click);
            // 
            // startSensingB
            // 
            this.startSensingB.Enabled = false;
            this.startSensingB.Location = new System.Drawing.Point(328, 682);
            this.startSensingB.Name = "startSensingB";
            this.startSensingB.Size = new System.Drawing.Size(128, 32);
            this.startSensingB.TabIndex = 5;
            this.startSensingB.Text = "Start Sensing";
            this.startSensingB.UseVisualStyleBackColor = true;
            this.startSensingB.Click += new System.EventHandler(this.startSensingB_Click);
            // 
            // stopSensingB
            // 
            this.stopSensingB.Location = new System.Drawing.Point(462, 682);
            this.stopSensingB.Name = "stopSensingB";
            this.stopSensingB.Size = new System.Drawing.Size(128, 32);
            this.stopSensingB.TabIndex = 6;
            this.stopSensingB.Text = "Stop Sensing";
            this.stopSensingB.UseVisualStyleBackColor = true;
            this.stopSensingB.Click += new System.EventHandler(this.stopSensingB_Click);
            // 
            // stopPlayingB
            // 
            this.stopPlayingB.Enabled = false;
            this.stopPlayingB.Location = new System.Drawing.Point(786, 682);
            this.stopPlayingB.Name = "stopPlayingB";
            this.stopPlayingB.Size = new System.Drawing.Size(128, 32);
            this.stopPlayingB.TabIndex = 8;
            this.stopPlayingB.Text = "Stop Playing";
            this.stopPlayingB.UseVisualStyleBackColor = true;
            this.stopPlayingB.Click += new System.EventHandler(this.stopPlayingB_Click);
            // 
            // startPlayingB
            // 
            this.startPlayingB.Location = new System.Drawing.Point(652, 682);
            this.startPlayingB.Name = "startPlayingB";
            this.startPlayingB.Size = new System.Drawing.Size(128, 32);
            this.startPlayingB.TabIndex = 7;
            this.startPlayingB.Text = "Start Playing";
            this.startPlayingB.UseVisualStyleBackColor = true;
            this.startPlayingB.Click += new System.EventHandler(this.startPlayingB_Click);
            // 
            // loadRecordingB
            // 
            this.loadRecordingB.Location = new System.Drawing.Point(956, 682);
            this.loadRecordingB.Name = "loadRecordingB";
            this.loadRecordingB.Size = new System.Drawing.Size(128, 32);
            this.loadRecordingB.TabIndex = 9;
            this.loadRecordingB.Text = "Load";
            this.loadRecordingB.UseVisualStyleBackColor = true;
            this.loadRecordingB.Click += new System.EventHandler(this.loadRecordingB_Click);
            // 
            // KinectSkeleViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 727);
            this.Controls.Add(this.loadRecordingB);
            this.Controls.Add(this.stopPlayingB);
            this.Controls.Add(this.startPlayingB);
            this.Controls.Add(this.stopSensingB);
            this.Controls.Add(this.startSensingB);
            this.Controls.Add(this.stopRecordingB);
            this.Controls.Add(this.startRecordingB);
            this.Controls.Add(this.topVewPB);
            this.Controls.Add(this.sideViewPB);
            this.Controls.Add(this.frontViewPB);
            this.Name = "KinectSkeleViewer";
            this.Text = "KinectSkeleViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KinectSkeleViewer_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.frontViewPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sideViewPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topVewPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox frontViewPB;
        private System.Windows.Forms.PictureBox sideViewPB;
        private System.Windows.Forms.PictureBox topVewPB;
        private System.Windows.Forms.Button startRecordingB;
        private System.Windows.Forms.Button stopRecordingB;
        private System.Windows.Forms.Button startSensingB;
        private System.Windows.Forms.Button stopSensingB;
        private System.Windows.Forms.Button stopPlayingB;
        private System.Windows.Forms.Button startPlayingB;
        private System.Windows.Forms.Button loadRecordingB;
    }
}