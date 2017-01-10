using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form
    {
        List<Button> wcisniete = new List<Button>();
        Button[,] mata;
        string[,] mataWart;
        int wymiarM;
        int wymiarN;
        int buttSize = 25;
        int wymogiPunktowe = 0;
        float TS = 0;

        private void DodajPunkt()
        {
            zebraneLabel.Text = (Convert.ToInt32(zebraneLabel.Text) + 1).ToString();

            if (Convert.ToInt32(zebraneLabel.Text) == wymogiPunktowe)
            {
                BlokujWszystkie();
                MessageBox.Show("WYGRANA!", "WYGRANA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        public Form1()
        {
            InitializeComponent();
            TS = new Button().Font.Size;
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program implementujacy alfa beta obciecie na potrzeby przedmiotu Algorytmy 2.\nAutor: Maciej Janczyk", "O programie Saper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void wyjdźToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void odkryjPole(int i, int j)
        {
            mata[i, j].Text = mataWart[i, j] != "-" ? mataWart[i, j] : ".";
            DodajPunkt();

            if (mata[i, j].Text == ".")
            {
                for (int fi = -1; fi <= 1; fi++)
                {
                    for (int fj = -1; fj <= 1; fj++)
                    {
                        if (fi == 0 && fj == 0)
                        {
                            continue;
                        }

                        int I = i + fi;
                        int J = j + fj;

                        if ((I >= 0 && I < wymiarM) && (J >= 0 && J < wymiarN))
                        {
                            if (mata[I, J].Text == "" && mataWart[I, J] != "x")
                                odkryjPole(I, J);
                        }
                    }
                }
            }
        }

        private void wcisnijPole(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                prawymNaPole(sender, e);
                return;
            }

            Button btn = (Button)sender;
            int i = tableLayoutPanel1.GetRow(btn);
            int j = tableLayoutPanel1.GetColumn(btn);

            if (btn.Text != "")
            {
                if (btn.Text != ".")
                {
                    int zebrane = 0;


                    for (int fi = -1; fi <= 1; fi++)
                    {
                        for (int fj = -1; fj <= 1; fj++)
                        {
                            if (fi == 0 && fj == 0)
                            {
                                continue;
                            }

                            int I = i + fi;
                            int J = j + fj;

                            if ((I >= 0 && I < wymiarM) && (J >= 0 && J < wymiarN))
                            {
                                if (mata[I, J].Text == "" && mataWart[I, J] != "x")
                                    odkryjPole(I, J);
                            }
                        }
                    }

                    if (zebrane == Convert.ToInt32(btn.Text))
                    {
                        for (int fi = -1; fi <= 1; fi++)
                        {
                            for (int fj = -1; fj <= 1; fj++)
                            {
                                if (fi == 0 && fj == 0)
                                {
                                    continue;
                                }

                                int I = i + fi;
                                int J = j + fj;

                                if ((I >= 0 && I < wymiarM) && (J >= 0 && J < wymiarN))
                                {
                                    if (mata[I, J].Text == "")
                                    {
                                        
                                    }
                                }
                            }
                        }
                    }
                }

                return;
            }

            btn.Text = mataWart[i, j] != "-" ? mataWart[i, j] : ".";

            if (btn.Text == "x")
            {
                BlokujWszystkie();
                MessageBox.Show("PRZEGRALES!", "PRZEGRANA", MessageBoxButtons.OK, MessageBoxIcon.Stop);                
                return;
            }

            if (btn.Text == ".")
            {
                odkryjPole(i, j);
            }
            else
            {
                DodajPunkt();
            }
        }

        private void BlokujWszystkie()
        {
            for (int i = 0; i < wymiarM; i++)
            {
                for (int j = 0; j < wymiarN; j++)
                {
                    mata[i, j].Enabled = false;
                }
            }
        }

        private void prawymNaPole(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int i = tableLayoutPanel1.GetRow(btn);
            int j = tableLayoutPanel1.GetColumn(btn);

            if (btn.Text == "")
            {
                btn.Text = "?";
            }
            else if (btn.Text == "?")
            {
                btn.Text = "";
            }
            else if (btn.Text != ".")
            {
                wcisniete = new List<Button>();

                for (int fi = -1; fi <= 1; fi++)
                {
                    for (int fj = -1; fj <= 1; fj++)
                    {
                        if (fi == 0 && fj == 0)
                        {
                            continue;
                        }

                        int I = i + fi;
                        int J = j + fj;

                        if ((I >= 0 && I < wymiarM) && (J >= 0 && J < wymiarN))
                        {
                            if (mata[I, J].Text == "")
                            {
                                mata[I, J].Enabled = false;
                                wcisniete.Add(mata[I, J]);
                            }
                        }
                    }
                }
            }
        }

        private void podniesPrzyciski(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (Button btn in wcisniete)
                {
                    btn.Enabled = true;
                }
            }
        }

        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zebraneLabel.Text = "0";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            using (StreamReader sr = new StreamReader(ofd.FileName))
            {
                wymiarM = Convert.ToInt32(sr.ReadLine());
                wymiarN = Convert.ToInt32(sr.ReadLine());

                if (tableLayoutPanel1.Controls.Count > 0)
                {
                    for (int i = 0; i < wymiarM; i++)
                    {
                        for (int j = 0; j < wymiarN; j++)
                        {
                            tableLayoutPanel1.Controls.Remove(mata[i, j]);
                        }
                    }
                }

                wymogiPunktowe = wymiarM * wymiarN;

                mata = new Button[wymiarM, wymiarN];
                mataWart = new string[wymiarM, wymiarN];

                tableLayoutPanel1.RowCount = wymiarM;
                tableLayoutPanel1.ColumnCount = wymiarN;

                for (int i = 0; i < wymiarM; i++)
                {
                    string line = sr.ReadLine();

                    for (int j = 0; j < line.Length; j++)
                    {
                        mataWart[i, j] = line[j].ToString();
                        mata[i, j] = new Button();
                        mata[i, j].Font = new Font(mata[i, j].Font, FontStyle.Bold);
                        mata[i, j].MouseDown += wcisnijPole;
                        mata[i, j].MouseUp += podniesPrzyciski;
                        mata[i, j].Size = new Size(buttSize, buttSize);
                        mata[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        mata[i, j].BackColor = Color.White;
                        
                        switch(line[j])
                        {
                            case '-':
                                break;
                            case 'x':
                                mata[i, j].Text = "x";
                                wymogiPunktowe--;
                                break;
                            case '1':
                                mata[i, j].Text = "1";
                                mata[i, j].ForeColor = Color.LightBlue;
                                break;
                            case '2':
                                mata[i, j].Text = "2";
                                mata[i, j].ForeColor = Color.LightGreen;
                                break;
                            case '3':
                                mata[i, j].Text = "3";
                                mata[i, j].ForeColor = Color.Red;
                                break;
                            case '4':
                                mata[i, j].Text = "4";
                                mata[i, j].ForeColor = Color.Blue;
                                break;
                            case '5':
                                mata[i, j].Text = "5";
                                mata[i, j].ForeColor = Color.Brown;
                                break;
                            case '6':
                                mata[i, j].Text = "6";
                                mata[i, j].ForeColor = Color.Green;
                                break;
                            default:
                                mata[i, j].Text = line[j].ToString();
                                break;
                        }

                        mata[i, j].Text = "";
                    }

                    for (int j = 0; j < line.Length; j++)
                    {
                        tableLayoutPanel1.Controls.Add(mata[i, j], j, i);
                    }
                }
            }

            potrzebneLabel.Text = wymogiPunktowe.ToString();
            this.Size = new Size((buttSize + 8) * wymiarM, (buttSize + 16) * wymiarN);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
