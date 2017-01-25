using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WyszukiwanieWzorca
{
    public static class ProsteWyszukiwanie
    {
        /*n=lenght(T);
3 m=length(P);
4 for (s=0 to n−m)
5 if (T[s + 1..s + m] = P[1..m] )
6 return s;
7 return −1;*/

        public static int Szukaj(string P, string T)
        {
            int n = T.Length, m = P.Length;

            for (int s = -1; s < n - m; s++)
            {
                var sbstr1 = T.Substring(s + 1, m);
                var sbstr2 = P.Substring(0, m);
                if (sbstr1 == P)
                {
                    return s;
                }
            }

            return -1;
        }
    }
}
