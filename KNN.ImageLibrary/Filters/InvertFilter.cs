using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.ImageLibrary.Filters
{
    public class InvertFilter : IFilter
    {
        public void Run(EPMImage image)
        {
            InvertImage(image);
        }

        private static void InvertImage(EPMImage image)
        {
            byte[] b = image.ByteArray;

            for (int i = 0, l = b.Length; i < l; i += 4)
            {
                b[i] = (byte)(255 - b[i]);          // b
                b[i + 1] = (byte)(255 - b[i + 1]);  // g
                b[i + 2] = (byte)(255 - b[i + 2]);  // r
            }

            image.ByteArray = b;
        }
    }
}
