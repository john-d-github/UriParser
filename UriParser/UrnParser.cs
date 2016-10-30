using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriParser
{

    /** 
        Parse a URN into its components, as defined below
      
        URI defined in RFC 2141 (May 1997).
            scheme : nid : nss
        scheme must be 'urn'
    **/

    public class UrnParser
    {

        // beginState and endState are dummy states - endState represents the max enum value + 1
        // used to size an array of states

        enum States
        {
            beginState = 0,
            initial = 1,
            scheme = 2,
            nidTok = 3,
            nid = 4,
            nssTok = 5,
            nss = 6,
            error1 = 7,
            error2 = 8,
            final = 9,
            endState = 10
        }

        enum Conds
        {
            beginCond = 0,
            colon = 1,
            notColon = 2,
            notEOS = 3,
            EOS = 4,
            endCond = 5
        }

        private Fsm fsm_;

        // External callers can read these properties but not update them
        public string Scheme { get; private set; }
        public string Nid { get; private set; }
        public string Nss { get; private set; }
        public string Error { get; private set; }

        // Some of these conditions are overlapping
        // Ensure that a given state does not use overlapping conditions

        private int[] fCalcCondition(char c)
        {
            var condition = new int[(int)Conds.endCond];
            if (c == ':') condition[(int)Conds.colon] = 1;
            if (c != ':') condition[(int)Conds.notColon] = 1;
            if (c != '\0') condition[(int)Conds.notEOS] = 1;
            if (c == '\0') condition[(int)Conds.EOS] = 1;
            return condition;
        }

        // The transition matrix[state,condition] represents for each state & condition, what the next state is
        // A value of 0 means that the condition is not applicable for that state

        private void SetCond(int[,] t, States s, int[,] condState)
        {
            for (int i = 0; i < condState.GetLength(0); ++i)
                t[(int)s, condState[i, 0]] = condState[i, 1];
        }

        public UrnParser()
        {
            int stateLen = (int)States.endState;                // Needed for 1st dimension of transition matrix (states)
            //int condLen = fCalcCondition(' ').Length;           // Needed for 2nd dimension of transition matrix (conditions)
            int condLen = (int)Conds.endCond;                   // Needed for 2nd dimension of transition matrix (conditions)
            var t = new int[stateLen, condLen];                 // default initialized to 0

            // (int)enum has a bit of a code smell. You could wrap (int,enum) pairs in a struct & pass an array of that struct. 
            // But this obscures the information you're trying to make clearer with `new struct(int,enum)` clutter.

            SetCond(t, States.initial, new int[,] { { (int)Conds.notEOS, (int)States.scheme }, { (int)Conds.EOS, (int)States.final } });
            SetCond(t, States.scheme, new int[,] { { (int)Conds.colon, (int)States.nidTok }, { (int)Conds.notColon, (int)States.scheme }, { (int)Conds.EOS, (int)States.final } });
            SetCond(t, States.nidTok, new int[,] { { (int)Conds.colon, (int)States.error1 }, { (int)Conds.notColon, (int)States.nid }, { (int)Conds.EOS, (int)States.final } });
            SetCond(t, States.nid, new int[,] { { (int)Conds.colon, (int)States.nssTok }, { (int)Conds.notColon, (int)States.nid }, { (int)Conds.EOS, (int)States.error2 } });
            SetCond(t, States.nssTok, new int[,] { { (int)Conds.notEOS, (int)States.nss }, { (int)Conds.EOS, (int)States.final } });
            SetCond(t, States.nss, new int[,] { { (int)Conds.notEOS, (int)States.nss }, { (int)Conds.EOS, (int)States.final } });
            SetCond(t, States.error1, new int[,] { { (int)Conds.notEOS, (int)States.error1 }, { (int)Conds.EOS, (int)States.final } });
            SetCond(t, States.error2, new int[,] { { (int)Conds.notEOS, (int)States.error2 }, { (int)Conds.EOS, (int)States.final } });

            var stateValue = new string[t.GetLength(0)];

            fsm_ = new Fsm(t, (int)States.initial, (int)States.final, fCalcCondition, stateValue);
        }

        private void Clear()
        {
            fsm_.Clear();
            Scheme = "";
            Nid = "";
            Nss = "";
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
            // Caution: C# will usually stop you reusing a vble with same name but not if a property has same name
            // I had `string Nid = ...` which hid the property `Nid` but no warning!
            Nid = fsm_.StateValue((int)States.nid);
            string nss = fsm_.StateValue((int)States.nss);

            // Post corrections - it's cleaner here to have a first-pass parse
            // and then have a second pass to tidy up afterwards, rather than trying to do everything in one pass

            // Any trailing spaces can be removed 
            Nss = nss.TrimEnd(new char[] { ' ' });

            string error1 = fsm_.StateValue((int)States.error1);
            string error2 = fsm_.StateValue((int)States.error2);
            string error3 = "";
            string error4 = "";
            if (Scheme != "urn") error3 = "Scheme=" + Scheme;
            if (Nid == "urn") error4 = "Nid=" + Nid;

            string error1Explain = "Missing NID";
            string error2Explain = "Missing NSS";
            string error3Explain = "Scheme must be 'urn'";
            string error4Explain = "NID cannot be 'urn'";
            if (error1 != "")
                Error += error1Explain + " Bad parse at: " + error1;
            if (error2 != "")
                Error += (Error == "" ? "" : "\n") + error2Explain + " Bad parse at: " + error2;
            if (error3 != "")
                Error += (Error == "" ? "" : "\n") + error3Explain + " Bad parse at: " + error3;
            if (error4 != "")
                Error += (Error == "" ? "" : "\n") + error4Explain + " Bad parse at: " + error4;
        }
    }
}
