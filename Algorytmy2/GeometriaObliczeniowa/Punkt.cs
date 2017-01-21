using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometriaObliczeniowa
{
    public class Punkt
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool UnDef { get; set; }
        public bool Paral { get; set; }

        public Punkt(double x, double y)
        {
            X = x;
            Y = y;
            UnDef = false;
            Paral = false;
        }

        public static bool operator ==(Punkt a, Punkt b)
        {
            if (Funkcje.CzyJestZerem(a.X - b.X) && Funkcje.CzyJestZerem(a.Y - b.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Punkt a, Punkt b)
        {
            return !(a == b);
        }

        public static bool operator <(Punkt a, Punkt b)
        {
            return a.X < b.X || (Funkcje.CzyJestZerem(a.X - b.X) && a.Y < b.Y);
        }

        public static bool operator >(Punkt a, Punkt b)
        {
            return !(a < b);
        }
    }
}
