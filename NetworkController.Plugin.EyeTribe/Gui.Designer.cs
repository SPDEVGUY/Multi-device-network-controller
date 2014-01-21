namespace NetworkController.Plugin.EyeTribe
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
            this.chkShowGaze = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCalibrate = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblConnected = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pbEyes = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pbGaze = new System.Windows.Forms.PictureBox();
            this.chkFixated = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPresence = new System.Windows.Forms.CheckBox();
            this.chkGaze = new System.Windows.Forms.CheckBox();
            this.chkFailed = new System.Windows.Forms.CheckBox();
            this.chkLostTracking = new System.Windows.Forms.CheckBox();
            this.chkLeftEye = new System.Windows.Forms.CheckBox();
            this.chkRightEye = new System.Windows.Forms.CheckBox();
            this.lblRawX = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRawY = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSmoothY = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblSmoothX = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblLeftRawY = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblLeftRawX = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblLeftSmoothY = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblLeftSmoothX = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblLeftPupilSize = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblRightPupilSize = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblRightSmoothY = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblRightSmoothX = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblRightRawY = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblRightRawX = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblLeftPupilY = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblLeftPupilX = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblRightPupilY = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblRightPupilX = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblHeadZ = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblHeadX = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblHeadY = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btnSweetSpot = new System.Windows.Forms.Button();
            this.btnOffset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbEyes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGaze)).BeginInit();
            this.SuspendLayout();
            // 
            // chkShowGaze
            // 
            this.chkShowGaze.AutoSize = true;
            this.chkShowGaze.Location = new System.Drawing.Point(763, 12);
            this.chkShowGaze.Name = "chkShowGaze";
            this.chkShowGaze.Size = new System.Drawing.Size(101, 17);
            this.chkShowGaze.TabIndex = 0;
            this.chkShowGaze.Text = "Show Gaze Dot";
            this.chkShowGaze.UseVisualStyleBackColor = true;
            this.chkShowGaze.CheckedChanged += new System.EventHandler(this.chkShowGaze_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Calibration Rating:";
            // 
            // lblCalibrate
            // 
            this.lblCalibrate.AutoSize = true;
            this.lblCalibrate.Location = new System.Drawing.Point(125, 28);
            this.lblCalibrate.Name = "lblCalibrate";
            this.lblCalibrate.Size = new System.Drawing.Size(55, 13);
            this.lblCalibrate.TabIndex = 2;
            this.lblCalibrate.TabStop = true;
            this.lblCalibrate.Text = "linkLabel1";
            this.lblCalibrate.Click += new System.EventHandler(this.lblCalibrate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Connected:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblConnected
            // 
            this.lblConnected.AutoSize = true;
            this.lblConnected.Location = new System.Drawing.Point(125, 9);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(89, 13);
            this.lblConnected.TabIndex = 4;
            this.lblConnected.Text = "Connection State";
            this.lblConnected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbEyes
            // 
            this.pbEyes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbEyes.Location = new System.Drawing.Point(15, 114);
            this.pbEyes.Name = "pbEyes";
            this.pbEyes.Size = new System.Drawing.Size(200, 200);
            this.pbEyes.TabIndex = 5;
            this.pbEyes.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Eyes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Gaze";
            // 
            // pbGaze
            // 
            this.pbGaze.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGaze.Location = new System.Drawing.Point(221, 114);
            this.pbGaze.Name = "pbGaze";
            this.pbGaze.Size = new System.Drawing.Size(200, 200);
            this.pbGaze.TabIndex = 7;
            this.pbGaze.TabStop = false;
            // 
            // chkFixated
            // 
            this.chkFixated.AutoSize = true;
            this.chkFixated.Enabled = false;
            this.chkFixated.Location = new System.Drawing.Point(430, 177);
            this.chkFixated.Name = "chkFixated";
            this.chkFixated.Size = new System.Drawing.Size(60, 17);
            this.chkFixated.TabIndex = 9;
            this.chkFixated.Text = "Fixated";
            this.chkFixated.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(427, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "States";
            // 
            // chkPresence
            // 
            this.chkPresence.AutoSize = true;
            this.chkPresence.Enabled = false;
            this.chkPresence.Location = new System.Drawing.Point(430, 131);
            this.chkPresence.Name = "chkPresence";
            this.chkPresence.Size = new System.Drawing.Size(71, 17);
            this.chkPresence.TabIndex = 11;
            this.chkPresence.Text = "Presence";
            this.chkPresence.UseVisualStyleBackColor = true;
            // 
            // chkGaze
            // 
            this.chkGaze.AutoSize = true;
            this.chkGaze.Enabled = false;
            this.chkGaze.Location = new System.Drawing.Point(430, 154);
            this.chkGaze.Name = "chkGaze";
            this.chkGaze.Size = new System.Drawing.Size(51, 17);
            this.chkGaze.TabIndex = 12;
            this.chkGaze.Text = "Gaze";
            this.chkGaze.UseVisualStyleBackColor = true;
            // 
            // chkFailed
            // 
            this.chkFailed.AutoSize = true;
            this.chkFailed.Enabled = false;
            this.chkFailed.Location = new System.Drawing.Point(430, 200);
            this.chkFailed.Name = "chkFailed";
            this.chkFailed.Size = new System.Drawing.Size(54, 17);
            this.chkFailed.TabIndex = 13;
            this.chkFailed.Text = "Failed";
            this.chkFailed.UseVisualStyleBackColor = true;
            // 
            // chkLostTracking
            // 
            this.chkLostTracking.AutoSize = true;
            this.chkLostTracking.Enabled = false;
            this.chkLostTracking.Location = new System.Drawing.Point(430, 223);
            this.chkLostTracking.Name = "chkLostTracking";
            this.chkLostTracking.Size = new System.Drawing.Size(91, 17);
            this.chkLostTracking.TabIndex = 14;
            this.chkLostTracking.Text = "Lost Tracking";
            this.chkLostTracking.UseVisualStyleBackColor = true;
            // 
            // chkLeftEye
            // 
            this.chkLeftEye.AutoSize = true;
            this.chkLeftEye.Enabled = false;
            this.chkLeftEye.Location = new System.Drawing.Point(430, 246);
            this.chkLeftEye.Name = "chkLeftEye";
            this.chkLeftEye.Size = new System.Drawing.Size(110, 17);
            this.chkLeftEye.TabIndex = 15;
            this.chkLeftEye.Text = "Left Eye Tracking";
            this.chkLeftEye.UseVisualStyleBackColor = true;
            // 
            // chkRightEye
            // 
            this.chkRightEye.AutoSize = true;
            this.chkRightEye.Enabled = false;
            this.chkRightEye.Location = new System.Drawing.Point(430, 269);
            this.chkRightEye.Name = "chkRightEye";
            this.chkRightEye.Size = new System.Drawing.Size(117, 17);
            this.chkRightEye.TabIndex = 16;
            this.chkRightEye.Text = "Right Eye Tracking";
            this.chkRightEye.UseVisualStyleBackColor = true;
            // 
            // lblRawX
            // 
            this.lblRawX.AutoSize = true;
            this.lblRawX.Location = new System.Drawing.Point(657, 115);
            this.lblRawX.Name = "lblRawX";
            this.lblRawX.Size = new System.Drawing.Size(13, 13);
            this.lblRawX.TabIndex = 17;
            this.lblRawX.Text = "0";
            this.lblRawX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(576, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Raw X";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(562, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Values";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(576, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Raw Y";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRawY
            // 
            this.lblRawY.AutoSize = true;
            this.lblRawY.Location = new System.Drawing.Point(657, 128);
            this.lblRawY.Name = "lblRawY";
            this.lblRawY.Size = new System.Drawing.Size(13, 13);
            this.lblRawY.TabIndex = 20;
            this.lblRawY.Text = "0";
            this.lblRawY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(728, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Smooth Y";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSmoothY
            // 
            this.lblSmoothY.AutoSize = true;
            this.lblSmoothY.Location = new System.Drawing.Point(809, 127);
            this.lblSmoothY.Name = "lblSmoothY";
            this.lblSmoothY.Size = new System.Drawing.Size(13, 13);
            this.lblSmoothY.TabIndex = 24;
            this.lblSmoothY.Text = "0";
            this.lblSmoothY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(728, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Smooth X";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSmoothX
            // 
            this.lblSmoothX.AutoSize = true;
            this.lblSmoothX.Location = new System.Drawing.Point(809, 114);
            this.lblSmoothX.Name = "lblSmoothX";
            this.lblSmoothX.Size = new System.Drawing.Size(13, 13);
            this.lblSmoothX.TabIndex = 22;
            this.lblSmoothX.Text = "0";
            this.lblSmoothX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(576, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Raw Y";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLeftRawY
            // 
            this.lblLeftRawY.AutoSize = true;
            this.lblLeftRawY.Location = new System.Drawing.Point(657, 197);
            this.lblLeftRawY.Name = "lblLeftRawY";
            this.lblLeftRawY.Size = new System.Drawing.Size(13, 13);
            this.lblLeftRawY.TabIndex = 28;
            this.lblLeftRawY.Text = "0";
            this.lblLeftRawY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(576, 183);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Raw X";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLeftRawX
            // 
            this.lblLeftRawX.AutoSize = true;
            this.lblLeftRawX.Location = new System.Drawing.Point(657, 184);
            this.lblLeftRawX.Name = "lblLeftRawX";
            this.lblLeftRawX.Size = new System.Drawing.Size(13, 13);
            this.lblLeftRawX.TabIndex = 26;
            this.lblLeftRawX.Text = "0";
            this.lblLeftRawX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(562, 160);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Left Eye";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(576, 229);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Smooth Y";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLeftSmoothY
            // 
            this.lblLeftSmoothY.AutoSize = true;
            this.lblLeftSmoothY.Location = new System.Drawing.Point(657, 230);
            this.lblLeftSmoothY.Name = "lblLeftSmoothY";
            this.lblLeftSmoothY.Size = new System.Drawing.Size(13, 13);
            this.lblLeftSmoothY.TabIndex = 33;
            this.lblLeftSmoothY.Text = "0";
            this.lblLeftSmoothY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(576, 216);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 13);
            this.label18.TabIndex = 32;
            this.label18.Text = "Smooth X";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLeftSmoothX
            // 
            this.lblLeftSmoothX.AutoSize = true;
            this.lblLeftSmoothX.Location = new System.Drawing.Point(657, 217);
            this.lblLeftSmoothX.Name = "lblLeftSmoothX";
            this.lblLeftSmoothX.Size = new System.Drawing.Size(13, 13);
            this.lblLeftSmoothX.TabIndex = 31;
            this.lblLeftSmoothX.Text = "0";
            this.lblLeftSmoothX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(576, 252);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 13);
            this.label20.TabIndex = 36;
            this.label20.Text = "Pupil Size";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLeftPupilSize
            // 
            this.lblLeftPupilSize.AutoSize = true;
            this.lblLeftPupilSize.Location = new System.Drawing.Point(657, 253);
            this.lblLeftPupilSize.Name = "lblLeftPupilSize";
            this.lblLeftPupilSize.Size = new System.Drawing.Size(13, 13);
            this.lblLeftPupilSize.TabIndex = 35;
            this.lblLeftPupilSize.Text = "0";
            this.lblLeftPupilSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(728, 252);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 13);
            this.label22.TabIndex = 47;
            this.label22.Text = "Pupil Size";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightPupilSize
            // 
            this.lblRightPupilSize.AutoSize = true;
            this.lblRightPupilSize.Location = new System.Drawing.Point(809, 253);
            this.lblRightPupilSize.Name = "lblRightPupilSize";
            this.lblRightPupilSize.Size = new System.Drawing.Size(13, 13);
            this.lblRightPupilSize.TabIndex = 46;
            this.lblRightPupilSize.Text = "0";
            this.lblRightPupilSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(728, 229);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 13);
            this.label24.TabIndex = 45;
            this.label24.Text = "Smooth Y";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightSmoothY
            // 
            this.lblRightSmoothY.AutoSize = true;
            this.lblRightSmoothY.Location = new System.Drawing.Point(809, 230);
            this.lblRightSmoothY.Name = "lblRightSmoothY";
            this.lblRightSmoothY.Size = new System.Drawing.Size(13, 13);
            this.lblRightSmoothY.TabIndex = 44;
            this.lblRightSmoothY.Text = "0";
            this.lblRightSmoothY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(728, 216);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 13);
            this.label26.TabIndex = 43;
            this.label26.Text = "Smooth X";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightSmoothX
            // 
            this.lblRightSmoothX.AutoSize = true;
            this.lblRightSmoothX.Location = new System.Drawing.Point(809, 217);
            this.lblRightSmoothX.Name = "lblRightSmoothX";
            this.lblRightSmoothX.Size = new System.Drawing.Size(13, 13);
            this.lblRightSmoothX.TabIndex = 42;
            this.lblRightSmoothX.Text = "0";
            this.lblRightSmoothX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(714, 160);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 13);
            this.label28.TabIndex = 41;
            this.label28.Text = "Right Eye";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(728, 196);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(39, 13);
            this.label29.TabIndex = 40;
            this.label29.Text = "Raw Y";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightRawY
            // 
            this.lblRightRawY.AutoSize = true;
            this.lblRightRawY.Location = new System.Drawing.Point(809, 197);
            this.lblRightRawY.Name = "lblRightRawY";
            this.lblRightRawY.Size = new System.Drawing.Size(13, 13);
            this.lblRightRawY.TabIndex = 39;
            this.lblRightRawY.Text = "0";
            this.lblRightRawY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(728, 183);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(39, 13);
            this.label31.TabIndex = 38;
            this.label31.Text = "Raw X";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightRawX
            // 
            this.lblRightRawX.AutoSize = true;
            this.lblRightRawX.Location = new System.Drawing.Point(809, 184);
            this.lblRightRawX.Name = "lblRightRawX";
            this.lblRightRawX.Size = new System.Drawing.Size(13, 13);
            this.lblRightRawX.TabIndex = 37;
            this.lblRightRawX.Text = "0";
            this.lblRightRawX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(576, 291);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 51;
            this.label10.Text = "Pupil Y";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLeftPupilY
            // 
            this.lblLeftPupilY.AutoSize = true;
            this.lblLeftPupilY.Location = new System.Drawing.Point(657, 292);
            this.lblLeftPupilY.Name = "lblLeftPupilY";
            this.lblLeftPupilY.Size = new System.Drawing.Size(13, 13);
            this.lblLeftPupilY.TabIndex = 50;
            this.lblLeftPupilY.Text = "0";
            this.lblLeftPupilY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(576, 278);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 13);
            this.label17.TabIndex = 49;
            this.label17.Text = "Pupil X";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLeftPupilX
            // 
            this.lblLeftPupilX.AutoSize = true;
            this.lblLeftPupilX.Location = new System.Drawing.Point(657, 279);
            this.lblLeftPupilX.Name = "lblLeftPupilX";
            this.lblLeftPupilX.Size = new System.Drawing.Size(13, 13);
            this.lblLeftPupilX.TabIndex = 48;
            this.lblLeftPupilX.Text = "0";
            this.lblLeftPupilX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(728, 292);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(40, 13);
            this.label21.TabIndex = 55;
            this.label21.Text = "Pupil Y";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightPupilY
            // 
            this.lblRightPupilY.AutoSize = true;
            this.lblRightPupilY.Location = new System.Drawing.Point(809, 293);
            this.lblRightPupilY.Name = "lblRightPupilY";
            this.lblRightPupilY.Size = new System.Drawing.Size(13, 13);
            this.lblRightPupilY.TabIndex = 54;
            this.lblRightPupilY.Text = "0";
            this.lblRightPupilY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(728, 279);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(40, 13);
            this.label25.TabIndex = 53;
            this.label25.Text = "Pupil X";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightPupilX
            // 
            this.lblRightPupilX.AutoSize = true;
            this.lblRightPupilX.Location = new System.Drawing.Point(809, 280);
            this.lblRightPupilX.Name = "lblRightPupilX";
            this.lblRightPupilX.Size = new System.Drawing.Size(13, 13);
            this.lblRightPupilX.TabIndex = 52;
            this.lblRightPupilX.Text = "0";
            this.lblRightPupilX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(576, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 57;
            this.label14.Text = "Head Z";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeadZ
            // 
            this.lblHeadZ.AutoSize = true;
            this.lblHeadZ.Location = new System.Drawing.Point(657, 72);
            this.lblHeadZ.Name = "lblHeadZ";
            this.lblHeadZ.Size = new System.Drawing.Size(13, 13);
            this.lblHeadZ.TabIndex = 56;
            this.lblHeadZ.Text = "0";
            this.lblHeadZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(576, 46);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 13);
            this.label19.TabIndex = 59;
            this.label19.Text = "Head X";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeadX
            // 
            this.lblHeadX.AutoSize = true;
            this.lblHeadX.Location = new System.Drawing.Point(657, 46);
            this.lblHeadX.Name = "lblHeadX";
            this.lblHeadX.Size = new System.Drawing.Size(13, 13);
            this.lblHeadX.TabIndex = 58;
            this.lblHeadX.Text = "0";
            this.lblHeadX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(576, 59);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(43, 13);
            this.label27.TabIndex = 61;
            this.label27.Text = "Head Y";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeadY
            // 
            this.lblHeadY.AutoSize = true;
            this.lblHeadY.Location = new System.Drawing.Point(657, 59);
            this.lblHeadY.Name = "lblHeadY";
            this.lblHeadY.Size = new System.Drawing.Size(13, 13);
            this.lblHeadY.TabIndex = 60;
            this.lblHeadY.Text = "0";
            this.lblHeadY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(562, 28);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(78, 13);
            this.label23.TabIndex = 62;
            this.label23.Text = "Head Tracking";
            // 
            // btnSweetSpot
            // 
            this.btnSweetSpot.Location = new System.Drawing.Point(274, 17);
            this.btnSweetSpot.Name = "btnSweetSpot";
            this.btnSweetSpot.Size = new System.Drawing.Size(75, 23);
            this.btnSweetSpot.TabIndex = 63;
            this.btnSweetSpot.Text = "Sweet Spot";
            this.btnSweetSpot.UseVisualStyleBackColor = true;
            this.btnSweetSpot.Click += new System.EventHandler(this.btnSweetSpot_Click);
            // 
            // btnOffset
            // 
            this.btnOffset.Location = new System.Drawing.Point(368, 17);
            this.btnOffset.Name = "btnOffset";
            this.btnOffset.Size = new System.Drawing.Size(75, 23);
            this.btnOffset.TabIndex = 64;
            this.btnOffset.Text = "Set Deltas";
            this.btnOffset.UseVisualStyleBackColor = true;
            this.btnOffset.Click += new System.EventHandler(this.btnOffset_Click);
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 330);
            this.Controls.Add(this.btnOffset);
            this.Controls.Add(this.btnSweetSpot);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.lblHeadY);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lblHeadX);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblHeadZ);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblRightPupilY);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.lblRightPupilX);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblLeftPupilY);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lblLeftPupilX);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblRightPupilSize);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lblRightSmoothY);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lblRightSmoothX);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.lblRightRawY);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.lblRightRawX);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lblLeftPupilSize);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lblLeftSmoothY);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblLeftSmoothX);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblLeftRawY);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblLeftRawX);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblSmoothY);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblSmoothX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblRawY);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblRawX);
            this.Controls.Add(this.chkRightEye);
            this.Controls.Add(this.chkLeftEye);
            this.Controls.Add(this.chkLostTracking);
            this.Controls.Add(this.chkFailed);
            this.Controls.Add(this.chkGaze);
            this.Controls.Add(this.chkPresence);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkFixated);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbGaze);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbEyes);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCalibrate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkShowGaze);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Gui";
            this.Text = "EyeTribe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Gui_FormClosed);
            this.Load += new System.EventHandler(this.Gui_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbEyes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGaze)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkShowGaze;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblCalibrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pbEyes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbGaze;
        private System.Windows.Forms.CheckBox chkFixated;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkPresence;
        private System.Windows.Forms.CheckBox chkGaze;
        private System.Windows.Forms.CheckBox chkFailed;
        private System.Windows.Forms.CheckBox chkLostTracking;
        private System.Windows.Forms.CheckBox chkLeftEye;
        private System.Windows.Forms.CheckBox chkRightEye;
        private System.Windows.Forms.Label lblRawX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRawY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSmoothY;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSmoothX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLeftRawY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblLeftRawX;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblLeftSmoothY;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblLeftSmoothX;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblLeftPupilSize;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblRightPupilSize;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblRightSmoothY;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblRightSmoothX;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblRightRawY;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblRightRawX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblLeftPupilY;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblLeftPupilX;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblRightPupilY;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblRightPupilX;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblHeadZ;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblHeadX;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblHeadY;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnSweetSpot;
        private System.Windows.Forms.Button btnOffset;
    }
}