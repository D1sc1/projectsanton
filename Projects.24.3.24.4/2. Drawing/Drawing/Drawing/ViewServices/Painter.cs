using Drawing.Models;
using Drawing.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.ViewServices
{
    internal class Painter
    {
        private Graphics _graphics;

        private Scaler _scaler;

        private readonly PictureBuilder _builder;

        public Painter(PictureBuilder builder)
        {
            _builder = builder;
        }

        public void Paint(Rectangle bounds, Graphics g)
        {
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            // Scaler меняет SourceBounds под пропорции окна, поэтому передаем копию,
            // иначе при каждом изменении размера окна границы рисунка "уплывают"
            _scaler = new Scaler
            {
                TargetBounds = bounds,
                SourceBounds = _builder.SourceBounds.Clone(),
            };

            _scaler.Initialize();

            _graphics = g;
            _graphics.SmoothingMode = SmoothingMode.AntiAlias;

            _builder.DrawPicture(this);

            _graphics = null;
        }

        public void DrawEllipse(Pen pen, Brush brush, RectangleModel rectangleModel)
        {
            var rectangle = _scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _graphics.FillEllipse(brush, rectangle);
            }

            if (pen != null)
            {
                _graphics.DrawEllipse(pen, rectangle);
            }
        }

        public void DrawRectangle(Pen pen, Brush brush, RectangleModel rectangleModel)
        {
            var rectangle = _scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _graphics.FillRectangle(brush, rectangle);
            }

            if (pen != null)
            {
                _graphics.DrawRectangle(pen, rectangle);
            }
        }

        public void DrawPie(Pen pen, Brush brush, RectangleModel rectangleModel, double startAngle, double sweepAngle)
        {
            var rectangle = _scaler.Scale(rectangleModel);
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
            {
                return;
            }

            if (brush != null)
            {
                _graphics.FillPie(brush, rectangle, (float)startAngle, (float)sweepAngle);
            }

            if (pen != null)
            {
                _graphics.DrawPie(pen, rectangle, (float)startAngle, (float)sweepAngle);
            }
        }

        public void DrawArc(Pen pen, RectangleModel rectangleModel, double startAngle, double sweepAngle)
        {
            var rectangle = _scaler.Scale(rectangleModel);
            if (rectangle.Width <= 0 || rectangle.Height <= 0)
            {
                return;
            }

            _graphics.DrawArc(pen, rectangle, (float)startAngle, (float)sweepAngle);
        }

        public void DrawLine(Pen pen, PointModel from, PointModel to)
        {
            _graphics.DrawLine(pen, _scaler.Scale(from), _scaler.Scale(to));
        }

        public void DrawPolygon(Pen pen, Brush brush, PointModel[] pointModels)
        {
            var points = pointModels.Select(pm => _scaler.Scale(pm)).ToArray();

            if (brush != null)
            {
                _graphics.FillPolygon(brush, points);
            }

            if (pen != null)
            {
                _graphics.DrawPolygon(pen, points);
            }
        }
    }
}
