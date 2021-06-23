namespace JournalReader
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelSource = new System.Windows.Forms.Panel();
            this.btnScan = new System.Windows.Forms.Button();
            this.comboBoxScan = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.labelSource = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnProcess = new System.Windows.Forms.Button();
            this.panelPictureBox = new System.Windows.Forms.Panel();
            this.labelPictureBox = new System.Windows.Forms.Label();
            this.labelTrackBar = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnSelect = new System.Windows.Forms.Button();
            this.panelProcess = new System.Windows.Forms.Panel();
            this.labelProcess = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panelProcess.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSource
            // 
            this.panelSource.AutoScroll = true;
            this.panelSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSource.Controls.Add(this.btnScan);
            this.panelSource.Controls.Add(this.comboBoxScan);
            this.panelSource.Controls.Add(this.btnOpen);
            this.panelSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelSource.Location = new System.Drawing.Point(32, 43);
            this.panelSource.Name = "panelSource";
            this.panelSource.Size = new System.Drawing.Size(298, 170);
            this.panelSource.TabIndex = 0;
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Gainsboro;
            this.btnScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScan.Enabled = false;
            this.btnScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnScan.ForeColor = System.Drawing.Color.Black;
            this.btnScan.Location = new System.Drawing.Point(72, 65);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(156, 32);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "Сканировать";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.BtnScan_Click);
            // 
            // comboBoxScan
            // 
            this.comboBoxScan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxScan.BackColor = System.Drawing.Color.Gainsboro;
            this.comboBoxScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxScan.ForeColor = System.Drawing.Color.Black;
            this.comboBoxScan.FormattingEnabled = true;
            this.comboBoxScan.Location = new System.Drawing.Point(15, 23);
            this.comboBoxScan.Name = "comboBoxScan";
            this.comboBoxScan.Size = new System.Drawing.Size(267, 24);
            this.comboBoxScan.TabIndex = 2;
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOpen.ForeColor = System.Drawing.Color.Black;
            this.btnOpen.Location = new System.Drawing.Point(72, 115);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(156, 32);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Открыть";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSource.Location = new System.Drawing.Point(56, 34);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(79, 17);
            this.labelSource.TabIndex = 0;
            this.labelSource.Text = "Источник";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(77, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(525, 708);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.Color.Gainsboro;
            this.btnProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcess.Enabled = false;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProcess.ForeColor = System.Drawing.Color.Black;
            this.btnProcess.Location = new System.Drawing.Point(72, 101);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(156, 54);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Обработать изображение";
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.BtnProcess_Click);
            // 
            // panelPictureBox
            // 
            this.panelPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPictureBox.BackColor = System.Drawing.Color.Gray;
            this.panelPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPictureBox.Controls.Add(this.labelTrackBar);
            this.panelPictureBox.Controls.Add(this.trackBar1);
            this.panelPictureBox.Controls.Add(this.pictureBox1);
            this.panelPictureBox.Location = new System.Drawing.Point(366, 0);
            this.panelPictureBox.Name = "panelPictureBox";
            this.panelPictureBox.Size = new System.Drawing.Size(675, 795);
            this.panelPictureBox.TabIndex = 3;
            // 
            // labelPictureBox
            // 
            this.labelPictureBox.AutoSize = true;
            this.labelPictureBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelPictureBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPictureBox.ForeColor = System.Drawing.Color.Crimson;
            this.labelPictureBox.Location = new System.Drawing.Point(44, 621);
            this.labelPictureBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.labelPictureBox.Name = "labelPictureBox";
            this.labelPictureBox.Size = new System.Drawing.Size(0, 24);
            this.labelPictureBox.TabIndex = 4;
            // 
            // labelTrackBar
            // 
            this.labelTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTrackBar.AutoSize = true;
            this.labelTrackBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTrackBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTrackBar.ForeColor = System.Drawing.Color.White;
            this.labelTrackBar.Location = new System.Drawing.Point(318, 759);
            this.labelTrackBar.Name = "labelTrackBar";
            this.labelTrackBar.Size = new System.Drawing.Size(74, 29);
            this.labelTrackBar.TabIndex = 3;
            this.labelTrackBar.Text = "100%";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.trackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBar1.Location = new System.Drawing.Point(-2, 759);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Minimum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(325, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickFrequency = 20;
            this.trackBar1.Value = 110;
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(72, 25);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(156, 54);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Выделить область";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // panelProcess
            // 
            this.panelProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProcess.Controls.Add(this.btnSelect);
            this.panelProcess.Controls.Add(this.btnProcess);
            this.panelProcess.Location = new System.Drawing.Point(32, 249);
            this.panelProcess.Name = "panelProcess";
            this.panelProcess.Size = new System.Drawing.Size(298, 180);
            this.panelProcess.TabIndex = 5;
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.labelProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProcess.Location = new System.Drawing.Point(56, 239);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(90, 17);
            this.labelProcess.TabIndex = 6;
            this.labelProcess.Text = "Обработка";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Location = new System.Drawing.Point(32, 464);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 125);
            this.panel1.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(25, 85);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(257, 20);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Сохранить вместе с изображением";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(72, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 54);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1041, 791);
            this.Controls.Add(this.labelPictureBox);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.panelSource);
            this.Controls.Add(this.panelPictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelProcess);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1050, 830);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journal Reader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelSource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelPictureBox.ResumeLayout(false);
            this.panelPictureBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panelProcess.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSource;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Panel panelPictureBox;
        private System.Windows.Forms.ComboBox comboBoxScan;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Panel panelProcess;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelTrackBar;
        private System.Windows.Forms.Label labelPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}