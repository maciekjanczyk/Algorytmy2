using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaszCLI
{
    using HaszNaListach;
    using KonsolaDlaHasz;

    class Program
    {
        public delegate T RandFunc<T>();

        static List<T> FillDataset<T>(int n, RandFunc<T> f)
        {
            List<T> ret = new List<T>();

            for (int i = 0; i < n; i++)
            {
                ret.Add(f());
            }

            return ret;
        }

        static float BezOgonka(float a, float b)
        {
            return a - (a % b);
        }

        static void Main(string[] args)
        {
            Random rand = new Random();
            List<float> dataset = FillDataset(5, () => { return BezOgonka(Convert.ToSingle(rand.NextDouble()), (float)0.001); });
            Hasz<float> hasz = new Hasz<float>(dataset, FunkcjeHaszujace.DlaLiczbOd0Do1);

            hasz.Wloz(dataset);

            KonsolaHash.TrybInteraktywny(hasz);            
        }
    }
}
