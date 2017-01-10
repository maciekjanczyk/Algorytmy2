using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPrzeplywForms
{
    public class Wezel
    {
        public readonly int Width = 80;
        public readonly int Height = 30;

        public Rectangle Rectangle { get; private set; }
        public string Text { get; private set; }
        public int Przeplyw { get; set; }
        public int Przepustowosc { get; set; }

        public Wezel(List<Rectangle> rect, string text, int przeplyw, int przepustowosc)
        {
            foreach (Rectangle r in rect)
            {
                
            }
        }
    }
}
