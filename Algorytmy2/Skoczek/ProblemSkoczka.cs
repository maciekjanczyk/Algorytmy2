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
        public int ZapelnionePola { get; private set; }        

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
            ZapelnionePola = 0;
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
            if (x < 0 || y < 0 || x > Szachownica.Length || y > Szachownica.Length)
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
        
        private bool Probuj(int i, int x, int y)
        {
            Stack<int[]> mozliweRuchy = ListaMozliwychRuchow(x, y);
            bool q = false;

            while ((!q) && mozliweRuchy.Count > 0)
            {
                int[] wspolrzedneRuchu = mozliweRuchy.Pop();

                if (CzyRuchDopuszczalny(wspolrzedneRuchu))
                {
                    Szachownica[x, y] = i;
                    ZapelnionePola += 1;

                    if (!Probuj(i + 1, wspolrzedneRuchu[0], wspolrzedneRuchu[1]))
                    {
                        Szachownica[x, y] = 0;
                        ZapelnionePola -= 1;
                        q = true;
                    }
                }
            }

            return q;
        }

        public int[,] RozwiazProblem(int[] start)
        {
            Probuj(1, start[0], start[1]);
            return Szachownica;
        }

        public int[,] RozwiazProblem(int startx, int starty)
        {
            return RozwiazProblem(new int[2] { startx, starty });
        }
    }
}
