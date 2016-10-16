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
            currState_ = initialState_;
            fCalcCondition_ = f;
            stateValue_ = stateValue;
        }

        public void Clear()
        {
            currState_ = initialState_;
            for( int i = 0; i < stateValue_.Length; ++i )
                stateValue_[i] = "";
        }

        public int NextState(char c)
        {
            var cond = fCalcCondition_(c);
            var nextState = finalState_;
            int condIndex = 0;
            for( int i = 1; i <= cond.Length; ++i )
            {
                if (transition_[currState_, i] != 0 && cond[i] == 1)
                {
                    nextState = transition_[currState_, i];
                    stateValue_[nextState] += c;
                    condIndex = i;
                    break;
                }
            }
            Console.WriteLine("Curr={0} Char={1} Next={2} Cond={3}", currState_, c, nextState, condIndex);
            currState_ = nextState;
            return currState_;
        }

        public string StateValue( int i )
        {
            return stateValue_[i];
        }
    }

    /**
    **/
    
    // Parse a URI into its components
    public class UriParser
    {
        private Fsm fsm_;

        public string Scheme;
        public string User;
        public string Password;
        public string Host;
        public string Port;
        public string Path;
        public string Query;
        public string Fragment;
 
        private int[] fCalcCondition(char c)
        {
            var condition = new int[15];
            if (c == ':') condition[1] = 1;
            if (c != ':') condition[2] = 1;
            if (c == '/') condition[3] = 1;
            if (c != '/') condition[4] = 1;
            if (c == '?') condition[5] = 1;
            if (c != '?' && c != '#') condition[6] = 1;
            if (c == '#') condition[7] = 1;
            if (c != '#') condition[8] = 1;
            if (c == '@') condition[ 9] = 1;
            if (c != '@' && c != ':' && c != '/') condition[10] = 1;
            if (c != '\0') condition[11] = 1;
            if (c == '\0') condition[12] = 1;
            if (c != ':' && c != '?') condition[13] = 1;
            if (c != '/' && c != '?') condition[14] = 1;
            return condition;
        }

        public UriParser()
        {
            var t = new int[16,15];     // default initilized to 0
            t[1, 11] = 2; t[1, 12] = 8;
            t[2, 1] = 3; t[2, 2] = 2; t[2, 12] = 8;
            t[3, 3] = 4; t[3, 4] = 5; t[3, 12] = 8;
            t[4, 3] = 9; t[4, 4] = 5; t[4, 12] = 8;
            t[5, 5] = 6; t[5, 6] = 5; t[5, 7] = 7; t[5, 12] = 8;
            t[6, 7] = 7; t[6, 8] = 6; t[6, 12] = 8;
            t[7, 8] = 7; t[7, 12] = 8;
            t[9, 3] = 5; t[9, 4] = 10; t[9, 12] = 8;
            t[10, 1] = 11; t[10, 3] = 5; t[10, 5] = 6; t[10, 9] = 14; t[10, 10] = 10; t[10, 12] = 8;
            t[11, 1] = 15; t[11, 3] = 5; t[11, 5] = 6; t[11, 9] = 12; t[11, 10] = 11; t[11, 12] = 8;
            t[12, 1] = 13; t[12, 3] = 5; t[12, 13] = 12; ; t[12, 5] = 6; t[12, 12] = 8;
            t[13, 3] = 5; t[13, 3] = 5; t[13, 14] = 13; t[13, 5] = 6; t[13, 12] = 8;
            t[14, 0] = 8;
            t[15, 0] = 8;

            var stateValue = new string[16];

            fsm_ = new Fsm(t, 1, 8, fCalcCondition, stateValue);
        }

        public void Parse(string uri)
        {
            fsm_.Clear();
            foreach (char c in uri)
            {
                fsm_.NextState(c);
            }
            fsm_.NextState('\0');

            Scheme = fsm_.StateValue(2);
            Host = fsm_.StateValue(12);
            string lhs = fsm_.StateValue(10);
            string rhs = fsm_.StateValue(11);
            if (Host == "")
            {
                Host = lhs;
                Port = rhs;
                User = "";
                Password = "";
            }
            else
            {
                Port = fsm_.StateValue(13);
                User = lhs;
                Password = rhs;
            }
            Path = fsm_.StateValue(5);
            Query = fsm_.StateValue(6);
            Fragment = fsm_.StateValue(7);
 
        }
    }
}
