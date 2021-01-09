using ArenaChaos_Reworked.GameElements.Enemies;
using System.Collections.Generic;
using System.Drawing;
using static ArenaChaos_Reworked.Utils;

namespace ArenaChaos_Reworked.GameElements {

    public delegate void EventTypeEnemyDie(EnemyElement enemy);

    public abstract class EnemyElement : GameElement {
        public static List<EnemyElement> enemies = new List<EnemyElement>();

        private int health;

        public int Health {
            get { return health; }
            set {
                health = value;

                if (health <= 0) {
                    EnemyDie?.Invoke(this);
                }
            }
        }

        protected EnemyElement(Size elementSize) : base(elementSize) {
            if (!(this is EnemyProjectile)) {
                // Place enemy random on the field
                Location = new Point(
                    rnd.Next(Width, GameWindowSize.Width - (Width * 2)),
                    rnd.Next(Height, GameWindowSize.Height - (Height * 2))
                    );

                enemies.Add(this);
            }
        }

        public override void MoveGameObject() {
            Point moveToPoint = GetMovePoint();

            Left += (Speed * moveToPoint.X);
            Top += (Speed * moveToPoint.Y);
        }

        protected abstract Point GetMovePoint();

        public virtual event EventTypeEnemyDie EnemyDie;
    }
}