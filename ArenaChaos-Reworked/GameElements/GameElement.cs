using System.Drawing;
using System.Windows.Forms;
using static ArenaChaos_Reworked.Utils;

namespace ArenaChaos_Reworked.GameElements {

    public abstract class GameElement : Panel {
        protected int Speed { get; set; }

        protected GameElement(Size elementSize) {
            Size = elementSize;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        public abstract void MoveGameObject();

        public bool CheckOutOfBounds() {
            if (Left >= GameWindowSize.Width || Left <= -Width || Top >= GameWindowSize.Height || Top <= -Height) {
                return true;
            }
            return false;
        }
    }
}