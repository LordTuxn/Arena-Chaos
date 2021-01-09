using ArenaChaos_Reworked.GameElements;
using System;
using System.Drawing;
using System.IO;
using System.Media;

namespace ArenaChaos_Reworked {

    public static class Utils {
        public static Random rnd = new Random();

        public static Size GameWindowSize { get; set; }

        public static Bitmap RotateImage(Image img, PlayerRotation direction) {
            //create a new empty bitmap to hold the rotated image
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics gfx = Graphics.FromImage(bmp);

            //Put the rotation point in the center of the image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //get the right direction and rotate the image
            switch (direction) {
                case PlayerRotation.Up:
                    gfx.RotateTransform(0f);
                    break;

                case PlayerRotation.Down:
                    gfx.RotateTransform(180f);
                    break;

                case PlayerRotation.Left:
                    gfx.RotateTransform(270f);
                    break;

                case PlayerRotation.Right:
                    gfx.RotateTransform(90f);
                    break;
            }

            //move the image back
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //draw passed in image onto graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose graphics object
            gfx.Dispose();

            //return rotated image
            return bmp;
        }

        public static void PlaySound(Stream stream) {
            SoundPlayer s = new SoundPlayer(stream);
            s.Play();
        }
    }
}