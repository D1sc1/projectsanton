using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        /// <summary>
        /// Смещение копии фигуры относительно оригинала при дублировании
        /// </summary>
        public const int DuplicateOffset = 20;

        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public static Color DefaultFillColor { get; set; } = Color.Yellow;

        public static Color DefaultBorderColor { get; set; } = Color.Blue;

        public PictureService()
        {
            PictureModel.Rectangles.Add(new RectangleModel { Left = 150, Top = 80, Width = 200, Height = 100, FillColor = Color.LightSkyBlue, });
            PictureModel.Rectangles.Add(new RectangleModel { Left = 300, Top = 140, Width = 180, Height = 150, ShapeType = ShapeType.Ellipse, FillColor = Color.LightGreen, BorderColor = Color.DarkGreen, });
            var newRectangle = new RectangleModel { Left = 420, Top = 90, Width = 160, Height = 140, ShapeType = ShapeType.Triangle, FillColor = Color.Orange, BorderColor = Color.SaddleBrown, };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.Rectangles.Add(new RectangleModel { Left = 180, Top = 220, Width = 120, Height = 160, ShapeType = ShapeType.Rhombus, FillColor = Color.Plum, BorderColor = Color.Purple, BorderWidth = 5, });
            PictureModel.SelectedRectangle = newRectangle;
        }

        public void CreateRectangleAndSetCreateMode(PointModel loc, ShapeType shapeType)
        {
            var newRectangle = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                ShapeType = shapeType,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
            };

            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            newRectangle.EditMode = EditMode.Creating;
        }

        public void FindAndSetMoveMode(PointModel loc)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedRectangle = selectedRect;

            if (selectedRect == null)
            {
                return;
            }

            selectedRect.EditMode = EditMode.Moving;
            selectedRect.Dx = loc.X - selectedRect.Left;
            selectedRect.Dy = loc.Y - selectedRect.Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void ResetMode()
        {
            var selectedRect = PictureModel.SelectedRectangle;

            if (selectedRect == null)
            {
                return;
            }

            selectedRect.Normalize();
            selectedRect.EditMode = EditMode.None;
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null)
            {
                return;
            }

            switch (selectedRect.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    MoveRightEdge(selectedRect, loc.X);
                    MoveBottomEdge(selectedRect, loc.Y);
                    break;

                case EditMode.ResizeR:
                    MoveRightEdge(selectedRect, loc.X);
                    break;

                case EditMode.ResizeB:
                    MoveBottomEdge(selectedRect, loc.Y);
                    break;

                case EditMode.ResizeL:
                    MoveLeftEdge(selectedRect, loc.X);
                    break;

                case EditMode.ResizeT:
                    MoveTopEdge(selectedRect, loc.Y);
                    break;

                case EditMode.ResizeTL:
                    MoveLeftEdge(selectedRect, loc.X);
                    MoveTopEdge(selectedRect, loc.Y);
                    break;

                case EditMode.ResizeTR:
                    MoveRightEdge(selectedRect, loc.X);
                    MoveTopEdge(selectedRect, loc.Y);
                    break;

                case EditMode.ResizeBL:
                    MoveLeftEdge(selectedRect, loc.X);
                    MoveBottomEdge(selectedRect, loc.Y);
                    break;

                case EditMode.Moving:
                    selectedRect.Left = loc.X - selectedRect.Dx;
                    selectedRect.Top = loc.Y - selectedRect.Dy;
                    break;
            }
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        /// <summary>
        /// Копия выделенной фигуры со смещением; копия становится выделенной
        /// </summary>
        public void DuplicateRectangle()
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null)
            {
                return;
            }

            var copy = selectedRect.Clone();
            copy.Left += DuplicateOffset;
            copy.Top += DuplicateOffset;

            PictureModel.Rectangles.Add(copy);
            PictureModel.SelectedRectangle = copy;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.FillColor = color;
            }
        }

        public void SetBorderColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.BorderColor = color;
            }
        }

        public void SetBorderWidth(int width)
        {
            if (PictureModel.SelectedRectangle != null && width > 0)
            {
                PictureModel.SelectedRectangle.BorderWidth = width;
            }
        }

        public void MoveForward()
        {
            var index = GetSelectedIndex();
            if (index < 0 || index == PictureModel.Rectangles.Count - 1)
            {
                return;
            }

            Swap(index, index + 1);
        }

        public void MoveBackward()
        {
            var index = GetSelectedIndex();
            if (index <= 0)
            {
                return;
            }

            Swap(index, index - 1);
        }

        public void BringToFront()
        {
            var index = GetSelectedIndex();
            if (index < 0)
            {
                return;
            }

            var rect = PictureModel.Rectangles[index];
            PictureModel.Rectangles.RemoveAt(index);
            PictureModel.Rectangles.Add(rect);
        }

        public void SendToBack()
        {
            var index = GetSelectedIndex();
            if (index < 0)
            {
                return;
            }

            var rect = PictureModel.Rectangles[index];
            PictureModel.Rectangles.RemoveAt(index);
            PictureModel.Rectangles.Insert(0, rect);
        }

        public void Save(string fileName)
        {
            new FileService().Save(fileName, PictureModel);
        }

        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);
        }

        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
        }

        private int GetSelectedIndex()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return -1;
            }

            return PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
        }

        private void Swap(int index1, int index2)
        {
            var rect = PictureModel.Rectangles[index1];
            PictureModel.Rectangles[index1] = PictureModel.Rectangles[index2];
            PictureModel.Rectangles[index2] = rect;
        }

        private static void MoveLeftEdge(RectangleModel rect, int x)
        {
            var right = rect.Right;
            rect.Left = x;
            rect.Width = right - x;
        }

        private static void MoveRightEdge(RectangleModel rect, int x)
        {
            rect.Width = x - rect.Left;
        }

        private static void MoveTopEdge(RectangleModel rect, int y)
        {
            var bottom = rect.Bottom;
            rect.Top = y;
            rect.Height = bottom - y;
        }

        private static void MoveBottomEdge(RectangleModel rect, int y)
        {
            rect.Height = y - rect.Top;
        }
    }
}
