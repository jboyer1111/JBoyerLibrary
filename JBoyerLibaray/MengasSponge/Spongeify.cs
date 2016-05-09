using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
            spongeify(picture, _settings.HoleColor, 0, 0, picture.Width, picture.Height);
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

        public static Image CreateImage(int level)
        {
            return CreateImage(level, _settings.BaseColor);
        }

        public static Image CreateImage(int level, Color baseColor)
        {
            return CreateImage(level, baseColor, _settings.HoleColor);
        }

        public static Image CreateImage(int level, Color baseColor, Color holeColor)
        {
            Image resultImage = null;
            Image innerImage = null;
            Image innerImageHole = null;
            if (level < 1)
            {
                return null;
            }
            else if (level == 1)
            {
                var innerBitmap = new Bitmap(1, 1);
                innerBitmap.SetPixel(0, 0, baseColor);
                innerImage = innerBitmap;

                var innerBitmapWhole = new Bitmap(1, 1);
                innerBitmapWhole.SetPixel(0, 0, holeColor);
                innerImageHole = innerBitmapWhole;
            }
            else
            {
                innerImage = CreateImage(level - 1, baseColor, holeColor);
                innerImageHole = createWhole(innerImage.Size, holeColor);
            }

            resultImage = new Bitmap(innerImage.Width * 3, innerImage.Height * 3);

            Graphics g = Graphics.FromImage(resultImage);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.DrawImage(innerImage, new Point(0, 0));
            g.DrawImage(innerImage, new Point(0, innerImage.Width));
            g.DrawImage(innerImage, new Point(0, innerImage.Width * 2));

            g.DrawImage(innerImage, new Point(innerImage.Height, 0));
            g.DrawImage(innerImageHole, new Point(innerImage.Height, innerImage.Width));
            g.DrawImage(innerImage, new Point(innerImage.Height, innerImage.Width * 2));

            g.DrawImage(innerImage, new Point(innerImage.Height * 2, 0));
            g.DrawImage(innerImage, new Point(innerImage.Height * 2, innerImage.Width));
            g.DrawImage(innerImage, new Point(innerImage.Height * 2, innerImage.Width * 2));

            return resultImage;
        }

        private static Image createWhole(Size size, Color color)
        {
            var image = new Bitmap(size.Width, size.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }
            return image;
        }
    }
}
