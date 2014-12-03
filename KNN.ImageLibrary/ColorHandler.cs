using System;
using System.Drawing;
using System.Globalization;

namespace EPM.ImageLibrary
{
    public class ColorHandler
    {

        private ColorHandler()
        {
        }

        /// <summary>
        /// Parse a web color type of string (for example "#FF0000") into a System.Drawing.Color object.
        /// </summary>
        /// <param name="colorString">Color in string format (i e "#FFFFFF")</param>
        /// <returns>Color</returns>
        public static Color StringToColor(string colorString)
        {
            Color color;

            // Remove # if any
            colorString = colorString.TrimStart('#');

            // Parse the color string
            int c = int.Parse(colorString, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture);

            if (colorString.Length == 3)
            {
                // Convert from RGB-form
                color = Color.FromArgb(255, (c & 0xf00) >> 8, (c & 0x0f0) >> 4, (c & 0x00f));
            }
            else
            {
                // Convert from RRGGBB-form
                color = Color.FromArgb(255, (c & 0xff0000) >> 16, (c & 0x00ff00) >> 8, (c & 0x0000ff));
            }

            return color;
        }

    }
}
