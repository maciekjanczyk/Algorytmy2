using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyklEulera
{
    public static class CyklEulera
    {       
        private static Stack<int> PobierzSasiadow(int[,] G, int v)
        {
            Stack<int> ret = new Stack<int>();

            for (int i = 0; i < G.GetLength(0); i++)
            {
                if (G[v, i] != 0)
                {
                    ret.Push(i);
                }
            }

            return ret;
        }

        private static Stack<int>[] TabliceSasiadow(int[,] G)
        {
            int len = G.GetLength(0);
            Stack<int>[] ret = new Stack<int>[len];

            for (int v = 0; v < len; v++)
            {
                ret[v] = PobierzSasiadow(G, v);
            }

            return ret;
        }

        public static List<int> Stworz(int[,] G, int u)
        {                        
            if (u < 0 || u >= G.GetLength(0))
            {
                return null;
            }

            List<int> ret = new List<int>();
            Stack<int> stos = new Stack<int>();
            Stack<int>[] listySasiadow = TabliceSasiadow(G);

            stos.Push(u);

            while (stos.Count > 0)
            {
                int v = stos.Peek();

                var S = listySasiadow[v];


                if (S.Count == 0)
                {
                    stos.Pop();
                    ret.Add(v);
                }
                else
                {
                    int w = S.Pop();
                    stos.Push(w);
                }
            }

            return ret;
        }
    }
}
