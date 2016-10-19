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
        private bool[] brakWWierszu;
        private bool[] brakNaPrzekatnej1;
        private bool[] brakNaPrzekatnej2;
        private int n;

        public Hetman8(int n = 8)
        {
            this.n = n;
            pozycjaKolumna = new int[n];
            brakWWierszu = Enumerable.Repeat(true, n).ToArray();
            brakNaPrzekatnej1 = Enumerable.Repeat(true, 2 * n - 1).ToArray();
            brakNaPrzekatnej2 = Enumerable.Repeat(true, 2 * n - 1).ToArray();
        }

        private int[] Probuj(int i, ref bool q)
        {                        
            for (int j = 0; j < n && (!q); j++)
            {               
                q = false;
                int idx1 = i + j;
                int idx2 = i - j + n - 1;

                if (brakWWierszu[j] && brakNaPrzekatnej1[idx1] && brakNaPrzekatnej2[idx2])
                {
                    pozycjaKolumna[i] = j;
                    brakWWierszu[j] = false;
                    brakNaPrzekatnej1[idx1] = false;
                    brakNaPrzekatnej2[idx2] = false;

                    if (i < n - 1)
                    {
                        Probuj(i + 1, ref q);

                        if (!q)
                        {
                            brakWWierszu[j] = true;
                            brakNaPrzekatnej1[idx1] = true;
                            brakNaPrzekatnej2[idx2] = true;
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

        private bool ProbujN(int i, int n_max, List<int[]> multiret, ref bool q)
        {
            for (int j = 0; j < n; j++)
            {
                q = false;
                int idx1 = i + j;
                int idx2 = i - j + n - 1;

                if (brakWWierszu[j] && brakNaPrzekatnej1[idx1] && brakNaPrzekatnej2[idx2])
                {
                    pozycjaKolumna[i] = j;
                    brakWWierszu[j] = false;
                    brakNaPrzekatnej1[idx1] = false;
                    brakNaPrzekatnej2[idx2] = false;

                    if (i < n - 1)
                    {
                        ProbujN(i + 1, n_max, multiret, ref q);

                        if (!q)
                        {
                            brakWWierszu[j] = true;
                            brakNaPrzekatnej1[idx1] = true;
                            brakNaPrzekatnej2[idx2] = true;
                        }
                    }
                    else
                    {
                        q = true;
                        multiret.Add((int[])pozycjaKolumna.Clone());

                        if (multiret.Count > n_max && n_max != -1)
                        {
                            return q;
                        }
                    }
                }
            }

            return q;
        }

        public int[] RozwiazProblem()
        {
            bool q = false;
            int[] ret = Probuj(0, ref q);

            return ret;
        }

        public List<int[]> RozwiazProblem(int n_max = -1)
        {
            List<int[]> multiret = new List<int[]>();
            bool q = false;
            ProbujN(0, n_max, multiret, ref q);

            return multiret;
        }
    }
}
