namespace NetworkController.Server.FormsApp
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdTcp = new System.Windows.Forms.RadioButton();
            this.rdUdp = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblPackets = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBandwidth = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblBandwidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblPackets);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.rdUdp);
            this.groupBox1.Controls.Add(this.rdTcp);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(407, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network Output";
            // 
            // rdTcp
            // 
            this.rdTcp.AutoSize = true;
            this.rdTcp.Enabled = false;
            this.rdTcp.Location = new System.Drawing.Point(66, 19);
            this.rdTcp.Name = "rdTcp";
            this.rdTcp.Size = new System.Drawing.Size(46, 17);
            this.rdTcp.TabIndex = 0;
            this.rdTcp.TabStop = true;
            this.rdTcp.Text = "TCP";
            this.rdTcp.UseVisualStyleBackColor = true;
            // 
            // rdUdp
            // 
            this.rdUdp.AutoSize = true;
            this.rdUdp.Checked = true;
            this.rdUdp.Location = new System.Drawing.Point(12, 19);
            this.rdUdp.Name = "rdUdp";
            this.rdUdp.Size = new System.Drawing.Size(48, 17);
            this.rdUdp.TabIndex = 1;
            this.rdUdp.TabStop = true;
            this.rdUdp.Text = "UDP";
            this.rdUdp.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(120, 19);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            9050,
            0,
            0,
            0});
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(200, 16);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblPackets
            // 
            this.lblPackets.AutoSize = true;
            this.lblPackets.Location = new System.Drawing.Point(374, 16);
            this.lblPackets.Name = "lblPackets";
            this.lblPackets.Size = new System.Drawing.Size(13, 13);
            this.lblPackets.TabIndex = 4;
            this.lblPackets.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Packets:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Bandwidth";
            // 
            // lblBandwidth
            // 
            this.lblBandwidth.AutoSize = true;
            this.lblBandwidth.Location = new System.Drawing.Point(374, 29);
            this.lblBandwidth.Name = "lblBandwidth";
            this.lblBandwidth.Size = new System.Drawing.Size(13, 13);
            this.lblBandwidth.TabIndex = 6;
            this.lblBandwidth.Text = "0";
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 489);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Network Controller Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RadioButton rdUdp;
        private System.Windows.Forms.RadioButton rdTcp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBandwidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPackets;
        private System.Windows.Forms.Timer updateTimer;
    }
}

