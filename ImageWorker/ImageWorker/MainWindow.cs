using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageWorker
{
    public partial class MainWindow : Form
    {
        bool singleWindow;
        bool doubleWindowMode;
        bool modeButtonMode;
        public MainWindow()
        {
            
            InitializeComponent();
            singleWindow = false;
            doubleWindowMode = false;
            modeButtonMode = true;
        }
        public MainWindow(Image image)
        {
            singleWindow = false;
            doubleWindowMode = false;
            modeButtonMode = true;
            InitializeComponent();
            pBox.Image = (Image)image.Clone();
        }

        private void OpenImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                initializeProccessing(1);
                pBox.Image = new Bitmap(dialog.FileName);
                lblWidth.Text = pBox.Image.Width.ToString();
                lblHeight.Text = pBox.Image.Height.ToString();
                float ratio = (float)pBox.Size.Width / (float)pBox.Image.Width;
                lblSizeRatio.Text = ((int)(ratio * 100.0)).ToString() + "%";
                lblWidth.Visible = lblHeight.Visible = lblSizeRatio.Visible = true;
                endProccessing();
            }
            
        }

        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image != null)
            {
                Bitmap obrazek = new Bitmap(pBox.Image);
                SaveFileDialog dialog = new SaveFileDialog();
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    initializeProccessing(1);
                    obrazek.Save(dialog.FileName);
                    endProccessing();
                }
            }
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (!doubleWindowMode)
            {
                Size newSize = new Size(this.Size.Width - 25, this.Size.Height - 100);
                panel1.Size = newSize;
                pBox.Size = newSize;
            }
            else
            {
                Size newSize = new Size(this.Size.Width / 2 - 50, this.Size.Height - 100);
                panel1.Size = newSize;
                pBox.Size = newSize;
                this.panel2.Location = new System.Drawing.Point(panel1.Size.Width + 50, 27);
                panel2.Size = newSize;
                pBox2.Size = newSize;
                this.modeButton.Location = new System.Drawing.Point(this.panel1.Location.X + this.panel1.Size.Width + 10, 179);
                this.proccessButton.Location = new System.Drawing.Point(this.panel1.Location.X + this.panel1.Size.Width + 10, 210);
            }
            if (pBox.Image != null)
            {
                float ratio = (float)pBox.Size.Width / (float)pBox.Image.Width;
                lblSizeRatio.Text = ((int)(ratio * 100.0)).ToString() + "%";
            }
        }
        private void initializeProccessing(int max)
        {
            processBar.Value = 0;
            processBar.Maximum = max;
            processBar.Visible = true;
            lblProcessing.Text = "Proccessing...";
            this.Refresh();
        }
        private void endProccessing()
        {
            processBar.Visible = false;
            lblProcessing.Text = "Idle";
            this.Refresh();
        }
        private void method1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
                return;
            Bitmap workingCopy = new Bitmap(pBox.Image);
            initializeProccessing(workingCopy.Width);
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    Color color = workingCopy.GetPixel(x,y);
                    byte newColor = (byte)((color.B + color.G + color.R)/3);
                    workingCopy.SetPixel(x,y,Color.FromArgb(newColor,newColor,newColor));
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }

        private void method2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
                return;
            Bitmap workingCopy = new Bitmap(pBox.Image);
            initializeProccessing(workingCopy.Height);
            Color color;
            byte newColor;
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    color = workingCopy.GetPixel(x,y);
                    newColor = (byte)(0.114*color.B + 0.587*color.G + 0.299* color.R);
                    workingCopy.SetPixel(x,y,Color.FromArgb(newColor,newColor,newColor));
                   
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }

        private void shadeOfGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
            {
                MessageBox.Show("No Image loaded. Load some image first.");
                return;
            }
            Color color;
            DialogResult result;
            do
            {
                result = colorChooser.ShowDialog();
                color = colorChooser.Color;
            }
            while (result == DialogResult.OK && ((color.B != color.G) || (color.B != color.R)));
            if (result == DialogResult.OK)
            {
                Bitmap workingCopy = new Bitmap(pBox.Image);
                initializeProccessing(workingCopy.Height);
                Color oldColor;
                byte newColor;
                for (int y = 0; y < workingCopy.Height; y++)
                {
                    for (int x = 0; x < workingCopy.Width; x++)
                    {
                        oldColor = workingCopy.GetPixel(x, y);
                        if (oldColor.G > (color.G - 10) && oldColor.G < (color.G + 10))
                        {
                            newColor = (byte)oldColor.G;
                        }
                        else
                        {
                            newColor = (byte)20;
                        }
                        workingCopy.SetPixel(x, y, Color.FromArgb(newColor, newColor, newColor));

                    }
                    processBar.PerformStep();
                }
                if (singleWindow)
                    pBox.Image = workingCopy;
                else
                {
                    (new MainWindow(workingCopy)).Show();
                }
                endProccessing();
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] values = new int[256];

            Bitmap bitmap = new Bitmap(pBox.Image);
            int color;
            for (color = 0; color < 256; color++)
            {
                values[color] = 0;
            }

            initializeProccessing(bitmap.Height);
            for (int j = 0; j < bitmap.Height; j++)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    color = bitmap.GetPixel(i, j).R;
                    values[color] += 1;

                }
                processBar.PerformStep();
            }
            endProccessing();
            Histogram form = new Histogram(values);
            form.Show();
        }

        private void histogramEqualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] values = new int[256];

            Bitmap workingCopy = new Bitmap(pBox.Image);
            int color;
            for (color = 0; color < 256; color++)
            {
                values[color] = 0;
            }

            initializeProccessing(workingCopy.Height);
            for (int j = 0; j < workingCopy.Height; j++)
            {
                for (int i = 0; i < workingCopy.Width; i++)
                {
                    color = workingCopy.GetPixel(i, j).R;
                    values[color] = values[color] + 1;

                }
                processBar.PerformStep();
            }
            endProccessing();


             
            // s - pixels count
            int s = workingCopy.Height * workingCopy.Width;
            // cdf - cumulative distribution function
            int[] cdf = new int[256];
            cdf[0] = values[0];
            for (color = 1; color < 256; color++)
            {
                cdf[color] = values[color] + cdf[color - 1];
            }
            // L is the number of grey levels use
            int L = 256;
            int cdfMin = cdf[0];
     
            // h(i) = ((cdf(i) - cdfMin) / (s - cdfMin)) * (L - 1) 
            double val;
            int[] h = new int[256];
            for (color = 0; color < 256; color++)
            {
                val = ((double)(cdf[color] - cdfMin) / (double)(s - cdfMin)) * (double)(L - 1);
                h[color] = (int)val;
            }
            initializeProccessing(workingCopy.Height);
            for (int j = 0; j < workingCopy.Height; j++)
            {
                for (int i = 0; i < workingCopy.Width; i++)
                {
                    color = workingCopy.GetPixel(i, j).R;
                    int newValue = h[color];
                    workingCopy.SetPixel(i,j,Color.FromArgb(newValue,newValue,newValue));

                }
                processBar.PerformStep();
            }
            endProccessing();
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
        }

        private void singleWindowModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            singleWindow = !singleWindow;
        }

        private void coutACindBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormInput form = new NormInput();
            form.ShowDialog();

            Bitmap workingCopy = new Bitmap(pBox.Image);
            int color;
            initializeProccessing(workingCopy.Height);
            for (int j = 0; j < workingCopy.Height; j++)
            {
                for (int i = 0; i < workingCopy.Width; i++)
                {
                    color = workingCopy.GetPixel(i, j).R;
                    color = (int)(form.a * Math.Pow(color, form.d)) + form.b;
                    color = color < form.minValue ? form.minValue : color;
                    color = color > form.maxValue ? form.maxValue : color;
                    workingCopy.SetPixel(i, j, Color.FromArgb(color, color, color));
                }
                processBar.PerformStep();
            }
            endProccessing();
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
        }

        private void coutCin1Cin2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setDoubleWindowMode(!doubleWindowMode);

        }

        private void setDoubleWindowMode(bool value)
        {
            if (value)
            {
                doubleWindowMode = true;
                this.Size = new Size(this.Size.Width * 2, this.Size.Height);
                Size newSize = new Size(this.Size.Width / 2 - 50, this.Size.Height - 100);

                // pBox2
                this.pBox2 = new PictureBox();
                this.pBox2.BackColor = System.Drawing.SystemColors.Window;
                this.pBox2.Location = new System.Drawing.Point(3, 0);
                this.pBox2.Name = "pBox2";
                this.pBox2.Size = new System.Drawing.Size(507, 378);
                this.pBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                this.pBox2.Click += new EventHandler(pBox2_Click);

                // panel2
                this.panel2 = new Panel();
                this.panel2.AutoScroll = true;
                this.panel2.AutoSize = true;
                this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                this.panel2.Controls.Add(this.pBox2);
                this.panel2.Location = new System.Drawing.Point(panel1.Size.Width + 50, 27);
                this.panel2.Name = "panel2";
                this.panel2.Size = new System.Drawing.Size(513, 381);

                // 
                // modeButton
                // 
                this.modeButton = new Button();
                this.modeButton.Location = new System.Drawing.Point(this.panel1.Location.X + this.panel1.Size.Width + 10, 179);
                this.modeButton.Name = "modeButton";
                this.modeButton.Size = new System.Drawing.Size(30, 23);
                this.modeButton.Text = "+";
                this.modeButton.UseVisualStyleBackColor = true;
                this.modeButton.Click += new System.EventHandler(this.modeButton_Click);

                // 
                // proccessButton
                // 
                this.proccessButton = new Button();
                this.proccessButton.Location = new System.Drawing.Point(this.panel1.Location.X + this.panel1.Size.Width + 10, 210);
                this.proccessButton.Name = "proccessButton";
                this.proccessButton.Size = new System.Drawing.Size(30, 23);
                this.proccessButton.Text = "=";
                this.proccessButton.UseVisualStyleBackColor = true;
                this.proccessButton.Click += new System.EventHandler(this.proccessButton_Click);

                this.Controls.Add(modeButton);
                this.Controls.Add(proccessButton);
                this.Controls.Add(this.panel2);
                panel1.Size = newSize;
                pBox.Size = newSize;
                panel2.Size = newSize;
                pBox.Size = newSize;
                this.modeButton.Location = new System.Drawing.Point(this.panel1.Location.X + this.panel1.Size.Width + 10, 179);
                this.proccessButton.Location = new System.Drawing.Point(this.panel1.Location.X + this.panel1.Size.Width + 10, 210);
                this.panel2.Location = new System.Drawing.Point(panel1.Size.Width + 50, 27);
            }
            else
            {
                doubleWindowMode = false;
                if (pBox2 != null)
                {
                    pBox2.Visible = false;
                }
                if (panel2 != null)
                    panel2.Visible = false;
                if (modeButton != null)
                    modeButton.Visible = false;
                if (proccessButton != null)
                    proccessButton.Visible = false;
                this.Size = new Size (this.panel1.Size.Width + 25,this.panel1.Size.Height + 100);

            }
            
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            if (this.modeButton != null)
            {

                if (modeButtonMode)
                {
                    this.modeButton.Text = "-";
                }
                else
                {
                    this.modeButton.Text = "+";
                }
                modeButtonMode = !modeButtonMode;
            }
        }

        private void proccessButton_Click(object sender, EventArgs e)
        {
            if (pBox != null && pBox2 != null && pBox.Image != null && pBox2.Image != null)
            {
                int width = pBox2.Image.Width < pBox.Image.Width ? pBox2.Image.Width : pBox.Image.Width;
                int height = pBox2.Image.Height < pBox.Image.Height ? pBox2.Image.Height : pBox.Image.Height;
                Bitmap workingCopy = new Bitmap(width, height);
                int colorR;
                int colorG;
                int colorB;
                initializeProccessing(workingCopy.Height);
                for (int j = 0; j < workingCopy.Height; j++)
                {
                    for (int i = 0; i < workingCopy.Width; i++)
                    {
                        if (modeButtonMode)
                        {
                            colorR = ((Bitmap)pBox.Image).GetPixel(i, j).R + ((Bitmap)pBox2.Image).GetPixel(i, j).R;
                            colorG = ((Bitmap)pBox.Image).GetPixel(i, j).G + ((Bitmap)pBox2.Image).GetPixel(i, j).G;
                            colorB = ((Bitmap)pBox.Image).GetPixel(i, j).B + ((Bitmap)pBox2.Image).GetPixel(i, j).B;
                        }
                        else
                        {
                            colorR = ((Bitmap)pBox.Image).GetPixel(i, j).R - ((Bitmap)pBox2.Image).GetPixel(i, j).R;
                            colorG = ((Bitmap)pBox.Image).GetPixel(i, j).G - ((Bitmap)pBox2.Image).GetPixel(i, j).G;
                            colorB = ((Bitmap)pBox.Image).GetPixel(i, j).B - ((Bitmap)pBox2.Image).GetPixel(i, j).B;
                        }
                        colorR = setNormalColor(colorR);
                        colorB = setNormalColor(colorB);
                        colorG = setNormalColor(colorG);
                        workingCopy.SetPixel(i, j, Color.FromArgb(colorR, colorG, colorB));
                    }
                    processBar.PerformStep();
                }
                endProccessing();
                if (singleWindow)
                    pBox.Image = workingCopy;
                else
                {
                    (new MainWindow(workingCopy)).Show();
                }
            }
            else
            {
                MessageBox.Show("Musisz załadować obydwa obrazki");
            }

        }
        private int setNormalColor(int color)
        {
            if (color > 255)
            {
                return 255;
            }
            if (color < 0)
            {
                return 0;
            }
            return color;
        }
        private void pBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                initializeProccessing(1);
                pBox.Image = new Bitmap(dialog.FileName);
                lblWidth.Text = pBox.Image.Width.ToString();
                lblHeight.Text = pBox.Image.Height.ToString();
                float ratio = (float)pBox.Size.Width / (float)pBox.Image.Width;
                lblSizeRatio.Text = ((int)(ratio * 100.0)).ToString() + "%";
                lblWidth.Visible = lblHeight.Visible = lblSizeRatio.Visible = true;
                endProccessing();
            }
        }
        private void pBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                initializeProccessing(1);
                pBox2.Image = new Bitmap(dialog.FileName);
                endProccessing();
            }
        }

        private void lowpassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
                return;
            LowPassFilter input = new LowPassFilter();
            input.ShowDialog();
            int margin;
            if (input.mode)
                margin = 2;
            else
                margin = 1;

            Bitmap workingCopy = new Bitmap(pBox.Image.Width,pBox.Image.Height);
            Bitmap original = (Bitmap) pBox.Image;
            initializeProccessing(workingCopy.Width);
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    Color newColor;
                    Color color = original.GetPixel(x, y);
                    if (x < margin || x > workingCopy.Width - (margin + 1) || y < margin || y > workingCopy.Height - (margin + 1))
                    {
                        if ((color.R + color.B + color.G) / 3 < 130)
                            newColor = Color.Black;
                        else
                            newColor = Color.White;
                    }
                    else
                    {
                        int colorR = 0;
                        int colorG = 0;
                        int colorB = 0;
                        for (int a = -margin; a < margin + 1; a++)
                            for (int b = -margin; b < margin + 1; b++)
                            {
                                Color clr = original.GetPixel(x + a, y + b);
                                colorR += clr.R;
                                colorG += clr.G;
                                colorB += clr.B;
                            }
                        int divisor = (margin * 2 + 1) * (margin * 2 + 1);
                        newColor = Color.FromArgb(colorR / divisor, colorG / divisor, colorB / divisor);
                    }
            
                    workingCopy.SetPixel(x, y, newColor);
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }

        private void version1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (pBox.Image == null)
                return;
            Bitmap workingCopy = new Bitmap(pBox.Image.Width,pBox.Image.Height);
            Bitmap original = (Bitmap) pBox.Image;
            initializeProccessing(workingCopy.Width);
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    Color newColor;
                    Color color = original.GetPixel(x, y);
                    if (x < 1 || x > workingCopy.Width - 2 || y < 1 || y > workingCopy.Height - 2)
                    {
                        if ((color.R + color.B + color.G) / 3 < 130)
                            newColor = Color.Black;
                        else
                            newColor = Color.White;
                    }
                    else
                    {
                        int colorR = 0;
                        int colorG = 0;
                        int colorB = 0;
 
                        Color clr = original.GetPixel(x + 1, y);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x , y + 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        newColor = Color.FromArgb(colorR > 0 ? colorR : 0, colorG > 0 ? colorG : 0, colorB > 0 ? colorB : 0);
                    }
            
                    workingCopy.SetPixel(x, y, newColor);
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }

        private void version2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
                return;
            Bitmap workingCopy = new Bitmap(pBox.Image.Width, pBox.Image.Height);
            Bitmap original = (Bitmap)pBox.Image;
            initializeProccessing(workingCopy.Width);
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    Color newColor;
                    Color color = original.GetPixel(x, y);
                    if (x < 1 || x > workingCopy.Width - 2 || y < 1 || y > workingCopy.Height - 2)
                    {
                        if ((color.R + color.B + color.G) / 3 < 130)
                            newColor = Color.Black;
                        else
                            newColor = Color.White;
                    }
                    else
                    {
                        int colorR = 0;
                        int colorG = 0;
                        int colorB = 0;

                        Color clr = original.GetPixel(x - 1, y);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x, y - 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        newColor = Color.FromArgb(colorR > 0 ? colorR : 0, colorG > 0 ? colorG : 0, colorB > 0 ? colorB : 0);
                    }

                    workingCopy.SetPixel(x, y, newColor);
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }

        private void togetherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
                return;
            Bitmap workingCopy = new Bitmap(pBox.Image.Width, pBox.Image.Height);
            Bitmap original = (Bitmap)pBox.Image;
            initializeProccessing(workingCopy.Width);
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    Color newColor;
                    Color color = original.GetPixel(x, y);
                    if (x < 1 || x > workingCopy.Width - 2 || y < 1 || y > workingCopy.Height - 2)
                    {
                        if ((color.R + color.B + color.G) / 3 < 130)
                            newColor = Color.Black;
                        else
                            newColor = Color.White;
                    }
                    else
                    {
                        int colorR = 0;
                        int colorG = 0;
                        int colorB = 0;

                        Color clr = original.GetPixel(x - 1, y);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x + 1, y);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x, y - 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        clr = original.GetPixel(x, y + 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        newColor = Color.FromArgb(setNormalColor(colorR), setNormalColor(colorG), setNormalColor(colorB));
                    }

                    workingCopy.SetPixel(x, y, newColor);
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
                return;
            Bitmap workingCopy = new Bitmap(pBox.Image.Width, pBox.Image.Height);
            Bitmap original = (Bitmap)pBox.Image;
            initializeProccessing(workingCopy.Width);
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    Color newColor;
                    Color color = original.GetPixel(x, y);
                    if (x < 1 || x > workingCopy.Width - 2 || y < 1 || y > workingCopy.Height - 2)
                    {
                        if ((color.R + color.B + color.G) / 3 < 130)
                            newColor = Color.Black;
                        else
                            newColor = Color.White;
                    }
                    else
                    {
                        int colorR = 0;
                        int colorG = 0;
                        int colorB = 0;

                        Color clr = original.GetPixel(x - 1, y - 1);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x, y - 1);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x + 1, y - 1);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x - 1, y + 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        clr = original.GetPixel(x, y + 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        clr = original.GetPixel(x + 1, y + 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        newColor = Color.FromArgb(setNormalColor(colorR), setNormalColor(colorG), setNormalColor(colorB));
                    }

                    workingCopy.SetPixel(x, y, newColor);
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pBox.Image == null)
                return;
            Bitmap workingCopy = new Bitmap(pBox.Image.Width, pBox.Image.Height);
            Bitmap original = (Bitmap)pBox.Image;
            initializeProccessing(workingCopy.Width);
            for (int y = 0; y < workingCopy.Height; y++)
            {
                for (int x = 0; x < workingCopy.Width; x++)
                {
                    Color newColor;
                    Color color = original.GetPixel(x, y);
                    if (x < 1 || x > workingCopy.Width - 2 || y < 1 || y > workingCopy.Height - 2)
                    {
                        if ((color.R + color.B + color.G) / 3 < 130)
                            newColor = Color.Black;
                        else
                            newColor = Color.White;
                    }
                    else
                    {
                        int colorR = 0;
                        int colorG = 0;
                        int colorB = 0;

                        Color clr = original.GetPixel(x - 1, y - 1);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x -1, y);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x - 1, y + 1);
                        colorR -= clr.R;
                        colorG -= clr.G;
                        colorB -= clr.B;
                        clr = original.GetPixel(x + 1, y - 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        clr = original.GetPixel(x + 1, y );
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        clr = original.GetPixel(x + 1, y + 1);
                        colorR += clr.R;
                        colorG += clr.G;
                        colorB += clr.B;
                        newColor = Color.FromArgb(setNormalColor(colorR), setNormalColor(colorG), setNormalColor(colorB));
                    }

                    workingCopy.SetPixel(x, y, newColor);
                }
                processBar.PerformStep();
            }
            if (singleWindow)
                pBox.Image = workingCopy;
            else
            {
                (new MainWindow(workingCopy)).Show();
            }
            endProccessing();
        }
        }
    
    
}
