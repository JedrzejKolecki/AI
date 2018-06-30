using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    public class Node<State>
    {
        public State StateOfNode;
        public Node<State> parent;
        public Func<State, State, bool> Comparer;
        public int currentDistance; //w TreeSearchMethodMap

        public Node(State state, Node<State> parent, Func<State, State, bool> comparer)
         {
             this.Comparer = comparer;
             this.StateOfNode = state;
             this.parent = parent;
         }
        
        public bool OnPathToRoot(State state)
        {
            Node<State> node = parent;
            if (node == null) return false;
            while (node.parent != null && !Comparer(node.StateOfNode, state))
            {
                node = node.parent;
            }
            if (!Comparer(node.StateOfNode, state))
                return false;
            return true;
        }

    }
}
