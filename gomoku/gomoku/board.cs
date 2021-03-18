using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gomoku
{
    class board
    {
        public static int size = 15;
        public static int center = size / 2;
        public int xv = 1;
        public int ov = -1;
        private static List<Tuple<int,int>> uvlist = new List<Tuple<int,int>>();
        private Form1 parent;
        private int fontsize = 14;

        //public static Dictionary<string, int> td = new Dictionary<string, int>() { 
        //{ "4win", 100 }, 
        //{ "3win", 50 }
        //};

        int[,] b = new int[size, size];
        float[,] bv = new float[size, size];
        cellvalueclass[,] cvx = new cellvalueclass[size, size];
        cellvalueclass[,] cvo = new cellvalueclass[size, size];
        double[,] cv = new double[size, size];
        int[,] prio = new int[size, size];
        int[,] offprio = new int[size, size];
        int[,] defprio = new int[size, size];

        public board(Form1 parentpar)
        {
            parent = parentpar;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    b[i, j] = 0;
            if ( uvlist.Count == 0)
            {
                //{ { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 } };
                uvlist.Add(Tuple.Create(-1,1));
                uvlist.Add(Tuple.Create( 0, 1));
                uvlist.Add(Tuple.Create( 1, 1));
                uvlist.Add(Tuple.Create( 1, 0));
            }
        }

        public void smallsize()
        {
            this.fontsize = 10;
            //parent.dg.DefaultCellStyle.Font = new Font("Arial", 10);
        }

        public board invert()
        {
            board bi = new board(parent);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    bi.b[i, j] = -this.b[i,j];
            return bi;
        }

        public board copy()
        {
            board bi = new board(parent);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    bi.b[i, j] = this.b[i, j];
            return bi;
        }

        public void DisplayBoard()
        {
            for (int i = 0; i < board.size; i++)
                for (int j = 0; j < board.size; j++)
                {
                    string c = " ";
                    if (b[i, j] != 0)
                        parent.dg[i, j].Style.Font = new System.Drawing.Font("Arial", this.fontsize);
                    if (b[i, j] == xv)
                        c = parent.xchar.ToString();
                    else if (b[i, j] == ov)
                        c = parent.ochar.ToString();
                    else if (parent.CB_showcellvalues.Checked)
                        c = cv[i, j].ToString();
                    else if (parent.CB_prio.Checked && prio[i,j] > 0)
                        c = prio[i, j].ToString();
                    parent.dg[i, j].Value = c;
                }
        }



        public void set(int x,int y,int value)
        {
            b[x, y] = value;
        }

        public void set(int[] xy, int value)
        {
            b[xy[0], xy[1]] = value;
        }


        public int get(int x, int y)
        {
            return b[x, y];
        }

        public bool inboard(int x,int y)
        {
            if (x < 0)
                return false;
            if (y < 0)
                return false;
            if (x >= size)
                return false;
            if (y >= size)
                return false;
            return true;
        }

        private int countline(int x,int y, Tuple<int,int> uv, int m)
        {
            return countline(x, y, uv.Item1, uv.Item2, m);
        }

        private int countline(int x, int y, int u, int v, int m)
        {
            int c = 0;
            int r = 1;
            while (inboard(x + r * u, y + r * v) && b[x + r * u, y + r * v] == m)
            {
                r++;
                c++;
            }

            r = 1;
            while (inboard(x - r * u, y - r * v) && b[x - r * u, y - r * v] == m)
            {
                r++;
                c++;
            }

            return c;
        }

        private int freebeyond(int x, int y, Tuple<int, int> uv, int m)
        {
            return freebeyond(x, y, uv.Item1, uv.Item2, m);
        }

        private int freebeyond(int x, int y, int u, int v, int m)
        {
            int c = 0;
            int r = 1;
            while (inboard(x + r * u, y + r * v) && b[x + r * u, y + r * v] == m)
            {
                r++;
                //c++;
            }
            if (inboard(x + r * u, y + r * v) && b[x + r * u, y + r * v] == 0)
                c++;

            r = 1;
            while (inboard(x - r * u, y - r * v) && b[x - r * u, y - r * v] == m)
            {
                r++;
                //c++;
            }
            if (inboard(x - r * u, y - r * v) && b[x - r * u, y - r * v] == 0)
                c++;

            return c;
        }

        private int freeline(int x, int y, Tuple<int, int> uv, int m)
        {
            return freeline(x, y, uv.Item1, uv.Item2, m, 0);
        }

        private int freeline(int x, int y, Tuple<int, int> uv, int m, int mode)
        {
            return freeline(x, y, uv.Item1, uv.Item2, m, mode);
        }

        private int freeline(int x, int y, int u, int v, int m, int mode)
        {
            //Mode:
            // 0 = both directions
            // 1 = positive uv-direction
            //-1 = negative uv-direction

            int c = 0;
            int r = 1;

            if (mode >= 0)
            {
                while (inboard(x + r * u, y + r * v) && b[x + r * u, y + r * v] != -m)
                {
                    r++;
                    c++;
                }
            }
            r = 1;

            if (mode <= 0)
            {
                while (inboard(x - r * u, y - r * v) && b[x - r * u, y - r * v] != -m)
                {
                    r++;
                    c++;
                }
            }

            return c;
        }

        private int xinfreeline(int x, int y, Tuple<int, int> uv, int m)
        {
            return xinfreeline(x, y, uv.Item1, uv.Item2, m, 0);
        }

        private int xinfreeline(int x, int y, Tuple<int, int> uv, int m, int mode)
        {
            return xinfreeline(x, y, uv.Item1, uv.Item2, m, mode);
        }

        private int xinfreeline(int x, int y, int u, int v, int m, int mode)
        {
            //Mode:
            // 0 = both directions
            // 1 = positive uv-direction
            //-1 = negative uv-direction

            int c = 0;
            int r = 1;

            if (mode >= 0)
            {
                while (inboard(x + r * u, y + r * v) && b[x + r * u, y + r * v] != -m)
                {
                    if (b[x + r * u, y + r * v] == m)
                        c++;
                    r++;
                }
            }
            r = 1;

            if (mode <= 0)
            {
                while (inboard(x - r * u, y - r * v) && b[x - r * u, y - r * v] != -m)
                {
                    if (b[x - r * u, y - r * v] == m)
                        c++;
                    r++;
                }
            }

            return c;
        }

        public bool win(int x, int y)
        {
            return win(x, y, b[x, y]);
        }

        public bool win(int x, int y, int m)
        {
            if (!inboard(x, y))
                return false;

            foreach (Tuple<int, int> uv in uvlist)
            {
                if (countline(x, y, uv, m) >= 4)
                    return true;
            }

            return false;
        }
        public int[] randommove(playerclass p)
        {
            return randommove(p, 15);
        }

        public int[] randommove(playerclass p, int diameter)
        {
            int x;
            int y;
            Random rnd = new Random();
            do
            {
                x = rnd.Next(diameter);
                y = rnd.Next(diameter);
                x = x + (center - diameter / 2);
                y = y + (center - diameter / 2);
            }
            while (b[x, y] != 0);

            return new int[5]{x,y,-1,-1,-1};
        }

        public int getdensity(int x, int y, int rr)
        {
            int n = 0;
            for (int i = x - rr; i <= x + rr;i++ )
                for (int j = x - rr; j <= x + rr; j++)
                {
                    if (inboard(i, j) && b[i, j] != 0)
                        n++;
                }
            return n;
        }

        public string gethalfline(int x, int y, Tuple<int,int> uv, int m, int sign)
        {
            StringBuilder sb = new StringBuilder("");
            for (int r=1;r<=5;r++)
            {
                int i = x + sign * uv.Item1 * r;
                int j = y + sign * uv.Item2 * r;
                if ( inboard(i,j))
                {
                    if (b[i, j] == 0)
                        sb.Append("_");
                    else if (b[i, j] == m)
                        sb.Append("x");
                    else
                        sb.Append("o");
                }
                else 
                    sb.Append("*");
            }
            return sb.ToString();
        }

        public cellvalueclass evaluateline(int x, int y, Tuple<int,int> uv, int m)
        {
            cellvalueclass cv = new cellvalueclass();

            cv.f["free"] = freeline(x, y, uv, m);
            cv.f["xxx"] = xinfreeline(x, y, uv, m);
            if (cv.f["free"] < 5)
            {
                cv.useless = true;
                return cv;
            }
            else if (cv.f["free"] == 5)
            {
                if (cv.f["xxx"] == 3)
                    cv.f["fours"] = 1;
                else if (cv.f["xxx"] == 2)
                    cv.f["pot4"] = 1;
            }
            else // 6+ cells free
            {
                string[] halflines = new string[2] { gethalfline(x, y, uv, m, 1), gethalfline(x, y, uv, m, -1) };
                int cl = countline(x, y, uv, m);
                if ( cl == 3)
                {
                    int fb = freebeyond(x, y, uv, m);
                    if (fb == 2)
                        cv.f["openfours"] = 1;
                    else if (fb == 1)
                        cv.f["fours"] = 1;
                }
                else if (cl == 2)
                {
                    int fb = freebeyond(x, y, uv, m);
                    if (fb == 2)
                    {
                        //do x-x_x case here!
                        for (int ihalf = 0; ihalf <= 1; ihalf++)
                        {
                            if (halflines[ihalf].StartsWith("x_x"))
                                cv.f["fours"]++;
                            else if (halflines[ihalf].StartsWith("xx_x"))
                                cv.f["fours"]++;
                            else if (halflines[ihalf].StartsWith("xx") && halflines[1-ihalf].StartsWith("_x"))
                                cv.f["fours"]++;
                        }
                        if ( cv.f["fours"] == 0)
                            cv.f["threes"] = 1;
                    }
                    else if (fb == 1)
                        cv.f["pot4"] = 1;
                }
                else if ( cv.f["xxx"] >= 1)
                {
                    for (int ihalf=0;ihalf<=1;ihalf++)
                    {
                        if (halflines[ihalf].StartsWith("_xxx") && halflines[1 - ihalf].StartsWith("_"))
                            cv.f["fours"]++;
                        else if (halflines[ihalf].StartsWith("_xx_") && halflines[1 - ihalf].StartsWith("_"))
                            cv.f["threes"]++;
                        else if (halflines[ihalf].StartsWith("x_x_") && halflines[1 - ihalf].StartsWith("_"))
                            cv.f["threes"]++;
                        else if (halflines[ihalf].StartsWith("x_") && halflines[1 - ihalf].StartsWith("_"))
                            cv.f["twos"]++;
                        else if (halflines[ihalf].StartsWith("_x_") && halflines[1 - ihalf].StartsWith("_"))
                            cv.f["twos"]++;
                        else if (halflines[ihalf].StartsWith("x_") && halflines[1 - ihalf].StartsWith("_"))
                            cv.f["twos"]++;
                        else if (halflines[ihalf].StartsWith("__x_") && halflines[1 - ihalf].StartsWith("_"))
                            cv.f["twos"]++;

                    }
                }

            }

            return cv;
        }

        public bool full()
        {
            foreach (int k in b)
                if (k == 0)
                    return false;
            return true;
        }

        public int boardcount()
        {
            int n = 0;
            foreach (int k in b)
                if (k != 0)
                    n++;
            return n;
        }

        public int[] computermove(playerclass p, bool dodisplay,int depth,DateTime starttime) 
        {
            //move[0] = x
            //move[1] = y
            //move[2] = threatlevel
            Random rnd = new Random();

            if ( boardcount() < 3)
            {
                return randommove(p, 3);
            }
            else if (boardcount() < 5 )
            {
                return randommove(p, 3);
            }

            int[] move = randommove(p);
            //bool forced = false;

            if (depth > parent.maxdepth)
            {
                parent.memo("maxdepth");
                return move;
            }
            if (DateTime.Now - starttime > parent.timepermove)
            {
                parent.memo("timeout");
                return move;
            }

            for (int i = 0; i < board.size; i++)
                for (int j = 0; j < board.size; j++)
                {
                    if ( b[i,j] == 0)
                        if (win(i, j, p.val))
                        {
                            move[0] = i;
                            move[1] = j;
                            return move;
                        }
                }

            for (int i = 0; i < board.size; i++)
                for (int j = 0; j < board.size; j++)
                {
                    if (b[i, j] == 0)
                        if (win(i, j, -p.val))
                        {
                            move[0] = i;
                            move[1] = j;
                            return move;
                        }
                }

            SortedDictionary<double, int[]> cvorder = new SortedDictionary<double, int[]>();

            int maxprio = -1;
            int maxoff = -1;
            int maxdef = -1;
            double maxcv = -1;
            for (int i = 0; i < board.size; i++)
                for (int j = 0; j < board.size; j++)
                {
                    if (b[i, j] == 0)
                    {
                        cvx[i, j] = new cellvalueclass();
                        foreach (Tuple<int,int> uv in uvlist)
                        {
                            cvx[i, j] = cvx[i, j].merge(evaluateline(i, j, uv, p.val));
                        }
                        cvx[i, j].f["density"] = (int)Math.Abs(getdensity(i, j, 2) - p.par.pp["optimaldensity"]);

                        cvo[i, j] = new cellvalueclass();
                        foreach (Tuple<int, int> uv in uvlist)
                        {
                            cvo[i, j] = cvo[i, j].merge(evaluateline(i, j, uv, -p.val));
                        }

                        cv[i, j] = cvx[i, j].totalvalue(p.par) 
                            + p.par.pp["oxratio"] * cvo[i, j].totalvalue(p.par) 
                            + p.par.pp["noise"]*rnd.NextDouble();
                        prio[i, j] = 2*cvx[i, j].priority() + cvo[i, j].priority();
                        double ccvv = cv[i, j];
                        if ( ccvv > 0.3)
                        {
                            while (cvorder.ContainsKey(ccvv))
                                ccvv += 0.001;
                            int[] cvmv = new int[5] { i, j, 0, 0, 0 };
                            cvorder.Add(-ccvv, cvmv);
                        }

                        if ( prio[i,j] > maxprio)
                        {
                            maxprio = prio[i, j];
                            maxcv = cv[i,j];
                            move[0] = i;
                            move[1] = j;
                        }
                        else if ( prio[i,j] == maxprio)
                        {
                            if ( cv[i,j] > maxcv)
                            {
                                maxcv = cv[i, j];
                                move[0] = i;
                                move[1] = j;
                            }
                        }
                        if (cvx[i, j].priority() > maxoff)
                            maxoff = cvx[i, j].priority();
                        if (cvo[i, j].priority() > maxdef)
                            maxdef = cvx[i, j].priority();
                    }
                }

            move[2] = maxprio;
            move[3] = maxoff;
            move[4] = maxdef;

            bool reachedlimit = false;
            bool win4found = false;
            bool win3found = false;

            if (move[2] < 1200) //search for VCF
            {
                playerclass other = p.otherplayer();
                //for (int i = 0; i < board.size; i++)
                foreach (double ccvv in cvorder.Keys)
                {
                    int i = cvorder[ccvv][0];
                    int j = cvorder[ccvv][1];
                    if (reachedlimit || win4found)
                        break;
                    //for (int j = 0; j < board.size; j++)
                    {
                        if (b[i, j] == 0)
                            if (cvx[i, j].f["fours"] > 0)
                            {
                                if (dodisplay)
                                    parent.memo("Testing " + i + "," + j);
                                board testboard = this.copy();
                                testboard.smallsize();
                                testboard.b[i, j] = p.val;
                                testboard.set(testboard.computermove(other, dodisplay, depth + 1, starttime), -p.val);
                                if (dodisplay)
                                    testboard.DisplayBoard();
                                int[] nextmove = testboard.computermove(p, dodisplay, depth + 1, starttime);
                                if (dodisplay)
                                    parent.memo("nextmove[2] = " + nextmove[2]);
                                if (nextmove[3] >= 600)
                                {
                                    move[0] = i;
                                    move[1] = j;
                                    move[2] = nextmove[2];
                                    parent.memo("==== 4-win found! ====");
                                    win4found = true;
                                    break;
                                }
                                else if (nextmove[2] < 0)
                                {
                                    reachedlimit = true;
                                    break;
                                }
                            }
                    }
                }
                if (dodisplay)
                    this.DisplayBoard();
            }

            if (depth > parent.maxdepth)
            {
                parent.memo("maxdepth");
                return move;
            }
            if (DateTime.Now - starttime > parent.timepermove)
            {
                parent.memo("timeout");
                return move;
            }
            if ( win4found)
            {
                parent.memo("win4found");
                return move;
            }

            if (move[2] < 10) //search for VCT
            {
                playerclass other = p.otherplayer();
                //for (int i = 0; i < board.size; i++)
                //{
                foreach (double ccvv in cvorder.Keys)
                {
                    int i = cvorder[ccvv][0];
                    int j = cvorder[ccvv][1];

                    if (reachedlimit || win3found)
                        break;

                    //for (int j = 0; j < board.size; j++)
                    {
                        if (b[i, j] == 0)
                            if (cvx[i, j].f["threes"] > 0)
                            {
                                if (dodisplay)
                                    parent.memo("Testing " + i + "," + j);
                                board testboard = this.copy();
                                testboard.smallsize();
                                testboard.b[i, j] = p.val;
                                testboard.set(testboard.computermove(other, dodisplay, depth + 1, starttime), -p.val);
                                if (dodisplay)
                                    testboard.DisplayBoard();
                                int[] nextmove = testboard.computermove(p, dodisplay, depth + 1, starttime);
                                if (dodisplay)
                                    parent.memo("nextmove[2] = " + nextmove[2]);
                                if (nextmove[3] >= 10)
                                {
                                    move[0] = i;
                                    move[1] = j;
                                    move[2] = nextmove[2];
                                    win3found = true;
                                    parent.memo("==== 3-win found! ====");
                                    break;
                                }
                                else if (nextmove[2] < 0)
                                {
                                    reachedlimit = true;
                                    break;
                                }
                            }
                    }
                }
                if (dodisplay)
                    this.DisplayBoard();

            }

            //parent.memo("Maxprio = " + maxprio);
            //parent.memo("Maxcv = " + maxcv);

            return move;
        }

    }
}
