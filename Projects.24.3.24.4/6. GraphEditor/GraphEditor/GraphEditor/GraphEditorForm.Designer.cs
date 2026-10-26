namespace GraphEditor
{
    partial class GraphEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEditorForm));
            this.GraphEditorToolStrip = new System.Windows.Forms.ToolStrip();
            this.CreateRectangleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CreateEllipseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CreateTriangleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CreateRhombusToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteRectangleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DuplicateToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.FillColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidthDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.BorderWidth1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth5MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth8MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.BringToFrontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveForwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveBackwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendToBackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FillColorDialog = new System.Windows.Forms.ColorDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileCreateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileExportDialog = new System.Windows.Forms.SaveFileDialog();
            this.GraphEditorToolStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GraphEditorToolStrip
            // 
            this.GraphEditorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateRectangleToolStripButton,
            this.CreateEllipseToolStripButton,
            this.CreateTriangleToolStripButton,
            this.CreateRhombusToolStripButton,
            this.toolStripSeparator2,
            this.DeleteRectangleToolStripButton,
            this.DuplicateToolStripButton,
            this.toolStripSeparator1,
            this.toolStripSplitButton1,
            this.BorderWidthDropDownButton,
            this.toolStripDropDownButton1});
            this.GraphEditorToolStrip.Location = new System.Drawing.Point(0, 24);
            this.GraphEditorToolStrip.Name = "GraphEditorToolStrip";
            this.GraphEditorToolStrip.Size = new System.Drawing.Size(900, 25);
            this.GraphEditorToolStrip.TabIndex = 0;
            this.GraphEditorToolStrip.Text = "toolStrip1";
            // 
            // CreateRectangleToolStripButton
            // 
            this.CreateRectangleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateRectangleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CreateRectangleToolStripButton.Image")));
            this.CreateRectangleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateRectangleToolStripButton.Name = "CreateRectangleToolStripButton";
            this.CreateRectangleToolStripButton.Size = new System.Drawing.Size(95, 22);
            this.CreateRectangleToolStripButton.Text = "Прямоугольник";
            this.CreateRectangleToolStripButton.Click += new System.EventHandler(this.CreateRectangleToolStripButton_Click);
            // 
            // CreateEllipseToolStripButton
            // 
            this.CreateEllipseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateEllipseToolStripButton.Name = "CreateEllipseToolStripButton";
            this.CreateEllipseToolStripButton.Size = new System.Drawing.Size(50, 22);
            this.CreateEllipseToolStripButton.Text = "Эллипс";
            this.CreateEllipseToolStripButton.Click += new System.EventHandler(this.CreateEllipseToolStripButton_Click);
            // 
            // CreateTriangleToolStripButton
            // 
            this.CreateTriangleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateTriangleToolStripButton.Name = "CreateTriangleToolStripButton";
            this.CreateTriangleToolStripButton.Size = new System.Drawing.Size(78, 22);
            this.CreateTriangleToolStripButton.Text = "Треугольник";
            this.CreateTriangleToolStripButton.Click += new System.EventHandler(this.CreateTriangleToolStripButton_Click);
            // 
            // CreateRhombusToolStripButton
            // 
            this.CreateRhombusToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateRhombusToolStripButton.Name = "CreateRhombusToolStripButton";
            this.CreateRhombusToolStripButton.Size = new System.Drawing.Size(38, 22);
            this.CreateRhombusToolStripButton.Text = "Ромб";
            this.CreateRhombusToolStripButton.Click += new System.EventHandler(this.CreateRhombusToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // DeleteRectangleToolStripButton
            // 
            this.DeleteRectangleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRectangleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRectangleToolStripButton.Image")));
            this.DeleteRectangleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRectangleToolStripButton.Name = "DeleteRectangleToolStripButton";
            this.DeleteRectangleToolStripButton.Size = new System.Drawing.Size(55, 22);
            this.DeleteRectangleToolStripButton.Text = "Удалить";
            this.DeleteRectangleToolStripButton.Click += new System.EventHandler(this.DeleteRectangleToolStripButton_Click);
            // 
            // DuplicateToolStripButton
            // 
            this.DuplicateToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DuplicateToolStripButton.Name = "DuplicateToolStripButton";
            this.DuplicateToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.DuplicateToolStripButton.Text = "Дублировать";
            this.DuplicateToolStripButton.Click += new System.EventHandler(this.DuplicateToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FillColorMenuItem,
            this.BorderColorMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripSplitButton1.Text = "Цвет";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.FillColorMenuItem_Click);
            // 
            // FillColorMenuItem
            // 
            this.FillColorMenuItem.Name = "FillColorMenuItem";
            this.FillColorMenuItem.Size = new System.Drawing.Size(140, 22);
            this.FillColorMenuItem.Text = "Заливка...";
            this.FillColorMenuItem.Click += new System.EventHandler(this.FillColorMenuItem_Click);
            // 
            // BorderColorMenuItem
            // 
            this.BorderColorMenuItem.Name = "BorderColorMenuItem";
            this.BorderColorMenuItem.Size = new System.Drawing.Size(140, 22);
            this.BorderColorMenuItem.Text = "Контур...";
            this.BorderColorMenuItem.Click += new System.EventHandler(this.BorderColorMenuItem_Click);
            // 
            // BorderWidthDropDownButton
            // 
            this.BorderWidthDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BorderWidthDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BorderWidth1MenuItem,
            this.BorderWidth2MenuItem,
            this.BorderWidth3MenuItem,
            this.BorderWidth5MenuItem,
            this.BorderWidth8MenuItem});
            this.BorderWidthDropDownButton.Name = "BorderWidthDropDownButton";
            this.BorderWidthDropDownButton.Size = new System.Drawing.Size(66, 22);
            this.BorderWidthDropDownButton.Text = "Толщина";
            this.BorderWidthDropDownButton.DropDownOpening += new System.EventHandler(this.BorderWidthDropDownButton_DropDownOpening);
            // 
            // BorderWidth1MenuItem
            // 
            this.BorderWidth1MenuItem.Name = "BorderWidth1MenuItem";
            this.BorderWidth1MenuItem.Size = new System.Drawing.Size(120, 22);
            this.BorderWidth1MenuItem.Tag = "1";
            this.BorderWidth1MenuItem.Text = "1 пикс.";
            this.BorderWidth1MenuItem.Click += new System.EventHandler(this.BorderWidthMenuItem_Click);
            // 
            // BorderWidth2MenuItem
            // 
            this.BorderWidth2MenuItem.Name = "BorderWidth2MenuItem";
            this.BorderWidth2MenuItem.Size = new System.Drawing.Size(120, 22);
            this.BorderWidth2MenuItem.Tag = "2";
            this.BorderWidth2MenuItem.Text = "2 пикс.";
            this.BorderWidth2MenuItem.Click += new System.EventHandler(this.BorderWidthMenuItem_Click);
            // 
            // BorderWidth3MenuItem
            // 
            this.BorderWidth3MenuItem.Name = "BorderWidth3MenuItem";
            this.BorderWidth3MenuItem.Size = new System.Drawing.Size(120, 22);
            this.BorderWidth3MenuItem.Tag = "3";
            this.BorderWidth3MenuItem.Text = "3 пикс.";
            this.BorderWidth3MenuItem.Click += new System.EventHandler(this.BorderWidthMenuItem_Click);
            // 
            // BorderWidth5MenuItem
            // 
            this.BorderWidth5MenuItem.Name = "BorderWidth5MenuItem";
            this.BorderWidth5MenuItem.Size = new System.Drawing.Size(120, 22);
            this.BorderWidth5MenuItem.Tag = "5";
            this.BorderWidth5MenuItem.Text = "5 пикс.";
            this.BorderWidth5MenuItem.Click += new System.EventHandler(this.BorderWidthMenuItem_Click);
            // 
            // BorderWidth8MenuItem
            // 
            this.BorderWidth8MenuItem.Name = "BorderWidth8MenuItem";
            this.BorderWidth8MenuItem.Size = new System.Drawing.Size(120, 22);
            this.BorderWidth8MenuItem.Tag = "8";
            this.BorderWidth8MenuItem.Text = "8 пикс.";
            this.BorderWidth8MenuItem.Click += new System.EventHandler(this.BorderWidthMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BringToFrontMenuItem,
            this.MoveForwardMenuItem,
            this.MoveBackwardMenuItem,
            this.SendToBackMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "Порядок";
            // 
            // BringToFrontMenuItem
            // 
            this.BringToFrontMenuItem.Name = "BringToFrontMenuItem";
            this.BringToFrontMenuItem.Size = new System.Drawing.Size(175, 22);
            this.BringToFrontMenuItem.Text = "На передний план";
            this.BringToFrontMenuItem.Click += new System.EventHandler(this.BringToFrontMenuItem_Click);
            // 
            // MoveForwardMenuItem
            // 
            this.MoveForwardMenuItem.Name = "MoveForwardMenuItem";
            this.MoveForwardMenuItem.Size = new System.Drawing.Size(175, 22);
            this.MoveForwardMenuItem.Text = "Ближе";
            this.MoveForwardMenuItem.Click += new System.EventHandler(this.MoveForwardMenuItem_Click);
            // 
            // MoveBackwardMenuItem
            // 
            this.MoveBackwardMenuItem.Name = "MoveBackwardMenuItem";
            this.MoveBackwardMenuItem.Size = new System.Drawing.Size(175, 22);
            this.MoveBackwardMenuItem.Text = "Дальше";
            this.MoveBackwardMenuItem.Click += new System.EventHandler(this.MoveBackwardMenuItem_Click);
            // 
            // SendToBackMenuItem
            // 
            this.SendToBackMenuItem.Name = "SendToBackMenuItem";
            this.SendToBackMenuItem.Size = new System.Drawing.Size(175, 22);
            this.SendToBackMenuItem.Text = "На задний план";
            this.SendToBackMenuItem.Click += new System.EventHandler(this.SendToBackMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileCreateMenuItem,
            this.FileOpenMenuItem,
            this.FileSaveMenuItem,
            this.FileSaveAsMenuItem,
            this.toolStripMenuItem1,
            this.FileExportMenuItem,
            this.toolStripMenuItem2,
            this.FileExitMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // FileCreateMenuItem
            // 
            this.FileCreateMenuItem.Name = "FileCreateMenuItem";
            this.FileCreateMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileCreateMenuItem.Size = new System.Drawing.Size(200, 22);
            this.FileCreateMenuItem.Text = "Создать";
            this.FileCreateMenuItem.Click += new System.EventHandler(this.FileCreateMenuItem_Click);
            // 
            // FileOpenMenuItem
            // 
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpenMenuItem.Size = new System.Drawing.Size(200, 22);
            this.FileOpenMenuItem.Text = "Открыть...";
            this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSaveMenuItem.Size = new System.Drawing.Size(200, 22);
            this.FileSaveMenuItem.Text = "Сохранить";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAsMenuItem
            // 
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.Size = new System.Drawing.Size(200, 22);
            this.FileSaveAsMenuItem.Text = "Сохранить как...";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(197, 6);
            // 
            // FileExportMenuItem
            // 
            this.FileExportMenuItem.Name = "FileExportMenuItem";
            this.FileExportMenuItem.Size = new System.Drawing.Size(200, 22);
            this.FileExportMenuItem.Text = "Экспорт...";
            this.FileExportMenuItem.Click += new System.EventHandler(this.FileExportMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(197, 6);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(200, 22);
            this.FileExitMenuItem.Text = "Выход";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // FileSaveDialog
            // 
            this.FileSaveDialog.Filter = "Проекты рисунков|*.piczip";
            // 
            // FileOpenDialog
            // 
            this.FileOpenDialog.Filter = "Проекты рисунков|*.piczip";
            // 
            // FileExportDialog
            // 
            this.FileExportDialog.Filter = "Изображения PNG|*.png";
            // 
            // GraphEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.GraphEditorToolStrip);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GraphEditorForm";
            this.Text = "Графический редактор";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphEditorForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphEditorForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseUp);
            this.GraphEditorToolStrip.ResumeLayout(false);
            this.GraphEditorToolStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip GraphEditorToolStrip;
        private System.Windows.Forms.ToolStripButton CreateRectangleToolStripButton;
        private System.Windows.Forms.ToolStripButton CreateEllipseToolStripButton;
        private System.Windows.Forms.ToolStripButton CreateTriangleToolStripButton;
        private System.Windows.Forms.ToolStripButton CreateRhombusToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton DeleteRectangleToolStripButton;
        private System.Windows.Forms.ToolStripButton DuplicateToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem FillColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderColorMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton BorderWidthDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth2MenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth3MenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth5MenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth8MenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem BringToFrontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveBackwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SendToBackMenuItem;
        private System.Windows.Forms.ColorDialog FillColorDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileCreateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem FileExportMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.SaveFileDialog FileSaveDialog;
        private System.Windows.Forms.OpenFileDialog FileOpenDialog;
        private System.Windows.Forms.SaveFileDialog FileExportDialog;
    }
}
