using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometriaObliczeniowa;
using ListResize;

namespace OtoczkaWypukla
{
    public enum Kierunek
    {
        Lewo,
        Prawo
    }

    public static class OtoczkaWypukla
    {
        public static List<Punkt> ConvexHull(List<Punkt> P)
        {
            int n = P.Count, k = 0;
            List<Punkt> H = (new Punkt[2 * n]).ToList();

            P = P.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            for (int i = 0; i < n; i++)
            {
                while (k >= 2 && Funkcje.Strona(H[k - 2], H[k - 1], P[i]) != 1)
                {
                    k--;
                }

                H[k++] = P[i];
            }

            for (int i = n - 2, t = k + 1; i >= 0; i--)
            {
                while (k >= t && Funkcje.Strona(H[k - 2], H[k - 1], P[i]) != 1)
                {
                    k--;
                }

                H[k++] = P[i];
            }

            //H.RemoveAll(x => ReferenceEquals(x, null));
            H.Resize(k);

            return H;
        }
    }
}
