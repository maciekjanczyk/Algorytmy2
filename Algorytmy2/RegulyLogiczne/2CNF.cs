using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulyLogiczne
{
    using ParserWyrazenLogicznych;
    using SilnieSpojneSkladowe;

    public class Problem2CNF
    {
        public List<List<int>> GrafImplikacji { get; set; }
        public int Offset { get; private set; }
        public SSS SSS { get; private set; }
        public bool Wynik { get; private set; }
        public int[] Wartosci { get; private set; }

        public Problem2CNF(string expr, string zmienna = "x")
        {
            GrafImplikacji = StworzGrafImplikacji(expr, zmienna);
            var gimp = ListaListDoMacierzy(GrafImplikacji);
            SSS = new SSS(gimp);
            int offset = GrafImplikacji.Count / 2;

            for (int u = 0; u < SSS.Size / 2; u++)
            {
                foreach (var rozw in SSS.Rozwiazanie)
                {
                    if (rozw.Contains(u) && rozw.Contains(u + offset))
                    {
                        Wynik = false;
                        return;
                    }
                }
            }

            Wynik = true;

            Wartosci = new int[SSS.Size];

            for (int i = 0; i < SSS.Size; i++)
            {
                Wartosci[i] = -1;
            }

            foreach (var c in SSS.Rozwiazanie)
            {
                int len2 = 0;

                foreach (int v in c)
                {
                    if (Wartosci[v] == -1)
                    {
                        len2++;
                    }
                }

                if (c.Count == len2)
                {
                    foreach (int v in c)
                    {
                        int factor = v >= SSS.Size / 2 ? -1 : 1;
                        Wartosci[v] = 0;
                        Wartosci[v + offset * factor] = 1;
                    }
                }
                else
                {
                    int ustalona = -1;

                    for (int v = 0; v < c.Count; v++)
                    {
                        if (Wartosci[v] != -1)
                        {
                            ustalona = v;
                            break;
                        }
                    }

                    for (int v = 0; v < c.Count; v++)
                    {
                        if (Wartosci[v] == -1)
                        {
                            int factor = v >= SSS.Size / 2 ? -1 : 1;
                            Wartosci[v] = Wartosci[ustalona];
                            Wartosci[v + offset * factor] = Wartosci[v] > 0 ? 0 : 1;
                        }
                    }
                }
            }
        }

        public static int[,] ListaListDoMacierzy(List<List<int>> gi)
        {
            int[,] ret = new int[gi.Count, gi.Count];

            for (int i = 0; i < gi.Count; i++)
            {
                for (int j = 0; j < gi.Count; j++)
                {
                    if (gi[i].Contains(j))
                    {
                        ret[i, j] = 1;
                    }
                    else
                    {
                        ret[i, j] = 0;
                    }
                }
            }

            return ret;
        }

        public List<List<int>> StworzGrafImplikacji(string expr, string zmienna)
        {
            List<List<int>> ret = new List<List<int>>();
            int maxidx = -1;
            List<Tuple<string, string>> lista = ParserWL.CNF2(expr, out maxidx);
            Offset = maxidx + 1;

            for (int i = 0; i < Offset * 2; i++)
            {
                ret.Add(new List<int>());
            }            

            foreach (var tuple in lista)
            {
                string it1 = tuple.Item1.Replace(zmienna, string.Empty);
                string it2 = tuple.Item2.Replace(zmienna, string.Empty);

                int idx1 = it1.Contains("!") ? Convert.ToInt32(it1.Replace("!", string.Empty)) + Offset : Convert.ToInt32(it1);
                int idx2 = it2.Contains("!") ? Convert.ToInt32(it2.Replace("!", string.Empty)) + Offset : Convert.ToInt32(it2);

                ret[idx1 < Offset ? idx1 + Offset : idx1 - Offset].Add(idx2);
                ret[idx2 < Offset ? idx2 + Offset : idx2 - Offset].Add(idx1);
            }

            return ret;
        }
    }
}
