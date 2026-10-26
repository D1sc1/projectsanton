using GraphEditor.Business.Models;
using GraphEditor.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();
            UpdateViewControls();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;
            _viewService.MouseDown(e.Location);
            UpdateViewControls();
            Refresh();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = false;
            _viewService.MouseUp();
            UpdateViewControls();
            Refresh();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _viewService.GetCursor(e.Location);

            if (!_isMouseDown)
            {
                return;
            }

            _viewService.MouseMove(e.Location);
            Refresh();
        }

        private void GraphEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteSelected();
                    break;

                case Keys.D when e.Control:
                    DuplicateSelected();
                    break;

                case Keys.Escape:
                    _viewService.CreateMode = false;
                    UpdateViewControls();
                    break;

                default:
                    return;
            }

            e.Handled = true;
        }

        private void CreateRectangleToolStripButton_Click(object sender, EventArgs e)
        {
            CreateShape(ShapeType.Rectangle);
        }

        private void CreateEllipseToolStripButton_Click(object sender, EventArgs e)
        {
            CreateShape(ShapeType.Ellipse);
        }

        private void CreateTriangleToolStripButton_Click(object sender, EventArgs e)
        {
            CreateShape(ShapeType.Triangle);
        }

        private void CreateRhombusToolStripButton_Click(object sender, EventArgs e)
        {
            CreateShape(ShapeType.Rhombus);
        }

        private void CreateShape(ShapeType shapeType)
        {
            _viewService.CreateButtonClicked(shapeType);
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            CreateRectangleToolStripButton.Checked = _viewService.IsCreating(ShapeType.Rectangle);
            CreateEllipseToolStripButton.Checked = _viewService.IsCreating(ShapeType.Ellipse);
            CreateTriangleToolStripButton.Checked = _viewService.IsCreating(ShapeType.Triangle);
            CreateRhombusToolStripButton.Checked = _viewService.IsCreating(ShapeType.Rhombus);

            DeleteRectangleToolStripButton.Enabled = _viewService.CanDelete;
            DuplicateToolStripButton.Enabled = _viewService.CanDelete;
            toolStripSplitButton1.Enabled = _viewService.CanEdit;
            BorderWidthDropDownButton.Enabled = _viewService.CanEdit;
            toolStripDropDownButton1.Enabled = _viewService.CanEdit;

            Text = string.IsNullOrEmpty(_viewService.FileName) ?
               "Графический редактор" :
               $"Графический редактор | {Path.GetFileName(_viewService.FileName)}";
        }

        private void DeleteRectangleToolStripButton_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DuplicateToolStripButton_Click(object sender, EventArgs e)
        {
            DuplicateSelected();
        }

        private void DeleteSelected()
        {
            if (!_viewService.CanDelete)
            {
                return;
            }

            _viewService.DeleteButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        private void DuplicateSelected()
        {
            if (!_viewService.CanDelete)
            {
                return;
            }

            _viewService.DuplicateButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _viewService.GetCurrentFillColor();
            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.SetFillColor(FillColorDialog.Color);
            Refresh();
        }

        private void BorderColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _viewService.GetCurrentBorderColor();
            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.SetBorderColor(FillColorDialog.Color);
            Refresh();
        }

        private void BorderWidthDropDownButton_DropDownOpening(object sender, EventArgs e)
        {
            var currentWidth = _viewService.GetCurrentBorderWidth().ToString();
            foreach (ToolStripMenuItem item in BorderWidthDropDownButton.DropDownItems)
            {
                item.Checked = (string)item.Tag == currentWidth;
            }
        }

        private void BorderWidthMenuItem_Click(object sender, EventArgs e)
        {
            var width = int.Parse((string)((ToolStripMenuItem)sender).Tag);
            _viewService.SetBorderWidth(width);
            Refresh();
        }

        private void BringToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.BringToFront();
            Refresh();
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveBackward();
            Refresh();
        }

        private void SendToBackMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SendToBack();
            Refresh();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.CreateNewPicture();
            UpdateViewControls();
            Refresh();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                _viewService.Open(FileOpenDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateViewControls();
            Refresh();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_viewService.FileName))
            {
                DoSaveAs();
            }
            else
            {
                _viewService.Save();
            }
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void DoSaveAs()
        {
            if (FileSaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Save(FileSaveDialog.FileName);
            UpdateViewControls();
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
