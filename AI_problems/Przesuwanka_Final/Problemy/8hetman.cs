using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    class _8hetman : IProblem<byte[]>
    {
        private byte[] initial;

        public _8hetman(byte[] Initial)
        {
            this.initial = Initial;
        }

        public byte[] InitialState //zwrocenie stanu poczatkowego, implementowane przez IProblem
        {
            get
            {
                return initial;
            }
        }
   
     
        public byte[] PoprawWiersze(byte[] newState) //jezeli powtarzaja sie wiersze 
        {
            Random rnd = new Random();
            List<byte> AllowedList = new List<byte>();
            byte[] ReturnState = CopyArray(newState);
            int x;
            for (int i = 0; i < newState.Length; i++)
            {
                AllowedList.Add((byte)i);
            }
            for (int i = 0; i < newState.Length; i++)//wyrzuca z listy te elementy ktore sa juz w tablicy
            {
                if (AllowedList.Contains(newState[i]))
                    AllowedList.RemoveAt(AllowedList.IndexOf(newState[i]));
            }

            for (int i = 0; i < ReturnState.Length; i++) 
            {
                for (int j = i + 1; j < ReturnState.Length; j++)
                {
                    if (ReturnState[i] == ReturnState[j]) //Jesli sa takie same, czyli ReturnState[i]==ReturnState[j]
                    {
                        x = rnd.Next(0, AllowedList.Count); //Wylosuje indeks nowego elementu z listy
                        ReturnState[i] = AllowedList[x]; //Podstawia nowy element z listy
                        AllowedList.RemoveAt(x); //wyrzuca nowy element, bo zostal juz uzyty wiec nie chcemy go wiecej wylosowac
                        break; //Ity element zostal zmieniony wiec nie porownujemy go z dalszymi, bo na pewno nie sa takie same
                    }
                }
            }
            return ReturnState;
        }

        private byte[] GenerujRozneStany(byte[] newState) //nieuzywane
        {
            Random rnd = new Random();
            byte f;
            List<byte> usedNumbers = new List<byte>(); //lista uzytych numerow wierszy
            for (int i = 0; i < newState.Length; i++)
            {
                for (int j = 0; newState[i] < newState.Length && j < newState.Length; j++)
                {
                        f = (byte)(rnd.Next(0, newState.Length)); //losuj z przedziału 0-7
                        if (!usedNumbers.Contains(f))
                        {
                            usedNumbers.Add(f);
                            newState[j] = f;
                        }
                }
            }
            usedNumbers.Clear();
            return newState;
        }
        
        private byte[] RandomizeArrayIndex(byte[] newState)  //nieuzywane
        {
            Random rnd = new Random();
            List<byte> AllowedList = new List<byte>(newState.ToList());
            byte[] ReturnState = CopyArray(newState);
            int x;
            for (int i = 0; i < newState.Length; i++)
            {
                        x = rnd.Next(0, AllowedList.Count);
                        ReturnState[i] = AllowedList[x]; 
                        AllowedList.RemoveAt(x); 
            }
            //PrintState(ReturnState);
            return ReturnState;
        }
     
        private byte[] ExchangeColumns(byte[] newState) //zamieniaj kolumny
        {
            Random rnd = new Random();
            byte[] ReturnState = CopyArray(newState);
            int x, y;
            byte remember;
            x = rnd.Next(0, newState.Length - 1);
            y = rnd.Next(0, newState.Length - 1); //Wylosuje indeks nowego elementu z listy

            while (x == y) x = rnd.Next(0, newState.Length - 1);

            remember = ReturnState[y];
            ReturnState[y] = ReturnState[x]; 
            ReturnState[x] = remember;

            return ReturnState;
        }

        private byte[] CopyArray(byte[] state) //kopiowanie tablicy, można użyć też .Clone()
        {
            byte[] returnedArrayBytes = new byte[state.Length];

            for (int i = 0; i < state.Length; i++)
            {
                returnedArrayBytes[i] = state[i];
            }

            return returnedArrayBytes;
        }


        public IList<byte[]> Expand(byte[] state)
        {
            List<byte[]> ListOfNewStates = new List<byte[]>();
            //sprawdzenie czy wiersze w kolumnach są różne

            for (int i = 0; i < state.Length; i++) //Dla kazdego elementu itego
            {
                for (int j = i + 1; j < state.Length; j++) //Dla kazdego z nastepnych po i'tym elemencie
                {
                    if (state[i] == state[j])
                    {
                        state = PoprawWiersze(state);
                        break; 
                    }
                        
                }
            }

            byte[] newState;
            int distance1,distance2;

            newState = CopyArray(state);
            ListOfNewStates.Add(newState);
            distance1 = CheckDistance(newState);
            newState = ExchangeColumns(newState);
            distance2 = CheckDistance(newState);

            while (distance2>=distance1)
            {
                newState = ExchangeColumns(newState);
                distance2 = CheckDistance(newState);
            }
            ListOfNewStates.Add(newState);
            return ListOfNewStates;
        }

        public bool IsGoal(byte[] state)
        {
            if (CheckDistance(state) == 0)
            {
                return true;
            }
            return false;
        }

        public void PrintState(byte[] state) //drukowanie stanu
        {
            for (int i = 0; i < state.Length; i++)
                Console.Write(state[i] + " ");
        }
        
        public Func<byte[], byte[], bool> Compare()
        {
            return IsTheSame;
        }
        
        private bool IsTheSame(byte[] state1, byte[] state2) //porownywanie stanow zeby sprawdzic czy sie cos powtarza
        {
            for (int i = 0; i < state1.Length; i++)
            {
                if (state1[i] != state2[i])
                {
                    return false;
                }
            }
            return true;
        }
    
        public int CheckDistance(byte[] state) //licz ilosc konflikow
        {
            int Check = 0; //szachowanie 
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state.Length; j++) //sprawdza w poziomie
                {
                    if (state[i] == state[j] && i != j) Check++;
                }

                for (int j = 1; i - j > 0; j++) //skos w lewo
                {
                    if (state[i - j] == state[i] + j) Check++;
                    if (state[i - j] == state[i] - j) Check++;
                }

                for (int j = 1; j + i < state.Length; j++) //skos prawo
                {
                    if (state[i + j] == state[i] + j) Check++;
                    if (state[i + j] == state[i] - j) Check++;
                }
              
            }
            return Check;
        }
        
        public int ExternalDistance(Node<byte[]> element) //potrzebne do kolejki priorytetowej 
        {
            int Check = 0;
            byte[] state = element.StateOfNode;
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state.Length; j++) //sprawdza w poziomie; w pionie nie trzeba
                {
                    if (state[i] == state[j] && i != j) Check++;
                }
                
                for (int j = 1; i - j > 0; j++) //skos w lewo??
                {
                    if (state[i - j] == state[i] + j) Check++;
                    if (state[i - j] == state[i] - j) Check++;
                }
                for (int j = 1; j + i < state.Length; j++) //skos prawo
                {
                    if (state[i + j] == state[i] + j) Check++;
                    if (state[i + j] == state[i] - j) Check++;
                }
            }
            return Check;
        }

        public Func<Node<byte[]>, int> Distance() //delegat
        {
            return ExternalDistance;
        }
    }

}
