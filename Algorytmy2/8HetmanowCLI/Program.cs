using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem8Hetmanow;

namespace _8HetmanowCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Jaki rozmiar szachownicy?: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Hetman8 hetman8 = new Hetman8(n);
            int[] rozw = hetman8.RozwiazProblem();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == rozw[j])
                    {
                        Console.Write("{0,3} ", rozw[j]);
                    }
                    else
                    {
                        Console.Write("{0,3} ", '-');
                    }
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
