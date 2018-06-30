using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka_Final
{
        public interface IProblem<State>
        {
            State InitialState { get; } //poczatkowy stan
            bool IsGoal(State state); //czy osiagnieto cel 
            IList<State> Expand(State state); //wyszukiwanie nowych stanow
            int CheckDistance(State state); //odleglosc, ilosc konfliktow
            Func<State, State, bool> Compare(); //porownywanie stanow, sprawdzanie powtorzen 
            void PrintState(State state); //drukowanie stanu 
    }
}
