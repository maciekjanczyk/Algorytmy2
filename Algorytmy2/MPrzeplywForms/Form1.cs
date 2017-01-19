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
using Microsoft.VisualBasic;
using MaksymalnyPrzeplyw;

namespace MPrzeplywForms
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
        public List<Tuple<int, int, Label>> polaczenia = new List<Tuple<int, int, Label>>();
        public int ilosc = 0;
        public Rectangle zaznaczony1 = Rectangle.Empty;
        public Rectangle zaznaczony2 = Rectangle.Empty;
        public int[,] przeplywy;
        public int[,] przepustowosci;

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
            }
            else
            {
                zaznaczony2 = rec;
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

        private void RysujPolaczenie()
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 255), 5);
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;

            graphics.DrawLine(pen, 
                new Point(zaznaczony1.X + zaznaczony1.Width/2, zaznaczony1.Y), 
                new Point(zaznaczony2.X, zaznaczony2.Y));

            int przeplyw = Convert.ToInt32(Interaction.InputBox("Wpisz liczbe", "Wpisz przeplyw: "));
            int przepustowosc = Convert.ToInt32(Interaction.InputBox("Wpisz liczbe", "Wpisz przepustowosc: "));
            int idx1 = Convert.ToInt32(labels[rects.IndexOf(zaznaczony1)].Text);
            int idx2 = Convert.ToInt32(labels[rects.IndexOf(zaznaczony2)].Text);

            przeplywy[idx1, idx2] = przeplyw;
            przepustowosci[idx1, idx2] = przepustowosc;

            Label text = new Label();
            text.Text = string.Format("{0}/{1}", przeplyw, przepustowosc);
            text.AutoSize = true;
            float x = Math.Abs((((float)(zaznaczony1.X + zaznaczony1.Width / 2)) + zaznaczony2.X)/(float)2.0);
            float y = Math.Abs(((float)zaznaczony1.Y + (float)zaznaczony2.Y)/(float)2.0);
            text.Location = new Point((int)x, (int)y);
            Controls.Add(text);

            polaczenia.Add(new Tuple<int, int, Label>(idx1, idx2, text));
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
            przeplywy = new int[ilosc, ilosc];
            przepustowosci = new int[ilosc, ilosc];

            button1.Enabled = true;
            button2.Enabled = false;
            rysowanie = false;
            drogi = true;
        }

        private void WypiszMacierze()
        {
            richTextBox1.Text = "Przeplywy:\n";

            for (int i = 0; i < ilosc; i++)
            {
                for (int j = 0; j < ilosc; j++)
                {
                    richTextBox1.Text += string.Format("{0,2} ", przeplywy[i, j]);
                }

                richTextBox1.Text += "\n";
            }

            richTextBox1.Text += "\nPrzepustowosci:\n";

            for (int i = 0; i < ilosc; i++)
            {
                for (int j = 0; j < ilosc; j++)
                {
                    richTextBox1.Text += string.Format("{0,2} ", przepustowosci[i, j]);
                }

                richTextBox1.Text += "\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[,] GRes = ProblemMaksymalnegoPrzeplywu.GrafResidualny(przeplywy, przepustowosci);
            ProblemMaksymalnegoPrzeplywu.FordFulkerson(przeplywy, przepustowosci, 0, ilosc - 1);

            foreach (var tupla in polaczenia)
            {
                int i = tupla.Item1, j = tupla.Item2;
                tupla.Item3.Text = string.Format("{0}/{1}", przeplywy[i, j], przepustowosci[i, j]);
            }

            WypiszMacierze();
        }
    }
}
