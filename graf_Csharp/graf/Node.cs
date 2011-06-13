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
    class Node
    {
        private int x;
        private int y;
        private int i;
        public Node()
        { }
        public Node(int x_, int y_, int i_)
        {
            x = x_;
            y = y_;
            i = i_;
        }
        public int X
        {
            set { x = value; }
            get { return x; }
        }
        public static bool operator ==(Node a, Node b)
        {
            if (a.i == b.i)
            {
                return true;
            }
            else
                return false;
        }
        public override bool Equals(object obj)
        {
            Node tmp = (Node)obj;
            if (tmp.i == this.i)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return this.I;
        }
        public static bool operator !=(Node a, Node b)
        {
            if (a == b)
                return false;
            else
                return true;
        }

        public static implicit operator Point(Node a)
        {
            Point z = new Point(a.X, a.Y);
            return z;
        }
        public int Y
        {
            set { y = value; }
            get { return y; }
        }
        public int I
        {
            set { i = value; }
            get { return i; }
        }
    }

}
