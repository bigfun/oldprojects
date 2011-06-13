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
    class UnionFind
    {
        private Dictionary<Node, Element> sets = null;
        private Dictionary<Node, int> sizes = null;
        class Element
        {
            public Element next = null;
            public Node value = null;
        }

        public UnionFind(Node[] tab)
        {
            sets = new Dictionary<Node, Element>();
            sizes = new Dictionary<Node, int>();
            foreach (Node elem in tab)
            {
                Element temp = new Element();
                temp.value = elem;
                sets.Add(elem, temp);
                sizes[elem] = 1;
            }
        }

        public Node Find(Node a)
        {
            Element temp = null;
            if (sets.ContainsKey(a)) return a; // jesli a jest reprezentantem zbioru zawierajacego a to zwracamy a
            else
            {
                foreach (Node elem in sets.Keys)
                {
                    temp = sets[elem];
                    while (temp != null)
                    {
                        if (temp.value == a)
                            return elem;
                        else
                        {
                            temp = temp.next;
                        }
                    }
                }
            }
            throw (new IndexOutOfRangeException("Blad Programu!"));


        }

        public bool Union(Node a, Node b)
        {
            Node fa = Find(a); // szukaj reprezentanta zbioru zawierającego element 'a'
            Node fb = Find(b); // szukaj reprezentanta zbioru zawierającego element 'b'

            if (fa == fb) return false; // nie trzeba nic łączyć
            if (sizes[fa] <= sizes[fb])
            {
                sizes[fb] += sizes[fa];
                Element temp = sets[fb];
                while (temp.next != null)
                    temp = temp.next;
                temp.next = sets[fa];
                sets.Remove(fa);
                sizes.Remove(fa);
            }
            else
            {
                sizes[fa] += sizes[fb];
                Element temp = sets[fa];
                while (temp.next != null)
                    temp = temp.next;
                temp.next = sets[fb];
                sets.Remove(fb);
                sizes.Remove(fb);
            }
            return true;
        }
    }
}
