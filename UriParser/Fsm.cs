using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriParser
{
    // Finite state machine
    public class Fsm
    {
        int[,] transition_;
        int initialState_;
        int finalState_;
        int currState_;
        public delegate int[] CalcCondition(char c);
        private CalcCondition fCalcCondition_;
        string[] stateValue_;

        public Fsm(int[,] transition, int initialState, int finalState, CalcCondition f, string[] stateValue)
        {
            transition_ = transition;
            initialState_ = initialState;
            finalState_ = finalState;
            fCalcCondition_ = f;
            stateValue_ = stateValue;
            currState_ = initialState_;
        }

        public void Clear()
        {
            currState_ = initialState_;
            for (int i = 0; i < stateValue_.Length; ++i)
                stateValue_[i] = "";
        }

        public int NextState(char c)
        {
            var cond = fCalcCondition_(c);
            var nextState = finalState_;
            //int condIndex = 0;
            for (int i = 1; i < cond.Length; ++i)
            {
                if (transition_[currState_, i] != 0 && cond[i] == 1)
                {
                    nextState = transition_[currState_, i];
                    if ( c != '\0' )
                        stateValue_[nextState] += c;
                    //condIndex = i;
                    break;
                }
            }
            //Console.WriteLine("Curr={0} Char={1} Next={2} Cond={3}", currState_, c == '\0' ? "EOS" : c.ToString(), nextState, condIndex);
            currState_ = nextState;
            return currState_;
        }

        public string StateValue(int i)
        {
            return stateValue_[i];
        }
    }

}
