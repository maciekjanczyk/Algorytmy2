using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbioryCLI
{
    using Zbiory;

    class Program
    {
        static void ZbioryOld()
        {
            Zbior zb = new Zbior(new int[] { 1, 3, 5, 7, 9 }.ToList());
            Console.WriteLine(zb);
            zb.Zlacz(1, 2, 1);
            Console.WriteLine(zb);
            zb.Zlacz(2, 3, 2);
            Console.WriteLine(zb);
        }

        static void WypiszGraf(int[,] G)
        {
            int size = G.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0,2} ", G[i, j]);
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int[,] G =
            {
                {0, 4, 5, 0, 0, 3},
                {4, 0, 0, 2, 2, 0},
                {5, 0, 0, 4, 0, 0},
                {0, 2, 4, 0, 3, 0},
                {0, 2, 0, 3, 0, 1},
                {3, 0, 0, 0, 1, 0}
            };

            Console.WriteLine("Graf wejsciowy: ");
            WypiszGraf(G);

            int[,] mst = MST.AlgorytmKruskala.Rozwiaz(G);
            Console.WriteLine("\nMST: ");
            WypiszGraf(mst);

            Console.ReadKey();
        }
    }
}
