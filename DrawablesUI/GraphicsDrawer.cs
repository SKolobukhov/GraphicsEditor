﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawablesUI
{
    public class GraphicsDrawer : IDrawer, IDisposable
    {
        private readonly Graphics graph;
        private float pointWidth;
        private Pen pen;

        public GraphicsDrawer(Graphics g)
        {
            graph = g;
            g.PageUnit = GraphicsUnit.Pixel;
            g.PageScale = 1;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SelectPen(Color.Black);
        }
        public void SelectPen(Color color, int width = 1)
        {
            pen?.Dispose();
            pen = new Pen(color, width);
            pointWidth = 2 * width / graph.PageScale;
        }

        public void DrawPoint(PointF point, Transformation trans)
        {
            using (var b = new SolidBrush(pen.Color))
            {
                if (!trans.transformationMatrix.IsIdentity)
                    graph.MultiplyTransform(trans.transformationMatrix);
                graph.FillEllipse(b, new RectangleF(
                    new PointF(point.X - pointWidth / 2, point.Y - pointWidth / 2),
                    new SizeF(pointWidth, pointWidth)
                    ));
                graph.ResetTransform();
            }
        }

        public void DrawLine(PointF start, PointF end, Transformation trans)
        {
            if (!trans.transformationMatrix.IsIdentity)
                graph.MultiplyTransform(trans.transformationMatrix);
            graph.DrawLine(pen, start, end);
            graph.ResetTransform();
        }

        public void DrawEllipseArc(PointF center, SizeF sizes, Transformation trans, float startAngle = 0, float endAngle = 360, float rotate = 0)
        {
            graph.TranslateTransform(center.X, center.Y);
            graph.RotateTransform(rotate);
            graph.TranslateTransform(-center.X, -center.Y);
            if (!trans.transformationMatrix.IsIdentity)
                graph.MultiplyTransform(trans.transformationMatrix);
            graph.DrawArc(pen, new RectangleF(
                new PointF(center.X - sizes.Width / 2, center.Y - sizes.Height / 2), sizes
                ), startAngle, endAngle);
            graph.ResetTransform();
        }

        public void Dispose()
        {
            pen.Dispose();
        }
    }
}