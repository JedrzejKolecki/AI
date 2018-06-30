using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    public interface IFringe<Element>
    {
        void Add(Element element); //dodaj element 
        bool IsEmpty { get; }
        Element Pop();
    }
}
