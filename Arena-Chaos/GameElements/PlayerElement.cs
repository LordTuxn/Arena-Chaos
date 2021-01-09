using System.Drawing;

namespace ArenaChaos_Reworked.GameElements {

    public enum PlayerRotation {
        Left, Right, Up, Down
    }

    public abstract class PlayerElement : GameElement {

        public PlayerElement(Size elementSize) : base(elementSize) {
        }

        public override void MoveGameObject() {
            // Move player or player projectile in the right direction
            switch (GetDirection()) {
                case PlayerRotation.Up:
                    Top -= Speed;
                    break;

                case PlayerRotation.Down:
                    Top += Speed;
                    break;

                case PlayerRotation.Left:
                    Left -= Speed;
                    break;

                case PlayerRotation.Right:
                    Left += Speed;
                    break;
            }

            CheckCollision();
        }

        protected abstract PlayerRotation GetDirection();

        protected virtual void CheckCollision() {
        }
    }
}