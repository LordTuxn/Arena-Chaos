using ArenaChaos_Reworked.GameElements;
using ArenaChaos_Reworked.GameElements.Enemies;
using ArenaChaos_Reworked.GameElements.Player;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static ArenaChaos_Reworked.Utils;

namespace ArenaChaos_Reworked {

    public partial class Main : Form {
        private Player player;

        private int score;

        public int Score {
            get { return score; }
            set {
                score = value;
                lblScore.Text = $"Score: {value}";
                lblScore.Location = new Point(ClientSize.Width - lblScore.Width, 0);
            }
        }

        private double wave;

        public double Wave {
            get { return wave; }
            set {
                wave = value;
                lblWave.Text = $"Wave: {value}";
                lblWave.Location = new Point(ClientSize.Width - lblWave.Width, lblWave.Height);

                //Function to get the required Enemies for any given wave.
                //E(w) = floor(11 - 10ℯ^(-0.2w))
                RequiredEnemies = Math.Floor(11 - 10 * Math.Pow(Math.E, value / -5));
                lblEnemiesLeft.Text = $"Required Enemies: {requiredEnemies}";
                lblEnemiesLeft.Location = new Point(ClientSize.Width - lblEnemiesLeft.Width, lblEnemiesLeft.Height * 2);
                player.Health = 3;
            }
        }

        private bool gameIsStarted;

        public bool GameIsStarted {
            get { return gameIsStarted; }
            set {
                gameIsStarted = value;
                if (value) {
                    pnlStartScreen.Visible = false;
                    pnlControls.Visible = false;
                }
            }
        }

        private double requiredEnemies;

        public double RequiredEnemies {
            get { return requiredEnemies; }
            set {
                requiredEnemies = value;
                lblEnemiesLeft.Text = $"Required Enemies: {requiredEnemies}";
                lblEnemiesLeft.Location = new Point(ClientSize.Width - lblEnemiesLeft.Width, lblEnemiesLeft.Height * 2);
                if (value <= 0) {
                    Wave++;
                }
            }
        }

        private int currentEnemies;

        public Main() {
            InitializeComponent();

            // Set window fullscreen
            WindowState = FormWindowState.Maximized;
            Show();
            
            // Get new window size
            Size newScreenSize = new Size(ClientSize.Width / 2, Convert.ToInt16(ClientSize.Height / 2 / 0.6));
            
            // Set new window size to maximum and minimum size
            MinimumSize = newScreenSize;
            MaximumSize = newScreenSize;

            // Hide minimize and maximize buttons
            MaximizeBox = false;
            MinimizeBox = false;
            WindowState = FormWindowState.Normal;

            CenterToScreen();
            Show();

            // Improve game performance
            DoubleBuffered = true;

            // Set static clientsize
            GameWindowSize = ClientSize;

            pnlStartScreen.Location = new Point(ClientSize.Width / 2 - pnlStartScreen.Width / 2, ClientSize.Height / 2 - pnlStartScreen.Height / 2);

            InitializeGame();

            tmrFixedDeltaTime.Start();
        }

        //---------------------
        // Initzialize Game
        //---------------------

        #region
        private void InitializeGame() {
            // Show start screen
            pnlStartScreen.Visible = true;
            pnlControls.Visible = true;

            // Add player
            player = new Player();
            player.GameOver += GameOver;
            Controls.Add(player);

            // Setup game values and objects
            Score = 0;
            currentEnemies = 0;
            RequiredEnemies = 0;
            tick = 0;
            Wave = 1;
            EnemyElement.enemies.Clear();

            player.Location = new Point(ClientSize.Width / 2 - player.Width / 2, pnlStartScreen.Location.Y + pnlStartScreen.Height + player.Height);
            Thread th = new Thread(() => Utils.PlaySound(Properties.Resources.restart));
            th.Start();
        }
        #endregion

        //---------------------
        // Timer
        //---------------------

        #region
        private int tick = 0;

        private void TmrPlayer_Tick(object sender, EventArgs e) {
            // If true, deaccelerate player otherwise move player in the current direction
            if (!player.IsMoving) {
                if (tmrPlayer.Interval <= 30) {
                    tmrPlayer.Interval += 1;
                } else {
                    return;
                }
            }
            player.MoveGameObject();
        }

