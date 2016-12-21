using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyWeryfikacyjne
{
    public static class CyklHamiltona
    {       
        public static bool Weryfikuj(int[,] x, int[] vprim)
        {
            int xSize = x.GetLength(0);
            int ySize = vprim.GetLength(0);

            if (ySize != xSize + 1 || vprim[0] != vprim[ySize - 1])
            {
                return false;
            }

            for (int i = 1; i < ySize; i++)
            {
                if (x[vprim[i - 1], vprim[i]] == 0)
                {
                    return false;
                }
            }

            for (int j = 0; j < xSize; j++)
            {
                bool wewnatrz = false;

                for (int k = 0; k < ySize - 1; k++)
                {
                    if (j == vprim[k])
                    {
                        if (wewnatrz)
                        {
                            return false;
                        }

                        wewnatrz = true;
                    }
                }

                if (!wewnatrz)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
