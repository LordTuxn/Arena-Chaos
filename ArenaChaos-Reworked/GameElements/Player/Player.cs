using System.Drawing;
using static ArenaChaos_Reworked.Utils;

namespace ArenaChaos_Reworked.GameElements.Player {

    public delegate void EventTypeGameOver();

    public class Player : PlayerElement {
        private PlayerRotation direction = PlayerRotation.Up;

        private Image playerImage;

        private int health;

        public int Health {
            get { return health; }
            set {
                health = value;

                switch (value) {
                    case 0:
                        GameOver?.Invoke();
                        break;

                    case 1:
                        playerImage = Properties.Resources.player_one_hp;
                        break;

                    case 2:
                        playerImage = Properties.Resources.player_two_hp;
                        break;

                    case 3:
                        playerImage = Properties.Resources.player_full_hp;
                        break;
                }

                BackgroundImage = RotateImage(playerImage, direction);
            }
        }

        public bool IsMoving { get; set; }

        public bool IsShooting { get; set; }

        public Player() : base(new Size(50, 50)) {
            Health = 3;
            Speed = 8;
        }

        protected override void CheckCollision() {
            // Check border collision
            if (Left < 0) {
                Left = 0;
            } else if (Left > GameWindowSize.Width - Width) {
                Left = GameWindowSize.Width - Width;
            } else if (Top < 0) {
                Top = 0;
            } else if (Top > GameWindowSize.Height - Height) {
                Top = GameWindowSize.Height - Height;
            }
        }

        public void SetDirection(PlayerRotation rotation) {
            direction = rotation;
            BackgroundImage = RotateImage(playerImage, direction);
            IsMoving = true;
        }

        public PlayerProjectile ShootProjectile() {
            return new PlayerProjectile(direction, direction == PlayerRotation.Up || direction == PlayerRotation.Down ?
                new Point(Location.X + 15, Location.Y) :
                new Point(Location.X, Location.Y + 15));
        }

        protected override PlayerRotation GetDirection() {
            return direction;
        }

        public event EventTypeGameOver GameOver;
    }
}