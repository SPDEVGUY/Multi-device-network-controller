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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Buttons", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Sliders", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Gestures", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Pressed", 2);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Buttons", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Unpressed", 3);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Up", 0);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Sliders", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPackets = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.inputList = new System.Windows.Forms.ListView();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.graphPicture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ySelect = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.zSelect = new System.Windows.Forms.ComboBox();
            this.xSelect = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.graphPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Packets Received:";
            // 
            // txtPackets
            // 
            this.txtPackets.AutoSize = true;
            this.txtPackets.Location = new System.Drawing.Point(139, 7);
            this.txtPackets.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtPackets.Name = "txtPackets";
            this.txtPackets.Size = new System.Drawing.Size(13, 13);
            this.txtPackets.TabIndex = 1;
            this.txtPackets.Text = "0";
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // inputList
            // 
            listViewGroup1.Header = "Buttons";
            listViewGroup1.Name = "Buttons";
            listViewGroup2.Header = "Sliders";
            listViewGroup2.Name = "Sliders";
            listViewGroup3.Header = "Gestures";
            listViewGroup3.Name = "Gestures";
            this.inputList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            listViewGroup4.Header = "Buttons";
            listViewGroup4.Name = "buttonsGroup";
            listViewItem1.Group = listViewGroup4;
            listViewItem2.Group = listViewGroup4;
            listViewGroup5.Header = "Sliders";
            listViewGroup5.Name = "slidersGroup";
            listViewItem3.Group = listViewGroup5;
            this.inputList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.inputList.Location = new System.Drawing.Point(11, 22);
            this.inputList.Margin = new System.Windows.Forms.Padding(2);
            this.inputList.Name = "inputList";
            this.inputList.Size = new System.Drawing.Size(203, 650);
            this.inputList.SmallImageList = this.iconList;
            this.inputList.TabIndex = 3;
            this.inputList.UseCompatibleStateImageBehavior = false;
            this.inputList.View = System.Windows.Forms.View.SmallIcon;
            this.inputList.ItemActivate += new System.EventHandler(this.inputList_ItemActivate);
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "Up.png");
            this.iconList.Images.SetKeyName(1, "Down.png");
            this.iconList.Images.SetKeyName(2, "Green.png");
            this.iconList.Images.SetKeyName(3, "Red.png");
            this.iconList.Images.SetKeyName(4, "Stop.png");
            // 
            // graphPicture
            // 
            this.graphPicture.Location = new System.Drawing.Point(224, 47);
            this.graphPicture.Name = "graphPicture";
            this.graphPicture.Size = new System.Drawing.Size(723, 624);
            this.graphPicture.TabIndex = 4;
            this.graphPicture.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(468, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Y:";
            // 
            // ySelect
            // 
            this.ySelect.FormattingEnabled = true;
            this.ySelect.Location = new System.Drawing.Point(491, 22);
            this.ySelect.MaxDropDownItems = 20;
            this.ySelect.Name = "ySelect";
            this.ySelect.Size = new System.Drawing.Size(215, 21);
            this.ySelect.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(712, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Z:";
            // 
            // zSelect
            // 
            this.zSelect.FormattingEnabled = true;
            this.zSelect.Location = new System.Drawing.Point(735, 22);
            this.zSelect.MaxDropDownItems = 20;
            this.zSelect.Name = "zSelect";
            this.zSelect.Size = new System.Drawing.Size(212, 21);
            this.zSelect.TabIndex = 9;
            // 
            // xSelect
            // 
            this.xSelect.FormattingEnabled = true;
            this.xSelect.Location = new System.Drawing.Point(244, 22);
            this.xSelect.MaxDropDownItems = 20;
            this.xSelect.Name = "xSelect";
            this.xSelect.Size = new System.Drawing.Size(215, 21);
            this.xSelect.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 683);
            this.Controls.Add(this.xSelect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zSelect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ySelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.graphPicture);
            this.Controls.Add(this.inputList);
            this.Controls.Add(this.txtPackets);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Network Controller Test Reciever";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.graphPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtPackets;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ListView inputList;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.PictureBox graphPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ySelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox zSelect;
        private System.Windows.Forms.ComboBox xSelect;
    }
}

