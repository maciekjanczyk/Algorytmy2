using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaszNaListach
{
    public static class FunkcjeHaszujace
    {
        public static int DlaLiczbOd0Do1(float a, int m)
        {
            return (int)(a * m);
        }

        public static int DlaLiczbOd0Do1(double a, int m)
        {
            return (int)(a * m);
        }

        public static int DlaLiczbCalkowitychOd0DoN(int a, int m)
        {
            return a % m;
        }

        public static int HaszowaniePrzezMnozenie(int a, int m)
        {
            return (int)(m * (a * Math.PI) % 1);
        }
    }

    public class Hasz<T> where T : IConvertible
    {
        public delegate int FunkcjaHasz(T el, int m);

        public int M { get; private set; }
        public FunkcjaHasz F { get; private set; }
        public List<T>[] Tab { get; private set; }
        public int StrFormat { get; set; }
        public int N { get; private set; }

        public Hasz(int m, FunkcjaHasz f, int strformat = 3)
        {
            Init(m, f, strformat);
        }

        public Hasz(IEnumerable<T> dataset, FunkcjaHasz f, int m = -1, int strformat = 3)
        {
            if (m == -1)
            {
                Init(dataset.Count() * 2, f, strformat);
            }
            else
            {
                Init(m, f, strformat);
            }

            Wloz(dataset);
        }

        private void Init(int m, FunkcjaHasz f, int strformat = 3)
        {
            M = m;
            F = f;
            N = 0;
            Tab = new List<T>[M];
            StrFormat = strformat;
        }

        public int LiczFunkcje(T el)
        {
            return F(el, M);
        }

        public int Szukaj(T el)
        {
            int idx = LiczFunkcje(el);

            if (Tab[idx] != null)
            {
                for (int i = 0; i < Tab[idx].Count; i++)
                {
                    if (Tab[idx][i].Equals(el))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public bool Wloz(T el)
        {
            int idx = LiczFunkcje(el);
            int odp = Szukaj(el);

            if (odp == -1)
            {
                if (Tab[idx] == null)
                {
                    Tab[idx] = new List<T>();
                }                

                Tab[idx].Add(el);
                N++;

                if (Tab.Length < N)
                {
                    PowiekszTablice();
                }

                return true;
            }

            return false;
        }

        public bool[] Wloz(IEnumerable<T> tab)
        {
            int count = tab.Count();
            bool[] ret = new bool[count];
            int i = 0;

            foreach (T el in tab)
            {
                ret[i] = Wloz(el);
                i++;
            }

            return ret;
        }

        public bool Usun(T el)
        {
            int idx = LiczFunkcje(el);
            int odp = Szukaj(el);

            if (odp != -1)
            {                
                Tab[idx].RemoveAt(odp);
                N--;

                if (M > 2.5 * N)
                {
                    PomnieszTablice();
                }

                return true;
            }

            return false;
        }

        private void PowiekszTablice()
        {
            N = 0;
            M = M * 2;

            List<T>[] oldTab = Tab;
            Tab = new List<T>[M];

            for (int i = 0; i < M / 2; i++)
            {
                if (oldTab[i] != null)
                {
                    foreach (T el in oldTab[i])
                    {
                        Wloz(el);
                    }
                }
            }
        }

        private void PomnieszTablice()
        {
            N = 0;
            M = M / 2;

            List<T>[] oldTab = Tab;
            Tab = new List<T>[M];

            for (int i = 0; i < M * 2; i++)
            {
                if (oldTab[i] != null)
                {
                    foreach (T el in oldTab[i])
                    {
                        Wloz(el);
                    }
                }
            }
        }

        public override string ToString()
        {
            string ret = "";
            string formatString = "{0," + StrFormat + "} ";

            for (int i = 0; i < M; i++)
            {
                ret += string.Format("[{0,3}] => [", i);

                if (Tab[i] != null)
                {
                    foreach (T el in Tab[i])
                    {
                        ret += string.Format(formatString, el);
                    }
                }                

                ret += "]\n";
            }

            return ret;
        }        
    }
}
