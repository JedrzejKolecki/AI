using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
        class QueueFringe<Element> : IFringe<Element>
        {
            private Queue<Element> queue = new Queue<Element>();

            public bool IsEmpty
            {
                get
                {
                    if (queue.Count == 0)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public void Add(Element element)
            {
                queue.Enqueue(element);
            }

            public Element Pop()
            {
                return queue.Dequeue(); //zwroc pierwszy element i usun go z kolejki 
            }
        }
    
}
