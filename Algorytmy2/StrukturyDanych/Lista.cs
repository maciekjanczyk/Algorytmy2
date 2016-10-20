using StrukturyDanych;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyDanych
{
    public class ElementListy<T>
    {
        public ElementListy<T> Nastepny { get; set; }
        public T Wartosc { get; set; }

        public ElementListy()
        {
            Wartosc = default(T);
        }
    }

    public class Lista<T> : IEnumerable
    {                
        public ElementListy<T> Korzen { get; set; }
        public int Count { get; set; }

        public T this[int i]
        {
            get
            {
                if (Korzen != null)
                {
                    int j = 0;
                    ElementListy<T> wsk = Korzen;

                    while (i != j)
                    {
                        wsk = wsk.Nastepny;

                        if (wsk == null)
                        {
                            return default(T);
                        }

                        j++;
                    }

                    return wsk.Wartosc;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public Lista()
        {
            Korzen = null;
        }

        public void Add(T el)
        {
            if (Korzen == null)
            {
                Korzen = new ElementListy<T>();
                Korzen.Wartosc = el;

                return;
            }

            ElementListy<T> wsk = Korzen;

            while (wsk.Nastepny != null)
            {
                wsk = wsk.Nastepny;
            }

            wsk.Nastepny = new ElementListy<T>();
            wsk.Nastepny.Wartosc = el;

            Count++;
        }

        public void RemoveAt(int i)
        {
            if (Korzen != null)
            {
                if (i == 0)
                {
                    Korzen = Korzen.Nastepny;
                }
                else
                {
                    int j = 0;
                    ElementListy<T> wsk = Korzen;
                    ElementListy<T> poprz = null;

                    while (i != j)
                    {
                        poprz = wsk;
                        wsk = wsk.Nastepny;

                        if (wsk == null)
                        {
                            return;
                        }

                        j++;
                    }

                    poprz.Nastepny = wsk.Nastepny;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new ListaNumerator<T>(this);
        }
    }

    public class ListaNumerator<T> : IEnumerator
    {
        Lista<T> lista;
        ElementListy<T> wsk;
        bool poPierwszejIteracji;

        public ListaNumerator(Lista<T> lista)
        {
            this.lista = lista;
            wsk = lista.Korzen;
            poPierwszejIteracji = false;
        }
               
        public object Current
        {
            get
            {
                return wsk.Wartosc;
            }
        }

        public bool MoveNext()
        {
            if (wsk != null)
            {
                if (!poPierwszejIteracji)
                {
                    poPierwszejIteracji = true;
                    return true;
                }
                else
                {
                    wsk = wsk.Nastepny;

                    if (wsk == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Reset()
        {
            wsk = lista.Korzen;
        }
    }
}
