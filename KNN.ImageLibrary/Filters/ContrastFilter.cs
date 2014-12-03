using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.ImageLibrary.Filters
{
    public class ContrastFilter : IFilter
    {
        private double _contrast;

        public ContrastFilter(int changeInContrast)
        {
            _contrast = 1 + ((double)changeInContrast / 100);
        }

        public void Run(EPMImage image)
        {
            ChangeContrast(image);
        }

        private void ChangeContrast(EPMImage image)
        {
            byte[] precalc = new byte[256];

            // Precalculate all changes
            for (int i = 0; i < 256; i++)
            {
                double val = i / 255.0;
                val -= 0.5;
                val *= _contrast;
                val += 0.5;
                val = (int)Math.Round(val * 255);
                if (val < 0)
                {
                    val = 0;
                }
                else if (val > 255)
                {
                    val = 255;
                }
                precalc[i] = (byte)val;
            }


            byte[] b = image.ByteArray;

            for (int i = 0, l = b.Length; i < l; i += 4)
            {
                b[i] = precalc[b[i]];          // b
                b[i + 1] = precalc[b[i + 1]];  // g
                b[i + 2] = precalc[b[i + 2]];  // r
            }

            image.ByteArray = b;

        }
    }
}
