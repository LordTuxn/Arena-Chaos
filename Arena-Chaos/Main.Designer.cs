namespace ArenaChaos_Reworked {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.pnlStartScreen = new System.Windows.Forms.Panel();
            this.lblEnemiesLeft = new System.Windows.Forms.Label();
            this.lblWave = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.tmrPlayer = new System.Windows.Forms.Timer(this.components);
            this.tmrFixedDeltaTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pnlStartScreen
            // 
            this.pnlStartScreen.BackgroundImage = global::ArenaChaos_Reworked.Properties.Resources.StartScreen;
            this.pnlStartScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlStartScreen.Location = new System.Drawing.Point(138, 103);
            this.pnlStartScreen.Margin = new System.Windows.Forms.Padding(2);
            this.pnlStartScreen.Name = "pnlStartScreen";
            this.pnlStartScreen.Size = new System.Drawing.Size(525, 244);
            this.pnlStartScreen.TabIndex = 10;
            // 
            // lblEnemiesLeft
            // 
            this.lblEnemiesLeft.AutoSize = true;
            this.lblEnemiesLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnemiesLeft.ForeColor = System.Drawing.Color.White;
            this.lblEnemiesLeft.Location = new System.Drawing.Point(590, 63);
            this.lblEnemiesLeft.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEnemiesLeft.Name = "lblEnemiesLeft";
            this.lblEnemiesLeft.Size = new System.Drawing.Size(208, 31);
            this.lblEnemiesLeft.TabIndex = 13;
            this.lblEnemiesLeft.Text = "Enemies left: 10";
            // 
            // lblWave
            // 
            this.lblWave.AutoSize = true;
            this.lblWave.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWave.ForeColor = System.Drawing.Color.White;
            this.lblWave.Location = new System.Drawing.Point(661, 32);
            this.lblWave.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWave.Name = "lblWave";
            this.lblWave.Size = new System.Drawing.Size(128, 31);
            this.lblWave.TabIndex = 12;
            this.lblWave.Text = "Wave: 10";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.White;
            this.lblScore.Location = new System.Drawing.Point(617, 0);
            this.lblScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(175, 31);
            this.lblScore.TabIndex = 11;
            this.lblScore.Text = "Score: 10000";
            // 
            // pnlControls
            // 
            this.pnlControls.BackgroundImage = global::ArenaChaos_Reworked.Properties.Resources.Controls;
            this.pnlControls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlControls.Location = new System.Drawing.Point(2, 0);
            this.pnlControls.Margin = new System.Windows.Forms.Padding(2);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(260, 112);
            this.pnlControls.TabIndex = 14;
            // 
            // tmrPlayer
            // 
            this.tmrPlayer.Interval = 10;
            this.tmrPlayer.Tick += new System.EventHandler(this.TmrPlayer_Tick);
            // 
            // tmrFixedDeltaTime
            // 
            this.tmrFixedDeltaTime.Interval = 25;
            this.tmrFixedDeltaTime.Tick += new System.EventHandler(this.TmrFixedDeltaTime_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(32)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.lblEnemiesLeft);
            this.Controls.Add(this.lblWave);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pnlStartScreen);
            this.Name = "Main";
            this.Text = "Arena Chaos";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlStartScreen;
        private System.Windows.Forms.Label lblEnemiesLeft;
        private System.Windows.Forms.Label lblWave;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Timer tmrPlayer;
        private System.Windows.Forms.Timer tmrFixedDeltaTime;
    }
}

