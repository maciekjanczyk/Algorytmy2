using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemPlecakowy
{
    public static class Plecak
    {
        public static int[,] PlecakDynamiczny2(int W, int[] w, int[] c)
        {
            if (w.Length != c.Length)
            {
                return null;
            }

            int N = w.Length;
            int[,] A = new int[N, W];

            for (int i = 0; i < N; i++)
            {
                A[i, 0] = 0;
            }

            for (int j = 0; j < W; j++)
            {
                A[0, j] = 0;
            }

            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (w[i] > j)
                    {
                        A[i, j] = A[i - 1, j];
                    }
                    else
                    {
                        A[i, j] = Math.Max(A[i - 1, j], A[i - j, j - w[i]] + c[i]);
                    }
                }
            }

            return A;
        }
    }
}
