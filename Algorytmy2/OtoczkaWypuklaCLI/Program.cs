using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtoczkaWypukla;
using GeometriaObliczeniowa;

namespace OtoczkaWypuklaCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Punkt> punkty = new List<Punkt>();

            punkty.Add(new Punkt(1.5, 0.5));
            punkty.Add(new Punkt(2.0, 1.0));
            punkty.Add(new Punkt(3.0, 1.0));
            punkty.Add(new Punkt(0.5, 1.5));
            punkty.Add(new Punkt(1.5, 1.5));
            punkty.Add(new Punkt(2.5, 2.0));
            punkty.Add(new Punkt(3.5, 2.0));
            punkty.Add(new Punkt(2.0, 2.5));
            punkty.Add(new Punkt(1.0, 2.5));
            punkty.Add(new Punkt(2.5, 3.0));

            List<Punkt> otoczka = OtoczkaWypukla.OtoczkaWypukla.ConvexHull(punkty);

            foreach (var pkt in otoczka)
            {
                Console.WriteLine("({0}, {1})", pkt.X, pkt.Y);
            }

            Console.ReadKey();
        }
    }
}
