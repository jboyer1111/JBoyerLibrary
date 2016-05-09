using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.ImageHelpers
{
    public class ImageTransparencyHelper
    {
        public ImageTransparencyHelper()
        {
            
        }

        public static Image RemoveHalfTransparency(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    byte alpha = color.A;
                    int opacity = 0;
                    if (alpha >= 127)
                    {
                        opacity = 255;
                    }
                    var newColor = Color.FromArgb(opacity, color);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            return bitmap;
        }
    }
}
