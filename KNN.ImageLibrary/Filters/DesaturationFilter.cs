using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.ImageLibrary.Filters
{
    public class DesaturationFilter : IFilter
    {

        public void Run(EPMImage image)
        {
            DesaturateImage(image);
        }

        private static void DesaturateImage(EPMImage image)
        {
            byte[] b = image.ByteArray;

            for (int i = 0, l = b.Length; i < l; i += 4)
            {
                b[i] = b[i + 1] = b[i + 2] = (byte)(.299 * b[i + 2] + .587 * b[i + 1] + .114 * b[i]);
            }

            image.ByteArray = b;
        }
    }
}
