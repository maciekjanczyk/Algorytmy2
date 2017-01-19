using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaSufiksowe
{
    public class WezelDrzewa
    {
        public WezelDrzewa[] Dzieci { get; set; }
        public WezelDrzewa Link { get; set; }
        public int Start { get; set; }
        public int Koniec { get; set; }
        public int Indeks { get; set; }

        public WezelDrzewa()
        {
            Dzieci = new WezelDrzewa[256];
        }
    }
}