        private void TmrFixedDeltaTime_Tick(object sender, EventArgs e) {
            if (GameIsStarted) {
                // if (tick % 4 == 0) {
                foreach (GameElement element in Controls.OfType<GameElement>()) {
                    if (!(element is Player)) {
                        element.MoveGameObject();

                        CheckCollisions(element);
                    }
                }
                //  }

                if (tick % 10 == 0) {
                    //Shoot Player Projectile
                    if (player.IsShooting) {
                        PlayerProjectile prj = player.ShootProjectile();
                        Controls.Add(prj);
                        Thread th = new Thread(() => PlaySound(Properties.Resources.playerShoot));
                        th.Start();
                    }

                    if (tick % 40 == 0) {
                        //Shoot Enemy Projectiles

                        foreach (EnemyElement enemy in Controls.OfType<EnemyElement>()) {
                            if (enemy is EnemySquare sq) {
                                EnemyProjectile prj = sq.ShootProjectile();
                                Controls.Add(prj);
                            } else if (enemy is EnemyCircle ci) {
                                EnemyProjectile[] prj = new EnemyProjectile[4];

                                prj[0] = ci.ShootProjectile(1, 1);
                                prj[1] = ci.ShootProjectile(1, -1);
                                prj[2] = ci.ShootProjectile(-1, -1);
                                prj[3] = ci.ShootProjectile(-1, 1);
                                for (int j = 0; j < prj.Length; j++) {
                                    Controls.Add(prj[j]);
                                }
                            }
                        }
                    }
                }

                // Spawn enemies
                if (tick % 40 == 0) {
                    if (GameIsStarted && currentEnemies != RequiredEnemies && RequiredEnemies > 1) {
                        EnemyElement enemy;
                        if (currentEnemies <= 2) {
                            enemy = new EnemySquare(player);
                        } else {
                            if (rnd.Next(0, 2) == 0) {
                                enemy = new EnemySquare(player);
                            } else {
                                enemy = new EnemyCircle();
                            }
                        }
                        enemy.EnemyDie += EnemyDie;
                        Controls.Add(enemy);
                        currentEnemies++;
                    }
                }
                tick++;
                if (tick >= 1000) { tick = 0; }
            }
        }
        #endregion

        //---------------------
        // Collisions Check
        //---------------------

        #region
        private void CheckCollisions(GameElement element) {
            if (element is PlayerProjectile) {
                foreach (EnemyElement enemyElement in Controls.OfType<EnemyElement>()) {
                    if (element.Bounds.IntersectsWith(enemyElement.Bounds)) {
                        if (enemyElement is EnemyProjectile) {
                            Controls.Remove(enemyElement);
                        } else {
                            enemyElement.Health -= 1;
                            Controls.Remove(element);
                            return;
                        }
                    }
                }
            } else if (element is EnemyProjectile) {
                if (element.Bounds.IntersectsWith(player.Bounds)) {
                    player.Health -= 1;
                    Controls.Remove(element);
                    return;
                }
            }

            if (element.CheckOutOfBounds()) {
                Controls.Remove(element);
            }
        }
        #endregion

        //---------------------
        // Events
        //---------------------

        #region
        private void EnemyDie(EnemyElement enemy) {
            if (enemy is EnemySquare) {
                Score += 10;
            } else {
                Score += 20;
            }

            RequiredEnemies -= 1;
            currentEnemies -= 1;

            EnemyElement.enemies.Remove(enemy);
            enemy.EnemyDie -= EnemyDie;
            Controls.Remove(enemy);
        }

        private void GameOver() {
            Thread th = new Thread(() => PlaySound(Properties.Resources.gameOver));
            th.Start();

            GameIsStarted = false;
            tmrPlayer.Stop();

            // Remove all game objects on the field
            List<GameElement> gameElementsToRemove = new List<GameElement>();
            gameElementsToRemove.AddRange(Controls.OfType<GameElement>());

            foreach (GameElement gameElement in gameElementsToRemove) {
                Controls.Remove(gameElement);
            }
            gameElementsToRemove.Clear();

            // Remove event from all enemies
            foreach (EnemyElement enemy in EnemyElement.enemies) {
                enemy.EnemyDie -= EnemyDie;
            }

            // Remove event from player
            player.GameOver -= GameOver;

            DialogResult result = MessageBox.Show($"Waves: {Wave}\nScore: {Score}\n\nDo you want to play again?", "Game Over", MessageBoxButtons.YesNo);

            if (DialogResult.Yes == result) {
                InitializeGame();
            } else {
                Application.Exit();
            }
        }

        public void Main_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Q) {
                Application.Exit();
            } else if (!GameIsStarted) {
                //Start Game
                GameIsStarted = true;
                tmrPlayer.Start();
            } else if (e.KeyCode == Keys.Space) {
                player.IsShooting = !player.IsShooting;
            } else if (!player.IsMoving) {
                tmrPlayer.Interval = 10;
                switch (e.KeyCode) {
                    case Keys.Up:
                        player.SetDirection(PlayerRotation.Up);
                        break;

                    case Keys.Down:
                        player.SetDirection(PlayerRotation.Down);
                        break;

                    case Keys.Left:
                        player.SetDirection(PlayerRotation.Left);
                        break;

                    case Keys.Right:
                        player.SetDirection(PlayerRotation.Right);
                        break;
                }
            }
        }

        // Stop the player when the key is up
        public void Main_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) {
                player.IsMoving = false;
            }
        }
        #endregion
    }
}