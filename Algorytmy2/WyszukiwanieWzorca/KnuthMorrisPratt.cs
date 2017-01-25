using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WyszukiwanieWzorca
{
    public class KnuthMorrisPratt
    {        
        public static void Szukaj(string wzorzec, string tekst, TextWriter tw)
        {
            int m = wzorzec.Length, n = tekst.Length;
            int i = 1, j = 0;
            int[] P = ObliczPrefiks(wzorzec);
            while (i <= n - m + 1)
            {
                j = P[j];
                while ((j < m) && (wzorzec[j] == tekst[i + j - 1])) j++;
                if (j == m) tw.WriteLine(i);
                i = i + Math.Max(1, j - P[j]);
            }
        }

        private static int[] ObliczPrefiks(string wzorzec)
        {
            int m = wzorzec.Length;
            int[] P = new int[m + 1];
            int t = 0;
            P[0] = 0; P[1] = 0;
            for (int j = 2; j <= m; j++)
            {
                while ((t > 0) && (wzorzec[t] != wzorzec[j - 1])) t = P[t];
                if (wzorzec[t] == wzorzec[j - 1]) t++;
                P[j] = t;
            }

            return P;
        }
    }
}
