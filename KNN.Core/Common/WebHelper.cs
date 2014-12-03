using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace EPM.Core
{
    /// <summary>
    /// Summary description for WebHelper
    /// </summary>
    public static class WebHelper
    {
        public static IList<T> FindControlsByType<T>(Control root)
            where T : Control
        {
            List<T> controls = new List<T>();
            FindControlsByType<T>(root, controls);
            return controls;
        }

        private static void FindControlsByType<T>(Control root, IList<T> controls)
            where T : Control
        {
            foreach (Control control in root.Controls)
            {
                if (control is T)
                {
                    controls.Add(control as T);
                }
                if (control.Controls.Count > 0)
                {
                    FindControlsByType<T>(control, controls);
                }
            }
        }
        public static string StripTagsCharArray(string source)
        {
            source = source.Replace("&nbsp;", "");
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }


    }
}