using AlarmClock.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        private CheckBox[] _dayCheckBoxes;

        internal AlarmClockState ClockState { get; set; }

        public SettingsForm()
        {
            InitializeComponent();

            // Порядок совпадает с AlarmClockState.DayByIndex: Пн..Вс
            _dayCheckBoxes = new[]
            {
                MondayCheckBox,
                TuesdayCheckBox,
                WednesdayCheckBox,
                ThursdayCheckBox,
                FridayCheckBox,
                SaturdayCheckBox,
                SundayCheckBox,
            };
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (ClockState.IsAlarmActive || ClockState.AlarmTimeOfDay != TimeSpan.Zero)
            {
                AlarmTimeTextBox.Text = ClockState.AlarmTimeOfDay.ToString(@"hh\:mm");
            }
            else
            {
                AlarmTimeTextBox.Text = DateTime.Now.AddHours(1).ToShortTimeString();
            }

            AlarmMessageTextBox.Text = ClockState.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = ClockState.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = ClockState.IsSoundActive;
            SnoozeMinutesNumericUpDown.Value = ClockState.SnoozeMinutes;

            for (int i = 0; i < _dayCheckBoxes.Length; i++)
            {
                _dayCheckBoxes[i].Checked = ClockState.RepeatDays.Contains(AlarmClockState.DayByIndex(i));
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // TimeSpan.TryParse("7") дает 7 суток, поэтому дополнительно проверяем диапазон
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime) ||
                alarmTime < TimeSpan.Zero || alarmTime >= TimeSpan.FromDays(1))
            {
                MessageBox.Show("Время задано неверно. Используйте формат ЧЧ:ММ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClockState.AlarmTimeOfDay = alarmTime;
            ClockState.AlarmMessage = AlarmMessageTextBox.Text;
            ClockState.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            ClockState.IsSoundActive = IsSoundActiveCheckBox.Checked;
            ClockState.SnoozeMinutes = (int)SnoozeMinutesNumericUpDown.Value;

            ClockState.RepeatDays.Clear();
            for (int i = 0; i < _dayCheckBoxes.Length; i++)
            {
                if (_dayCheckBoxes[i].Checked)
                {
                    ClockState.RepeatDays.Add(AlarmClockState.DayByIndex(i));
                }
            }

            ClockState.Schedule(DateTime.Now);

            DialogResult = DialogResult.OK;
        }

        private void WorkDaysButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _dayCheckBoxes.Length; i++)
            {
                _dayCheckBoxes[i].Checked = i < 5;
            }
        }

        private void ClearDaysButton_Click(object sender, EventArgs e)
        {
            foreach (var checkBox in _dayCheckBoxes)
            {
                checkBox.Checked = false;
            }
        }
    }
}
