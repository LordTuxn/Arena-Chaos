using System.Drawing;

namespace ArenaChaos_Reworked.GameElements.Enemies {

    public class EnemyCircle : EnemyElement {

        public EnemyCircle() : base(new Size(50, 50)) {
            BackgroundImage = Properties.Resources.enemyCircle_low;

            Health = 10;
        }

        protected override Point GetMovePoint() {
            return new Point(0, 0); // Return 0,0 point locations to don't move the enemy
        }

        public EnemyProjectile ShootProjectile(int moveX, int moveY) {
            return new EnemyProjectile(new Point(Location.X + (Width / 2), Location.Y + (Height / 2)), moveX, moveY);
        }
    }
}