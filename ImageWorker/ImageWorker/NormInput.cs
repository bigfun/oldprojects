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
    public partial class NormInput : Form
    {
        public double a;
        public int b;
        public double d;
        public int maxValue;
        public int minValue;
        public NormInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = (double)numA.Value;
            b = (int)numB.Value;
            d = (double)numD.Value;
            maxValue = (int)numMaxVal.Value;
            minValue = (int)numMinVal.Value;
            Close();
        }
    }
}
