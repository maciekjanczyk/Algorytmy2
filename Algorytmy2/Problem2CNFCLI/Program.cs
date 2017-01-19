using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulyLogiczne;
using SilnieSpojneSkladowe;

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

        static void TestujSSS()
        {
            int[,] G = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    G[i, j] = 0; ;

            G[0, 1] = 1;
            G[1, 2] = 1;
            G[1, 4] = 1;
            G[1, 5] = 1;
            G[2, 3] = 1;
            G[2, 6] = 1;
            G[3, 2] = 1;
            G[3, 7] = 1;
            G[4, 0] = 1;
            G[4, 5] = 1;
            G[5, 6] = 1;
            G[6, 5] = 1;
            G[6, 7] = 1;
            G[7, 7] = 1;

            SSS sss = new SSS(G);
            int size = sss.Rozwiazanie.GetLength(1);

            /*for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0,2} ", sss.Rozwiazanie[i, j]);
                }

                Console.WriteLine();
            }*/
            foreach (var s in sss.Rozwiazanie)
            {
                foreach (int jj in s)
                {
                    Console.Write("{0} ", jj);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            //string expr = "(x0|x2)&(x0|!x3)&(x1|!x3)&(x1|!x4)&(x2|!x4)&(x0|!x5)&(x1|!x5)&(x2|!x5)&(x3|x6)&(x4|x6)&(x5|x6)";
            string expr = "(x0|x2)&(x0|!x3)&(x1|!x3)&(x1|!x4)&(x2|!x4)&(x0|!x5)&(x1|!x5)&(!x2|!x5)&(x3|x6)&(x4|x6)&(x5|!x6)";            
            //string expr = "(x0|x1)&(!x0|x1)";

            Problem2CNF cnf = new Problem2CNF(expr);

            Console.WriteLine("Regula:\n{0}\n", expr);
            Console.WriteLine("Czy regula jest spelnialna: {0}", cnf.Wynik);
            //TestujSSS();
            if (cnf.Wynik)
            {
                Console.WriteLine("\nRozwiazanie:");

                for (int i = 0; i < cnf.Wartosci.Length / 2; i++)
                {
                    Console.Write("x{0} ", i);
                }

                Console.WriteLine();
                for (int i = 0; i < cnf.Wartosci.Length / 2; i++)
                {
                    Console.Write("{0,2} ", cnf.Wartosci[i]);
                }

                Console.WriteLine();
                for (int i = cnf.Wartosci.Length / 2; i < cnf.Wartosci.Length; i++)
                {
                    Console.Write("{0,2} ", cnf.Wartosci[i]);
                }
            }

            Console.ReadKey();
        }
    }
}
