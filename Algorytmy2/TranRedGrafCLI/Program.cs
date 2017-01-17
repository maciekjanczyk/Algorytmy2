using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranRedGrafCLI
{
    class Program
    {
        static int[,] WczytajGraf()
        {
            Console.Write("Z ilu wierzcholkow sklada sie graf?: ");
            int iloscWierzcholkow = Convert.ToInt32(Console.ReadLine());
            int[,] graf = new int[iloscWierzcholkow, iloscWierzcholkow];

            Console.WriteLine("Wpisz graf skierowany (0 - brak krawedzi, 1 - jest krawedz). Liczby oddzielaj spacja:");
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
            int[,] tdomkniecie1 = TranzytywnaRedukcja.TranzytywnaRedukcja.Hsu(graf);
            int[,] tdomkniecie2 = TranzytywnaRedukcja.TranzytywnaRedukcja.Hsu2(graf);

            Console.WriteLine();
            PokazGraf(graf, "Graf wejsciowy");
            Console.WriteLine();
            PokazGraf(tdomkniecie1, "Tranzytywna redukcja metoda Hsu");
            Console.WriteLine();
            PokazGraf(tdomkniecie2, "Tranzytywna redukcja metoda Hsu2");

            Console.ReadKey();
        }
    }
}
