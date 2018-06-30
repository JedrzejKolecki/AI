using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
    class Program
    {
        //DLA MAPY
        public static void WyswietlSciezke(Node<Miasto> wynik) //dla MAPY
        {
            string name;
            int i;
            int dystans=0;
            int count = 1;
            Node<Miasto> wynik2; 
            List<string> miasto = new List<string>();
            while (wynik.parent!=null)
            {
                count++;
                name=wynik.StateOfNode.Name;
                miasto.Add(name);
                wynik2 = wynik; 
                wynik = wynik.parent;
                dystans += checkDistance(wynik.StateOfNode, wynik2.StateOfNode);//wynik.currentDistance;
                //Console.WriteLine(dystans);
            }

            miasto.Add(wynik.StateOfNode.Name);
            miasto.Reverse();

            for (i=0;i<miasto.Count;i++)
            {
                Console.Write(miasto[i] + " - ");
            }
            Console.WriteLine("\nCałkowity koszt: " + dystans + "\n");
            Console.WriteLine("\nKroki: " + count + "\n");
        }


        public static int checkDistance(Miasto state, Miasto state2)
        {
            int distance = 0;
            {
                foreach (Sasiad s in state.SasiednieMiasta)
                {
                    if (s.miasto.Name == state2.Name) distance += s.distance;
                }
                return distance;
            }
        }

        //DLA HETMANOW
        //DLA PRZESUWANKI

        public static void WyswietlSciezke(Node<byte[,]> wynik) //dla MAPY
        {
            int count = 1;
            while (wynik.parent != null)
            {
                count++;
                Console.WriteLine("\nKroki: " + count + "\n");
            }
        }

        static void Main(string[] args)
        {
            //---------------------------------------------------------------------------------------------
            Console.Write("\nMAPA RUMUNII\n\n");

            GeneratedMap generuj = new GeneratedMap();
            Map map = new Map(generuj, "Oradea", "Lugoj");
            TreeSearch PrzeszukiwanieMapy = new TreeSearch();

            Console.Write("\n\n--KOLEJKA:\n-----------------------------\n");
            QueueFringe<Node<Miasto>> FringeQueueMap = new QueueFringe<Node<Miasto>>();
            Node<Miasto> WynikQueueMap = PrzeszukiwanieMapy.TreeSearchMethodMap(map, FringeQueueMap);
            Console.WriteLine("\n");
            Console.WriteLine("PRZEBYTA DROGA: \n");
            WyswietlSciezke(WynikQueueMap);
            Console.ReadKey();

            Console.Write("\n\n--KOLEJKA PRIORYTETOWA\n-----------------------------\n");
            QueueFringe_Priorytet<Node<Miasto>> FringePriorityQueue = new QueueFringe_Priorytet<Node<Miasto>>(map.Distance_Priority());
            Node<Miasto> WynikPriorityQueue = PrzeszukiwanieMapy.TreeSearchMethodMap(map, FringePriorityQueue);
            Console.WriteLine("\n");
            Console.WriteLine("PRZEBYTA DROGA: \n");
            WyswietlSciezke(WynikPriorityQueue);
            Console.ReadKey();

            Console.Write("\n\n--STOS\n-----------------------------\n");

            StackFringe<Node<Miasto>> FringeStackMap = new StackFringe<Node<Miasto>>();
            Node<Miasto> WynikStackMap = PrzeszukiwanieMapy.TreeSearchMethodMap(map, FringeStackMap);
            Console.WriteLine("\n");
            Console.WriteLine("PRZEBYTA DROGA: \n");
            WyswietlSciezke(WynikStackMap);
            Console.ReadKey();
            
            Console.Write("\n\n--A*\n-----------------------------\n");
            A_z_gwiazdka<Node<Miasto>> FringeA = new A_z_gwiazdka<Node<Miasto>>(map.Distance_A());
            Node<Miasto> WynikAMap = PrzeszukiwanieMapy.TreeSearchMethodMap(map, FringeA);
            Console.WriteLine("\n");
            Console.WriteLine("PRZEBYTA DROGA: \n");
            WyswietlSciezke(WynikAMap);
            Console.ReadKey();
            


            //---------------------------------------------------------------------------------------------

            byte[] init = new byte[8]
            {
                0, 5, 3, 4, 5, 6, 2, 1
            };
            
            Console.Write("\n\nPROGRAM nHETMANów\n\n");

            _8hetman Hetmany = new _8hetman(init);
            TreeSearch PrzeszukiwanieHetman = new TreeSearch(); //tworzenie nowego obiektu TreeSearch
            Console.Write("\n\n--STOS\n-----------------------------\n");
            StackFringe<Node<byte[]>> FringeStackHetman = new StackFringe<Node<byte[]>>(); //stos 
            Node<byte[]> WynikStack = PrzeszukiwanieHetman.TreeSearchMethod(Hetmany, FringeStackHetman); //TreeSearchWithStack to metoda obiektu
            Console.WriteLine("\n");
            Hetmany.PrintState(WynikStack.StateOfNode);
            Console.ReadKey();

            Console.Write("\n\n--KOLEJKA:\n-----------------------------\n");
            QueueFringe<Node<byte[]>> FringeQueueHetman = new QueueFringe<Node<byte[]>>(); //kolejka zwykla
            Node<byte[]> WynikQueue = PrzeszukiwanieHetman.TreeSearchMethod(Hetmany, FringeQueueHetman); //TreeSearchWithStack to metoda obiektu
            Console.WriteLine("\n");
            Hetmany.PrintState(WynikQueue.StateOfNode);
            Console.ReadKey();

            Console.Write("\n\n--KOLEJKA PRIORYTETOWA:\n-----------------------------\n");
            QueueFringe_Priorytet<Node<byte[]>> FringePriorityHetman = new QueueFringe_Priorytet<Node<byte[]>>(Hetmany.Distance());
            Node<byte[]> WynikQueuePriority = PrzeszukiwanieHetman.TreeSearchMethod(Hetmany, FringePriorityHetman); //TreeSearchWithStack to metoda obiektu
            Console.WriteLine("\n");
            Hetmany.PrintState(WynikQueuePriority.StateOfNode);
            Console.ReadKey();

            //---------------------------------------------------------------------------------------------

            Console.Write("\n\nPROGRAM PRZESUWANKA\n");
            
            byte[,] initial =
            {
                {4,2,0},
                {7,3,5},
                {1,6,8}
            };

            byte[,] goal =
            {
                {0,1,2},
                {3,4,5},
                {6,7,8}
            };

            Przesuwanka przesuwanka = new Przesuwanka(initial, goal);
            TreeSearch PrzeszukiwaniePrzesuwanki = new TreeSearch();

            Console.Write("\n\n--KOLEJKA:\n-----------------------------\n");
            QueueFringe<Node<byte[,]>> FringeQueuePrzesuwanka = new QueueFringe<Node<byte[,]>>();
            Node<byte[,]> WynikKolejka = PrzeszukiwaniePrzesuwanki.TreeSearchMethod(przesuwanka, FringeQueuePrzesuwanka);
            Console.WriteLine("\n");
            przesuwanka.PrintState(WynikKolejka.StateOfNode);
            WyswietlSciezke(WynikKolejka);
            Console.ReadKey();

            Console.Write("\n\n--KOLEJKA PRIORYTETOWA:\n-----------------------------\n");

            QueueFringe_Priorytet<Node<byte[,]>> FringePriorityPrzesuwanka = new QueueFringe_Priorytet<Node<byte[,]>>(przesuwanka.Distance());
            Node<byte[,]> WynikKolejkaPriorytet = PrzeszukiwaniePrzesuwanki.TreeSearchMethod(przesuwanka, FringePriorityPrzesuwanka);
            Console.WriteLine("\n");
            przesuwanka.PrintState(WynikKolejkaPriorytet.StateOfNode);
            Console.ReadKey();

            Console.Write("\n\n--STOS\n-----------------------------\n");

            StackFringe<Node<byte[,]>> FringeStackPrzesuwanka = new StackFringe<Node<byte[,]>>();
            Node<byte[,]> WynikStos = PrzeszukiwaniePrzesuwanki.TreeSearchMethod(przesuwanka, FringeStackPrzesuwanka);
            Console.WriteLine("\n");
            przesuwanka.PrintState(WynikStos.StateOfNode);
            Console.ReadKey();
        }
    }
}
