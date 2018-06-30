using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    class QueueFringe_Priorytet<Element> : IFringe<Element>
    {
        public List<Tuple<int, Element>> SortedList = new List<Tuple<int, Element>>();
        Func<Element, int> Distance;

        public QueueFringe_Priorytet(Func<Element, int> Distance)
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
            SortedList.Insert(i, new Tuple<int, Element>(Distance(element), element)); //dodaj do kolejki (dystans tego elementu,element)
        }

        public Element Pop()
        {
            Element element = SortedList[0].Item2;
            SortedList.RemoveAt(0);
            return element;
        }

    }
    
}
