using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
            {
                return;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var rectangle in viewModel.Rectangles)
            {
                using (var pen = new Pen(rectangle.BorderColor, rectangle.BorderWidth))
                {
                    using (var brush = new SolidBrush(rectangle.FillColor))
                    {
                        DrawShape(g, pen, brush, rectangle.ShapeType, rectangle.Rectangle);
                    }
                }
            }

            if (isInteractive && viewModel.SelectedRectangle != null)
            {
                g.SmoothingMode = SmoothingMode.None;

                // Пунктирная рамка вокруг выделенной фигуры
                using (var framePen = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dash, })
                {
                    g.DrawRectangle(framePen, viewModel.SelectedRectangle.Rectangle);
                }

                foreach (var marker in viewModel.Markers)
                {
                    var brush = marker.IsActive ? Brushes.Black : Brushes.White;

                    g.FillRectangle(brush, marker.Rectangle);
                    g.DrawRectangle(Pens.Black, marker.Rectangle);
                }
            }
        }

        private void DrawShape(Graphics g, Pen pen, Brush brush, ShapeType shapeType, Rectangle rect)
        {
            switch (shapeType)
            {
                case ShapeType.Ellipse:
                    g.FillEllipse(brush, rect);
                    g.DrawEllipse(pen, rect);
                    break;

                case ShapeType.Triangle:
                    var triangle = new[]
                    {
                        new Point(rect.Left + rect.Width / 2, rect.Top),
                        new Point(rect.Right, rect.Bottom),
                        new Point(rect.Left, rect.Bottom),
                    };
                    g.FillPolygon(brush, triangle);
                    g.DrawPolygon(pen, triangle);
                    break;

                case ShapeType.Rhombus:
                    var rhombus = new[]
                    {
                        new Point(rect.Left + rect.Width / 2, rect.Top),
                        new Point(rect.Right, rect.Top + rect.Height / 2),
                        new Point(rect.Left + rect.Width / 2, rect.Bottom),
                        new Point(rect.Left, rect.Top + rect.Height / 2),
                    };
                    g.FillPolygon(brush, rhombus);
                    g.DrawPolygon(pen, rhombus);
                    break;

                default:
                    g.FillRectangle(brush, rect);
                    g.DrawRectangle(pen, rect);
                    break;
            }
        }
    }
}
