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

        static List<T> WypelnijDanymi<T>(int n, RandFunc<T> f)
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

        static Type StartTyp()
        {
            bool dziala = true;

            while (dziala)
            {
                Console.Clear();
                Console.WriteLine("Wybierz typ obiektow:");
                Console.WriteLine("1. Int32");
                Console.WriteLine("2. Float");
                Console.WriteLine("3. String");
                Console.WriteLine("4. Koniec programu");
                Console.Write("\nWybor: ");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        return typeof(int);
                    case '2':
                        return typeof(float);
                    case '3':
                        return typeof(string);
                    case '4':
                        dziala = false;
                        break;
                }
            }

            return null;
        }

        static int StartN()
        {
            int ret = -1;

            while (ret < 0)
            {
                Console.Clear();
                Console.Write("Ilosc danych: ");
                try
                {
                    ret = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException) { }                    
            }

            return ret;
        }

        static object GlownaProcedura(Type typ, int n)
        {
            Random rand = new Random();
            object ret = null;

            if (typ == typeof(float))
            {
                List<float> dataset = WypelnijDanymi(n, () => { return BezOgonka(Convert.ToSingle(rand.NextDouble()), (float)0.001); });
                Hasz<float> hasz = new Hasz<float>(dataset, FunkcjeHaszujace.DlaLiczbOd0Do1);
                hasz.Wloz(dataset);
                ret = hasz;
            }                    
            else if (typ == typeof(int))
            {
                List<int> dataset = WypelnijDanymi(n, () => { return rand.Next(0, 2 * n); });
                Hasz<int> hasz = new Hasz<int>(dataset, FunkcjeHaszujace.DlaLiczbCalkowitychOd0DoN);
                hasz.Wloz(dataset);
                ret = hasz;
            }
            else if (typ == typeof(string))
            {
                List<string> dataset = WypelnijDanymi(n, () => { return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 4); });
                Hasz<string> hasz = new Hasz<string>(dataset, FunkcjeHaszujace.DlaNapisowConajmniej3);
                hasz.Wloz(dataset);
                ret = hasz;
            }

            return ret;
        }

        static void Main(string[] args)
        {
            Type typ = StartTyp();
            int n = StartN();
            object hasz = GlownaProcedura(typ, n);
            
            if (typ == typeof(float))
            {
                ((Hasz<float>)hasz).TrybInteraktywny();
            }
            else if (typ == typeof(int))
            {
                ((Hasz<int>)hasz).TrybInteraktywny();
            }
            else if (typ == typeof(string))
            {
                ((Hasz<string>)hasz).TrybInteraktywny();
            }
        }
    }
}
