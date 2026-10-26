using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Models
{
    internal class RectangleModel
    {
        public PointModel Location { get; set; } = new PointModel();

        public SizeModel Size { get; set; } = new SizeModel();

        public double X
        {
            get => Location.X;
            set => Location.X = value;
        }

        public double Y
        {
            get => Location.Y;
            set => Location.Y = value;
        }

        public double Width
        {
            get => Size.Width;
            set => Size.Width = value;
        }

        public double Height
        {
            get => Size.Height;
            set => Size.Height = value;
        }

        public double Left => X;

        public double Right => X + Width;

        public double Bottom => Y;

        public double Top => Y + Height;

        public PointModel Center => new PointModel { X = X + Width / 2, Y = Y + Height / 2, };

        public RectangleModel Clone()
        {
            return new RectangleModel { X = X, Y = Y, Width = Width, Height = Height, };
        }

        /// <summary>
        /// Квадрат, описанный вокруг окружности с центром в точке center
        /// </summary>
        public static RectangleModel FromCenter(PointModel center, double radius)
        {
            return new RectangleModel
            {
                X = center.X - radius,
                Y = center.Y - radius,
                Width = radius * 2,
                Height = radius * 2,
            };
        }
    }
}
