using AlarmClock.Model;
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

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        private const string ImageFolderName = "Images";

        private string[] _imageFileNames = new string[0];

        private int _imageIndex;

        internal AlarmClockState ClockState { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AlarmMessageLabel.Text = string.IsNullOrWhiteSpace(ClockState.AlarmMessage) ?
                $"{DateTime.Now:HH:mm} - пора вставать!" :
                ClockState.AlarmMessage;

            SnoozeButton.Text = $"Отложить на {ClockState.SnoozeMinutes} мин.";
            InitializeImages();
        }

        private void InitializeImages()
        {
            if (Directory.Exists(ImageFolderName))
            {
                _imageFileNames = Directory.EnumerateFiles(ImageFolderName, "*.jpg").ToArray();
            }

            _imageIndex = 0;
            ShowCurrentImage();
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _imageFileNames.Length)
            {
                _imageIndex = 0;
            }

            ShowCurrentImage();
        }

        private void ShowCurrentImage()
        {
            if (_imageFileNames.Length == 0)
            {
                return;
            }

            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }
    }
}
