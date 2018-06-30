using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    public class TreeSearch
    {
        public Node<State> TreeSearchMethod<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
        {
            fringe.Add(new Node<State>(problem.InitialState, null, problem.Compare()));
            while (!fringe.IsEmpty)
            {
                Node<State> node = fringe.Pop();

                if (problem.IsGoal(node.StateOfNode))
                {
                    Console.Write("\n\nFinalne ustawienie:\n");
                    return node;
                }
                
                Console.WriteLine("\n");
                problem.PrintState(node.StateOfNode);
                Console.Write(" --- Distance: ");
                Console.Write(problem.CheckDistance(node.StateOfNode));
                
                foreach (State state in problem.Expand(node.StateOfNode))
                {
                    if (!node.OnPathToRoot(state))
                    {
                        fringe.Add(new Node<State>(state, node, problem.Compare()));
                    }
                }
            }
            return null;
        }

        public Node<State> TreeSearchMethodMap<State>(IProblem<State> problem, IFringe<Node<State>> fringe) //w programie głownym jest przegladanie drogi
        {
            fringe.Add(new Node<State>(problem.InitialState, null, problem.Compare()));
            while (!fringe.IsEmpty)
            {
                
                Node<State> node = fringe.Pop();
                if (problem.IsGoal(node.StateOfNode))
                {
                    return node;
                }

                foreach (State state in problem.Expand(node.StateOfNode))
                {
                    if (!node.OnPathToRoot(state))
                    {
                        node.currentDistance = problem.CheckDistance(node.StateOfNode);
                        fringe.Add(new Node<State>(state, node, problem.Compare()));
                    }
                }
            }

            return null;
        }
    }
}

