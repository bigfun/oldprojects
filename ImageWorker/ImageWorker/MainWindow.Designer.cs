namespace ImageWorker
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadesOfGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.method1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.method2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadeOfGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramEqualizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coutACindBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coutCin1Cin2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowpassFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robertsGradientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.version1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.version2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pewittToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleWindowModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.processBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWidth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHeight = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSizeRatio = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorChooser = new System.Windows.Forms.ColorDialog();
            this.togetherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(569, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveImageAsToolStripMenuItem,
            this.exitProgramToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.OpenImageToolStripMenuItem_Click);
            // 
            // saveImageAsToolStripMenuItem
            // 
            this.saveImageAsToolStripMenuItem.Name = "saveImageAsToolStripMenuItem";
            this.saveImageAsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.saveImageAsToolStripMenuItem.Text = "Save Image as ...";
            this.saveImageAsToolStripMenuItem.Click += new System.EventHandler(this.saveImageAsToolStripMenuItem_Click);
            // 
            // exitProgramToolStripMenuItem
            // 
            this.exitProgramToolStripMenuItem.Name = "exitProgramToolStripMenuItem";
            this.exitProgramToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitProgramToolStripMenuItem.Text = "Exit";
            this.exitProgramToolStripMenuItem.Click += new System.EventHandler(this.exitProgramToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shadesOfGrayToolStripMenuItem,
            this.shadeOfGrayToolStripMenuItem,
            this.histogramToolStripMenuItem,
            this.histogramEqualizationToolStripMenuItem,
            this.normalizationToolStripMenuItem,
            this.filtersToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // shadesOfGrayToolStripMenuItem
            // 
            this.shadesOfGrayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.method1ToolStripMenuItem,
            this.method2ToolStripMenuItem});
            this.shadesOfGrayToolStripMenuItem.Name = "shadesOfGrayToolStripMenuItem";
            this.shadesOfGrayToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.shadesOfGrayToolStripMenuItem.Text = "Black && White";
            // 
            // method1ToolStripMenuItem
            // 
            this.method1ToolStripMenuItem.Name = "method1ToolStripMenuItem";
            this.method1ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.method1ToolStripMenuItem.Text = "Method 1 ";
            this.method1ToolStripMenuItem.Click += new System.EventHandler(this.method1ToolStripMenuItem_Click);
            // 
            // method2ToolStripMenuItem
            // 
            this.method2ToolStripMenuItem.Name = "method2ToolStripMenuItem";
            this.method2ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.method2ToolStripMenuItem.Text = "Method 2";
            this.method2ToolStripMenuItem.Click += new System.EventHandler(this.method2ToolStripMenuItem_Click);
            // 
            // shadeOfGrayToolStripMenuItem
            // 
            this.shadeOfGrayToolStripMenuItem.Name = "shadeOfGrayToolStripMenuItem";
            this.shadeOfGrayToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.shadeOfGrayToolStripMenuItem.Text = "Shade of Gray";
            this.shadeOfGrayToolStripMenuItem.Click += new System.EventHandler(this.shadeOfGrayToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.histogramToolStripMenuItem.Text = "Histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // histogramEqualizationToolStripMenuItem
            // 
            this.histogramEqualizationToolStripMenuItem.Name = "histogramEqualizationToolStripMenuItem";
            this.histogramEqualizationToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.histogramEqualizationToolStripMenuItem.Text = "Histogram Equalization";
            this.histogramEqualizationToolStripMenuItem.Click += new System.EventHandler(this.histogramEqualizationToolStripMenuItem_Click);
            // 
            // normalizationToolStripMenuItem
            // 
            this.normalizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coutACindBToolStripMenuItem,
            this.coutCin1Cin2ToolStripMenuItem});
            this.normalizationToolStripMenuItem.Name = "normalizationToolStripMenuItem";
            this.normalizationToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.normalizationToolStripMenuItem.Text = "Normalization/Transformation";
            // 
            // coutACindBToolStripMenuItem
            // 
            this.coutACindBToolStripMenuItem.Name = "coutACindBToolStripMenuItem";
            this.coutACindBToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.coutACindBToolStripMenuItem.Text = "Cout = a * Cin ^d + b";
            this.coutACindBToolStripMenuItem.Click += new System.EventHandler(this.coutACindBToolStripMenuItem_Click);
            // 
            // coutCin1Cin2ToolStripMenuItem
            // 
            this.coutCin1Cin2ToolStripMenuItem.Name = "coutCin1Cin2ToolStripMenuItem";
            this.coutCin1Cin2ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.coutCin1Cin2ToolStripMenuItem.Text = "Cout = Cin1 +/- Cin2";
            this.coutCin1Cin2ToolStripMenuItem.Click += new System.EventHandler(this.coutCin1Cin2ToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowpassFilterToolStripMenuItem,
            this.robertsGradientsToolStripMenuItem,
            this.pewittToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // lowpassFilterToolStripMenuItem
            // 
            this.lowpassFilterToolStripMenuItem.Name = "lowpassFilterToolStripMenuItem";
            this.lowpassFilterToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.lowpassFilterToolStripMenuItem.Text = "Low-pass filter";
            this.lowpassFilterToolStripMenuItem.Click += new System.EventHandler(this.lowpassFilterToolStripMenuItem_Click);
            // 
            // robertsGradientsToolStripMenuItem
            // 
            this.robertsGradientsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.version1ToolStripMenuItem,
            this.version2ToolStripMenuItem,
            this.togetherToolStripMenuItem});
            this.robertsGradientsToolStripMenuItem.Name = "robertsGradientsToolStripMenuItem";
            this.robertsGradientsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.robertsGradientsToolStripMenuItem.Text = "Robert\'s Gradients";
            // 
            // version1ToolStripMenuItem
            // 
            this.version1ToolStripMenuItem.Name = "version1ToolStripMenuItem";
            this.version1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.version1ToolStripMenuItem.Text = "Version 1";
            this.version1ToolStripMenuItem.Click += new System.EventHandler(this.version1ToolStripMenuItem_Click);
            // 
            // version2ToolStripMenuItem
            // 
            this.version2ToolStripMenuItem.Name = "version2ToolStripMenuItem";
            this.version2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.version2ToolStripMenuItem.Text = "Version 2";
            this.version2ToolStripMenuItem.Click += new System.EventHandler(this.version2ToolStripMenuItem_Click);
            // 
            // pewittToolStripMenuItem
            // 
            this.pewittToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem,
            this.verticalToolStripMenuItem});
            this.pewittToolStripMenuItem.Name = "pewittToolStripMenuItem";
            this.pewittToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.pewittToolStripMenuItem.Text = "Prewitt masks";
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.horizontalToolStripMenuItem_Click);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleWindowModeToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // singleWindowModeToolStripMenuItem
            // 
            this.singleWindowModeToolStripMenuItem.CheckOnClick = true;
            this.singleWindowModeToolStripMenuItem.Name = "singleWindowModeToolStripMenuItem";
            this.singleWindowModeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.singleWindowModeToolStripMenuItem.Text = "Single Window Mode";
            this.singleWindowModeToolStripMenuItem.Click += new System.EventHandler(this.singleWindowModeToolStripMenuItem_Click);
            // 
            // pBox
            // 
            this.pBox.BackColor = System.Drawing.SystemColors.Window;
            this.pBox.Location = new System.Drawing.Point(3, 0);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(507, 378);
            this.pBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBox.TabIndex = 0;
            this.pBox.TabStop = false;
            this.pBox.Click += new System.EventHandler(this.pBox_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.pBox);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 381);
            this.panel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processBar,
            this.lblProcessing,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.lblWidth,
            this.toolStripStatusLabel4,
            this.lblHeight,
            this.toolStripStatusLabel3,
            this.lblSizeRatio});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(569, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "Status Bar";
            // 
            // processBar
            // 
            this.processBar.Name = "processBar";
            this.processBar.Size = new System.Drawing.Size(100, 16);
            this.processBar.Visible = false;
            // 
            // lblProcessing
            // 
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(26, 17);
            this.lblProcessing.Text = "Idle";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel1.Text = "                ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel2.Text = "Width: ";
            // 
            // lblWidth
            // 
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(45, 17);
            this.lblWidth.Text = "[width]";
            this.lblWidth.Visible = false;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(49, 17);
            this.toolStripStatusLabel4.Text = "Height: ";
            // 
            // lblHeight
            // 
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(49, 17);
            this.lblHeight.Text = "[height]";
            this.lblHeight.Visible = false;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel3.Text = "Current Size:";
            // 
            // lblSizeRatio
            // 
            this.lblSizeRatio.Name = "lblSizeRatio";
            this.lblSizeRatio.Size = new System.Drawing.Size(39, 17);
            this.lblSizeRatio.Text = "[ratio]";
            this.lblSizeRatio.Visible = false;
            // 
            // togetherToolStripMenuItem
            // 
            this.togetherToolStripMenuItem.Name = "togetherToolStripMenuItem";
            this.togetherToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.togetherToolStripMenuItem.Text = "together";
            this.togetherToolStripMenuItem.Click += new System.EventHandler(this.togetherToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(569, 483);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Image Worker";
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitProgramToolStripMenuItem;
        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar processBar;
        private System.Windows.Forms.ToolStripStatusLabel lblProcessing;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadesOfGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem method1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem method2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblWidth;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblHeight;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblSizeRatio;
        private System.Windows.Forms.ToolStripMenuItem shadeOfGrayToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorChooser;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramEqualizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleWindowModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coutACindBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coutCin1Cin2ToolStripMenuItem;
        private System.Windows.Forms.Button proccessButton;
        private System.Windows.Forms.Button modeButton;
        private System.Windows.Forms.PictureBox pBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowpassFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem robertsGradientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pewittToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem version1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem version2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem togetherToolStripMenuItem;
    }
}

