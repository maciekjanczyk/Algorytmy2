using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranzytywneDomkniecie
{
    public class TranzytywneDomkniecie
    {
        public static int[,] FloydWarshall(int[,] graf)
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
			        for (int j = 0; j < iloscWezlow; j++)
			        {
				        ret[i, j] = ret[i, j] > 0 || (ret[i, k] > 0 && ret[k, j] > 0) ? 1 : 0;
			        }
                }       
	        }

            return ret;          
        }
    }
}
