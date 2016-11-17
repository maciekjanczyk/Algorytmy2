using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulyLogiczne
{
    using ParserWyrazenLogicznych;

    public class Problem2CNF
    {
        public List<List<int>> GrafImplikacji { get; set; }
        public int Offset { get; private set; }

        public Problem2CNF(string expr, string zmienna = "x")
        {
            GrafImplikacji = StworzGrafImplikacji(expr, zmienna);
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
