using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.ImageLibrary.Filters
{
    public class UnsharpMaskFilter : IFilter
    {
        double _radius;
        double _amount;

        public UnsharpMaskFilter(double radius, double amount)
        {
            _radius = radius;
            _amount = amount;
        }

        public void Run(EPMImage image)
        {
            Sharpen(image, _amount, _radius);
        }

        private static void Sharpen(EPMImage image, double amount, double radius)
        {
            byte[] src = image.ByteArray;
            byte[] dest = new byte[src.Length];

            GaussianBlurFilter.GaussianBlur(image.Width, image.Height, radius, amount, ref src, ref dest);

            int i = 0;
            int r, g, b;

            for (int x = 0, l = image.Width; x < l; x++)
            {
                for (int y = 0, k = image.Height; y < k; y++)
                {
                    // Apply difference of gaussian blur filter
                    b = src[i] + (int)((src[i] - dest[i]) * amount);
                    g = src[i + 1] + (int)((src[i + 1] - dest[i + 1]) * amount);
                    r = src[i + 2] + (int)((src[i + 2] - dest[i + 2]) * amount);

                    // Keep inside range 0 to 255
                    if (r < 0)
                        r = 0;
                    else if (r > 255)
                        r = 255;
                    if (g < 0)
                        g = 0;
                    else if (g > 255)
                        g = 255;
                    if (b < 0)
                        b = 0;
                    else if (b > 255)
                        b = 255;

                    // Write back final bytes
                    dest[i] = (byte)b;
                    dest[i + 1] = (byte)g;
                    dest[i + 2] = (byte)r;

                    i += 4;
                }
            }

            image.ByteArray = dest;
        }
    }
}
