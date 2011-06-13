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
    public partial class LowPassFilter : Form
    {
        public bool mode;
        public LowPassFilter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.matrixSize.SelectedIndex == 0)
                mode = false;
            else
                mode = true;
            Close();
        }
    }
}