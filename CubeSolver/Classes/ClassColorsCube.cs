using Microsoft.Maui.Controls.Shapes;
using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassColorsCube
    {
        /// <summary>
        /// Reset the colors of the cube
        /// </summary>
        public static void ResetCube()
        {
            int nItem;

            for (nItem = 0; nItem < 9; nItem++)
            {
                aPieces[nItem] = aFaceColors[1];
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                aPieces[nItem] = aFaceColors[2];
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                aPieces[nItem] = aFaceColors[3];
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                aPieces[nItem] = aFaceColors[4];
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                aPieces[nItem] = aFaceColors[5];
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                aPieces[nItem] = aFaceColors[6];
            }
        }

        /// <summary>
        /// Get the hexadecimal color code from the polygon fill property
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static string GetHexColorPolygon(Polygon polygon)
        {
            SolidColorBrush brush = (SolidColorBrush)polygon.Fill;
            Color color = brush.Color;

            color = Color.FromRgb(color.Red, color.Green, color.Blue);
            return color.ToHex();
        }

        ///// <summary>
        ///// Get the decimal color code from the polygon fill property
        ///// </summary>
        ///// <param name="polygon"></param>
        ///// <returns></returns>
        //public static int GetDecColorPolygon(Polygon polygon)
        //{
        //    SolidColorBrush brush = (SolidColorBrush)polygon.Fill;
        //    Color color = brush.Color;

        //    color = Color.FromRgb(color.Red, color.Green, color.Blue);
        //    return int.Parse(color.ToHex().Replace("#", ""), NumberStyles.HexNumber);
        //}

        /// <summary>
        /// Lightens a hex color by a specified amount
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <remarks> Example:
        /// var lighter = LightenHex("#FF4F4F", 0.2); // returns "#FF7A7A"</remarks>
        public static string LightenHex(string hex, double amount = 0.2)
        {
            if (string.IsNullOrWhiteSpace(hex))
            {
                Debug.WriteLine("LightenHex error: Hex color string is null or empty.");
                return hex;
            }

            hex = hex.TrimStart('#');   // Remove the '#' if present

            if (hex.Length == 3)        // short form e.g. "f4f"
            {
                hex = string.Concat(hex.Select(c => new string(c, 2)));
            }

            if (hex.Length != 6)
            {
                Debug.WriteLine($"LightenHex: Expected 6-digit hex. {nameof(hex)}");
                return hex;
            }

            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);

            byte Lerp(byte c) => (byte)Math.Round(c + (255 - c) * amount);

            return $"#{Lerp(r):X2}{Lerp(g):X2}{Lerp(b):X2}";
        }

        ///// <summary>
        ///// Lightens a Microsoft.Maui.Graphics.Color by a specified amount
        ///// </summary>
        ///// <param name="c"></param>
        ///// <param name="amount"></param>
        ///// <returns></returns>
        ///// <remarks> Example:
        ///// var orig = Microsoft.Maui.Graphics.Color.FromArgb("#FF4F4F");
        ///// var lighterColor = LightenColor(orig, 0.2);</remarks>
        //public static Microsoft.Maui.Graphics.Color LightenColor(Microsoft.Maui.Graphics.Color c, double amount = 0.2)
        //{
        //    double Lerp(double v) => v + (1.0 - v) * amount;
        //    return Microsoft.Maui.Graphics.Color.FromRgb(Lerp(c.Red), Lerp(c.Green), Lerp(c.Blue));
        //}
    }
}
