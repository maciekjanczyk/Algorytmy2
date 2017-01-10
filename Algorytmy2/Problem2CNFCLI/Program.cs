using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulyLogiczne;

namespace Problem2CNFCLI
{
    class Program
    {
        static void WyswietlGrafImplikacji(Problem2CNF cnf)
        {
            for (int i = 0; i < cnf.GrafImplikacji.Count; i++)
            {
                Console.Write("{0}: ", i < cnf.Offset ? Convert.ToString(i) : "!" + Convert.ToString(i - cnf.Offset));
                for (int j = 0; j < cnf.GrafImplikacji[i].Count; j++)
                {
                    int s = cnf.GrafImplikacji[i][j];
                    Console.Write("{0}, ", s < cnf.Offset ? Convert.ToString(s) : "!" + Convert.ToString(s - cnf.Offset));
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            string expr = "(x0|x2)&(x0|!x3)&(x1|!x3)&(x1|!x4)&(x2|!x4)&(x0|!x5)&(x1|!x5)&(x2|!x5)&(x3|x6)&(x4|x6)&(x5|x6)";
            Problem2CNF cnf = new Problem2CNF(expr);

            WyswietlGrafImplikacji(cnf);

            Console.ReadKey();
        }
    }
}
