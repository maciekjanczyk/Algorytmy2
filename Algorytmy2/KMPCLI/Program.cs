using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPCLI
{
    class Program
    {
        static void ProsteWyszukiwanie(string wzorzec, string wyraz)
        {
            Console.WriteLine("Proste wyszukiwanie:");
            int pos = WyszukiwanieWzorca.ProsteWyszukiwanie.Szukaj(wzorzec, wyraz);

            if (pos == 1)
            {
                Console.WriteLine("Nie znaleziono.");
            }
            else
            {
                Console.WriteLine("Indeks: {0}", pos);
                Console.WriteLine(wyraz);

                for (int i = 0; i <= pos; i++)
                {
                    Console.Write(" ");
                }

                Console.WriteLine(wzorzec);
            }
        }

        static void KMP(string wzorzec, string wyraz)
        {
            Console.WriteLine("KMP:");
            Console.WriteLine(wyraz);
            WyszukiwanieWzorca.KnuthMorrisPratt.Szukaj(wyraz, wzorzec, Console.Out);            
        }

        static void Main(string[] args)
        {
            Console.Write("Wpisz wyraz: ");
            string wyraz = Console.ReadLine();
            Console.Write("Wpisz wzorzec: ");
            string wzorzec = Console.ReadLine();
            Console.WriteLine();

            ProsteWyszukiwanie(wzorzec, wyraz);
            Console.WriteLine();
            KMP(wzorzec, wyraz);

            Console.ReadKey();
        }
    }
}
