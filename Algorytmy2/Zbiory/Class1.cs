using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zbiory
{
    public class Zbior
    {
        public int[] Elements { get; private set; }
        private int[] count;
        private int[] name;
        private int[] father;
        private int[] root;
        private int n;

        public Zbior(IEnumerable<int> dane)
        {
            n = dane.Count();
            count = new int[n];
            name = new int[n];
            father = new int[n];
            root = new int[n];
            Elements = new int[n];

            int i = 0;
            foreach (int el in dane)
            {
                Elements[i] = el;
                count[i] = 1;
                name[i] = i;
                father[i] = -1;
                root[i] = i;
                i++;
            }
        }

        public int Szukaj(int i)
        {
            List<int> list = new List<int>();
            int v = i;

            while (father[v] != -1)
            {
                list.Add(v);
                v = father[v];
            }

            foreach (int w in list)
            {
                father[w] = v;
            }

            return name[v];
        }

        public void Zlacz(int i, int j, int k)
        {
            if (count[root[i]] > count[root[j]])
            {
                int tmp = i;
                i = j;
                j = tmp;
            }

            int large = root[j];
            int small = root[i];

            father[small] = large;
            count[large] += count[small];
            name[large] = k;
            root[k] = large;
        }

        public override string ToString()
        {            
            StringWriter tw = new StringWriter();
            Wypisz(tw);

            return tw.ToString();
        }

        public void Wypisz(TextWriter tw)
        {
            Console.WriteLine("{0,4} {1,4} {2,4} {3,4} {4,4}", "ELEM", "NAME", "CNT", "FATH", "ROOT");

            for (int i = 0; i < n; i++)
            {                
                Console.WriteLine("{0,4} {1,4} {2,4} {3,4} {4,4}", Elements[i], name[i], count[i], father[i], root[i]);
            }
        }
    }
}
