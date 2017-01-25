using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemPlecakowy;

namespace PlecakCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            /**                75 7
2 Spodnie dżinsowe 150 8
3 Sweter           250 6
4 Czapka baseball   35 4
5 Kąpielówki        10 3
6 Obuwie sportowe  100 9*/

            int[] c = 
            {
                75,
                150,
                250,
                35,
                10,
                100
            };

            int[] w =
            {
                7,
                8,
                6,
                4,
                3,
                9
            };

            var A = Plecak.PlecakDynamiczny2(10, w, c);

            for (int i = 0; i < c.Length; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write("{0,3} ", A[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
