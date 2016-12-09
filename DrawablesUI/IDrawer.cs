using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawablesUI
{
    public interface IDrawer
    {
        Matrix Transform { get; set; }

        void SelectPen(Color color, int width=1);
        
        void DrawPoint(PointF point);
        void DrawLine(PointF start, PointF end);
        void DrawEllipseArc(PointF center, SizeF size, 
            float startAngle=0, float endAngle=360, float rotate=0);

        void StartDraw();
        void EndDraw();
    }
}