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
            for( int i = 1; i < cond.Length; ++i )
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

    // beginState and endState are dummy states - endState represents the max enum value + 1
    // used to size an array of states

    enum States
    {
        beginState = 0,
        initial = 1,
        scheme = 2,
        slash1 = 3,
        slash2 = 4,
        path = 5,
        query = 6,
        fragment = 7,
        final = 8,
        hostgroup = 9,
        lhs = 10,
        rhs = 11,
        host = 12,
        port = 13,
        error1 = 14,
        error2 = 15,
        endState = 16
    }

    // No longer used
    struct CondState
    {
        public int cond;
        public States state;
        public CondState( int cond, States state )
        {
            this.cond = cond;
            this.state = state;
        }
    }

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
        public string Error;
 
        // Some of these conditions are overlapping
        // Ensure that a given state does not use overlapping conditions

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

        // The transition matrix[state,condition] represents for each state & condition, what the next state is
        // A value of 0 means that the condition is not applicable for that state

        private void SetCond( int[,] t, States s, int[,] condState )
        {
            for (int i = 0; i < condState.GetLength(0); ++i)
                t[(int)s, condState[i,0]] = condState[i,1];
        }

        public UriParser()
        {
            var t = new int[(int)States.endState,15];     // default initilized to 0

            // (int)enum has a bit of a code smell. You could wrap (int,enum) pairs in a struct & pass an array of that struct. 
            // But this obscures the information you're trying to make clearer with `new struct(int,enum)` clutter.
 
            SetCond(t, States.initial, new int[,] { {11, (int)States.scheme }, { 12, (int)States.final} } );
            SetCond(t, States.scheme, new int[,] { {1, (int)States.slash1}, {2, (int)States.scheme}, {12, (int)States.final} });
            SetCond(t, States.slash1, new int[,] { { 3, (int)States.slash2 }, { 4, (int)States.path }, { 12, (int)States.final } });
            SetCond(t, States.slash2, new int[,] { { 3, (int)States.hostgroup }, { 4, (int)States.path }, { 12, (int)States.final } });
            SetCond(t, States.path, new int[,] { { 5, (int)States.query }, { 6, (int)States.path }, { 7, (int)States.fragment }, { 12, (int)States.final } });
            SetCond(t, States.query, new int[,] { { 7, (int)States.fragment }, { 8, (int)States.query }, { 12, (int)States.final } });
            SetCond(t, States.fragment, new int[,] { { 8, (int)States.fragment }, { 12, (int)States.final } });
            SetCond(t, States.hostgroup, new int[,] { { 3, (int)States.path }, { 4, (int)States.lhs }, { 12, (int)States.final } });
            SetCond(t, States.lhs, new int[,] { { 1, (int)States.rhs }, { 3, (int)States.path }, { 5, (int)States.query }, { 9, (int)States.error1 }, { 10, (int)States.lhs }, { 12, (int)States.final } });
            SetCond(t, States.rhs, new int[,] { { 1, (int)States.error2 }, { 3, (int)States.path }, { 5, (int)States.query }, { 9, (int)States.host }, { 10, (int)States.rhs }, { 12, (int)States.final } });
            SetCond(t, States.host, new int[,] { { 1, (int)States.port }, { 3, (int)States.path }, { 5, (int)States.query }, { 13, (int)States.host }, { 12, (int)States.final } });
            SetCond(t, States.port, new int[,] { { 1, (int)States.error2 }, { 3, (int)States.path }, { 5, (int)States.query }, { 14, (int)States.port }, { 12, (int)States.final } });
            SetCond(t, States.error1, new int[,] { { 11, (int)States.error1 }, { 12, (int)States.final } });
            SetCond(t, States.error2, new int[,] { { 11, (int)States.error2 }, { 12, (int)States.final } });

            var stateValue = new string[t.GetLength(0)];

            fsm_ = new Fsm(t, (int)States.initial, (int)States.final, fCalcCondition, stateValue);
        }

        public void Parse(string uri)
        {
            fsm_.Clear();
            foreach (char c in uri)
            {
                fsm_.NextState(c);
            }
            fsm_.NextState('\0');           // Use '\0' as flag to signal EOS

            Scheme = fsm_.StateValue((int)States.scheme);
            Host = fsm_.StateValue((int)States.host);
            string lhs = fsm_.StateValue((int)States.lhs);
            string rhs = fsm_.StateValue((int)States.rhs);
            if (Host == "")
            {
                Host = lhs;
                Port = rhs;
                User = "";
                Password = "";
            }
            else
            {
                Port = fsm_.StateValue((int)States.port);
                User = lhs;
                Password = rhs;
            }
            Path = fsm_.StateValue((int)States.path);
            Query = fsm_.StateValue((int)States.query);
            Fragment = fsm_.StateValue((int)States.fragment);
            Error = fsm_.StateValue((int)States.error1) + " " + fsm_.StateValue((int)States.error2);
        }
    }
}
