using Drawing.Models;
using Drawing.ViewServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    /// <summary>
    /// Рисунок "Маяк на берегу ночного моря".
    /// Координаты модельные: ось Y направлена вверх, уровень моря - Y = 2
    /// </summary>
    internal class PictureBuilder
    {
        private const double TowerBottom = 4.5;

        private const double TowerTop = 13;

        private const double TowerBottomHalfWidth = 2;

        private const double TowerTopHalfWidth = 1.2;

        private static readonly Color SkyColor = Color.MidnightBlue;

        private static readonly PointModel LanternCenter = new PointModel { X = 0, Y = 14.2, };

        // Перья и кисти создаются один раз: рисунок перерисовывается по таймеру
        private readonly Pen _outlinePen = new Pen(Color.Black, 2);

        private readonly Pen _thinPen = new Pen(Color.Black, 1);

        private readonly Pen _boatOutlinePen = new Pen(Color.Black, 1.5f);

        private readonly Pen _mastPen = new Pen(Color.BurlyWood, 2);

        private readonly Pen _moonPathPen = new Pen(Color.FromArgb(160, Color.LightYellow), 2);

        private readonly Pen _wavePen = new Pen(Color.LightSteelBlue, 1.5f);

        private readonly Pen _railPen = new Pen(Color.Gainsboro, 1.5f);

        private readonly Pen _gullPen = new Pen(Color.WhiteSmoke, 2);

        private readonly Brush _skyBrush = new SolidBrush(SkyColor);

        private readonly Brush _seaBrush = new SolidBrush(Color.FromArgb(20, 40, 90));

        private readonly Brush _cloudBrush = new SolidBrush(Color.FromArgb(110, Color.LightSlateGray));

        private readonly Brush _beamBrush = new SolidBrush(Color.FromArgb(70, 255, 255, 150));

        private readonly Brush _haloBrush = new SolidBrush(Color.FromArgb(90, Color.Gold));

        private static readonly double[,] Stars =
        {
            { -9, 17 }, { -7.5, 16 }, { -5, 17.3 }, { -3, 15.8 }, { -1.5, 17.5 }, { 3, 16.5 }, { 5, 17.6 },
            { 7, 15.6 }, { 9, 17 }, { 16.5, 17.2 }, { 18.5, 15.8 }, { 17, 12.8 }, { -8.5, 11.5 }, { 4, 14.5 },
            { 10.5, 12.8 }, { 18, 9.5 }, { -9.3, 8 },
        };

        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -10,
            Y = -2,
            Width = 30,
            Height = 20,
        };

        /// <summary>
        /// Текущий угол поворота луча маяка, градусы
        /// </summary>
        public double BeamAngle { get; private set; }

        public void RotateBeam(double deltaAngle)
        {
            BeamAngle = (BeamAngle + deltaAngle) % 360;
        }

        public void DrawPicture(Painter painter)
        {
            DrawSky(painter);
            DrawSea(painter);
            DrawBoat(painter);
            DrawIsland(painter);
            DrawTower(painter);
            DrawBeams(painter);
            DrawLantern(painter);
            DrawGulls(painter);
        }

        private void DrawSky(Painter painter)
        {
            // Небо и море рисуются с запасом, чтобы при любых пропорциях окна не было пустых полей
            var sky = new RectangleModel { X = -100, Y = 2, Width = 230, Height = 100, };
            painter.DrawRectangle(null, _skyBrush, sky);

            for (int i = 0; i < Stars.GetLength(0); i++)
            {
                var radius = i % 3 == 0 ? 0.13 : 0.08;
                var star = RectangleModel.FromCenter(new PointModel { X = Stars[i, 0], Y = Stars[i, 1], }, radius);
                painter.DrawEllipse(null, Brushes.White, star);
            }

            // Месяц: желтый круг, частично закрытый кругом цвета неба
            painter.DrawEllipse(null, Brushes.LightYellow, RectangleModel.FromCenter(new PointModel { X = 13, Y = 15, }, 1.3));
            painter.DrawEllipse(null, _skyBrush, RectangleModel.FromCenter(new PointModel { X = 13.6, Y = 15.4, }, 1.2));

            painter.DrawEllipse(null, _cloudBrush, new RectangleModel { X = -7.5, Y = 12.8, Width = 3.2, Height = 1.2, });
            painter.DrawEllipse(null, _cloudBrush, new RectangleModel { X = -6.3, Y = 13.3, Width = 3, Height = 1.5, });
            painter.DrawEllipse(null, _cloudBrush, new RectangleModel { X = -4.8, Y = 12.9, Width = 2.8, Height = 1.1, });
        }

        private void DrawSea(Painter painter)
        {
            var sea = new RectangleModel { X = -100, Y = -100, Width = 230, Height = 102, };
            painter.DrawRectangle(null, _seaBrush, sea);

            // Лунная дорожка
            for (int i = 0; i < 6; i++)
            {
                var y = 1.6 - i * 0.6;
                var halfLength = 0.4 + i * 0.25;
                painter.DrawLine(_moonPathPen, new PointModel { X = 13 - halfLength, Y = y, }, new PointModel { X = 13 + halfLength, Y = y, });
            }

            // Волны - верхние половинки эллипсов
            for (double x = -49.5; x < 59.5; x += 1.6)
            {
                painter.DrawArc(_wavePen, new RectangleModel { X = x, Y = 1.1, Width = 1.2, Height = 0.6, }, 200, 140);
                painter.DrawArc(_wavePen, new RectangleModel { X = x + 0.8, Y = -0.6, Width = 1.2, Height = 0.6, }, 200, 140);
            }
        }

        private void DrawBoat(Painter painter)
        {
            var hull = new[]
            {
                new PointModel { X = 8.5, Y = 1.8, },
                new PointModel { X = 13.5, Y = 1.8, },
                new PointModel { X = 12.7, Y = 0.8, },
                new PointModel { X = 9.3, Y = 0.8, },
            };
            painter.DrawPolygon(_boatOutlinePen, Brushes.SaddleBrown, hull);

            painter.DrawLine(_mastPen, new PointModel { X = 11, Y = 1.8, }, new PointModel { X = 11, Y = 5.6, });

            var mainSail = new[]
            {
                new PointModel { X = 11.1, Y = 5.4, },
                new PointModel { X = 11.1, Y = 2.1, },
                new PointModel { X = 13.3, Y = 2.1, },
            };
            painter.DrawPolygon(_boatOutlinePen, Brushes.WhiteSmoke, mainSail);

            var foreSail = new[]
            {
                new PointModel { X = 10.9, Y = 4.8, },
                new PointModel { X = 10.9, Y = 2.1, },
                new PointModel { X = 9.3, Y = 2.1, },
            };
            painter.DrawPolygon(_boatOutlinePen, Brushes.Gainsboro, foreSail);
        }

        private void DrawIsland(Painter painter)
        {
            var rock = new[]
            {
                new PointModel { X = -8, Y = 2, },
                new PointModel { X = -6.5, Y = 3.2, },
                new PointModel { X = -4, Y = 4, },
                new PointModel { X = -2.5, Y = TowerBottom, },
                new PointModel { X = 2.5, Y = TowerBottom, },
                new PointModel { X = 4, Y = 3.6, },
                new PointModel { X = 5.5, Y = 2.6, },
                new PointModel { X = 6.5, Y = 2, },
            };
            painter.DrawPolygon(_outlinePen, Brushes.DimGray, rock);

            painter.DrawEllipse(null, Brushes.SlateGray, new RectangleModel { X = -6, Y = 2.4, Width = 1.6, Height = 0.8, });
            painter.DrawEllipse(null, Brushes.SlateGray, new RectangleModel { X = 3, Y = 2.5, Width = 1.3, Height = 0.7, });
            painter.DrawEllipse(null, Brushes.DarkOliveGreen, new RectangleModel { X = -4.2, Y = 3.6, Width = 1.8, Height = 0.6, });
        }

        private void DrawTower(Painter painter)
        {
            painter.DrawPolygon(_outlinePen, Brushes.White, TowerSlice(TowerBottom, TowerTop));

            // Красные полосы
            painter.DrawPolygon(null, Brushes.Firebrick, TowerSlice(6, 7.5));
            painter.DrawPolygon(null, Brushes.Firebrick, TowerSlice(9, 10.5));
            painter.DrawPolygon(_outlinePen, null, TowerSlice(TowerBottom, TowerTop));

            // Дверь и окна
            painter.DrawRectangle(_outlinePen, Brushes.SaddleBrown, new RectangleModel { X = -0.5, Y = TowerBottom, Width = 1, Height = 1.6, });
            painter.DrawRectangle(_thinPen, Brushes.Khaki, new RectangleModel { X = -0.25, Y = 8, Width = 0.5, Height = 0.8, });
            painter.DrawRectangle(_thinPen, Brushes.Khaki, new RectangleModel { X = -0.25, Y = 11.4, Width = 0.5, Height = 0.8, });

            // Галерея с перилами
            painter.DrawRectangle(_outlinePen, Brushes.DimGray, new RectangleModel { X = -1.9, Y = TowerTop, Width = 3.8, Height = 0.4, });

            for (double x = -1.8; x <= 1.81; x += 0.6)
            {
                painter.DrawLine(_railPen, new PointModel { X = x, Y = TowerTop + 0.4, }, new PointModel { X = x, Y = TowerTop + 1, });
            }

            painter.DrawLine(_railPen, new PointModel { X = -1.9, Y = TowerTop + 1, }, new PointModel { X = 1.9, Y = TowerTop + 1, });
        }

        private void DrawBeams(Painter painter)
        {
            var beamBounds = RectangleModel.FromCenter(LanternCenter, 16);

            painter.DrawPie(null, _beamBrush, beamBounds, BeamAngle - 10, 20);
            painter.DrawPie(null, _beamBrush, beamBounds, BeamAngle + 170, 20);
        }

        private void DrawLantern(Painter painter)
        {
            // Ореол вокруг лампы
            painter.DrawEllipse(null, _haloBrush, RectangleModel.FromCenter(LanternCenter, 1.8));

            painter.DrawRectangle(_outlinePen, Brushes.Gold, new RectangleModel { X = -1, Y = TowerTop + 0.4, Width = 2, Height = 1.6, });
            painter.DrawLine(_thinPen, new PointModel { X = -0.33, Y = TowerTop + 0.4, }, new PointModel { X = -0.33, Y = TowerTop + 2, });
            painter.DrawLine(_thinPen, new PointModel { X = 0.33, Y = TowerTop + 0.4, }, new PointModel { X = 0.33, Y = TowerTop + 2, });

            var roof = new[]
            {
                new PointModel { X = -1.4, Y = TowerTop + 2, },
                new PointModel { X = 1.4, Y = TowerTop + 2, },
                new PointModel { X = 0, Y = TowerTop + 3.6, },
            };
            painter.DrawPolygon(_outlinePen, Brushes.DarkRed, roof);
            painter.DrawEllipse(_outlinePen, Brushes.DarkRed, RectangleModel.FromCenter(new PointModel { X = 0, Y = TowerTop + 3.7, }, 0.2));
        }

        private void DrawGulls(Painter painter)
        {
            DrawGull(painter, _gullPen, 5, 11.5);
            DrawGull(painter, _gullPen, 7.2, 12.6);
            DrawGull(painter, _gullPen, -5.5, 9.5);
        }

        private void DrawGull(Painter painter, Pen pen, double x, double y)
        {
            painter.DrawArc(pen, new RectangleModel { X = x, Y = y, Width = 0.8, Height = 0.5, }, 180, 180);
            painter.DrawArc(pen, new RectangleModel { X = x + 0.8, Y = y, Width = 0.8, Height = 0.5, }, 180, 180);
        }

        /// <summary>
        /// Часть башни-трапеции между высотами bottom и top
        /// </summary>
        private PointModel[] TowerSlice(double bottom, double top)
        {
            var bottomHalfWidth = TowerHalfWidth(bottom);
            var topHalfWidth = TowerHalfWidth(top);

            return new[]
            {
                new PointModel { X = -bottomHalfWidth, Y = bottom, },
                new PointModel { X = bottomHalfWidth, Y = bottom, },
                new PointModel { X = topHalfWidth, Y = top, },
                new PointModel { X = -topHalfWidth, Y = top, },
            };
        }

        private double TowerHalfWidth(double y)
        {
            return TowerBottomHalfWidth - (TowerBottomHalfWidth - TowerTopHalfWidth) * (y - TowerBottom) / (TowerTop - TowerBottom);
        }
    }
}
