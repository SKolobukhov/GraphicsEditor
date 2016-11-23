using System.Drawing;

namespace DrawablesUI
{
    public interface IDrawer
    {
        void SelectPen(Color color, int width=1);
        void DrawPoint(PointF point, Transformation trans);
        void DrawLine(PointF start, PointF end, Transformation trans);
        void DrawEllipseArc(PointF center, SizeF size, Transformation trans,
            float startAngle=0, float endAngle=360, float rotate=0);
    }
}