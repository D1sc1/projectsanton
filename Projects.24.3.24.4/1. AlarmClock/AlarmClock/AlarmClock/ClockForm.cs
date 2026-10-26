using AlarmClock.Forms;
using AlarmClock.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private readonly AlarmClockState _clockState = new AlarmClockState();

        public ClockForm()
        {
            InitializeComponent();
            UpdateView();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            DisplayLabel.Text = now.ToLongTimeString();
            DateLabel.Text = now.ToString("dddd, d MMMM yyyy");

            if (!_clockState.IsAlarmActive)
            {
                RemainingLabel.Text = "Будильник выключен";
                return;
            }

            if (!_clockState.IsAwakeActivated)
            {
                RemainingLabel.Text = $"До срабатывания: {FormatRemaining(_clockState.AlarmTime - now)}";
            }

            if (!_clockState.IsAwakeActivated && now >= _clockState.AlarmTime)
            {
                _clockState.IsAwakeActivated = true;
                RemainingLabel.Text = "Подъем!";

                var awakeForm = new AwakeForm { ClockState = _clockState };
                var result = awakeForm.ShowDialog();

                if (result == DialogResult.Retry)
                {
                    _clockState.Snooze(DateTime.Now);
                }
                else
                {
                    _clockState.Dismiss(DateTime.Now);
                }

                UpdateView();
                return;
            }

            if (_clockState.IsSoundActive && _clockState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm { ClockState = _clockState };

            if (settingsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            if (_clockState.IsAlarmActive)
            {
                var snoozeText = _clockState.IsSnoozed ? " (отложен)" : string.Empty;
                Text = $"Будильник. Сработает {_clockState.AlarmTime:ddd dd.MM HH:mm}{snoozeText}, повтор: {_clockState.RepeatDaysText}";
            }
            else
            {
                Text = "Будильник";
            }
        }

        private static string FormatRemaining(TimeSpan remaining)
        {
            if (remaining < TimeSpan.Zero)
            {
                remaining = TimeSpan.Zero;
            }

            if (remaining.TotalDays >= 1)
            {
                return $"{(int)remaining.TotalDays} д. {remaining:hh\\:mm\\:ss}";
            }

            return remaining.ToString(@"hh\:mm\:ss");
        }
    }
}
