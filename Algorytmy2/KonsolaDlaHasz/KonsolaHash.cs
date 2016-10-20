using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonsolaDlaHasz
{
    using HaszNaListach;

    public static class KonsolaHash
    {
        public static void TrybInteraktywny<T>(Hasz<T> hasz) where T : IConvertible
        {
            string command = "";
            bool status = true;

            while (command != "quit")
            {
                Console.WriteLine("{0}", hasz);
                Console.WriteLine("Status ostatnie komendy: {0}, Ilosc danych: {1}", status, hasz.N);
                Console.Write("cmd$ ");
                command = Console.ReadLine();
                status = WykonajKomende(hasz, command);
                Console.Clear();
            }
        }

        public static bool WykonajKomende<T>(Hasz<T> hasz, string command) where T : IConvertible
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
                                hasz.Wloz((T)Convert.ChangeType(arg, typeof(T)));
                            }
                        }

                        break;

                    case "usun":
                        {
                            args.RemoveAt(0);
                            ret = true;

                            foreach (string arg in args)
                            {
                                hasz.Usun((T)Convert.ChangeType(arg, typeof(T)));
                            }
                        }

                        break;

                    case "szukaj":
                        {
                            ret = hasz.Szukaj((T)Convert.ChangeType(args[1], typeof(T))) != -1 ? true : false;
                        }

                        break;

                    case "help":
                        {
                            Console.Clear();
                            Console.WriteLine("dodaj [el1] [el2] ...");
                            Console.WriteLine("usun [el1] [el2] ...");
                            Console.WriteLine("szukaj [el]");
                            Console.WriteLine("help");
                            Console.WriteLine("quit");
                            Console.WriteLine("\nNacisnij klawisz...");
                            Console.ReadKey();
                        }

                        break;
                }
            }
            catch (IndexOutOfRangeException) { }

            return ret;
        }
    }
}
