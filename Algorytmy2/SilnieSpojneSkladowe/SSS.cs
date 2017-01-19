using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilnieSpojneSkladowe
{
    public enum Kolor
    {
        Bialy,
        Czerwony,
        Niebieski
    }

    public class SSS
    {       
        public int[,] G { get; private set; }
        public Kolor[] Kolor { get; private set; }
        public int[] P { get; private set; }
        public int[] D { get; private set; }
        public int[] F { get; private set; }
        public int[] FOld { get; private set; }
        public int Time { get; private set; }
        public int Size { get; private set; }
        public List<int>[] Rozwiazanie { get; private set; }

        public SSS(int[,] G)
        {
            this.G = G;
            Size = G.GetLength(1);
            MainProcedure();
        }

        private void InitTabs()
        {
            Kolor = new Kolor[G.GetLength(1)];
            P = new int[G.GetLength(1)];
            D = new int[G.GetLength(1)];
            F = new int[G.GetLength(1)];
        }

        private void DFS()
        {
            InitTabs();

            for (int u = 0; u < Size; u++)
            {
                Kolor[u] = SilnieSpojneSkladowe.Kolor.Bialy;
                P[u] = -1;
            }

            Time = 0;

            for (int u = 0; u < Size; u++)
            {
                if (Kolor[u] == SilnieSpojneSkladowe.Kolor.Bialy)
                {
                    DFSV(u);
                }
            }
        }

        private void DFS2()
        {
            FOld = (int[])F.Clone();

            List<int> tmpList = FOld.ToList();
            Queue<int> kolejkaWezlow = new Queue<int>();

            for (int i = 0; i < tmpList.Count; i++)
            {
                int max = tmpList.Max();
                int idx = tmpList.IndexOf(max);
                kolejkaWezlow.Enqueue(idx);
                tmpList[idx] = Int32.MinValue;
            }

            InitTabs();

            for (int u = 0; u < Size; u++)
            {
                Kolor[u] = SilnieSpojneSkladowe.Kolor.Bialy;
                P[u] = -1;
            }

            Time = 0;

            while (kolejkaWezlow.Count > 0)
            {
                int u = kolejkaWezlow.Dequeue();

                if (Kolor[u] == SilnieSpojneSkladowe.Kolor.Bialy)
                {
                    DFSV(u);
                }
            }
        }

        private void DFSV(int u)
        {
            Kolor[u] = SilnieSpojneSkladowe.Kolor.Czerwony;
            Time++;
            D[u] = Time;

            for (int v = 0; v < Size; v++)
            {                
                if ((G[u, v] != 0) && (Kolor[v] == SilnieSpojneSkladowe.Kolor.Bialy))
                {
                    P[v] = u;
                    DFSV(v);
                }
            }

            Kolor[u] = SilnieSpojneSkladowe.Kolor.Niebieski;
            Time++;
            F[u] = Time;
        }

        private int[,] GrafTransponowany()
        {
            int[,] Gt = new int[G.GetLength(1), G.GetLength(1)];

            for (int i = 0; i < Size; i++)
            { 
                for (int j = 0; j < Size; j++)                
                {
                    Gt[i, j] = G[j, i];
                }
            }

            return Gt;
        }

        private void MainProcedure2()
        {
            DFS();

        }

        private void MainProcedure()
        {            
            DFS();
            G = GrafTransponowany();
            DFS2();

            List<int>[] drzewaPrzeszukiwan = new List<int>[Size];
            bool[] czyDrzewoNiezerowe = new bool[Size];
            int ileDNiezer = 0;

            for (int i = 0; i < Size; i++)
            {
                drzewaPrzeszukiwan[i] = new List<int>();
                czyDrzewoNiezerowe[i] = false;
            }

            for (int i = 0; i < Size; i++)
            {
                int j = i;

                while (P[j] != -1)
                {
                    if (P[j] != j)
                    {
                        j = P[j];
                    }
                    else
                    {
                        break;
                    }
                }

                drzewaPrzeszukiwan[j].Add(i);

                if (!czyDrzewoNiezerowe[j])
                {
                    czyDrzewoNiezerowe[j] = true;
                    ileDNiezer++;
                }
            }

            Rozwiazanie = new List<int>[ileDNiezer];
            int rozw = 0;

            for (int i = 0; i < Size; i++)
            {
                if (czyDrzewoNiezerowe[i])
                {
                    Rozwiazanie[rozw] = drzewaPrzeszukiwan[i];
                    rozw++;
                }
            }
            
            int[] minTimeInSSS = new int[ileDNiezer];

            for (int i = 0; i < ileDNiezer; i++)
            {
                minTimeInSSS[i] = int.MaxValue;
                foreach (int j in Rozwiazanie[i])
                {
                    minTimeInSSS[i] = Math.Min(minTimeInSSS[i], D[j]);
                }
            }

            Array.Sort(minTimeInSSS, Rozwiazanie);
        }
    }
}
