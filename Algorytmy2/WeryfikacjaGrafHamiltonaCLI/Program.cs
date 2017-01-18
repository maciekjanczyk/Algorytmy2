using AlgorytmyWeryfikacyjne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeryfikacjaGrafHamiltonaCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] graf = new int[,]
            {
                { 0, 1, 0, 1, 1, 0, 0, 0 },
                { 1, 0, 1, 0, 1, 1, 0, 0 },
                { 0, 1, 0, 1, 0, 0, 1, 0 },
                { 1, 0, 1, 0, 0, 1, 1, 0 },
                { 1, 1, 0, 0, 1, 0, 0, 1 },
                { 0, 1, 0, 1, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 1, 1, 1, 0 }
            };
            int[] vprim = new int[] { 0, 4, 7, 6, 2, 1, 5, 3, 0 };

            Console.WriteLine(CyklHamiltona.Weryfikuj(graf, vprim));

            graf = new int[,]
            {
                {0, 1, 0, 1, 0},
                {0, 0, 1, 0, 0},
                {1, 0, 0, 0, 0},
                {0, 0, 0, 0, 1},
                {1, 0, 0, 0, 0}
            };
            vprim = new int[] { 0, 1, 2, 0, 3, 4, 0 };

            Console.WriteLine(CyklHamiltona.Weryfikuj(graf, vprim));

            Console.ReadKey();
        }
    }
}
