using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaszCLI
{
    using HaszNaListach;

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
            List<float> dataset = FillDataset(20, () => { return BezOgonka(Convert.ToSingle(rand.NextDouble()), (float)0.001); });
            Hasz<float> hasz = new Hasz<float>(40, FunkcjeHaszujace.DlaLiczbOd0Do1, 2);

            hasz.Wloz(dataset);                        

            string command = "";
            bool status = true;

            while (command != "quit")
            {
                Console.WriteLine("{0}", hasz);
                Console.WriteLine("Status ostatnie komendy: {0}", status);
                Console.Write("cmd$ ");
                command = Console.ReadLine();
                status = hasz.ExecuteCMD(command);
                Console.Clear();
            }
        }
    }
}
