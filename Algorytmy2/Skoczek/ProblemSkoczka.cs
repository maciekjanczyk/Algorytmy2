using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSkoczka
{
    public class Skoczek
    {
        public int[,] Szachownica { get; private set; }

        public Skoczek(int[,] szach)
        {
            Init(szach);
        }

        public Skoczek(int n)
        {
            int[,] szach = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    szach[i, j] = 0;
                }
            }

            Init(szach);
        }

        public void Init(int[,] szach)
        {
            Szachownica = szach;
        }

        private Stack<int[]> ListaMozliwychRuchow(int x, int y)
        {
            Stack<int[]> ret = new Stack<int[]>();

            ret.Push(new int[2] { x - 1, y + 2 });
            ret.Push(new int[2] { x + 1, y - 2 });
            ret.Push(new int[2] { x + 1, y + 2 });
            ret.Push(new int[2] { x - 1, y - 2 });
            ret.Push(new int[2] { x + 2, y - 1 });
            ret.Push(new int[2] { x - 2, y + 1 });
            ret.Push(new int[2] { x + 2, y + 1 });
            ret.Push(new int[2] { x - 2, y - 1 });

            return ret;
        }

        private bool CzyRuchDopuszczalny(int x, int y)
        {
            if (x < 0 || y < 0 || x > Szachownica.GetLength(0) - 1 || y > Szachownica.GetLength(0) - 1)
            {
                return false;
            }
            else if (Szachownica[x, y] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CzyRuchDopuszczalny(int[] pkt)
        {
            return CzyRuchDopuszczalny(pkt[0], pkt[1]);
        }
        
        private bool Probuj(int i, int x, int y, ref bool q)
        {
            Stack<int[]> mozliweRuchy = ListaMozliwychRuchow(x, y);

            do
            {
                int[] wspolrzedneRuchu = mozliweRuchy.Pop();

                if (CzyRuchDopuszczalny(wspolrzedneRuchu))
                {
                    Szachownica[wspolrzedneRuchu[0], wspolrzedneRuchu[1]] = i;

                    if (i < Szachownica.Length)
                    {
                        Probuj(i + 1, wspolrzedneRuchu[0], wspolrzedneRuchu[1], ref q);

                        if (!q)
                        {
                            Szachownica[wspolrzedneRuchu[0], wspolrzedneRuchu[1]] = 0;
                        }
                    }
                    else
                    {
                        q = true;
                    }
                }
            }
            while ((!q) && mozliweRuchy.Count > 0);

            return q;
        }

        private bool ProbujN(int i, int x, int y, List<int[,]> bufor, int n_max, ref bool q)
        {
            Stack<int[]> mozliweRuchy = ListaMozliwychRuchow(x, y);

            do
            {
                int[] wspolrzedneRuchu = mozliweRuchy.Pop();

                if (CzyRuchDopuszczalny(wspolrzedneRuchu))
                {
                    Szachownica[wspolrzedneRuchu[0], wspolrzedneRuchu[1]] = i;

                    if (i < Szachownica.Length)
                    {
                        ProbujN(i + 1, wspolrzedneRuchu[0], wspolrzedneRuchu[1], bufor, n_max, ref q);                        
                    }
                    else
                    {
                        q = true;
                        bufor.Add((int[,])Szachownica.Clone());               
                        
                        if (bufor.Count > n_max && n_max != -1)
                        {
                            return q;
                        }
                    }
                    
                    Szachownica[wspolrzedneRuchu[0], wspolrzedneRuchu[1]] = 0;
                }
            }
            while (mozliweRuchy.Count > 0);

            return q;
        }

        public int[,] RozwiazProblem(int[] start)
        {
            if (Szachownica[start[0], start[1]] == 0)
            {
                Szachownica[start[0], start[1]] = 1;
            }

            bool q = false;
            
            Probuj(2, start[0], start[1], ref q);            

            return Szachownica;
        }

        public int[,] RozwiazProblem(int startx, int starty)
        {
            return RozwiazProblem(new int[2] { startx, starty });
        }

        public List<int[,]> RozwiazProblem(int[] start, int n_max = -1)
        {
            if (Szachownica[start[0], start[1]] == 0)
            {
                Szachownica[start[0], start[1]] = 1;
            }

            bool q = false;
            List<int[,]> ret = new List<int[,]>();

            ProbujN(2, start[0], start[1], ret, -1, ref q);

            return ret;
        }

        public List<int[,]> RozwiazProblem(int startx, int starty, int n_max = -1)
        {
            return RozwiazProblem(new int[2] { startx, starty }, n_max);
        }
    }
}
