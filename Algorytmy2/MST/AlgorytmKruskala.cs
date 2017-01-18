using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zbiory;

namespace MST
{
    public static class AlgorytmKruskala
    {
        public static int[,] Rozwiaz(int[,] G)
        {
            int size = G.GetLength(0);
            Zbior zbior = new Zbior(Enumerable.Range(0, size));
            int[,] A = new int[size, size];
            int i = 1;

            List<Tuple<int[], int>> E = PosortujKrawedzie(G);

            foreach (var e in E)
            {
                int u = e.Item1[0], v = e.Item1[1], waga = e.Item2;

                if (zbior.Szukaj(u) != zbior.Szukaj(v))
                {
                    A[u, v] = waga;
                    zbior.Zlacz(u, v, u);
                }
            }
        
            return A;
        }

        private static List<Tuple<int[], int>> PosortujKrawedzie(int[,] G)
        {
            List<Tuple<int[], int>> E = new List<Tuple<int[], int>>();
            int size = G.GetLength(0);

            for (int u = 0; u < size; u++)
            {
                for (int v = 0; v < size; v++)
                {
                    if (G[u, v] != 0)
                    {
                        E.Add(new Tuple<int[], int>(new int[] { u, v }, G[u, v]));
                    }
                }
            }

            E.Sort((e1, e2) => { return e1.Item2 > e2.Item2 ? 1 : -1; });
            
            return E;
        }
    }
}
