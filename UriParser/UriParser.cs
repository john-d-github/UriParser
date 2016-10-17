using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriParser
{
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
        error3 = 16,
        pathTok = 17,
        queryTok = 18,
        fragTok = 19,
        hostTok = 20,
        rhsTok = 21,
        portTok = 22,
        endState = 23
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

    /** 
        Parse a URI into its components, as defined below
      
        URI defined in RFC 3986 (Jan 2005).
	        scheme : [ // [ user : password @ ] host [ : port ] ] [ / ] path [ ? query ] [ # fragment ]
	    Can be empty string - see RFC 3986 (2005) Section 4.1
    **/

    public class UriParser
    {
        private Fsm fsm_;

        // External callers can read these properties but not update them
        public string Scheme { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string Path { get; private set; }
        public string Query { get; private set; }
        public string Fragment { get; private set; }
        public string Error { get; private set; }
 
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
            if (c == '@') condition[9] = 1;
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
            int stateLen = (int)States.endState;                // Needed for 1st dimension of transition matrix (states)
            int condLen = fCalcCondition(' ').Length;           // Needed for 2nd dimension of transition matrix (conditions)
            var t = new int[stateLen, condLen];                 // default initilized to 0

            // (int)enum has a bit of a code smell. You could wrap (int,enum) pairs in a struct & pass an array of that struct. 
            // But this obscures the information you're trying to make clearer with `new struct(int,enum)` clutter.
 
            SetCond(t, States.initial, new int[,] { {11, (int)States.scheme }, { 12, (int)States.final} } );
            SetCond(t, States.scheme, new int[,] { {1, (int)States.slash1}, {2, (int)States.scheme}, {12, (int)States.final} });
            SetCond(t, States.slash1, new int[,] { { 3, (int)States.slash2 }, { 4, (int)States.path }, { 12, (int)States.final } });
            SetCond(t, States.slash2, new int[,] { { 3, (int)States.hostgroup }, { 4, (int)States.path }, { 12, (int)States.final } });
            SetCond(t, States.path, new int[,] { { 5, (int)States.queryTok }, { 6, (int)States.path }, { 7, (int)States.fragTok }, { 12, (int)States.final } });
            SetCond(t, States.query, new int[,] { { 5, (int)States.error3 }, { 7, (int)States.fragTok }, { 8, (int)States.query }, { 12, (int)States.final } });
            SetCond(t, States.fragment, new int[,] { { 8, (int)States.fragment }, { 12, (int)States.final } });
            SetCond(t, States.hostgroup, new int[,] { { 3, (int)States.path }, { 4, (int)States.lhs }, { 12, (int)States.final } });
            SetCond(t, States.lhs, new int[,] { { 1, (int)States.rhsTok }, { 3, (int)States.pathTok }, { 5, (int)States.queryTok }, { 9, (int)States.error1 }, { 10, (int)States.lhs }, { 12, (int)States.final } });
            SetCond(t, States.rhs, new int[,] { { 1, (int)States.error2 }, { 3, (int)States.pathTok }, { 5, (int)States.queryTok }, { 9, (int)States.hostTok }, { 10, (int)States.rhs }, { 12, (int)States.final } });
            SetCond(t, States.host, new int[,] { { 1, (int)States.portTok }, { 3, (int)States.pathTok }, { 5, (int)States.queryTok }, { 13, (int)States.host }, { 12, (int)States.final } });
            SetCond(t, States.port, new int[,] { { 1, (int)States.error2 }, { 3, (int)States.pathTok }, { 5, (int)States.queryTok }, { 14, (int)States.port }, { 12, (int)States.final } });
            SetCond(t, States.error1, new int[,] { { 11, (int)States.error1 }, { 12, (int)States.final } });
            SetCond(t, States.error2, new int[,] { { 11, (int)States.error2 }, { 12, (int)States.final } });
            SetCond(t, States.error3, new int[,] { { 11, (int)States.error3 }, { 12, (int)States.final } });
            SetCond(t, States.pathTok, new int[,] { { 11, (int)States.path }, { 12, (int)States.final } });
            SetCond(t, States.queryTok, new int[,] { { 5, (int)States.error3 }, { 11, (int)States.query }, { 12, (int)States.final } });
            SetCond(t, States.fragTok, new int[,] { { 11, (int)States.fragment }, { 12, (int)States.final } });
            SetCond(t, States.hostTok, new int[,] { { 11, (int)States.host }, { 12, (int)States.final } });
            SetCond(t, States.rhsTok, new int[,] { { 9, (int)States.hostTok }, { 11, (int)States.rhs }, { 12, (int)States.final } });
            SetCond(t, States.portTok, new int[,] { { 1, (int)States.error2 }, { 11, (int)States.port }, { 12, (int)States.final } });

            var stateValue = new string[t.GetLength(0)];

            fsm_ = new Fsm(t, (int)States.initial, (int)States.final, fCalcCondition, stateValue);
        }

        private void Clear()
        {
            fsm_.Clear();
            Scheme = "";
            User = "";
            Password = "";
            Host = "";
            Port = "";
            Path = "";
            Query = "";
            Fragment = "";
            Error = "";
        }

        public void Parse(string uri)
        {
            Clear();
            foreach (char c in uri)
            {
                fsm_.NextState(c);
            }
            fsm_.NextState('\0');           // Use '\0' as flag to signal EOS (end of string)

            Scheme = fsm_.StateValue((int)States.scheme);
            string host = fsm_.StateValue((int)States.host);
            string lhs = fsm_.StateValue((int)States.lhs);
            string rhs = fsm_.StateValue((int)States.rhs);
            if (host == "")
            {
                Host = lhs;
                Port = rhs;
                User = "";
                Password = "";
            }
            else
            {
                Host = host;
                Port = fsm_.StateValue((int)States.port);
                User = lhs;
                Password = rhs;
            }

            // Any trailing spaces or '/' can be removed 
            Path = fsm_.StateValue((int)States.path).TrimEnd( new char[] { ' ', '/' } );
            // Distinguish between `foo:xyz` (relative) and `foo:/xyz` (absolute). Path should be `xyz` and `/xyz` respectively
            // So if there is no Host, and the state `slash2` captured '/', then treat as absolute path
            if ( Host == "" && fsm_.StateValue((int)States.slash2) == "/" )
                Path = "/" + Path;

            Query = fsm_.StateValue((int)States.query);
            Fragment = fsm_.StateValue((int)States.fragment);

            string error1Explain = "Parsing hostgroup - if you find '@' without having seen `:rhs`, you're missing password";
            string error2Explain = "Parsing hostgroup - extra colon found in user:pass or host:port";
            string error3Explain = "Parsing query - extra '?' found";
            string error1 = fsm_.StateValue((int)States.error1);
            string error2 = fsm_.StateValue((int)States.error2);
            string error3 = fsm_.StateValue((int)States.error3);
            if (error1 != "")
                Error += error1Explain + " Bad parse at: " + error1;
            if (error2 != "")
                Error += (Error == "" ? "" : "\n") + error2Explain + " Bad parse at: " + error2;
            if (error3 != "")
                Error += (Error == "" ? "" : "\n") + error3Explain + " Bad parse at: " + error3;

            // If no ':' was found following Scheme, then what was parsed into Scheme is not valid - it's probably the host.
            // Add this to error list
            if (Scheme != "" && fsm_.StateValue((int)States.slash1) != ":")
                Error += (Error == "" ? "": "\n") + "Missing scheme (no colon found to terminate it)";
        }
    }
}
