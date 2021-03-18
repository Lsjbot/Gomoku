using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomoku
{
    class cellvalueclass
    {
        public bool useless = true;
        public Dictionary<string, int> f = new Dictionary<string, int>() 
        { { "twos", 0 }, 
        { "threes", 0 }, 
        { "fours", 0 }, 
        { "openfours", 0 }, 
        { "pot4", 0 }, 
        { "free", 0 }, //free cells in line;
        { "xxx", 0 }, //own marks in free line
        { "density", 0 },
        { "", 0 }

        };

        public cellvalueclass merge(cellvalueclass c2)
        {
            cellvalueclass cv = new cellvalueclass();
            cv.useless = this.useless && c2.useless;

            foreach (string s in f.Keys)
                cv.f[s] = this.f[s] + c2.f[s];

            return cv;
        }

        public double totalvalue(playerparams ppp)
        {
            //if (useless)
            //    return 0;

            double t = 0;
            foreach (string s in f.Keys)
            {
                t += f[s] *ppp.pp[s];
            }

            return t;
        }

        public int priority()
        {
            int prio = 0;

            if (f["openfours"] > 0) //fill open 3s
                prio += 1000 + f["openfours"];
            if (f["fours"] >= 2) //4-4-threat
                prio += 800 + f["fours"];
            if (f["fours"] * f["threes"] > 0) //4-3-threat
                prio += 600 + f["fours"] * f["threes"];
            if (f["threes"] >= 2) //3-3-threat
                prio += 10 + f["threes"];

            return prio;
        }
    }
}
