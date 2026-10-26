using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    /// <summary>
    /// Фигура, заданная описанным прямоугольником
    /// </summary>
    public class RectangleModel
    {
        public const int DefaultBorderWidth = 3;

        public int Dx { get; set; }

        public int Dy { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Right => Left + Width;

        public int Bottom => Top + Height;

        public EditMode EditMode { get; set; }

        public ShapeType ShapeType { get; set; } = ShapeType.Rectangle;

        public Color FillColor { get; set; } = Color.Yellow;

        public Color BorderColor { get; set; } = Color.Blue;

        public int BorderWidth { get; set; } = DefaultBorderWidth;

        /// <summary>
        /// Попадание точки в фигуру с учетом ее формы
        /// </summary>
        public bool IsInside(PointModel loc)
        {
            var left = Math.Min(Left, Right);
            var right = Math.Max(Left, Right);
            var top = Math.Min(Top, Bottom);
            var bottom = Math.Max(Top, Bottom);

            if (loc.X < left || loc.X > right || loc.Y < top || loc.Y > bottom)
            {
                return false;
            }

            var width = right - left;
            var height = bottom - top;
            if (width == 0 || height == 0)
            {
                return true;
            }

            // Координаты относительно центра, нормированные к полуосям: от -1 до 1
            var nx = (loc.X - (left + right) / 2.0) / (width / 2.0);
            var ny = (loc.Y - (top + bottom) / 2.0) / (height / 2.0);

            switch (ShapeType)
            {
                case ShapeType.Ellipse:
                    return nx * nx + ny * ny <= 1;

                case ShapeType.Rhombus:
                    return Math.Abs(nx) + Math.Abs(ny) <= 1;

                case ShapeType.Triangle:
                    // Вершина сверху посередине, основание снизу: |nx| <= (ny + 1) / 2
                    return Math.Abs(nx) <= (ny + 1) / 2;

                default:
                    return true;
            }
        }

        public void Normalize()
        {
            if (Width < 0)
            {
                Left += Width;
                Width = -Width;
            }

            if (Height < 0)
            {
                Top += Height;
                Height = -Height;
            }
        }

        public RectangleModel Clone()
        {
            return new RectangleModel
            {
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height,
                ShapeType = ShapeType,
                FillColor = FillColor,
                BorderColor = BorderColor,
                BorderWidth = BorderWidth,
            };
        }
    }
}
