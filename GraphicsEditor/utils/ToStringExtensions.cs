using System.Drawing;

namespace GraphicsEditor
{
    public static class ToStringExtensions
    {
        public static string ToStringPosition(this PointF point)
        {
            return $"Точка({point.X}, {point.Y})";
        }

        public static string ToStringSize(this SizeF size)
        {
            return $"Размеры({size.Width}, {size.Height})";
        }
    }
}