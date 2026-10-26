using Drawing.Services;
using Drawing.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        private const double BeamStep = 3;

        private readonly PictureBuilder _builder = new PictureBuilder();

        private readonly Painter _painter;

        public DrawingForm()
        {
            InitializeComponent();
            _painter = new Painter(_builder);
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            _painter.Paint(ClientRectangle, e.Graphics);
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void BeamTimer_Tick(object sender, EventArgs e)
        {
            _builder.RotateBeam(BeamStep);
            Invalidate();
        }

        private void DrawingForm_MouseClick(object sender, MouseEventArgs e)
        {
            // Щелчок мышью останавливает/запускает вращение луча
            BeamTimer.Enabled = !BeamTimer.Enabled;
        }
    }
}
