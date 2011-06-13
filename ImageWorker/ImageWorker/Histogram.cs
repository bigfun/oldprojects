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
    public partial class Histogram : Form
    {
        public Histogram(int[] values)
        {
            InitializeComponent();
            Bitmap shadesBitmap = new Bitmap(picShades.Width, picShades.Height);
            for (int i = 0; i < picShades.Width; i++)
            {
                for (int j = 0; j < picShades.Height; j++)
                {
                    shadesBitmap.SetPixel(i, j, Color.FromArgb(255 - i, 255 - i, 255 - i));
                }
            }
            picShades.Image = new Bitmap(shadesBitmap);
            int max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }

            }
            Bitmap histogramBitmap = new Bitmap(picHistogram.Width, picHistogram.Height);
            int value;
            for (int i = 0; i < picHistogram.Width; i++)
            {
                value = values[i] * picHistogram.Height / max;
                for (int j = 0; j < picHistogram.Height; j++)
                {
                    if (value > j)
                    {
                        histogramBitmap.SetPixel(i, picHistogram.Height - j - 1, Color.FromArgb(10, 10, 10));
                    }
                    else if (value == j)
                    {
                        histogramBitmap.SetPixel(i, picHistogram.Height - j - 1, Color.FromArgb(255, 0, 0));
                    }
                    else
                    {
                        histogramBitmap.SetPixel(i, picHistogram.Height - j - 1, Color.FromArgb(255 , 230, 255 ));
                    }
                }


            }
            picHistogram.Image = new Bitmap(histogramBitmap);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitmap = new Bitmap(picHistogram.Image);
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.ShowDialog();

                bitmap.Save(saveDialog.FileName);
            }
            catch
            {
                MessageBox.Show("Unable to save histogram.");
            }
        }


    }
}
