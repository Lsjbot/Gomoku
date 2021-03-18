using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace gomoku
{
    public partial class Form1 : Form
    {
        board mainboard;
        public char xchar = 'X';
        public char ochar = 'O';
        playerclass[] player = new playerclass[2];

        static int nplayer = 20;
        playerclass[] playerpool = new playerclass[nplayer];
        private Label[] playerlabels = new Label[nplayer];
        public TimeSpan timepermove = new TimeSpan(0,0,30); //seconds
        public int maxdepth = 8;

        int inturn;

        public Form1()
        {
            InitializeComponent();

            Button testbutton = new Button();
            testbutton.Text = "Testbutton";
            testbutton.Location = new Point(10, 10);
            testbutton.Size = new System.Drawing.Size(100, 100);
            this.Controls.Add(testbutton);

            memo("controls count " + this.Controls.Count);

            for (int i = 0; i < nplayer; i++)
            {
                playerlabels[i] = new Label();
                //playerpool[i] = new playerclass('z', 0, false, "Player" + i);
                //playerpool[i].mutate(mutationrate);
                playerlabels[i] = new Label();
                playerlabels[i].Text = "Player " + i;//playerpool[i].name;
                playerlabels[i].Location = new Point(610, 400 + 20 * i);
                playerlabels[i].Visible = true;
                playerlabels[i].Size = new System.Drawing.Size(100, 20);
                this.Controls.Add(playerlabels[i]);


            }

            memo("after adding labels");
            memo("controls count " + this.Controls.Count);

            NewGame(false, true);


        }

        private void NewGame(bool p1human, bool p2human)
        {

            board bb = new board(this);
            playerclass p0 = new playerclass(xchar, bb.xv, p1human, "Computer player");
            playerclass p1 = new playerclass(ochar, bb.ov, p2human,"Computer player");

            DateTime before = new DateTime(2019,01,01);
            TimeSpan newest = new TimeSpan(0);
            string newestfn = "";
            foreach (string fn in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                if (File.GetCreationTime(fn) - before > newest)
                {
                    newest = File.GetCreationTime(fn) - before;
                    newestfn = fn;
                }
            }
            using (StreamReader sr = new StreamReader(newestfn))
            {
                string pstring = sr.ReadLine();
                p0.import(pstring);
                p1.import(pstring);
            }

            if (p0.ishuman)
                p0.name = "Human player";

            if (p1.ishuman)
                p1.name = "Human player";

            NewGame(p0, p1,bb);
        }

        private void NewGame(playerclass p0, playerclass p1)
        {
            board bb = new board(this);
            NewGame(p0, p1, bb);
        }

        private void NewGame(playerclass p0, playerclass p1, board bb)
        {
            mainboard = bb;
            inturn = 0;
            if (!p0.ishuman)
            {
                mainboard.set(board.center, board.center, mainboard.xv);
                inturn = 1;
            }

            DataGridSetup();
            
            player[0] = p0;
            player[0].mark = xchar;
            player[0].val = mainboard.xv;
            player[1] = p1;
            player[1].mark = ochar;
            player[1].val = mainboard.ov;


        }

        private void DataGridSetup()
        {
            dg.ColumnCount = board.size+1;
            dg.RowCount = board.size+1;
            //dg.Columns[2].Width = 4;
            foreach (DataGridViewColumn dc in dg.Columns)
            {
                dc.Width = 27;
            }
            dg.DefaultCellStyle.Font = new Font("Arial", 12);
            dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dg[board.center, board.center].Value = xchar;
            //for (int j = 0; j < board.size; j++)
            //{
            //    DataGridViewRow dr = new DataGridViewRow();
            //    dg.Rows.Add(dr);
            //}

            mainboard.DisplayBoard();

        }

        //private void DisplayBoard(board bb)
        //{
        //    for (int i = 0; i < board.size; i++)
        //        for (int j = 0; j < board.size; j++)
        //        {
        //            char c = ' ';
        //            if (bb.get(i, j) == bb.xv)
        //                c = xchar;
        //            else if (bb.get(i, j) == bb.ov)
        //                c = ochar;
        //            dg[i, j].Value = c;
        //        }

        //}

        private int makeplay(int[] xy, bool dodisplay)
        {
            if (xy.Length > 1)
                return makeplay(xy[0], xy[1],dodisplay);
            else
                return -1;
        }

        private int makeplay(int x,int y,bool dodisplay)
        {
            int winner = -1;
            mainboard.set(x, y, player[inturn].val);

            //dg[x, y].Value = player[inturn].mark;
            
            if ( dodisplay)
                mainboard.DisplayBoard();

            dg[x, y].Selected = true;

            if (mainboard.win(x, y))
            {
                winner = inturn;
                //this.Close();
            }
            else if (mainboard.full())
            {
                winner = -2;
            }
            else
            {
                inturn = 1 - inturn; // (0 -> 1, 1 -> 0)

                if (!player[inturn].ishuman)
                {
                    winner = makeplay(mainboard.computermove(player[inturn],dodisplay,0,DateTime.Now),dodisplay);
                    //inturn = 1 - inturn; // (0 -> 1, 1 -> 0)
                }
            }
            return winner;
        }

        public void memo(string s)
        {
            richTextBox1.AppendText(s + "\n");
            richTextBox1.ScrollToCaret();
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mainboard.get(e.ColumnIndex, e.RowIndex) != 0)
                return;

            int winner = makeplay(e.ColumnIndex, e.RowIndex,true);
            if ( winner >= 0)
                MessageBox.Show(player[winner].mark + " wins!");
            else if (winner == -2)
                MessageBox.Show("The game is a draw.");

        }

        private void CB_showcellvalues_CheckedChanged(object sender, EventArgs e)
        {
            int wid = 27;
            int font = 12;
            if (CB_showcellvalues.Checked || CB_prio.Checked)
            {
                wid = 40;
                font = 7;
            }
            foreach (DataGridViewColumn dc in dg.Columns)
            {
                dc.Width = wid;
            }
            dg.DefaultCellStyle.Font = new Font("Arial", font);
            mainboard.DisplayBoard();
        }

        private void Quitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            NewGame(RB_humanstart.Checked, RB_computerstart.Checked);
        }

        private void ComputerGameButton_Click(object sender, EventArgs e)
        {
            NewGame(false, false);
            makeplay(mainboard.computermove(player[inturn],true,0,DateTime.Now),true);
        }



        private void EvolveButton_Click(object sender, EventArgs e)
        {
            Label[] playerlabels = new Label[nplayer];

            double mutationrate = 1.0;
            for (int i = 0; i < nplayer; i++)
            {
                playerlabels[i] = new Label();
                playerpool[i] = new playerclass('z', 0, false, "Player_" + i);
                playerpool[i].mutate(mutationrate);
                playerpool[i].namelabel = playerlabels[i];
                playerlabels[i].Text = playerpool[i].name;
                playerlabels[i].Visible = true;
                playerlabels[i].AutoSize = true;
            }

            mutationrate = 0.5;

            for (int iround = 0; iround < 100; iround++)
            {
                memo("Round " + iround);
                for (int i = 0; i < nplayer; i++)
                {
                    for (int j = 0; j < nplayer; j++)
                    {
                        if (j == i)
                            continue;
                        //memo("Playing " + i + " vs. " + j);
                        NewGame(playerpool[i], playerpool[j]);
                        int winner = makeplay(7, 7,false);
                        if (winner >= 0)
                        {
                            int ijwin = i;
                            int ijlose = j;
                            if (winner > 0)
                            {
                                ijwin = j;
                                ijlose = i;
                            }
                            //memo(" winner: " + ijwin);
                            playerpool[ijwin].wins++;
                            playerpool[ijlose].losses++;
                        }
                    }
                }

                int maxwinner = -1;
                int maxwin = -1;
                int maxloser = -1;
                int maxloss = -1;

                playerparams ppsum = new playerparams();
                ppsum.zero();

                for (int i = 0; i < nplayer; i++)
                {
                    memo(playerpool[i].name + ": " + playerpool[i].wins + " wins, " + playerpool[i].losses + " losses.");
                    if (playerpool[i].wins > maxwin)
                    {
                        maxwin = playerpool[i].wins;
                        maxwinner = i;
                    }
                    if (playerpool[i].losses > maxloss)
                    {
                        maxloss = playerpool[i].losses;
                        maxloser = i;
                    }
                    playerpool[i].wins = 0;
                    playerpool[i].losses = 0;

                    foreach (string s in playerpool[i].par.pp.Keys)
                        ppsum.pp[s] += playerpool[i].par.pp[s];
                }

                foreach (string s in ppsum.pp.Keys)
                    memo(s + ": " + (ppsum.pp[s] / nplayer).ToString("F2"));

                playerclass ppref = new playerclass('z',0,false,"ref");
                foreach (string s in ppsum.pp.Keys)
                    memo(s + "(diff): " + (ppsum.pp[s] / nplayer - ppref.par.pp[s]).ToString("F2"));


                playerpool[maxloser] = playerpool[maxwinner].spawn(mutationrate);
                if (iround % 10 == 0)
                {
                    playerpool[maxloser].par.restoredefaults();
                    playerpool[maxloser].mutate(3 * mutationrate);
                    playerpool[maxloser].name = "X" + iround;
                }
                mutationrate *= 0.98;
            }

            string fnbase = "evolution";
            string fn;
            int k=0;
            do
            {
                fn = fnbase + k + ".txt";
                k++;
            }
            while (File.Exists(fn));

            using (StreamWriter sw = new StreamWriter(fn))
            {
                for (int i = 0; i < nplayer; i++)
                {
                    string s = playerpool[i].export();
                    memo(s);
                    sw.WriteLine(s);
                }
            }
        }

        
    }
}
