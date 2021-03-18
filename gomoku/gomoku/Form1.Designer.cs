namespace gomoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dg = new System.Windows.Forms.DataGridView();
            this.CB_showcellvalues = new System.Windows.Forms.CheckBox();
            this.CB_prio = new System.Windows.Forms.CheckBox();
            this.Quitbutton = new System.Windows.Forms.Button();
            this.ComputerGameButton = new System.Windows.Forms.Button();
            this.NewGameButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RB_humanstart = new System.Windows.Forms.RadioButton();
            this.RB_computerstart = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.EvolveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(12, 12);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 24;
            this.dg.Size = new System.Drawing.Size(722, 618);
            this.dg.TabIndex = 0;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellContentClick);
            this.dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellContentClick);
            // 
            // CB_showcellvalues
            // 
            this.CB_showcellvalues.AutoSize = true;
            this.CB_showcellvalues.Location = new System.Drawing.Point(88, 792);
            this.CB_showcellvalues.Name = "CB_showcellvalues";
            this.CB_showcellvalues.Size = new System.Drawing.Size(134, 21);
            this.CB_showcellvalues.TabIndex = 1;
            this.CB_showcellvalues.Text = "Show cell values";
            this.CB_showcellvalues.UseVisualStyleBackColor = true;
            this.CB_showcellvalues.CheckedChanged += new System.EventHandler(this.CB_showcellvalues_CheckedChanged);
            // 
            // CB_prio
            // 
            this.CB_prio.AutoSize = true;
            this.CB_prio.Location = new System.Drawing.Point(89, 828);
            this.CB_prio.Name = "CB_prio";
            this.CB_prio.Size = new System.Drawing.Size(137, 21);
            this.CB_prio.TabIndex = 2;
            this.CB_prio.Text = "Show prio values";
            this.CB_prio.UseVisualStyleBackColor = true;
            this.CB_prio.CheckedChanged += new System.EventHandler(this.CB_showcellvalues_CheckedChanged);
            // 
            // Quitbutton
            // 
            this.Quitbutton.Location = new System.Drawing.Point(908, 826);
            this.Quitbutton.Name = "Quitbutton";
            this.Quitbutton.Size = new System.Drawing.Size(147, 63);
            this.Quitbutton.TabIndex = 3;
            this.Quitbutton.Text = "Quit";
            this.Quitbutton.UseVisualStyleBackColor = true;
            this.Quitbutton.Click += new System.EventHandler(this.Quitbutton_Click);
            // 
            // ComputerGameButton
            // 
            this.ComputerGameButton.Location = new System.Drawing.Point(819, 213);
            this.ComputerGameButton.Name = "ComputerGameButton";
            this.ComputerGameButton.Size = new System.Drawing.Size(178, 66);
            this.ComputerGameButton.TabIndex = 4;
            this.ComputerGameButton.Text = "Computer vs computer";
            this.ComputerGameButton.UseVisualStyleBackColor = true;
            this.ComputerGameButton.Click += new System.EventHandler(this.ComputerGameButton_Click);
            // 
            // NewGameButton
            // 
            this.NewGameButton.Location = new System.Drawing.Point(820, 295);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(177, 52);
            this.NewGameButton.TabIndex = 5;
            this.NewGameButton.Text = "New game";
            this.NewGameButton.UseVisualStyleBackColor = true;
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RB_humanstart);
            this.panel1.Controls.Add(this.RB_computerstart);
            this.panel1.Location = new System.Drawing.Point(819, 370);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 6;
            // 
            // RB_humanstart
            // 
            this.RB_humanstart.AutoSize = true;
            this.RB_humanstart.Location = new System.Drawing.Point(26, 50);
            this.RB_humanstart.Name = "RB_humanstart";
            this.RB_humanstart.Size = new System.Drawing.Size(113, 21);
            this.RB_humanstart.TabIndex = 1;
            this.RB_humanstart.Text = "Human starts";
            this.RB_humanstart.UseVisualStyleBackColor = true;
            // 
            // RB_computerstart
            // 
            this.RB_computerstart.AutoSize = true;
            this.RB_computerstart.Checked = true;
            this.RB_computerstart.Location = new System.Drawing.Point(26, 23);
            this.RB_computerstart.Name = "RB_computerstart";
            this.RB_computerstart.Size = new System.Drawing.Size(129, 21);
            this.RB_computerstart.TabIndex = 0;
            this.RB_computerstart.TabStop = true;
            this.RB_computerstart.Text = "Computer starts";
            this.RB_computerstart.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(312, 648);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(462, 241);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // EvolveButton
            // 
            this.EvolveButton.Location = new System.Drawing.Point(819, 128);
            this.EvolveButton.Name = "EvolveButton";
            this.EvolveButton.Size = new System.Drawing.Size(176, 64);
            this.EvolveButton.TabIndex = 8;
            this.EvolveButton.Text = "Evolve players";
            this.EvolveButton.UseVisualStyleBackColor = true;
            this.EvolveButton.Click += new System.EventHandler(this.EvolveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(841, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "test";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 904);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EvolveButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NewGameButton);
            this.Controls.Add(this.ComputerGameButton);
            this.Controls.Add(this.Quitbutton);
            this.Controls.Add(this.CB_prio);
            this.Controls.Add(this.CB_showcellvalues);
            this.Controls.Add(this.dg);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dg;
        public System.Windows.Forms.CheckBox CB_showcellvalues;
        public System.Windows.Forms.CheckBox CB_prio;
        private System.Windows.Forms.Button Quitbutton;
        private System.Windows.Forms.Button ComputerGameButton;
        private System.Windows.Forms.Button NewGameButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RB_humanstart;
        private System.Windows.Forms.RadioButton RB_computerstart;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button EvolveButton;
        private System.Windows.Forms.Label label1;
    }
}

