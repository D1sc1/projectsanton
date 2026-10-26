using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Model
{
    internal class AlarmClockState
    {
        private static readonly DayOfWeek[] WeekOrder =
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday,
        };

        private static readonly string[] ShortDayNames = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };

        /// <summary>
        /// Ближайшее время срабатывания (дата и время)
        /// </summary>
        public DateTime AlarmTime { get; set; }

        /// <summary>
        /// Время суток, на которое заведен будильник
        /// </summary>
        public TimeSpan AlarmTimeOfDay { get; set; }

        /// <summary>
        /// Сообщение при срабатывании
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// Включен режим ожидания срабатывания
        /// </summary>
        public bool IsAlarmActive { get; set; }

        /// <summary>
        /// Срабатывание сопровождается звуковым сигналом
        /// </summary>
        public bool IsSoundActive { get; set; }

        /// <summary>
        /// Срабатывание будильника зафиксировано
        /// </summary>
        public bool IsAwakeActivated { get; set; }

        /// <summary>
        /// Дни недели, по которым будильник повторяется.
        /// Пустой набор означает однократный будильник
        /// </summary>
        public ISet<DayOfWeek> RepeatDays { get; } = new HashSet<DayOfWeek>();

        /// <summary>
        /// На сколько минут откладывается сигнал кнопкой "Отложить"
        /// </summary>
        public int SnoozeMinutes { get; set; } = 5;

        /// <summary>
        /// Сигнал был отложен
        /// </summary>
        public bool IsSnoozed { get; set; }

        /// <summary>
        /// Будильник повторяется по дням недели
        /// </summary>
        public bool IsRepeating => RepeatDays.Count > 0;

        /// <summary>
        /// Дни повтора в виде строки "Пн Ср Пт"
        /// </summary>
        public string RepeatDaysText =>
            IsRepeating
                ? string.Join(" ", WeekOrder.Where(d => RepeatDays.Contains(d)).Select(d => ShortDayNames[Array.IndexOf(WeekOrder, d)]))
                : "однократно";

        /// <summary>
        /// Вычисляет ближайший момент срабатывания после заданного момента времени
        /// с учетом дней повтора
        /// </summary>
        public DateTime CalculateNextAlarmTime(DateTime now)
        {
            for (int dayOffset = 0; dayOffset <= 7; dayOffset++)
            {
                var candidate = now.Date.AddDays(dayOffset) + AlarmTimeOfDay;
                if (candidate <= now)
                {
                    continue;
                }

                if (!IsRepeating || RepeatDays.Contains(candidate.DayOfWeek))
                {
                    return candidate;
                }
            }

            return now.Date.AddDays(1) + AlarmTimeOfDay;
        }

        /// <summary>
        /// Назначает ближайшее срабатывание
        /// </summary>
        public void Schedule(DateTime now)
        {
            AlarmTime = CalculateNextAlarmTime(now);
            IsSnoozed = false;
            IsAwakeActivated = false;
        }

        /// <summary>
        /// Откладывает сигнал на SnoozeMinutes минут
        /// </summary>
        public void Snooze(DateTime now)
        {
            AlarmTime = now.AddMinutes(SnoozeMinutes);
            IsSnoozed = true;
            IsAwakeActivated = false;
        }

        /// <summary>
        /// Сигнал выключен пользователем ("Я проснулся").
        /// Повторяющийся будильник переводится на следующий день повтора,
        /// однократный - выключается
        /// </summary>
        public void Dismiss(DateTime now)
        {
            if (IsRepeating)
            {
                Schedule(now);
                return;
            }

            IsAlarmActive = false;
            IsAwakeActivated = false;
            IsSnoozed = false;
        }

        /// <summary>
        /// Индекс дня недели в порядке Пн..Вс
        /// </summary>
        public static DayOfWeek DayByIndex(int index) => WeekOrder[index];
    }
}
