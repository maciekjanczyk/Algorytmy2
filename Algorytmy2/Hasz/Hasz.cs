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
    }

    public class Hasz<T> where T: IConvertible
    {
        public delegate int FunkcjaHasz(T el, int m);

        public int M { get; private set; }
        public FunkcjaHasz F { get; private set; }
        public List<T>[] Tab { get; private set; }
        public int StrFormat { get; set; }

        public Hasz(int m, FunkcjaHasz f, int strformat = 3)
        {
            M = m;
            F = f;
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

                return true;
            }

            return false;
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

        public bool ExecuteCMD(string command)
        {
            bool ret = false;

            try
            {
                List<string> args = command.Split(' ').ToList();

                switch (args[0])
                {
                    case "dodaj":
                        {
                            args.RemoveAt(0);
                            ret = true;

                            foreach (string arg in args)
                            {
                                Wloz((T)Convert.ChangeType(arg, typeof(T)));
                            }
                        }

                        break;

                    case "usun":
                        {
                            args.RemoveAt(0);
                            ret = true;

                            foreach (string arg in args)
                            {
                                Usun((T)Convert.ChangeType(arg, typeof(T)));                                
                            }
                        }  

                        break;

                    case "szukaj":
                        {
                            ret = Szukaj((T)Convert.ChangeType(args[1], typeof(T))) != -1 ? true : false;
                        }

                        break;
                }
            }
            catch (Exception) { }

            return ret;
        }
    }
}
