using System.Drawing;

namespace ArenaChaos_Reworked.GameElements.Player {

    public class PlayerProjectile : PlayerElement {
        private readonly PlayerRotation direction;

        public PlayerProjectile(PlayerRotation direction, Point spawnLocation) :
            base((direction == PlayerRotation.Up || direction == PlayerRotation.Down) ? new Size(20, 50) : new Size(50, 20)) {
            BackColor = Color.White;
            Location = spawnLocation;
            this.direction = direction;

            Speed = 15;
        }

        protected override PlayerRotation GetDirection() {
            return direction;
        }
    }
}