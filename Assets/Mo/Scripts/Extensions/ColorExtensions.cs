using UnityEngine;

namespace Mo.Extensions
{
    public static class ColorExtensions
    {
        public static Color CopyWithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}