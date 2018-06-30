using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    class Map : IProblem<Miasto>
    {
        private Miasto initial;
        private Miasto goal;
        private GeneratedMap mapa;
        

        public Map(GeneratedMap map, string poczatek, string koniec) //utworzenie mapy, podać miejscowość początkową i końcową
        {
            mapa = map;
            initial = FindNameOfCity(poczatek); 
            goal = FindNameOfCity(koniec);
            Console.WriteLine("ZADANIE: Z " + initial.Name + " Do " + goal.Name);
        }

        public Miasto InitialState
        {
            get
            {
                return this.initial;
            }
        }

        public bool IsGoal(Miasto state)
        {
            if (state.Name == goal.Name) return true;
            return false;
        }

        public void PrintState(Miasto state)
        {
            Console.WriteLine(state.Name);
        }
        
        public Miasto FindNameOfCity(string name) //znalezienie indeksu miasta o nazwie name
        {
            for (int i = 0; i < mapa.CountCity ; i++)
            {
                if (mapa.Miasta[i].Name == name)
                {
                    return mapa.Miasta[i];
                }
            }
            return null;
        }
        
//----------------------
        public IList<Miasto> Expand(Miasto state)
        {
            List<Miasto> ListOfNewStates = new List<Miasto>();
            
            foreach (Sasiad s in state.SasiednieMiasta)
                {
                    ListOfNewStates.Add(s.miasto);
                }
                return ListOfNewStates;
        }

        public int CheckDistance(Miasto state)
        {
            int distance = 0;
            if (FindNameOfCity(state.Name)!=null)
            {
                foreach (Sasiad s in state.SasiednieMiasta)
                {
                        distance = s.distance; //sasiedzi maja informacje o odleglosci od siebie
                }
                return distance;
            }
            return 0;
        }
        
        public double LiniaProsta(Miasto state1, Miasto state2) //liczenie odleglosci w linii prostej ze wzoru
        {
            double Z = 0;
            double x = (state2.X - state1.X);
            double y = (state2.Y - state1.Y);
            Z = (x * x) + (y * y);
            return Math.Sqrt(Z);
        }

        public double ExternalDistance_A(Node<Miasto> element) //heurystyka dla A*, odległość miedzy sasiadami + odległość w linii prostej od celu 
        {
            double distance = 0;
                foreach (Sasiad s in element.StateOfNode.SasiednieMiasta)
                {
                    distance = LiniaProsta(s.miasto, goal) + s.distance;
                }
                return distance;
        }

        public Func<Node<Miasto>, double> Distance_A() //delegat
        {
            return ExternalDistance_A;
        }


        public int ExternalDistance_Priority(Node<Miasto> element) //heurystyka dla kolejki priorytetowej, odległość do sasiadow 
        {
            int distance = 0;
            foreach (Sasiad s in element.StateOfNode.SasiednieMiasta)
            {
                distance = s.distance;
            }
            return distance;
        }
        
        public Func<Node<Miasto>, int> Distance_Priority() //delegat
        {
            return ExternalDistance_Priority;
        }
       

        public Func<Miasto, Miasto, bool> Compare()
        {
            return IsTheSame;
        }

        public bool IsTheSame(Miasto miasto1, Miasto miasto2)
        {
            if (miasto1.Name == miasto2.Name) return true;
            return false;
        }
    }

    public class Miasto
    {
        public string Name; 
        public int X;
        public int Y;

        public List<Sasiad> SasiednieMiasta;

        public Miasto(string name, int x, int y)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
            SasiednieMiasta = new List<Sasiad>(); //kazde miasto ma liste swoich sasiadow
        }


        public void DodajSasiada(Miasto miasto, int dystans)
        {
            Sasiad sasiad = new Sasiad(miasto, dystans); //sasiad to miasto z odlegloscia do sasiada
            SasiednieMiasta.Add(sasiad);
        }
    }

    public class Sasiad 
    {
        public Miasto miasto;
        public int distance; 

        public Sasiad(Miasto miasto, int dist)
        {
            this.miasto = miasto;
            this.distance = dist;
        }
    }

    public class GeneratedMap 
    {
        public List<Miasto> Miasta;
        public int CountCity;
        
        public GeneratedMap()
        {
            //dodaj miasta (nazwa,koordynata x, koordynata y)
            Miasta = new List<Miasto>();

            Miasta.Add(new Miasto("Oradea", 200, 50)); //[0] - Oradea
            Miasta.Add(new Miasto("Zerind", 140, 140)); //[1] - Zerind
            Miasta.Add(new Miasto("Arad", 100, 230)); //[2] - Arad
            Miasta.Add(new Miasto("Timisoara", 100, 427));//[3] -  Timisoara
            Miasta.Add(new Miasto("Lugoj", 280, 510)); //[4] - Lugoj
            Miasta.Add(new Miasto("Mehadia", 280, 600));//[5] -  Mehadia
            Miasta.Add(new Miasto("Drobeta", 275, 690));//[6] -  Drobeta
            Miasta.Add(new Miasto("Sibiu", 380, 310)); //[7] - Sibiu
            Miasta.Add(new Miasto("Rimnicu Vilcea", 440, 428));//[8] -  Rimnicu
            Miasta.Add(new Miasto("Craiova", 480, 712)); //[9] - Craiova
            Miasta.Add(new Miasto("Fagaras", 610, 333));//[10] - Fagaras
            Miasta.Add(new Miasto("Pitesti", 644, 520));//[11] -  Pitesti
            Miasta.Add(new Miasto("Bucharest", 830, 620));//[12] -  Bucharest
            Miasta.Add(new Miasto("Giurgiu", 775, 755));//[13] -  Giurgiu
            Miasta.Add(new Miasto("Urziceni", 960, 575));//[14] -  Urziceni
            Miasta.Add(new Miasto("Vaslui", 1090, 345));//[15] -  Vaslui
            Miasta.Add(new Miasto("Iasi", 1010, 200)); //[16] - Iasi
            Miasta.Add(new Miasto("Neamt", 845, 120));//[17] -  Neamt
            Miasta.Add(new Miasto("Hirsova", 1140, 575));//[18] -  Hirsova
            Miasta.Add(new Miasto("Eforie", 1210, 696)); //[19] - Eforie

            CountCity=Miasta.Count();
            //dodanie sąsiadów 

            //[0] - Oradea
            Miasta[0].DodajSasiada(Miasta[1], 71);
            Miasta[0].DodajSasiada(Miasta[7], 151);
            //[1] - Zerind
            Miasta[1].DodajSasiada(Miasta[0], 71);
            Miasta[1].DodajSasiada(Miasta[2], 75);

            //[2] - Arad
            Miasta[2].DodajSasiada(Miasta[1], 75);
            Miasta[2].DodajSasiada(Miasta[3], 118);
            Miasta[2].DodajSasiada(Miasta[7], 140);

            //[3] -  Timisoara
            Miasta[3].DodajSasiada(Miasta[2], 118);
            Miasta[3].DodajSasiada(Miasta[4], 111);

            //[4] - Lugoj
            Miasta[4].DodajSasiada(Miasta[3], 111);
            Miasta[4].DodajSasiada(Miasta[5], 70);

            //[5] -  Mehadia
            Miasta[5].DodajSasiada(Miasta[4], 70);
            Miasta[5].DodajSasiada(Miasta[6], 75);

            //[6] -  Drobeta
            Miasta[6].DodajSasiada(Miasta[5], 75);
            Miasta[6].DodajSasiada(Miasta[9], 120);

            //[7] - Sibiu
            Miasta[7].DodajSasiada(Miasta[0], 151);
            Miasta[7].DodajSasiada(Miasta[2], 140);
            Miasta[7].DodajSasiada(Miasta[8], 80);
            Miasta[7].DodajSasiada(Miasta[10], 99);

            //[8] -  Rimnicu
            Miasta[8].DodajSasiada(Miasta[7], 80);
            Miasta[8].DodajSasiada(Miasta[9], 146);
            Miasta[8].DodajSasiada(Miasta[11], 97);

            //[9] - Craiova
            Miasta[9].DodajSasiada(Miasta[6], 120);
            Miasta[9].DodajSasiada(Miasta[8], 146);
            Miasta[9].DodajSasiada(Miasta[11], 138);

            //[10] - Fagaras
            Miasta[10].DodajSasiada(Miasta[7], 99);
            Miasta[10].DodajSasiada(Miasta[12], 211);

            //[11] -  Pitesti
            Miasta[11].DodajSasiada(Miasta[8], 97);
            Miasta[11].DodajSasiada(Miasta[9], 138);
            Miasta[11].DodajSasiada(Miasta[12], 101);

            //[12] -  Bucharest
            Miasta[12].DodajSasiada(Miasta[10], 211);
            Miasta[12].DodajSasiada(Miasta[11], 101);
            Miasta[12].DodajSasiada(Miasta[13], 90);
            Miasta[12].DodajSasiada(Miasta[14], 85);

            //[13] -  Giurgiu
            Miasta[13].DodajSasiada(Miasta[12], 90);

            //[14] -  Urziceni
            Miasta[14].DodajSasiada(Miasta[12], 85);
            Miasta[14].DodajSasiada(Miasta[15], 142);
            Miasta[14].DodajSasiada(Miasta[18], 98);

            //[15] -  Vaslui
            Miasta[15].DodajSasiada(Miasta[14], 142);
            Miasta[15].DodajSasiada(Miasta[16], 92);

            //[16] - Iasi
            Miasta[16].DodajSasiada(Miasta[15], 92);
            Miasta[16].DodajSasiada(Miasta[17], 87);

            //[17] -  Neamt
            Miasta[17].DodajSasiada(Miasta[16], 87);

            //[18] -  Hirsova
            Miasta[18].DodajSasiada(Miasta[14], 98);
            Miasta[18].DodajSasiada(Miasta[19], 86);

            //[19] - Eforie
            Miasta[19].DodajSasiada(Miasta[18], 86);
            /*
            Console.WriteLine("Lista miast:");
            for (int i = 0; i < CountCity; i++)
            {
                Console.WriteLine(Miasta[i].Name);
            }
            */
        }

    }
}