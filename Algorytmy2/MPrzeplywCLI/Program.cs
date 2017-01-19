using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPrzeplywCLI
{
    using MaksymalnyPrzeplyw;

    class Program
    {
        static void Main(string[] args)
        {
            int[,] przeplyw =
            {
                { 0, 11, 8, 0, 0, 0 },
                { 0, 0, 1, 12, 0, 0 },
                { 0, 1, 0, 0, 11, 0 },
                { 0, 0, 4, 0, 0, 15 },
                { 0, 0, 0, 7, 0, 4 },
                { 0, 0, 0, 0, 0, 0 }
            };

            int[,] przepustowosc =
            {
                { 0, 16, 13, 0, 0, 0 },
                { 0, 0, 11, 12, 0, 0 },
                { 0, 4, 0, 0, 14, 0 },
                { 0, 0, 9, 0, 0, 20 },
                { 0, 0, 0, 7, 0, 4 },
                { 0, 0, 0, 0, 0, 0 }
            };

            /*int[,] przeplyw = new int[7, 7];

            int[,] przepustowosc =
            {
                {0, 9, 0, 0, 9, 0, 0},
                {0, 0, 7, 3, 0, 0, 0},
                {0, 0, 0, 4, 0, 0, 6},
                {0, 0, 0, 0, 0, 2, 9},
                {0, 0, 0, 3, 0, 6, 0},
                {0, 0, 0, 0, 0, 0, 8},
                {0, 0, 0, 0, 0, 0, 0}
            };*/

            int[,] GRes = ProblemMaksymalnegoPrzeplywu.GrafResidualny(przeplyw, przepustowosc);

            ProblemMaksymalnegoPrzeplywu.FordFulkerson(przeplyw, przepustowosc, 0, 6);

            for (int i = 0; i < przeplyw.GetLength(0); i++)
            {
                for (int j = 0; j < przeplyw.GetLength(0); j++)
                {
                    Console.Write("{0,3} ", GRes[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 0; i < przeplyw.GetLength(0); i++)
            {
                for (int j = 0; j < przeplyw.GetLength(0); j++)
                {
                    Console.Write("{0,3} ", przeplyw[i, j]);
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
