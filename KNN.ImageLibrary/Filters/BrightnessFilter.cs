using System;


namespace EPM.ImageLibrary.Filters
{
    public class BrightnessFilter : IFilter
    {
        private double _brightness;
        private byte[] _precalcTable;

        public BrightnessFilter(int changeInBrightness)
        {
            _brightness = 1 + ((double)changeInBrightness / 100);
        }

        public void Run(EPMImage image)
        {
            PrecalculateTable();
            ChangeBrightness(image);
        }

        private void PrecalculateTable()
        {
            _precalcTable = new byte[256];

            // Precalculate all changes
            for (int i = 0; i < 256; i++)
            {
                int val = (int)Math.Round(i * _brightness);
                if (val < 0)
                {
                    val = 0;
                }
                else if (val > 255)
                {
                    val = 255;
                }
                _precalcTable[i] = (byte)val;
            }
        }

        private void ChangeBrightness(EPMImage image)
        {

            byte[] b = image.ByteArray;

            for (int i = 0, l = b.Length; i < l; i += 4)
            {
                b[i] = _precalcTable[b[i]];          // b
                b[i + 1] = _precalcTable[b[i + 1]];  // g
                b[i + 2] = _precalcTable[b[i + 2]];  // r
            }

            image.ByteArray = b;
        }
    }
}
