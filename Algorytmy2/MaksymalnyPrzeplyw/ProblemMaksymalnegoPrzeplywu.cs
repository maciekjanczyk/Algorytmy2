using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaksymalnyPrzeplyw
{
    public class ProblemMaksymalnegoPrzeplywu
    {
        public static int[] DFS(int[,] G)
        {
            int N = G.GetLength(0);
            int[] kolor = new int[N];
            int[] p = new int[N];
            int[] d = new int[N];
            int[] f = new int[N];
            int time = 0;

            for (int u = 0; u < N; u++)
            {
                if (kolor[u] == 0)
                {
                    time = DFSV(G, u, kolor, p, d, f, time);
                }
            }

            return f;
        }

        private static int DFSV(int[,] G, int u, int[] kolor, int[] p, int[] d, int[] f, int time)
        {
            int N = G.GetLength(0);
            kolor[u] = 1;
            int new_time = time + 1;
            d[u] = new_time;

            for (int v = 0; v < N; v++)
            {
                if (G[u, v] != 0)
                {
                    if (kolor[v] == 0)
                    {
                        p[v] = u;
                        new_time = DFSV(G, v, kolor, p, d, f, new_time);
                    }
                }
            }

            kolor[u] = 2;
            new_time++;
            f[u] = new_time;

            return new_time;
        }

        public static bool CzyIstniejeDroga(int[,] G, List<int> odwiedzone, int s_index, int t_index, List<int> droga, int poprzednik = -1)
        {
            if (s_index == t_index)
            {
                droga.Add(s_index);
                return true;
            }
            else
            {
                odwiedzone.Add(s_index);
                bool ret = false;
                int N = G.GetLength(0);

                for (int i = 0; i < N; i++)
                {
                    if (odwiedzone.Contains(i))
                    {
                        continue;
                    }
                    else if (G[s_index, i] != 0)
                    {
                        ret = CzyIstniejeDroga(G, odwiedzone, i, t_index, droga, s_index);

                        if (ret)
                        {
                            droga.Add(s_index);
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public static int[,] GrafResidualny(int[,] przeplyw, int[,] przepustowosc)
        {
            int N = przeplyw.GetLength(0);
            int[,] ret = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    ret[i, j] = przepustowosc[i, j] - przeplyw[i, j];
                    //ret[j, i] = przeplyw[i, j];
                }
            }

            return ret;
        }

        private static int MinimalnyCf(int[,] Gr, List<int> droga)
        {
            List<int> wartosciCf = new List<int>();

            for (int i = 1; i < droga.Count; i++)
            {
                wartosciCf.Add(Gr[droga[i - 1], droga[i]]);
            }

            return wartosciCf.Min();
        }

        public static void FordFulkerson(int[,] przeplyw, int[,] przepustowosc, int s_index, int t_index)
        {
            int wymiar = przeplyw.GetLength(0);
            int[,] f = new int[wymiar, wymiar];
            int[,] G = GrafResidualny(przeplyw, przepustowosc);
            List<int> droga = new List<int>();
            List<int> odwiedzone = new List<int>();

            while (CzyIstniejeDroga(G, odwiedzone, s_index, t_index, droga))
            {
                droga.Reverse();
                int min_cf = MinimalnyCf(G, droga);

                for (int i = 1; i < droga.Count; i++)
                {
                    przeplyw[droga[i - 1], droga[i]] += min_cf;
                    G[droga[i - 1], droga[i]] -= min_cf;
                }

                droga = new List<int>();
                odwiedzone = new List<int>();
            }
        }
    }
}
