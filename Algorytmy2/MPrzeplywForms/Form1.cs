using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPrzeplywForms
{
    public partial class Form1 : Form
    {
        public readonly int NodeWidth = 80;
        public readonly int NodeHeight = 30;

        public int MouseX { get; private set; }
        public int MouseY { get; private set; }
        public Graphics graphics;
        public List<Rectangle> rects;
        private Rectangle upRec = Rectangle.Empty;
        private bool up = false;

        public Form1()
        {
            rects = new List<Rectangle>();
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

        private void Form1_Click(object sender, EventArgs e)
        {
            if (!up)
            {                
                upRec = CurrentRec(MouseX, MouseY);

                if (upRec != Rectangle.Empty)
                {
                    up = true;
                }
            }  
            else
            {
                upRec.X = MouseX;
                upRec.Y = MouseY;
                graphics.DrawRectangle(Pens.Black, upRec);
                up = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rectangle rectangle = new Rectangle(5, 5, 15, 15);
            rects.Add(rectangle);
            graphics.DrawRectangle(Pens.Black, rectangle);
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
        }
    }
}
