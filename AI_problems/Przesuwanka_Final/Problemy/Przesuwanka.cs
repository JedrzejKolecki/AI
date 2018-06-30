using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{

    class Przesuwanka : IProblem<byte[,]>
    {
        private byte[,] initial;
        private byte[,] goal;

        public Przesuwanka(byte[,] initial, byte[,] goal) //poczatkowe ulozenie przesuwanki i docelowe 
        {
            this.initial = initial;
            this.goal = goal;
        }

        public byte[,] InitialState
        {
            get
            {
                return initial;
            }
        }

        public bool IsGoal(byte[,] state)
        {
            for (int i = 0; i < goal.GetLength(0); i++)
            {
                for (int j = 0; j < goal.GetLength(1); j++)
                {
                    if (goal[i, j] != state[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void PrintState(byte[,] state)
        {
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    Console.Write(state[i, j] + "\t");
                }
                Console.Write("\n");
            }
        }
        //----------------


        public IList<byte[,]> Expand(byte[,] state)
        {
            int size = state.GetLength(0);
            int distance = CheckDistance(state);
            byte[] findZero = find(0, state); //znajdz lokalizacje pustego miejsca (0)
            byte[,] newState = new byte[size, size]; //kwadrat
            List<byte[,]> ListOfNewStates = new List<byte[,]>(4); //max. 4 mozliwosci 
            byte x;

            if (findZero[1] > 0)
            {
                newState = (byte[,])state.Clone();
                x = state[findZero[0], findZero[1] - 1]; //wartosc ktora zamieniamy z zerem

                newState[findZero[0], findZero[1] - 1] = 0; // newstate[pierwsza koordynate zera, druga koordynata]
                newState[findZero[0], findZero[1]] = x;
                ListOfNewStates.Add(newState);
            }
            if (findZero[0] > 0)
            {
                newState = (byte[,])state.Clone();
                x = state[findZero[0] - 1, findZero[1]]; //wartosc ktora zamieniamy z zerem

                newState[findZero[0] - 1, findZero[1]] = 0; 
                newState[findZero[0], findZero[1]] = x;
                ListOfNewStates.Add(newState);
            }
            if (findZero[0] < size - 1)
            {
                newState = (byte[,])state.Clone();
                x = state[findZero[0] + 1, findZero[1]]; //wartosc ktora zamieniamy z zerem

                newState[findZero[0] + 1, findZero[1]] = 0; 
                newState[findZero[0], findZero[1]] = x;
                ListOfNewStates.Add(newState);
            }
            if (findZero[1] < size - 1)
            {
                newState = (byte[,])state.Clone();
                x = state[findZero[0], findZero[1] + 1]; //wartosc ktora zamieniamy z zerem

                newState[findZero[0], findZero[1] + 1] = 0; 
                newState[findZero[0], findZero[1]] = x;
                ListOfNewStates.Add(newState);
            }
            return ListOfNewStates;
        }

        
        private byte[] find(byte element, byte[,] state) //szukanie elementu w tablicy 
        {
            for (byte i = 0; i < state.GetLength(0); i++)
            {
                for (byte j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == element)
                    {
                        return new byte[2] { i, j };
                    }
                }
            }
            return null; 
        }

        public int CheckDistance(byte[,] state) //licz konflikty - ile liczb nie jest na swoim miejscu 
        {
            int conflict=0; 
            for (byte i = 0; i < goal.GetLength(0); i++)
            {
                for (byte j = 0; j < goal.GetLength(1); j++)
                {
                    if(state[i,j]!=goal[i,j]) conflict++;
                }
            }
            return conflict;
        }

        public Func<byte[,], byte[,], bool> Compare()
        {
            return IsTheSame;
        }

        private bool IsTheSame(byte[,] CurrentState, byte[,] NewState)
        {
            for (int i = 0; i < CurrentState.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentState.GetLength(1); j++)
                {
                    if (CurrentState[i, j] != NewState[i, j])
                        return false;
                }
            }
            return true;
        }

        public int ExternalDistance(Node<byte[,]> element)
        {
            int conflicts = 0;
            byte[,] state = element.StateOfNode;

            for (int i = 0; i < goal.GetLength(0); i++)
            {
                for (int j = 0; j < goal.GetLength(1); j++)
                {
                    if (goal[i, j] != state[i, j])
                    {
                        conflicts++;
                    }
                }
            }
            return conflicts;
        }

        public Func<Node<byte[,]>, int> Distance() //delegat
        {
            return ExternalDistance;
        }

        public int ExternalDistance_A(Node<byte[,]> element) //dla A* powinny być jeszcze + liczba kroków
        {
            int conflicts = 0;
            byte[,] state = element.StateOfNode;

            for (int i = 0; i < goal.GetLength(0); i++)
            {
                for (int j = 0; j < goal.GetLength(1); j++)
                {
                    if (goal[i, j] != state[i, j])
                    {
                        conflicts++;
                    }
                }
            }
            return conflicts;
        }

        public Func<Node<byte[,]>, int> Distance_A() //delegat
        {
            return ExternalDistance_A;
        }

    }
}