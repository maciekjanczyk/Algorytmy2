using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemSkoczka;

namespace SkoczekCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj wymiar szachownicy: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Skoczek skoczek = new Skoczek(n);
            Console.Write("Podaj punkt startowy (indeksacja od 1, notacja z przecinkiem bez spacji): ");
            string[] separated = Console.ReadLine().Split(',');
            int x = Convert.ToInt32(separated[0]);
            int y = Convert.ToInt32(separated[1]);
            int [,] rozw = skoczek.RozwiazProblem(x, y);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0,2} ", rozw[i, j]);                    
                }

                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
