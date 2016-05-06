using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.MengasSponge
{
    public static class Spongeify
    {
        private static SpongeifySettings _settings = SpongeifySettings.DefaultSettings;

        public static void SpongeifyImage(Bitmap picture)
        {
            spongeify(picture, _settings.Color, 0, 0, picture.Width, picture.Height);
        }

        public static void SpongeifyImage(Bitmap picture, Color color)
        {
            spongeify(picture, color, 0, 0, picture.Width, picture.Height);
        }

        private static void spongeify(Bitmap picture, Color color, int xStart, int yStart, int xStop, int yStop)
        {
            var workAreaX = xStop - xStart;
            var workAreaY = yStop - yStart;

            if (workAreaX < 3 && workAreaY < 3)
            {
                return;
            }

            var xSize = workAreaX / 3;
            var ySize = workAreaY / 3;

            var workAreaXStart = xStart + xSize;
            var workAreaYStart = yStart + ySize;

            var workAreaXStop = workAreaXStart + xSize;
            var workAreaYStop = workAreaYStart + ySize;

            for (int i = workAreaXStart; i < workAreaXStop; i++)
            {
                for (int j = workAreaYStart; j < workAreaYStop; j++)
                {
                    picture.SetPixel(i, j, color);
                }
            }

            // Spongeify remaining 8 peices
            spongeify(picture, color, xStart, yStart, workAreaXStart, workAreaYStart);
            spongeify(picture, color, xStart, workAreaYStart, workAreaXStart, workAreaYStop);
            spongeify(picture, color, xStart, workAreaYStop, workAreaXStart, yStop);

            spongeify(picture, color, workAreaXStart, yStart, workAreaXStop, workAreaYStart);
            spongeify(picture, color, workAreaXStart, workAreaYStop, workAreaXStop, yStop);

            spongeify(picture, color, workAreaXStop, yStart, xStop, workAreaYStart);
            spongeify(picture, color, workAreaXStop, workAreaYStart, xStop, workAreaYStop);
            spongeify(picture, color, workAreaXStop, workAreaYStop, xStop, yStop);
        }
    }
}
