using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    class A_z_gwiazdka<Element> : IFringe<Element>
    {
        public List<Tuple<double, Element>> SortedList = new List<Tuple<double, Element>>();
        Func<Element, double> Distance;

        public A_z_gwiazdka (Func<Element, double> Distance)
        {
            this.Distance = Distance;
        }

        public bool IsEmpty
        {
            get
            {
                if (SortedList.Count == 0)
                {
                    return true;
                }
                return false;
            }

        }

        public void Add(Element element)
        {
            int i;
            for (i = 0; i < SortedList.Count; i++)
            {
                if (SortedList[i] == null || SortedList[i].Item1 > Distance(element))
                    break;
            }
            SortedList.Insert(i, new Tuple<double, Element>(Distance(element), element)); //dodaj do kolejki (dystans tego elementu,element)
        }

        public Element Pop()
        {
            Element element = SortedList[0].Item2; //wybierz pierwszy element
            SortedList.RemoveAt(0); //usun pierwszy element z kolejki 
            return element;
        }
    }
}
