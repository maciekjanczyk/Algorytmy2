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
        static void WypiszSzachownice(int[] rozw)
        {
            int n = rozw.Length;

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
        }

        static void Main(string[] args)
        {
            Console.Write("Jaki rozmiar szachownicy?: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Hetman8 hetman8 = new Hetman8(n);

            Console.Write("Kilka rozwiazan? [t/n]: ");
            bool kilkaRozw = Console.ReadLine().Equals("t") ? true : false;

            if (!kilkaRozw)
            {
                int[] rozw = hetman8.RozwiazProblem();
                WypiszSzachownice(rozw);
            }
            else
            {
                List<int[]> rozwiazania = hetman8.RozwiazProblem(-1);
                
                foreach (int[] rozw in rozwiazania)
                {
                    WypiszSzachownice(rozw);
                }
            }        

            Console.ReadKey();
        }
    }
}
