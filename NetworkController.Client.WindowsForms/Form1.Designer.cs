namespace NetworkController.Client.WindowsForms
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPackets = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.lstButtonStates = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstSliders = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstGestures = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Packets Received:";
            // 
            // txtPackets
            // 
            this.txtPackets.AutoSize = true;
            this.txtPackets.Location = new System.Drawing.Point(185, 9);
            this.txtPackets.Name = "txtPackets";
            this.txtPackets.Size = new System.Drawing.Size(16, 17);
            this.txtPackets.TabIndex = 1;
            this.txtPackets.Text = "0";
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // lstButtonStates
            // 
            this.lstButtonStates.Location = new System.Drawing.Point(11, 21);
            this.lstButtonStates.Name = "lstButtonStates";
            this.lstButtonStates.Size = new System.Drawing.Size(189, 178);
            this.lstButtonStates.TabIndex = 2;
            this.lstButtonStates.UseCompatibleStateImageBehavior = false;
            this.lstButtonStates.View = System.Windows.Forms.View.List;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstButtonStates);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 215);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Button States";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstSliders);
            this.groupBox2.Location = new System.Drawing.Point(231, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(482, 423);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sliders";
            // 
            // lstSliders
            // 
            this.lstSliders.Location = new System.Drawing.Point(11, 21);
            this.lstSliders.Name = "lstSliders";
            this.lstSliders.Size = new System.Drawing.Size(465, 388);
            this.lstSliders.TabIndex = 2;
            this.lstSliders.UseCompatibleStateImageBehavior = false;
            this.lstSliders.View = System.Windows.Forms.View.List;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstGestures);
            this.groupBox3.Location = new System.Drawing.Point(12, 263);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 202);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gestures";
            // 
            // lstGestures
            // 
            this.lstGestures.Location = new System.Drawing.Point(11, 21);
            this.lstGestures.Name = "lstGestures";
            this.lstGestures.Size = new System.Drawing.Size(189, 167);
            this.lstGestures.TabIndex = 2;
            this.lstGestures.UseCompatibleStateImageBehavior = false;
            this.lstGestures.View = System.Windows.Forms.View.List;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 595);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPackets);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Network Controller Test Reciever";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtPackets;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ListView lstButtonStates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstSliders;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lstGestures;
    }
}

