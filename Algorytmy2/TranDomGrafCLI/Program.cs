using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranDomGrafCLI
{
    class Program
    {
        static int[,] WczytajGraf()
        {
            Console.Write("Z ilu wierzcholkow sklada sie graf?: ");
            int iloscWierzcholkow = Convert.ToInt32(Console.ReadLine());
            int[,] graf = new int[iloscWierzcholkow, iloscWierzcholkow];

            Console.WriteLine("Wpisz graf skierowany (0 - brak krawedzi, 1 - jest krawedz). Liczby oddzielaj spacja. Pozycje (i,i) oznacz jako 1:");
            for (int i = 0; i < iloscWierzcholkow; i++)
            {
                string[] linia = Console.ReadLine().Split(' ').ToArray();

                for (int j = 0; j < iloscWierzcholkow; j++)
                {
                    graf[i, j] = Convert.ToInt32(linia[j]);
                }
            }

            return graf;
        }

        static void PokazGraf(int[,] graf, string podpis)
        {
            int iloscWierzcholkow = graf.GetLength(0);

            Console.WriteLine("{0}: ", podpis);

            for (int i = 0; i < iloscWierzcholkow; i++)
            {
                for (int j = 0; j < iloscWierzcholkow; j++)
                {
                    Console.Write("{0,2} ", graf[i, j]);
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {            
            int[,] graf = WczytajGraf();            
            int[,] tdomkniecie = TranzytywneDomkniecie.TranzytywneDomkniecie.FloydWarshall(graf);

            Console.WriteLine();
            PokazGraf(graf, "Graf wejsciowy");
            Console.WriteLine();
            PokazGraf(tdomkniecie, "Tranzytywne domkniecie");

            Console.ReadKey();
        }
    }
}
