using GeometriaObliczeniowa;
using OtoczkaWypukla;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtoczkaWypuklaForms
{
    public partial class Form1 : Form
    {
        public Graphics graphics;
        public int MouseX = 0;
        public int MouseY = 0;
        public int PointSize = 8;
        public List<Punkt> punkty = new List<Punkt>();

        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();            
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            int x = MouseX - PointSize / 2;
            int y = MouseY - PointSize / 2;

            graphics.FillEllipse(Brushes.Red, new Rectangle(x, y, PointSize, PointSize));
            punkty.Add(new Punkt(x, y));
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
        }

        private void Wyczysc()
        {
            graphics.Clear(Color.White);
            punkty = new List<Punkt>();
        }

        private void wyczyscButton_Click(object sender, EventArgs e)
        {
            Wyczysc();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Wyczysc();
        }

        private void otoczkaButton_Click(object sender, EventArgs e)
        {
            List<Punkt> H = OtoczkaWypukla.OtoczkaWypukla.ConvexHull(punkty);

            for (int i = 1; i < H.Count; i++)
            {
                graphics.DrawLine(Pens.Black,
                    Convert.ToSingle(H[i - 1].X), Convert.ToSingle(H[i - 1].Y),
                    Convert.ToSingle(H[i].X), Convert.ToSingle(H[i].Y));
            }
        }
    }
}
