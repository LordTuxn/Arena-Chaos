using System.Drawing;

namespace ArenaChaos_Reworked.GameElements.Enemies {

    public class EnemyProjectile : EnemyElement {
        private readonly Point movePoint;

        public EnemyProjectile(Point spawnLocation, int moveX, int moveY) : base(new Size(30, 30)) {
            BackgroundImage = Properties.Resources.enemyProjectile_low;
            Location = new Point(spawnLocation.X - Width / 2, spawnLocation.Y - Height / 2);
            Speed = 5;

            movePoint = new Point(moveX, moveY);
        }

        protected override Point GetMovePoint() {
            return movePoint;
        }
    }
}