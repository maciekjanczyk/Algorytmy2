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
        public List<int>[] SSSDrogi { get; private set; }

        public Problem2CNF(string expr, string zmienna = "x")
        {
            GrafImplikacji = StworzGrafImplikacji(expr, zmienna);
            SSS = new SSS(ListaListDoMacierzy(GrafImplikacji));
            int offset = GrafImplikacji.Count / 2; // bo !2 = 2 + offset
            TworzDrogiSSS();

            for (int u = 0; u < SSS.Size; u++)
            {
                if (SSSDrogi[u].Count != SSSDrogi[u].Distinct().Count())
                {
                    Wynik = false;
                    return;
                }
            }

            Wynik = true;
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
                        ret[i, j] = -1;
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

        private void TworzDrogiSSS()
        {
            SSSDrogi = new List<int>[SSS.Size];

            for (int i = 0; i < SSS.Size; i++)
            {
                SSSDrogi[i] = new List<int>();
            }

            for (int i = 0; i < SSS.Size; i++)
            {
                for (int j = 0; j < SSS.Size; j++)
                {
                    if (SSS.Rozwiazanie[i, j] != -1)
                    {
                        SSSDrogi[i].Add(j);
                    }
                }
            }

            // tworzymy drogi bez negacji - dzieki temu zamiast sprawdzac czy 2 i !2 sa na tej samej SSS to zobaczymy
            // czy na ktorejs SSS wystepuje 2 razy ta sama liczba

            int offset = SSS.Size / 2;
            for (int i = 0; i < SSS.Size; i++)
            {
                for (int j = 0; j < SSSDrogi[i].Count; j++)
                {
                    if (SSSDrogi[i][j] >= offset) // przez to ze !2 to = 2 + offset
                    {
                        SSSDrogi[i][j] -= offset;
                    }
                }
            }
        }
    }
}
