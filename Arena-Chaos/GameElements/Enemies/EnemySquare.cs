using System;
using System.Drawing;
using static ArenaChaos_Reworked.Utils;

namespace ArenaChaos_Reworked.GameElements.Enemies {

    public class EnemySquare : EnemyElement {
        private Player.Player player;
        private Point moveToPoint;

        public EnemySquare(Player.Player player) : base(new Size(50, 50)) {
            BackgroundImage = Properties.Resources.enemySquare_low;
            Speed = 1;
            Health = 5;

            this.player = player;
        }

        protected override Point GetMovePoint() {
            if (Left < player.Location.X) {
                moveToPoint.X = 1;
            } else {
                moveToPoint.X = -1;
            }

            // Set move point on the y-axis

            if (Top < player.Location.Y) {
                moveToPoint.Y = 1;
            } else {
                moveToPoint.Y = -1;
            }

            //Check if enemy collides with another enemy and prevent it
            foreach (EnemyElement enemy in enemies) {
                Rectangle collisionCheck = new Rectangle(new Point(moveToPoint.X - Width / 2, moveToPoint.Y - Height / 2), new Size(Width, Height));

                if (enemy != this && enemy.Bounds.IntersectsWith(collisionCheck)) {
                    // Change enemy moving point to a random position to prevent collision with another enemy
                    moveToPoint = new Point(rnd.Next(0, 2) * 2 - 1,
                                         rnd.Next(0, 2) * 2 - 1); // Get a random - or + speed
                }
            }

            return moveToPoint;
        }

        public EnemyProjectile ShootProjectile() {
            int moveX, moveY;

            if (Math.Abs(player.Location.X - Location.X) < 5) { //The player is on the same X axis, meaning he is either above or below
                moveX = 0;
            } else if (player.Location.X > Location.X) { //Player is on the right
                moveX = 1;
            } else { //Player is on the left
                moveX = -1;
            }

            if (player.Location.Y > Location.Y) {
                moveY = 1;
            } else if (Math.Abs(player.Location.Y - Location.Y) < 5) {
                moveY = 0;
            } else {
                moveY = -1;
            }

            return new EnemyProjectile(new Point(Location.X + (Width / 2), Location.Y + (Height / 2)), moveX, moveY);
        }
    }
}