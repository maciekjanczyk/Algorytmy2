using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranzytywnaRedukcja
{
    public class TranzytywnaRedukcja
    {
        public static int[,] Hsu(int[,] graf)
        {
            int iloscWezlow = graf.GetLength(0);
            int[,] ret = new int[iloscWezlow, iloscWezlow];

            for (int i = 0; i < iloscWezlow; i++)
            {
                for (int j = 0; j < iloscWezlow; j++)
                {
                    ret[i, j] = graf[i, j];
                }
            }

            for (int k = 0; k < iloscWezlow; k++)
            {
                for (int i = 0; i < iloscWezlow; i++)
                {
                    if (ret[i, k] > 0)
                    {
                        for (int j = 0; j < iloscWezlow; j++)
                        {
                            if (ret[k, j] > 0)
                            {
                                ret[i, j] = 0;
                            }
                        }
                    }
                }
            }

            return ret;
        }

        private static int[,] MacierzSciezek(int[,] graf)
        {
            int[,] ret = (int[,])graf.Clone();
            int iloscWezlow = ret.GetLength(0);

            for (int i = 0; i < iloscWezlow; i++)
            {
                for (int j = 0; j < iloscWezlow; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (ret[j, i] > 0)
                    {
                        for (int k = 0; k < iloscWezlow; k++)
                        {
                            if (ret[j, k] == 0)
                            {
                                ret[j, k] = ret[i, k];
                            }
                        }
                    }
                }
            }

            return ret;
        }

        public static int[,] Hsu2(int[,] graf)
        {
            return Hsu(MacierzSciezek(graf));
        }
    }
}
