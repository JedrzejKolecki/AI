using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    class StackFringe<Element> : IFringe<Element> //stos 
    {
        Stack<Element> stack = new Stack<Element>();
        public bool IsEmpty
        {
            get
            {
                if (stack.Count == 0)
                {
                    return true;
                }
                return false;
            }

        }

        public void Add(Element element)
        {
            stack.Push(element);
        }

        public Element Pop()
        {
            return stack.Pop(); //zwroc i usun element z gory stosu
        }
    }
}
