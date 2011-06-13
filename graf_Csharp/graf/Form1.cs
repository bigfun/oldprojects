using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace graf
{
    public partial class grafForm : Form
    {
        private List<Graph> graphs = null;
        private Bitmap bufor = null;
        private Graphics painting = null;
        private int how_many = 0;
        private Stopwatch stoper = null;
        long start_mem = 0;
        public grafForm()
        {
            InitializeComponent();
        }

        private void txtNodes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.KeyChar = (char)0;
        }

        private void grafForm_Load(object sender, EventArgs e)
        {
            // Tworzymy niezbędne obiekty:
            graphs = new List<Graph>();
            stoper = new Stopwatch();
            bufor = new Bitmap(grafPanel.Width, grafPanel.Height);
            painting = Graphics.FromImage(bufor);
            ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.lblError, "Hello");
            start_mem = GC.GetTotalMemory(true);

        }
        private void drawNodes(Node[] nodes,SolidBrush brush)
        {
                foreach (Node node in nodes)
                {
                    painting.DrawEllipse(new Pen(Color.Black,1), node.X - 2, node.Y - 2, 4, 4);
                    painting.FillEllipse(brush, node.X - 2, node.Y - 2, 4, 4);
                }
                grafPanel.Invalidate();
        }

        private void drawLines(List<GraphLine>lines, Pen myPen)
        {
                foreach (GraphLine line in lines)
                {
                    painting.DrawLine(myPen, line.From, line.To);
                }
        }
        private void grafForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            painting.Dispose();
            bufor.Dispose();
        }

        private void grafPanel_Paint(object sender, PaintEventArgs e)
        {
            if (bufor != null)
            {
                e.Graphics.DrawImage(bufor, 0, 0);
            }
        }


        private void btnKruskal_Click(object sender, EventArgs e)
        {
            lblError.Text = "Wyznaczanie MST dla grafów przy pomocy algorytmu Kruskala...";
            if (graphs != null)
            {
                List<List<GraphLine>> MST = new List<List<GraphLine>>();
                stoper.Reset();
                stoper.Start();
                foreach (Graph graph in graphs)
                {
                    MST.Add(graph.GetMST(true));
                }
                stoper.Stop();
                lblError.Text = "MST wyznaczone.";
                lblTime.Text = ((float)stoper.ElapsedMilliseconds / 1000f).ToString();
                stoper.Reset();
                stoper.Start();
                for (int i = 0; i < MST.Count; i++)
                {
                    drawLines(MST[i], graphs[i].getPen());
                }
                stoper.Stop();
                lblPainting.Text = ((float)stoper.ElapsedMilliseconds / 1000f).ToString();
                long current_mem = GC.GetTotalMemory(true);
                current_mem = current_mem - start_mem;
                lblMem.Text = ((float)current_mem / 1000f).ToString();
            }
            else
                lblError.Text = "Blad - nie udalo sie wyznaczyc MST, brak grafow.";
            grafPanel.Invalidate();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnKruskal.Enabled = false;
            btnPrim.Enabled = false;
            graphs = new List<Graph>();
            lblGraphCount.Text = graphs.Count.ToString();
            painting.Clear(Color.White);
            lblError.Text = "Pole rysowania wyczyszczone, grafy wykasowane.";
            grafPanel.Invalidate();
            this.start_mem = GC.GetTotalMemory(true);
            lblMem.Text = "0";
            lblPainting.Text = "0";
            lblTime.Text = "0";
        }

        private void btnAddGraph_Click(object sender, EventArgs e)
        {
            how_many = Int32.Parse(txtNodes.Text);
            if (how_many < 2)
                lblError.Text = "Błąd - minimalna ilość wierzchołków to 2. ";
            else
            {
                lblError.Text = "Dodawanie nowego grafu...";
                graphs.Add(new Graph(how_many, grafPanel.Size.Width, grafPanel.Size.Height, colorLine.BackColor, colorNode.BackColor));

                lblGraphCount.Text = graphs.Count.ToString();
                drawNodes(graphs[graphs.Count-1].GetNodes(),graphs[graphs.Count-1].getBrush());                  
                grafPanel.Invalidate();
                btnKruskal.Enabled = true;
                btnPrim.Enabled = true;
                lblError.Text = "Nowy graf dodany. ";
                long current_mem = GC.GetTotalMemory(true);
                current_mem = current_mem - start_mem;
                lblMem.Text = ((float)current_mem/1000f).ToString();
            }
             

        }

        private void colorNode_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                colorNode.BackColor = colorDialog.Color;
        }

        private void colorLine_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                colorLine.BackColor = colorDialog.Color;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }



}