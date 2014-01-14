namespace NetworkController.Plugin.Kinect
{
    partial class Gui
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
            this.components = new System.ComponentModel.Container();
            this.frontViewPB = new System.Windows.Forms.PictureBox();
            this.sideViewPB = new System.Windows.Forms.PictureBox();
            this.topVewPB = new System.Windows.Forms.PictureBox();
            this.viewRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.sendPoints = new System.Windows.Forms.CheckedListBox();
            this.chkSendLocalizedPositions = new System.Windows.Forms.CheckBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.frontViewPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sideViewPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topVewPB)).BeginInit();
            this.SuspendLayout();
            // 
            // frontViewPB
            // 
            this.frontViewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frontViewPB.Location = new System.Drawing.Point(9, 10);
            this.frontViewPB.Margin = new System.Windows.Forms.Padding(2);
            this.frontViewPB.Name = "frontViewPB";
            this.frontViewPB.Size = new System.Drawing.Size(417, 540);
            this.frontViewPB.TabIndex = 0;
            this.frontViewPB.TabStop = false;
            // 
            // sideViewPB
            // 
            this.sideViewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sideViewPB.Location = new System.Drawing.Point(430, 10);
            this.sideViewPB.Margin = new System.Windows.Forms.Padding(2);
            this.sideViewPB.Name = "sideViewPB";
            this.sideViewPB.Size = new System.Drawing.Size(384, 268);
            this.sideViewPB.TabIndex = 1;
            this.sideViewPB.TabStop = false;
            // 
            // topVewPB
            // 
            this.topVewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topVewPB.Location = new System.Drawing.Point(430, 283);
            this.topVewPB.Margin = new System.Windows.Forms.Padding(2);
            this.topVewPB.Name = "topVewPB";
            this.topVewPB.Size = new System.Drawing.Size(384, 267);
            this.topVewPB.TabIndex = 2;
            this.topVewPB.TabStop = false;
            // 
            // viewRefreshTimer
            // 
            this.viewRefreshTimer.Enabled = true;
            this.viewRefreshTimer.Tick += new System.EventHandler(this.viewRefreshTimer_Tick);
            // 
            // sendPoints
            // 
            this.sendPoints.CheckOnClick = true;
            this.sendPoints.FormattingEnabled = true;
            this.sendPoints.Location = new System.Drawing.Point(819, 56);
            this.sendPoints.Name = "sendPoints";
            this.sendPoints.Size = new System.Drawing.Size(154, 469);
            this.sendPoints.TabIndex = 3;
            this.sendPoints.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.sendPoints_ItemCheck);
            // 
            // chkSendLocalizedPositions
            // 
            this.chkSendLocalizedPositions.AutoSize = true;
            this.chkSendLocalizedPositions.Checked = true;
            this.chkSendLocalizedPositions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSendLocalizedPositions.Location = new System.Drawing.Point(819, 33);
            this.chkSendLocalizedPositions.Name = "chkSendLocalizedPositions";
            this.chkSendLocalizedPositions.Size = new System.Drawing.Size(133, 17);
            this.chkSendLocalizedPositions.TabIndex = 5;
            this.chkSendLocalizedPositions.Text = "Localize Joint Postions";
            this.chkSendLocalizedPositions.UseVisualStyleBackColor = true;
            this.chkSendLocalizedPositions.CheckedChanged += new System.EventHandler(this.chkSendLocalizedPositions_CheckedChanged);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(819, 10);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(83, 17);
            this.chkEnabled.TabIndex = 6;
            this.chkEnabled.Text = "Send Points";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(819, 527);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 559);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.chkSendLocalizedPositions);
            this.Controls.Add(this.sendPoints);
            this.Controls.Add(this.topVewPB);
            this.Controls.Add(this.sideViewPB);
            this.Controls.Add(this.frontViewPB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Gui";
            this.Text = "Kinect Viewer";
            this.Activated += new System.EventHandler(this.Gui_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.frontViewPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sideViewPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topVewPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox frontViewPB;
        private System.Windows.Forms.PictureBox sideViewPB;
        private System.Windows.Forms.PictureBox topVewPB;
        private System.Windows.Forms.Timer viewRefreshTimer;
        private System.Windows.Forms.CheckedListBox sendPoints;
        private System.Windows.Forms.CheckBox chkSendLocalizedPositions;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Button btnSave;
    }
}