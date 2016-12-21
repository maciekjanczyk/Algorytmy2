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
            List<int> droga = new List<int>();
            List<int> tmp = new List<int>();
            int[,] G =
            {
                { 0, 16, 13, 0, 0, 0 },
                { 0, 0, 10, 12, 0, 0 },
                { 0, 4, 0, 0, 14, 0 },
                { 0, 0, 9, 0, 0, 20 },
                { 0, 0, 0, 7, 0, 4 },
                { 0, 0, 0, 0, 0, 0 }
            };            

            ProblemMaksymalnegoPrzeplywu.CzyIstniejeDroga(G, tmp, 0, 5, droga);
            droga.Reverse();
            
            foreach (int d in droga)
            {
                Console.WriteLine(d);
            }

            Console.ReadKey();
        }
    }
}
