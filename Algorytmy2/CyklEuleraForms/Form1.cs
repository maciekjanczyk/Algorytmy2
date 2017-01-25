using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyklEuleraForms
{
    public partial class Form1 : Form
    {
        public bool rysowanie = true;
        public bool drogi = false;
        public int MouseX { get; private set; }
        public int MouseY { get; private set; }
        public Graphics graphics;
        public List<Rectangle> rects;
        public List<Label> labels;
        public List<Tuple<int, int>> polaczenia = new List<Tuple<int, int>>();
        public int ilosc = 0;
        public Rectangle zaznaczony1 = Rectangle.Empty;
        public Rectangle zaznaczony2 = Rectangle.Empty;
        public int[,] Graf;


        public Form1()
        {
            rects = new List<Rectangle>();
            labels = new List<Label>();
            InitializeComponent();
            graphics = CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Rectangle CurrentRec(int x, int y)
        {
            foreach (var rect in rects)
            {
                if ((x >= rect.X && x <= (rect.X + rect.Width)) && (y >= rect.Y && y <= (rect.Y + rect.Height)))
                {
                    return rect;
                }
            }

            return Rectangle.Empty;
        }

        private void cyfraClick(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            Rectangle rec = rects[labels.IndexOf(lab)];

            if (zaznaczony1 == Rectangle.Empty)
            {
                zaznaczony1 = rec;
                richTextBox1.Text += "\nWcisnieto " + labels.IndexOf(lab).ToString() + "\n";
                PrzewinKonsole();
            }
            else
            {
                zaznaczony2 = rec;
                richTextBox1.Text += "\nWcisnieto " + labels.IndexOf(lab).ToString() + "\n";
                PrzewinKonsole();
                RysujPolaczenie();
            }
        }

        private void RysujWezel()
        {
            Rectangle rectangle = new Rectangle(MouseX, MouseY, 30, 30);
            var label = new Label();
            label.Click += cyfraClick;
            label.Text = ilosc.ToString();
            label.Font = new Font(label.Font.FontFamily, (float)13.0);
            ilosc++;
            label.Location = new Point(MouseX + 3, MouseY + 3);
            label.Size = new Size(20, 20);
            Controls.Add(label);
            labels.Add(label);
            rects.Add(rectangle);
            graphics.DrawRectangle(Pens.Black, rectangle);
        }

        private void RysujPolaczenie(bool czerwony = false)
        {
            Color kol;

            if (!czerwony)
            {
                kol = Color.FromArgb(255, 0, 0, 255);
            }
            else
            {
                kol = Color.FromArgb(255, 255, 0, 0);
            }

            Pen pen = new Pen(kol, 5);
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;

            graphics.DrawLine(pen,
                new Point(zaznaczony1.X + zaznaczony1.Width / 2, zaznaczony1.Y),
                new Point(zaznaczony2.X, zaznaczony2.Y));

            int idx1 = Convert.ToInt32(labels[rects.IndexOf(zaznaczony1)].Text);
            int idx2 = Convert.ToInt32(labels[rects.IndexOf(zaznaczony2)].Text);

            polaczenia.Add(new Tuple<int, int>(idx1, idx2));
            Graf[idx1, idx2] = 1;

            WypiszMacierze();

            zaznaczony1 = Rectangle.Empty;
            zaznaczony2 = Rectangle.Empty;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (rysowanie)
            {
                RysujWezel();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            rysowanie = true;
            drogi = false;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
            label1.Text = string.Format("({0},{1})", MouseX, MouseY);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graf = new int[ilosc, ilosc];

            button1.Enabled = true;
            button2.Enabled = false;
            rysowanie = false;
            drogi = true;
        }

        private void WypiszMacierze()
        {            
            richTextBox1.Text += "\nGraf:\n";

            for (int i = 0; i < ilosc; i++)
            {
                for (int j = 0; j < ilosc; j++)
                {
                    richTextBox1.Text += string.Format("{0,2} ", Graf[i, j]);
                }

                richTextBox1.Text += "\n";
            }

            PrzewinKonsole();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int v = Convert.ToInt32(Interaction.InputBox("Wpisz numer wierzcholka", "Wpisz numer wierzcholka: "));

            List<int> cykl = CyklEulera.CyklEulera.Stworz(Graf, v);

            richTextBox1.Text += "\nCykl Eulera:\n";
            cykl.Reverse();

            foreach (var u in cykl)
            {
                richTextBox1.Text += u.ToString() + " ";
            }
            richTextBox1.Text += "\n";

            PrzewinKonsole();
        }

        private void PrzewinKonsole()
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
