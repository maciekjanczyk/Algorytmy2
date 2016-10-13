using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem8Hetmanow
{
    public class Hetman8
    {
        private int[] pozycjaKolumna;
        private bool[] wWierszu;
        private bool[] naPrzekatnej1;
        private bool[] naPrzekatnej2;
        private int n;

        public Hetman8(int n = 8)
        {
            this.n = n;
            pozycjaKolumna = new int[n];
            wWierszu = new bool[n];
            naPrzekatnej1 = new bool[2 * n - 1];
            naPrzekatnej2 = new bool[2 * n - 1];
        }

        private int[] Probuj(int i, ref bool q)
        {            
            int j = -1;

            while ((!q) && (j < n - 1))
            {
                j += 1;
                q = false;

                if (!wWierszu[j] && !naPrzekatnej1[i + j] && !naPrzekatnej2[System.Math.Abs(i - j)])
                {
                    pozycjaKolumna[i] = j;
                    wWierszu[j] = true;
                    naPrzekatnej1[i + j] = true;
                    naPrzekatnej2[System.Math.Abs(i - j)] = true;

                    if (i < n)
                    {
                        Probuj(i + 1, ref q);

                        if (!q)
                        {
                            wWierszu[j] = false;
                            naPrzekatnej1[i + j] = false;
                            naPrzekatnej2[System.Math.Abs(i - j)] = false;
                        }
                    }
                    else
                    {
                        q = true;
                    }
                }
            }            

            return pozycjaKolumna;
        }

        public int[] RozwiazProblem()
        {
            bool q = false;
            return Probuj(0, ref q);
        }
    }
}
