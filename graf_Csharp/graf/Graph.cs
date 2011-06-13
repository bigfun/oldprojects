using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace graf
{
    class Graph
    {
        private Node[] nodes = null;
        private GraphLine[] lines = null;
        private List<GraphLine> MST = null;
        private Pen myPen = null;
        private SolidBrush brush = null;
        private bool isKruskal = true;
        private static Random rndGenerate = new Random();
        public Graph(int how_many, int width, int height, Color penColor, Color brushColor)
        {
            myPen = new Pen(penColor, 1);
            brush = new SolidBrush(brushColor);
            nodes = new Node[how_many];
            Node temp;
            for (int i = 0; i < how_many; i++)
            {
                do
                {
                    temp = new Node(rndGenerate.Next(0, width), rndGenerate.Next(0, height), i);
                }
                while (Array.IndexOf<Node>(nodes, temp) != -1);
                nodes[i] = temp;
            }
        }
        public SolidBrush getBrush()
        {
            return brush;
        }
        public Pen getPen()
        {
            return myPen;
        }
        public Node[] GetNodes()
        {
            return nodes;
        }

        public List<GraphLine> GetMST(bool useKruskal)
        {
                isKruskal = useKruskal;
                if (lines == null)
                    lines = new GraphLine[(nodes.Length * (nodes.Length - 1)) / 2];
                if (isKruskal)
                    Kruskal();
                else
                    Prim();
                return MST;
        }

        private void Prim()
        {
            throw new NotImplementedException();
        }
        private void Kruskal()
        {
            int z = 0;
            MST = new List<GraphLine>(nodes.Length - 1);
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    lines[z++] = new GraphLine(nodes[i], nodes[j]);
                }
            }
            Array.Sort(lines);
            z = 0;
            UnionFind finder = new UnionFind(nodes);
            while (z < lines.Length - 1)
            {
                if (finder.Union(lines[z].From, lines[z].To))
                {
                    MST.Add(lines[z]);
                }
                z++;
            }
        }

    }

}
