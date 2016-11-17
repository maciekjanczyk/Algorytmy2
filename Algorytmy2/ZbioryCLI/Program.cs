using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbioryCLI
{
    using Zbiory;

    class Program
    {
        static void Main(string[] args)
        {
            Zbior zb = new Zbior(new int[] { 1, 3, 5, 7, 9 }.ToList());
            Console.WriteLine(zb);
            zb.Zlacz(1, 2, 1);
            Console.WriteLine(zb);
            zb.Zlacz(2, 3, 2);
            Console.WriteLine(zb);

            Console.ReadKey();
        }
    }
}
