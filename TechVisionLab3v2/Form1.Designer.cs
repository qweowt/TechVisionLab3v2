namespace TechVisionLab3v2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            SelectPicture = new Button();
            listView1 = new ListView();
            RminNum = new NumericUpDown();
            GminNum = new NumericUpDown();
            BminNum = new NumericUpDown();
            BmaxNum = new NumericUpDown();
            GmaxNum = new NumericUpDown();
            RmaxNum = new NumericUpDown();
            SetMaskBtn = new Button();
            SetWhiteMaskBtn = new Button();
            ColorPatternChoose = new ComboBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            numericRmin = new NumericUpDown();
            numericDensity = new NumericUpDown();
            numericRmax = new NumericUpDown();
            numericClusterCount = new NumericUpDown();
            numericTrust = new NumericUpDown();
            AutoMode = new CheckBox();
            SearchObject = new Button();
            EncodeObject = new Button();
            listBox = new ListBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RminNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GminNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BminNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BmaxNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GmaxNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RmaxNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericRmin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericDensity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericRmax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericClusterCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericTrust).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(12, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(640, 480);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // SelectPicture
            // 
            SelectPicture.Location = new Point(658, 12);
            SelectPicture.Name = "SelectPicture";
            SelectPicture.Size = new Size(206, 23);
            SelectPicture.TabIndex = 1;
            SelectPicture.Text = "Select picture";
            SelectPicture.UseVisualStyleBackColor = true;
            SelectPicture.Click += SelectPicture_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(658, 41);
            listView1.Name = "listView1";
            listView1.Size = new Size(206, 139);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // RminNum
            // 
            RminNum.Location = new Point(870, 41);
            RminNum.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            RminNum.Name = "RminNum";
            RminNum.Size = new Size(56, 23);
            RminNum.TabIndex = 3;
            // 
            // GminNum
            // 
            GminNum.Location = new Point(870, 70);
            GminNum.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            GminNum.Name = "GminNum";
            GminNum.Size = new Size(56, 23);
            GminNum.TabIndex = 4;
            // 
            // BminNum
            // 
            BminNum.Location = new Point(870, 99);
            BminNum.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            BminNum.Name = "BminNum";
            BminNum.Size = new Size(56, 23);
            BminNum.TabIndex = 5;
            // 
            // BmaxNum
            // 
            BmaxNum.Location = new Point(932, 99);
            BmaxNum.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            BmaxNum.Name = "BmaxNum";
            BmaxNum.Size = new Size(56, 23);
            BmaxNum.TabIndex = 8;
            BmaxNum.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // GmaxNum
            // 
            GmaxNum.Location = new Point(932, 70);
            GmaxNum.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            GmaxNum.Name = "GmaxNum";
            GmaxNum.Size = new Size(56, 23);
            GmaxNum.TabIndex = 7;
            GmaxNum.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // RmaxNum
            // 
            RmaxNum.Location = new Point(932, 41);
            RmaxNum.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            RmaxNum.Name = "RmaxNum";
            RmaxNum.Size = new Size(56, 23);
            RmaxNum.TabIndex = 6;
            RmaxNum.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // SetMaskBtn
            // 
            SetMaskBtn.Location = new Point(870, 128);
            SetMaskBtn.Name = "SetMaskBtn";
            SetMaskBtn.Size = new Size(118, 23);
            SetMaskBtn.TabIndex = 9;
            SetMaskBtn.Text = "Mask";
            SetMaskBtn.UseVisualStyleBackColor = true;
            SetMaskBtn.Click += SetMaskBtn_Click;
            // 
            // SetWhiteMaskBtn
            // 
            SetWhiteMaskBtn.Location = new Point(870, 157);
            SetWhiteMaskBtn.Name = "SetWhiteMaskBtn";
            SetWhiteMaskBtn.Size = new Size(118, 23);
            SetWhiteMaskBtn.TabIndex = 10;
            SetWhiteMaskBtn.Text = "White mask";
            SetWhiteMaskBtn.UseVisualStyleBackColor = true;
            SetWhiteMaskBtn.Click += SetWhiteMaskBtn_Click;
            // 
            // ColorPatternChoose
            // 
            ColorPatternChoose.FormattingEnabled = true;
            ColorPatternChoose.Items.AddRange(new object[] { "Red", "Blue", "Yellow", "Black-white" });
            ColorPatternChoose.Location = new Point(870, 12);
            ColorPatternChoose.Name = "ColorPatternChoose";
            ColorPatternChoose.Size = new Size(118, 23);
            ColorPatternChoose.TabIndex = 11;
            ColorPatternChoose.SelectedIndexChanged += ColorPatternChoose_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(668, 195);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(848, 195);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(128, 128);
            pictureBox2.TabIndex = 13;
            pictureBox2.TabStop = false;
            // 
            // numericRmin
            // 
            numericRmin.Location = new Point(838, 342);
            numericRmin.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericRmin.Name = "numericRmin";
            numericRmin.Size = new Size(70, 23);
            numericRmin.TabIndex = 3;
            numericRmin.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // numericDensity
            // 
            numericDensity.DecimalPlaces = 2;
            numericDensity.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericDensity.Location = new Point(918, 342);
            numericDensity.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericDensity.Name = "numericDensity";
            numericDensity.Size = new Size(70, 23);
            numericDensity.TabIndex = 6;
            numericDensity.Value = new decimal(new int[] { 3, 0, 0, 65536 });
            // 
            // numericRmax
            // 
            numericRmax.Location = new Point(838, 371);
            numericRmax.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericRmax.Name = "numericRmax";
            numericRmax.Size = new Size(70, 23);
            numericRmax.TabIndex = 3;
            numericRmax.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // numericClusterCount
            // 
            numericClusterCount.Location = new Point(918, 371);
            numericClusterCount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericClusterCount.Name = "numericClusterCount";
            numericClusterCount.Size = new Size(70, 23);
            numericClusterCount.TabIndex = 3;
            numericClusterCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericTrust
            // 
            numericTrust.DecimalPlaces = 2;
            numericTrust.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericTrust.Location = new Point(658, 342);
            numericTrust.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericTrust.Name = "numericTrust";
            numericTrust.Size = new Size(150, 23);
            numericTrust.TabIndex = 6;
            numericTrust.Value = new decimal(new int[] { 50, 0, 0, 131072 });
            // 
            // AutoMode
            // 
            AutoMode.AutoSize = true;
            AutoMode.Location = new Point(658, 371);
            AutoMode.Name = "AutoMode";
            AutoMode.Size = new Size(86, 19);
            AutoMode.TabIndex = 14;
            AutoMode.Text = "Auto mode";
            AutoMode.UseVisualStyleBackColor = true;
            // 
            // SearchObject
            // 
            SearchObject.Location = new Point(838, 400);
            SearchObject.Name = "SearchObject";
            SearchObject.Size = new Size(150, 35);
            SearchObject.TabIndex = 1;
            SearchObject.Text = "Search";
            SearchObject.UseVisualStyleBackColor = true;
            SearchObject.Click += SearchObject_Click;
            // 
            // EncodeObject
            // 
            EncodeObject.Location = new Point(838, 441);
            EncodeObject.Name = "EncodeObject";
            EncodeObject.Size = new Size(150, 35);
            EncodeObject.TabIndex = 15;
            EncodeObject.Text = "Encode";
            EncodeObject.UseVisualStyleBackColor = true;
            EncodeObject.Click += EncodeObject_Click;
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(658, 398);
            listBox.Name = "listBox";
            listBox.Size = new Size(174, 94);
            listBox.TabIndex = 38;
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 508);
            Controls.Add(listBox);
            Controls.Add(EncodeObject);
            Controls.Add(AutoMode);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(ColorPatternChoose);
            Controls.Add(SetWhiteMaskBtn);
            Controls.Add(SetMaskBtn);
            Controls.Add(BmaxNum);
            Controls.Add(GmaxNum);
            Controls.Add(numericTrust);
            Controls.Add(numericDensity);
            Controls.Add(RmaxNum);
            Controls.Add(BminNum);
            Controls.Add(numericClusterCount);
            Controls.Add(numericRmax);
            Controls.Add(numericRmin);
            Controls.Add(GminNum);
            Controls.Add(RminNum);
            Controls.Add(listView1);
            Controls.Add(SearchObject);
            Controls.Add(SelectPicture);
            Controls.Add(pictureBox);
            Name = "Form1";
            Text = "TechVisionLab3 Klimenko";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)RminNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)GminNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)BminNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)BmaxNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)GmaxNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)RmaxNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericRmin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericDensity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericRmax).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericClusterCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericTrust).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private Button SelectPicture;
        private ListView listView1;
        private NumericUpDown RminNum;
        private NumericUpDown GminNum;
        private NumericUpDown BminNum;
        private NumericUpDown BmaxNum;
        private NumericUpDown GmaxNum;
        private NumericUpDown RmaxNum;
        private Button SetMaskBtn;
        private Button SetWhiteMaskBtn;
        private ComboBox ColorPatternChoose;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private NumericUpDown numericRmin;
        private NumericUpDown numericDensity;
        private NumericUpDown numericRmax;
        private NumericUpDown numericClusterCount;
        private NumericUpDown numericTrust;
        private CheckBox AutoMode;
        private Button SearchObject;
        private Button EncodeObject;
        private ListBox listBox;
    }
}
