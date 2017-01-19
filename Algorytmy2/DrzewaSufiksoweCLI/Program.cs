using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaSufiksoweCLI
{
    using DrzewaSufiksowe;

    class Program
    {
        static void Main(string[] args)
        {
            /*Console.Write("Wpisz napis do drzewa sufiksowego: ");
            string str = Console.ReadLine();
            DrzewoSufiksowe drzewo = new DrzewoSufiksowe(str);

            while (true)
            {
                Console.Write("Sprawdz podciag: ");
                string str2 = Console.ReadLine();
                string odp = drzewo.SprawdzNapis(str) ? "" : " nie ";
                Console.WriteLine("Napis {0}{1} jest podciagiem napisu {2}.", str2, odp, str);
            }*/
            DrzewaSufiksowe.GlobalMembers.Main(new string[] { });
        }
    }
}
