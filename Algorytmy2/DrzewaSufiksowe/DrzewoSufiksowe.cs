using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaSufiksowe
{
    public class DrzewoSufiksowe
    {
        public string Tekst { get; private set; }
        public WezelDrzewa Root { get; private set; }
        public WezelDrzewa OstatniNowyWezel { get; private set; }
        public WezelDrzewa AktywnyWezel { get; private set; }

        public int AktywnaKrawedz { get; private set; }
        public int AktywnaDlugosc { get; private set; }

        public int PozostalaLiczbaSufiksow { get; private set; }
        public int KoniecLiscia { get; private set; }
        public int KoniecRoot { get; private set; }
        public int SplitKoniec { get; private set; }
        public int Rozmiar { get; private set; }

        public DrzewoSufiksowe(string tekst)
        {
            Root = null;
            OstatniNowyWezel = null;
            AktywnyWezel = null;
            AktywnaKrawedz = -1;
            AktywnaDlugosc = 0;
            PozostalaLiczbaSufiksow = 0;
            KoniecLiscia = -1;
            KoniecRoot = -1;
            SplitKoniec = -1;
            Rozmiar = -1;
            Tekst = tekst;

            BudujDrzewoSufiksowe();
        }

        private WezelDrzewa NowyWezel(int start, int koniec)
        {
            WezelDrzewa ret = new WezelDrzewa();

            ret.Link = Root;
            ret.Start = start;
            ret.Koniec = koniec;
            
            ret.Indeks = -1;

            return ret;
        }

        private int DlugoscKrawedzi(WezelDrzewa wezel)
        {
            if (wezel == Root)
                return 0;

            return wezel.Koniec - wezel.Start + 1;
        }

        private int ZejdzNizej(WezelDrzewa wezel)
        {
            if (AktywnaDlugosc >= DlugoscKrawedzi(wezel))
            {
                AktywnaKrawedz += DlugoscKrawedzi(wezel);
                AktywnaDlugosc -= DlugoscKrawedzi(wezel);
                AktywnyWezel = wezel;

                return 1;
            }

            return 0;
        }

        private void RozszerzDrzewo(int pos)
        {
            KoniecLiscia = pos;
            PozostalaLiczbaSufiksow++;
            OstatniNowyWezel = null;

            while (PozostalaLiczbaSufiksow > 0)
            {
                if (AktywnaDlugosc == 0)
                {
                    AktywnaKrawedz = pos;
                }

                if (AktywnyWezel.Dzieci[Tekst[AktywnaKrawedz]] == null)
                {
                    AktywnyWezel.Dzieci[Tekst[AktywnaKrawedz]] = NowyWezel(pos, KoniecLiscia);

                    if (OstatniNowyWezel != null)
                    {
                        OstatniNowyWezel.Link = AktywnyWezel;
                        OstatniNowyWezel = null;
                    }
                }
                else
                {
                    WezelDrzewa nast = AktywnyWezel.Dzieci[Tekst[AktywnaKrawedz]];

                    if (ZejdzNizej(nast) != 0)
                    {
                        continue;
                    }

                    if (Tekst[nast.Start + AktywnaDlugosc] == Tekst[pos])
                    {
                        if (OstatniNowyWezel != null && AktywnyWezel != Root)
                        {
                            OstatniNowyWezel.Link = AktywnyWezel;
                            OstatniNowyWezel = null;
                        }

                        AktywnaDlugosc++;
                        break;
                    }

                    SplitKoniec = nast.Start + AktywnaDlugosc - 1;

                    WezelDrzewa split = NowyWezel(nast.Start, SplitKoniec);
                    AktywnyWezel.Dzieci[Tekst[AktywnaKrawedz]] = split;

                    split.Dzieci[Tekst[pos]] = NowyWezel(pos, KoniecLiscia);
                    nast.Start += AktywnaDlugosc;
                    split.Dzieci[Tekst[nast.Start]] = nast;

                    if (OstatniNowyWezel != null)
                    {
                        OstatniNowyWezel.Link = split;
                    }

                    OstatniNowyWezel = split;
                }

                PozostalaLiczbaSufiksow--;

                if (AktywnyWezel == Root && AktywnaDlugosc > 0)
                {
                    AktywnaDlugosc--;
                    AktywnaKrawedz = pos - PozostalaLiczbaSufiksow + 1;
                }
                else if (AktywnyWezel != Root)
                {
                    AktywnyWezel = AktywnyWezel.Link;
                }
            }
        }

        private void UstawIndeksDFS(WezelDrzewa wezel, int dl_etykiety)
        {
            if (wezel == null)
            {
                return;
            }

            int lisc = 1;

            for (int i = 0; i < 256; i++)
            {
                if (wezel.Dzieci[i] != null)
                {                
                    lisc = 0;
                    UstawIndeksDFS(wezel.Dzieci[i], dl_etykiety + DlugoscKrawedzi(wezel.Dzieci[i]));
                }
            }

            if (lisc == 1)
            {
                wezel.Indeks = Rozmiar - dl_etykiety;
            }
        }

        private void BudujDrzewoSufiksowe()
        {
            Rozmiar = Tekst.Length;
            KoniecRoot = -1;
            Root = NowyWezel(-1, KoniecRoot);
            AktywnyWezel = Root; 

            for (int i = 0; i < Rozmiar; i++)
            {
                RozszerzDrzewo(i);
            }

            int dl_etykiety = 0;
            UstawIndeksDFS(Root, dl_etykiety);
        }

        private int OdwiedzKrawedz(string str, int idx, int start, int koniec)
        {            
            for (int i = start; i <= koniec && idx != str.Length; i++, idx++)
            {
                if (Tekst[i] != str[idx])
                {
                    return -1;
                }
            }

            if (idx == str.Length)
            {
                return 1;
            }

            return 0; 
        }

        private int Odwiedz(WezelDrzewa wezel, string str, int idx)
        {
            if (wezel == null)
            {
                return -1;
            }

            int ret = -1;
            
            if (wezel.Start != -1)
            {
                ret = OdwiedzKrawedz(str, idx, wezel.Start, wezel.Koniec);

                if (ret != 0)
                {
                    return ret;
                }
            }
            
            idx = idx + DlugoscKrawedzi(wezel);

            if (wezel.Dzieci[str[idx]] != null)
            {
                return Odwiedz(wezel.Dzieci[str[idx]], str, idx);
            }
            else
            {
                return -1;
            }
        }

        public bool SprawdzNapis(string str)
        {          
            return Odwiedz(Root, str, 0) == 1;                
        }
    }
}
