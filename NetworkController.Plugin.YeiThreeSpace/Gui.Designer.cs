namespace NetworkController.Plugin.YeiThreeSpace
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
            this.btnTare = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.lstSensors = new System.Windows.Forms.ListView();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkGravity = new System.Windows.Forms.CheckBox();
            this.chkCompass = new System.Windows.Forms.CheckBox();
            this.chkGyro = new System.Windows.Forms.CheckBox();
            this.chkRotation = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnTare
            // 
            this.btnTare.Enabled = false;
            this.btnTare.Location = new System.Drawing.Point(78, 12);
            this.btnTare.Name = "btnTare";
            this.btnTare.Size = new System.Drawing.Size(168, 23);
            this.btnTare.TabIndex = 2;
            this.btnTare.Text = "Tare Sensor";
            this.btnTare.UseVisualStyleBackColor = true;
            this.btnTare.Click += new System.EventHandler(this.btnTare_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // lstSensors
            // 
            this.lstSensors.Location = new System.Drawing.Point(12, 12);
            this.lstSensors.Name = "lstSensors";
            this.lstSensors.Size = new System.Drawing.Size(60, 230);
            this.lstSensors.TabIndex = 3;
            this.lstSensors.UseCompatibleStateImageBehavior = false;
            this.lstSensors.View = System.Windows.Forms.View.List;
            this.lstSensors.SelectedIndexChanged += new System.EventHandler(this.lstSensors_SelectedIndexChanged);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Enabled = false;
            this.chkEnabled.Location = new System.Drawing.Point(78, 41);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(101, 17);
            this.chkEnabled.TabIndex = 4;
            this.chkEnabled.Text = "Sensor Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // chkGravity
            // 
            this.chkGravity.AutoSize = true;
            this.chkGravity.Enabled = false;
            this.chkGravity.Location = new System.Drawing.Point(78, 64);
            this.chkGravity.Name = "chkGravity";
            this.chkGravity.Size = new System.Drawing.Size(136, 17);
            this.chkGravity.TabIndex = 5;
            this.chkGravity.Text = "Accelerometer Enabled";
            this.chkGravity.UseVisualStyleBackColor = true;
            this.chkGravity.CheckedChanged += new System.EventHandler(this.chkGravity_CheckedChanged);
            // 
            // chkCompass
            // 
            this.chkCompass.AutoSize = true;
            this.chkCompass.Enabled = false;
            this.chkCompass.Location = new System.Drawing.Point(78, 87);
            this.chkCompass.Name = "chkCompass";
            this.chkCompass.Size = new System.Drawing.Size(111, 17);
            this.chkCompass.TabIndex = 6;
            this.chkCompass.Text = "Compass Enabled";
            this.chkCompass.UseVisualStyleBackColor = true;
            this.chkCompass.CheckedChanged += new System.EventHandler(this.chkCompass_CheckedChanged);
            // 
            // chkGyro
            // 
            this.chkGyro.AutoSize = true;
            this.chkGyro.Enabled = false;
            this.chkGyro.Location = new System.Drawing.Point(78, 110);
            this.chkGyro.Name = "chkGyro";
            this.chkGyro.Size = new System.Drawing.Size(90, 17);
            this.chkGyro.TabIndex = 7;
            this.chkGyro.Text = "Gryo Enabled";
            this.chkGyro.UseVisualStyleBackColor = true;
            this.chkGyro.CheckedChanged += new System.EventHandler(this.chkGyro_CheckedChanged);
            // 
            // chkRotation
            // 
            this.chkRotation.AutoSize = true;
            this.chkRotation.Enabled = false;
            this.chkRotation.Location = new System.Drawing.Point(78, 133);
            this.chkRotation.Name = "chkRotation";
            this.chkRotation.Size = new System.Drawing.Size(108, 17);
            this.chkRotation.TabIndex = 8;
            this.chkRotation.Text = "Rotation Enabled";
            this.chkRotation.UseVisualStyleBackColor = true;
            this.chkRotation.CheckedChanged += new System.EventHandler(this.chkRotation_CheckedChanged);
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 251);
            this.Controls.Add(this.chkRotation);
            this.Controls.Add(this.chkGyro);
            this.Controls.Add(this.chkCompass);
            this.Controls.Add(this.chkGravity);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.lstSensors);
            this.Controls.Add(this.btnTare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Gui";
            this.Text = "YEI Three Space";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Gui_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTare;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ListView lstSensors;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkGravity;
        private System.Windows.Forms.CheckBox chkCompass;
        private System.Windows.Forms.CheckBox chkGyro;
        private System.Windows.Forms.CheckBox chkRotation;
    }
}