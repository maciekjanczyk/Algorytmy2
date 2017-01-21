using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometriaObliczeniowa
{
    public static class Funkcje
    {
        public readonly static double EPS = 1E-9;

        public static bool CzyJestZerem(double x)
        {
            return x >= -EPS && x <= EPS;
        }

        public static double Skal(Punkt a, Punkt b, Punkt c)
        {
            return (b.X - a.X) * (c.X - a.X) + (b.Y - a.Y) * (c.Y - a.Y);
        }

        public static double Dist(Punkt a, Punkt b)
        {
            return Math.Sqrt(Skal(a, b, b));
        }

        public static double DlRzutu(Punkt a, Punkt b, Punkt c)
        {
            return Skal(a, b, c) / Dist(a, b);
        }

        public static Punkt Rzut(Punkt a, Punkt b, Punkt c)
        {
            double f = Skal(a, b, c) / Skal(a, b, b);

            return new Punkt(a.X + f * (b.X - a.X), a.Y + f * (b.Y - a.Y));
        }

        public static double Det(Punkt a, Punkt b, Punkt c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }

        public static double Det(Punkt a, Punkt b, Punkt c, Punkt d)
        {
            return (Det(a, d, b) + Det(a, b, c)) / 2.0;
        }

        public static int Sgn(double x)
        {
            return x < -EPS ? -1 : (x > EPS ? 1 : 0);
        }

        public static int Strona(Punkt a, Punkt b, Punkt c)
        {
            return Sgn(Det(a, b, c));
        }

        public static double Pole(Punkt a, Punkt b, Punkt c)
        {
            return Math.Abs(Det(a, b, c)) / 2.0;
        }

        public static Punkt PrzecieciePunktow(Punkt a, Punkt b, Punkt p, Punkt q)
        {
            double p1 = Det(a, b, p, q);
            double p2 = Det(a, b, p) / 2.0;
            double st = p2 / p1;
            Punkt res_p = new Punkt(0.0, 0.0);

            if (CzyJestZerem(p1))
            {
                res_p.UnDef = true;

                return res_p;
            }

            if (CzyJestZerem(p2 * 2.0 - p1))
            {
                res_p.Paral = true;

                return res_p;
            }

            return new Punkt(p.X + (q.X - p.X) * st, p.Y + (q.Y - p.Y) * st);
        }

        public static Punkt PrzeciecieOdcinkow(Punkt a, Punkt b, Punkt c, Punkt d)
        {
            Punkt p = PrzecieciePunktow(a, b, c, d);

            if (p.Paral || p.UnDef)
            {
                p.UnDef = true;
            }

            if (Segment(a, b, p) != 0 || Segment(c, d, p) != 0)
            {
                p.UnDef = true;
            }

            return p;
        }

        public static int Segment(Punkt a, Punkt b, Punkt c)
        {
            if (Skal(a, b, c) < 0.0)
            {
                return -1;
            }
            else if (Skal(b, a, c) < 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
