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
    class GraphLine : IComparable
    {
        private Node from;
        private Node to;
        private double length;

        #region IComparable Members
        public int CompareTo(object obj)
        {
            GraphLine temp = (GraphLine)obj;
            if (temp.Length < this.Length)
                return 1;
            if (temp.Length > this.Length)
                return -1;
            else
                return 0;
        }

        #endregion
        public static bool operator ==(GraphLine a, GraphLine b)
        {
            if (a.from == b.from && a.to == b.to)
                return true;
            else
                return false;
        }
        public static bool operator !=(GraphLine a, GraphLine b)
        {
            if (a == b)
                return false;
            else
                return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            GraphLine x = (GraphLine)obj;
            if (this.from == x.from && this.to == x.to)
                return true;
            else
                return false;
        }
        public GraphLine()
        {
            from = new Node();
            to = new Node();
            length = 0.0f;
        }
        public GraphLine(Node p1, Node p2)
        {
            from = p1;
            to = p2;
            Calc_length();
        }
        public double Length
        {
            get { return length; }
        }
        private void Calc_length()
        {
            this.length = Math.Sqrt((Math.Pow((this.from.X - this.to.X), 2) + Math.Pow((this.from.Y - this.to.Y), 2)));
        }
        public Node From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
                Calc_length();
            }
        }
        public Node To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
                Calc_length();
            }
        }

    }
}
