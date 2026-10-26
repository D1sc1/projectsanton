namespace AlarmClock.Forms
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AlarmTimeTextBox = new System.Windows.Forms.TextBox();
            this.AlarmMessageTextBox = new System.Windows.Forms.TextBox();
            this.IsAlarmActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.IsSoundActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.RepeatGroupBox = new System.Windows.Forms.GroupBox();
            this.MondayCheckBox = new System.Windows.Forms.CheckBox();
            this.TuesdayCheckBox = new System.Windows.Forms.CheckBox();
            this.WednesdayCheckBox = new System.Windows.Forms.CheckBox();
            this.ThursdayCheckBox = new System.Windows.Forms.CheckBox();
            this.FridayCheckBox = new System.Windows.Forms.CheckBox();
            this.SaturdayCheckBox = new System.Windows.Forms.CheckBox();
            this.SundayCheckBox = new System.Windows.Forms.CheckBox();
            this.WorkDaysButton = new System.Windows.Forms.Button();
            this.ClearDaysButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SnoozeMinutesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.RepeatGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SnoozeMinutesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время срабатывания:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Сообщение:";
            //
            // AlarmTimeTextBox
            //
            this.AlarmTimeTextBox.Location = new System.Drawing.Point(137, 12);
            this.AlarmTimeTextBox.Name = "AlarmTimeTextBox";
            this.AlarmTimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.AlarmTimeTextBox.TabIndex = 1;
            //
            // AlarmMessageTextBox
            //
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(137, 38);
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(177, 20);
            this.AlarmMessageTextBox.TabIndex = 3;
            //
            // RepeatGroupBox
            //
            this.RepeatGroupBox.Controls.Add(this.MondayCheckBox);
            this.RepeatGroupBox.Controls.Add(this.TuesdayCheckBox);
            this.RepeatGroupBox.Controls.Add(this.WednesdayCheckBox);
            this.RepeatGroupBox.Controls.Add(this.ThursdayCheckBox);
            this.RepeatGroupBox.Controls.Add(this.FridayCheckBox);
            this.RepeatGroupBox.Controls.Add(this.SaturdayCheckBox);
            this.RepeatGroupBox.Controls.Add(this.SundayCheckBox);
            this.RepeatGroupBox.Controls.Add(this.WorkDaysButton);
            this.RepeatGroupBox.Controls.Add(this.ClearDaysButton);
            this.RepeatGroupBox.Location = new System.Drawing.Point(12, 66);
            this.RepeatGroupBox.Name = "RepeatGroupBox";
            this.RepeatGroupBox.Size = new System.Drawing.Size(302, 78);
            this.RepeatGroupBox.TabIndex = 4;
            this.RepeatGroupBox.TabStop = false;
            this.RepeatGroupBox.Text = "Повторять по дням недели";
            //
            // MondayCheckBox
            //
            this.MondayCheckBox.AutoSize = true;
            this.MondayCheckBox.Location = new System.Drawing.Point(9, 20);
            this.MondayCheckBox.Name = "MondayCheckBox";
            this.MondayCheckBox.Size = new System.Drawing.Size(39, 17);
            this.MondayCheckBox.TabIndex = 0;
            this.MondayCheckBox.Text = "Пн";
            this.MondayCheckBox.UseVisualStyleBackColor = true;
            //
            // TuesdayCheckBox
            //
            this.TuesdayCheckBox.AutoSize = true;
            this.TuesdayCheckBox.Location = new System.Drawing.Point(50, 20);
            this.TuesdayCheckBox.Name = "TuesdayCheckBox";
            this.TuesdayCheckBox.Size = new System.Drawing.Size(39, 17);
            this.TuesdayCheckBox.TabIndex = 1;
            this.TuesdayCheckBox.Text = "Вт";
            this.TuesdayCheckBox.UseVisualStyleBackColor = true;
            //
            // WednesdayCheckBox
            //
            this.WednesdayCheckBox.AutoSize = true;
            this.WednesdayCheckBox.Location = new System.Drawing.Point(91, 20);
            this.WednesdayCheckBox.Name = "WednesdayCheckBox";
            this.WednesdayCheckBox.Size = new System.Drawing.Size(39, 17);
            this.WednesdayCheckBox.TabIndex = 2;
            this.WednesdayCheckBox.Text = "Ср";
            this.WednesdayCheckBox.UseVisualStyleBackColor = true;
            //
            // ThursdayCheckBox
            //
            this.ThursdayCheckBox.AutoSize = true;
            this.ThursdayCheckBox.Location = new System.Drawing.Point(132, 20);
            this.ThursdayCheckBox.Name = "ThursdayCheckBox";
            this.ThursdayCheckBox.Size = new System.Drawing.Size(39, 17);
            this.ThursdayCheckBox.TabIndex = 3;
            this.ThursdayCheckBox.Text = "Чт";
            this.ThursdayCheckBox.UseVisualStyleBackColor = true;
            //
            // FridayCheckBox
            //
            this.FridayCheckBox.AutoSize = true;
            this.FridayCheckBox.Location = new System.Drawing.Point(173, 20);
            this.FridayCheckBox.Name = "FridayCheckBox";
            this.FridayCheckBox.Size = new System.Drawing.Size(39, 17);
            this.FridayCheckBox.TabIndex = 4;
            this.FridayCheckBox.Text = "Пт";
            this.FridayCheckBox.UseVisualStyleBackColor = true;
            //
            // SaturdayCheckBox
            //
            this.SaturdayCheckBox.AutoSize = true;
            this.SaturdayCheckBox.Location = new System.Drawing.Point(214, 20);
            this.SaturdayCheckBox.Name = "SaturdayCheckBox";
            this.SaturdayCheckBox.Size = new System.Drawing.Size(39, 17);
            this.SaturdayCheckBox.TabIndex = 5;
            this.SaturdayCheckBox.Text = "Сб";
            this.SaturdayCheckBox.UseVisualStyleBackColor = true;
            //
            // SundayCheckBox
            //
            this.SundayCheckBox.AutoSize = true;
            this.SundayCheckBox.Location = new System.Drawing.Point(255, 20);
            this.SundayCheckBox.Name = "SundayCheckBox";
            this.SundayCheckBox.Size = new System.Drawing.Size(39, 17);
            this.SundayCheckBox.TabIndex = 6;
            this.SundayCheckBox.Text = "Вс";
            this.SundayCheckBox.UseVisualStyleBackColor = true;
            //
            // WorkDaysButton
            //
            this.WorkDaysButton.Location = new System.Drawing.Point(9, 45);
            this.WorkDaysButton.Name = "WorkDaysButton";
            this.WorkDaysButton.Size = new System.Drawing.Size(100, 23);
            this.WorkDaysButton.TabIndex = 7;
            this.WorkDaysButton.Text = "Будни";
            this.WorkDaysButton.UseVisualStyleBackColor = true;
            this.WorkDaysButton.Click += new System.EventHandler(this.WorkDaysButton_Click);
            //
            // ClearDaysButton
            //
            this.ClearDaysButton.Location = new System.Drawing.Point(115, 45);
            this.ClearDaysButton.Name = "ClearDaysButton";
            this.ClearDaysButton.Size = new System.Drawing.Size(100, 23);
            this.ClearDaysButton.TabIndex = 8;
            this.ClearDaysButton.Text = "Однократно";
            this.ClearDaysButton.UseVisualStyleBackColor = true;
            this.ClearDaysButton.Click += new System.EventHandler(this.ClearDaysButton_Click);
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отложить на, мин.:";
            //
            // SnoozeMinutesNumericUpDown
            //
            this.SnoozeMinutesNumericUpDown.Location = new System.Drawing.Point(137, 153);
            this.SnoozeMinutesNumericUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.SnoozeMinutesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SnoozeMinutesNumericUpDown.Name = "SnoozeMinutesNumericUpDown";
            this.SnoozeMinutesNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.SnoozeMinutesNumericUpDown.TabIndex = 6;
            this.SnoozeMinutesNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            //
            // IsAlarmActiveCheckBox
            //
            this.IsAlarmActiveCheckBox.AutoSize = true;
            this.IsAlarmActiveCheckBox.Location = new System.Drawing.Point(15, 182);
            this.IsAlarmActiveCheckBox.Name = "IsAlarmActiveCheckBox";
            this.IsAlarmActiveCheckBox.Size = new System.Drawing.Size(126, 17);
            this.IsAlarmActiveCheckBox.TabIndex = 7;
            this.IsAlarmActiveCheckBox.Text = "Будильник включен";
            this.IsAlarmActiveCheckBox.UseVisualStyleBackColor = true;
            //
            // IsSoundActiveCheckBox
            //
            this.IsSoundActiveCheckBox.AutoSize = true;
            this.IsSoundActiveCheckBox.Location = new System.Drawing.Point(15, 205);
            this.IsSoundActiveCheckBox.Name = "IsSoundActiveCheckBox";
            this.IsSoundActiveCheckBox.Size = new System.Drawing.Size(158, 17);
            this.IsSoundActiveCheckBox.TabIndex = 8;
            this.IsSoundActiveCheckBox.Text = "Звуковой сигнал включен";
            this.IsSoundActiveCheckBox.UseVisualStyleBackColor = true;
            //
            // OkButton
            //
            this.OkButton.Location = new System.Drawing.Point(158, 235);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 9;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            //
            // CancelButton
            //
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(239, 235);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            //
            // SettingsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 270);
            this.Controls.Add(this.SnoozeMinutesNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RepeatGroupBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.IsSoundActiveCheckBox);
            this.Controls.Add(this.IsAlarmActiveCheckBox);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.AlarmTimeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.RepeatGroupBox.ResumeLayout(false);
            this.RepeatGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SnoozeMinutesNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AlarmTimeTextBox;
        private System.Windows.Forms.TextBox AlarmMessageTextBox;
        private System.Windows.Forms.CheckBox IsAlarmActiveCheckBox;
        private System.Windows.Forms.CheckBox IsSoundActiveCheckBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.GroupBox RepeatGroupBox;
        private System.Windows.Forms.CheckBox MondayCheckBox;
        private System.Windows.Forms.CheckBox TuesdayCheckBox;
        private System.Windows.Forms.CheckBox WednesdayCheckBox;
        private System.Windows.Forms.CheckBox ThursdayCheckBox;
        private System.Windows.Forms.CheckBox FridayCheckBox;
        private System.Windows.Forms.CheckBox SaturdayCheckBox;
        private System.Windows.Forms.CheckBox SundayCheckBox;
        private System.Windows.Forms.Button WorkDaysButton;
        private System.Windows.Forms.Button ClearDaysButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown SnoozeMinutesNumericUpDown;
    }
}
