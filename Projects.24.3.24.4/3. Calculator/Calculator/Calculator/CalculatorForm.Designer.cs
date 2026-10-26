namespace Calculator
{
    partial class CalculatorForm
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
            this.ExpressionLabel = new System.Windows.Forms.Label();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.MemoryLabel = new System.Windows.Forms.Label();
            this.MemoryClearButton = new System.Windows.Forms.Button();
            this.MemoryRecallButton = new System.Windows.Forms.Button();
            this.MemoryAddButton = new System.Windows.Forms.Button();
            this.MemorySubtractButton = new System.Windows.Forms.Button();
            this.BackspaceButton = new System.Windows.Forms.Button();
            this.ClearEntryButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.NegateButton = new System.Windows.Forms.Button();
            this.SqrtButton = new System.Windows.Forms.Button();
            this.PercentButton = new System.Windows.Forms.Button();
            this.Digit7Button = new System.Windows.Forms.Button();
            this.Digit8Button = new System.Windows.Forms.Button();
            this.Digit9Button = new System.Windows.Forms.Button();
            this.DivideButton = new System.Windows.Forms.Button();
            this.SquareButton = new System.Windows.Forms.Button();
            this.Digit4Button = new System.Windows.Forms.Button();
            this.Digit5Button = new System.Windows.Forms.Button();
            this.Digit6Button = new System.Windows.Forms.Button();
            this.MultiplyButton = new System.Windows.Forms.Button();
            this.ReciprocalButton = new System.Windows.Forms.Button();
            this.Digit1Button = new System.Windows.Forms.Button();
            this.Digit2Button = new System.Windows.Forms.Button();
            this.Digit3Button = new System.Windows.Forms.Button();
            this.SubtractButton = new System.Windows.Forms.Button();
            this.PowerButton = new System.Windows.Forms.Button();
            this.Digit0Button = new System.Windows.Forms.Button();
            this.DecimalPointButton = new System.Windows.Forms.Button();
            this.MoveXToYButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.EqualButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExpressionLabel
            // 
            this.ExpressionLabel.BackColor = System.Drawing.Color.Black;
            this.ExpressionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExpressionLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ExpressionLabel.Location = new System.Drawing.Point(12, 9);
            this.ExpressionLabel.Name = "ExpressionLabel";
            this.ExpressionLabel.Size = new System.Drawing.Size(399, 24);
            this.ExpressionLabel.TabIndex = 0;
            this.ExpressionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.Chartreuse;
            this.DisplayLabel.Location = new System.Drawing.Point(12, 33);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(399, 60);
            this.DisplayLabel.TabIndex = 1;
            this.DisplayLabel.Text = "0";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MemoryLabel
            // 
            this.MemoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MemoryLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.MemoryLabel.Location = new System.Drawing.Point(12, 98);
            this.MemoryLabel.Name = "MemoryLabel";
            this.MemoryLabel.Size = new System.Drawing.Size(399, 20);
            this.MemoryLabel.TabIndex = 2;
            this.MemoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MemoryClearButton
            // 
            this.MemoryClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MemoryClearButton.Location = new System.Drawing.Point(12, 124);
            this.MemoryClearButton.Name = "MemoryClearButton";
            this.MemoryClearButton.Size = new System.Drawing.Size(75, 49);
            this.MemoryClearButton.TabIndex = 3;
            this.MemoryClearButton.Text = "MC";
            this.MemoryClearButton.UseVisualStyleBackColor = true;
            this.MemoryClearButton.Click += new System.EventHandler(this.MemoryClearButton_Click);
            // 
            // MemoryRecallButton
            // 
            this.MemoryRecallButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MemoryRecallButton.Location = new System.Drawing.Point(93, 124);
            this.MemoryRecallButton.Name = "MemoryRecallButton";
            this.MemoryRecallButton.Size = new System.Drawing.Size(75, 49);
            this.MemoryRecallButton.TabIndex = 4;
            this.MemoryRecallButton.Text = "MR";
            this.MemoryRecallButton.UseVisualStyleBackColor = true;
            this.MemoryRecallButton.Click += new System.EventHandler(this.MemoryRecallButton_Click);
            // 
            // MemoryAddButton
            // 
            this.MemoryAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MemoryAddButton.Location = new System.Drawing.Point(174, 124);
            this.MemoryAddButton.Name = "MemoryAddButton";
            this.MemoryAddButton.Size = new System.Drawing.Size(75, 49);
            this.MemoryAddButton.TabIndex = 5;
            this.MemoryAddButton.Text = "M+";
            this.MemoryAddButton.UseVisualStyleBackColor = true;
            this.MemoryAddButton.Click += new System.EventHandler(this.MemoryAddButton_Click);
            // 
            // MemorySubtractButton
            // 
            this.MemorySubtractButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MemorySubtractButton.Location = new System.Drawing.Point(255, 124);
            this.MemorySubtractButton.Name = "MemorySubtractButton";
            this.MemorySubtractButton.Size = new System.Drawing.Size(75, 49);
            this.MemorySubtractButton.TabIndex = 6;
            this.MemorySubtractButton.Text = "M-";
            this.MemorySubtractButton.UseVisualStyleBackColor = true;
            this.MemorySubtractButton.Click += new System.EventHandler(this.MemorySubtractButton_Click);
            // 
            // BackspaceButton
            // 
            this.BackspaceButton.BackColor = System.Drawing.Color.MistyRose;
            this.BackspaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackspaceButton.Location = new System.Drawing.Point(336, 124);
            this.BackspaceButton.Name = "BackspaceButton";
            this.BackspaceButton.Size = new System.Drawing.Size(75, 49);
            this.BackspaceButton.TabIndex = 7;
            this.BackspaceButton.Text = "←";
            this.BackspaceButton.UseVisualStyleBackColor = false;
            this.BackspaceButton.Click += new System.EventHandler(this.BackspaceButton_Click);
            // 
            // ClearEntryButton
            // 
            this.ClearEntryButton.BackColor = System.Drawing.Color.MistyRose;
            this.ClearEntryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearEntryButton.Location = new System.Drawing.Point(12, 179);
            this.ClearEntryButton.Name = "ClearEntryButton";
            this.ClearEntryButton.Size = new System.Drawing.Size(75, 49);
            this.ClearEntryButton.TabIndex = 8;
            this.ClearEntryButton.Text = "CE";
            this.ClearEntryButton.UseVisualStyleBackColor = false;
            this.ClearEntryButton.Click += new System.EventHandler(this.ClearEntryButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.Color.MistyRose;
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearButton.Location = new System.Drawing.Point(93, 179);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 49);
            this.ClearButton.TabIndex = 9;
            this.ClearButton.Text = "C";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // NegateButton
            // 
            this.NegateButton.BackColor = System.Drawing.Color.AliceBlue;
            this.NegateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NegateButton.Location = new System.Drawing.Point(174, 179);
            this.NegateButton.Name = "NegateButton";
            this.NegateButton.Size = new System.Drawing.Size(75, 49);
            this.NegateButton.TabIndex = 10;
            this.NegateButton.Text = "±";
            this.NegateButton.UseVisualStyleBackColor = false;
            this.NegateButton.Click += new System.EventHandler(this.UnaryOperationButton_Click);
            // 
            // SqrtButton
            // 
            this.SqrtButton.BackColor = System.Drawing.Color.AliceBlue;
            this.SqrtButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SqrtButton.Location = new System.Drawing.Point(255, 179);
            this.SqrtButton.Name = "SqrtButton";
            this.SqrtButton.Size = new System.Drawing.Size(75, 49);
            this.SqrtButton.TabIndex = 11;
            this.SqrtButton.Text = "√";
            this.SqrtButton.UseVisualStyleBackColor = false;
            this.SqrtButton.Click += new System.EventHandler(this.UnaryOperationButton_Click);
            // 
            // PercentButton
            // 
            this.PercentButton.BackColor = System.Drawing.Color.AliceBlue;
            this.PercentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PercentButton.Location = new System.Drawing.Point(336, 179);
            this.PercentButton.Name = "PercentButton";
            this.PercentButton.Size = new System.Drawing.Size(75, 49);
            this.PercentButton.TabIndex = 12;
            this.PercentButton.Text = "%";
            this.PercentButton.UseVisualStyleBackColor = false;
            this.PercentButton.Click += new System.EventHandler(this.UnaryOperationButton_Click);
            // 
            // Digit7Button
            // 
            this.Digit7Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit7Button.Location = new System.Drawing.Point(12, 234);
            this.Digit7Button.Name = "Digit7Button";
            this.Digit7Button.Size = new System.Drawing.Size(75, 49);
            this.Digit7Button.TabIndex = 13;
            this.Digit7Button.Text = "7";
            this.Digit7Button.UseVisualStyleBackColor = true;
            this.Digit7Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit8Button
            // 
            this.Digit8Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit8Button.Location = new System.Drawing.Point(93, 234);
            this.Digit8Button.Name = "Digit8Button";
            this.Digit8Button.Size = new System.Drawing.Size(75, 49);
            this.Digit8Button.TabIndex = 14;
            this.Digit8Button.Text = "8";
            this.Digit8Button.UseVisualStyleBackColor = true;
            this.Digit8Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit9Button
            // 
            this.Digit9Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit9Button.Location = new System.Drawing.Point(174, 234);
            this.Digit9Button.Name = "Digit9Button";
            this.Digit9Button.Size = new System.Drawing.Size(75, 49);
            this.Digit9Button.TabIndex = 15;
            this.Digit9Button.Text = "9";
            this.Digit9Button.UseVisualStyleBackColor = true;
            this.Digit9Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // DivideButton
            // 
            this.DivideButton.BackColor = System.Drawing.Color.AliceBlue;
            this.DivideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DivideButton.Location = new System.Drawing.Point(255, 234);
            this.DivideButton.Name = "DivideButton";
            this.DivideButton.Size = new System.Drawing.Size(75, 49);
            this.DivideButton.TabIndex = 16;
            this.DivideButton.Text = "/";
            this.DivideButton.UseVisualStyleBackColor = false;
            this.DivideButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // SquareButton
            // 
            this.SquareButton.BackColor = System.Drawing.Color.AliceBlue;
            this.SquareButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SquareButton.Location = new System.Drawing.Point(336, 234);
            this.SquareButton.Name = "SquareButton";
            this.SquareButton.Size = new System.Drawing.Size(75, 49);
            this.SquareButton.TabIndex = 17;
            this.SquareButton.Text = "x²";
            this.SquareButton.UseVisualStyleBackColor = false;
            this.SquareButton.Click += new System.EventHandler(this.UnaryOperationButton_Click);
            // 
            // Digit4Button
            // 
            this.Digit4Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit4Button.Location = new System.Drawing.Point(12, 289);
            this.Digit4Button.Name = "Digit4Button";
            this.Digit4Button.Size = new System.Drawing.Size(75, 49);
            this.Digit4Button.TabIndex = 18;
            this.Digit4Button.Text = "4";
            this.Digit4Button.UseVisualStyleBackColor = true;
            this.Digit4Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit5Button
            // 
            this.Digit5Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit5Button.Location = new System.Drawing.Point(93, 289);
            this.Digit5Button.Name = "Digit5Button";
            this.Digit5Button.Size = new System.Drawing.Size(75, 49);
            this.Digit5Button.TabIndex = 19;
            this.Digit5Button.Text = "5";
            this.Digit5Button.UseVisualStyleBackColor = true;
            this.Digit5Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit6Button
            // 
            this.Digit6Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit6Button.Location = new System.Drawing.Point(174, 289);
            this.Digit6Button.Name = "Digit6Button";
            this.Digit6Button.Size = new System.Drawing.Size(75, 49);
            this.Digit6Button.TabIndex = 20;
            this.Digit6Button.Text = "6";
            this.Digit6Button.UseVisualStyleBackColor = true;
            this.Digit6Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // MultiplyButton
            // 
            this.MultiplyButton.BackColor = System.Drawing.Color.AliceBlue;
            this.MultiplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MultiplyButton.Location = new System.Drawing.Point(255, 289);
            this.MultiplyButton.Name = "MultiplyButton";
            this.MultiplyButton.Size = new System.Drawing.Size(75, 49);
            this.MultiplyButton.TabIndex = 21;
            this.MultiplyButton.Text = "*";
            this.MultiplyButton.UseVisualStyleBackColor = false;
            this.MultiplyButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // ReciprocalButton
            // 
            this.ReciprocalButton.BackColor = System.Drawing.Color.AliceBlue;
            this.ReciprocalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReciprocalButton.Location = new System.Drawing.Point(336, 289);
            this.ReciprocalButton.Name = "ReciprocalButton";
            this.ReciprocalButton.Size = new System.Drawing.Size(75, 49);
            this.ReciprocalButton.TabIndex = 22;
            this.ReciprocalButton.Text = "1/x";
            this.ReciprocalButton.UseVisualStyleBackColor = false;
            this.ReciprocalButton.Click += new System.EventHandler(this.UnaryOperationButton_Click);
            // 
            // Digit1Button
            // 
            this.Digit1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit1Button.Location = new System.Drawing.Point(12, 344);
            this.Digit1Button.Name = "Digit1Button";
            this.Digit1Button.Size = new System.Drawing.Size(75, 49);
            this.Digit1Button.TabIndex = 23;
            this.Digit1Button.Text = "1";
            this.Digit1Button.UseVisualStyleBackColor = true;
            this.Digit1Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit2Button
            // 
            this.Digit2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit2Button.Location = new System.Drawing.Point(93, 344);
            this.Digit2Button.Name = "Digit2Button";
            this.Digit2Button.Size = new System.Drawing.Size(75, 49);
            this.Digit2Button.TabIndex = 24;
            this.Digit2Button.Text = "2";
            this.Digit2Button.UseVisualStyleBackColor = true;
            this.Digit2Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // Digit3Button
            // 
            this.Digit3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit3Button.Location = new System.Drawing.Point(174, 344);
            this.Digit3Button.Name = "Digit3Button";
            this.Digit3Button.Size = new System.Drawing.Size(75, 49);
            this.Digit3Button.TabIndex = 25;
            this.Digit3Button.Text = "3";
            this.Digit3Button.UseVisualStyleBackColor = true;
            this.Digit3Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // SubtractButton
            // 
            this.SubtractButton.BackColor = System.Drawing.Color.AliceBlue;
            this.SubtractButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubtractButton.Location = new System.Drawing.Point(255, 344);
            this.SubtractButton.Name = "SubtractButton";
            this.SubtractButton.Size = new System.Drawing.Size(75, 49);
            this.SubtractButton.TabIndex = 26;
            this.SubtractButton.Text = "-";
            this.SubtractButton.UseVisualStyleBackColor = false;
            this.SubtractButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // PowerButton
            // 
            this.PowerButton.BackColor = System.Drawing.Color.AliceBlue;
            this.PowerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PowerButton.Location = new System.Drawing.Point(336, 344);
            this.PowerButton.Name = "PowerButton";
            this.PowerButton.Size = new System.Drawing.Size(75, 49);
            this.PowerButton.TabIndex = 27;
            this.PowerButton.Text = "^";
            this.PowerButton.UseVisualStyleBackColor = false;
            this.PowerButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // Digit0Button
            // 
            this.Digit0Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Digit0Button.Location = new System.Drawing.Point(12, 399);
            this.Digit0Button.Name = "Digit0Button";
            this.Digit0Button.Size = new System.Drawing.Size(75, 49);
            this.Digit0Button.TabIndex = 28;
            this.Digit0Button.Text = "0";
            this.Digit0Button.UseVisualStyleBackColor = true;
            this.Digit0Button.Click += new System.EventHandler(this.DigitButton_Click);
            // 
            // DecimalPointButton
            // 
            this.DecimalPointButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DecimalPointButton.Location = new System.Drawing.Point(93, 399);
            this.DecimalPointButton.Name = "DecimalPointButton";
            this.DecimalPointButton.Size = new System.Drawing.Size(75, 49);
            this.DecimalPointButton.TabIndex = 29;
            this.DecimalPointButton.Text = ",";
            this.DecimalPointButton.UseVisualStyleBackColor = true;
            this.DecimalPointButton.Click += new System.EventHandler(this.DecimalPointButton_Click);
            // 
            // MoveXToYButton
            // 
            this.MoveXToYButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MoveXToYButton.Location = new System.Drawing.Point(174, 399);
            this.MoveXToYButton.Name = "MoveXToYButton";
            this.MoveXToYButton.Size = new System.Drawing.Size(75, 49);
            this.MoveXToYButton.TabIndex = 30;
            this.MoveXToYButton.Text = "X→Y";
            this.MoveXToYButton.UseVisualStyleBackColor = true;
            this.MoveXToYButton.Click += new System.EventHandler(this.MoveXToYButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.AliceBlue;
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddButton.Location = new System.Drawing.Point(255, 399);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 49);
            this.AddButton.TabIndex = 31;
            this.AddButton.Text = "+";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // EqualButton
            // 
            this.EqualButton.BackColor = System.Drawing.Color.LightGreen;
            this.EqualButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EqualButton.Location = new System.Drawing.Point(336, 399);
            this.EqualButton.Name = "EqualButton";
            this.EqualButton.Size = new System.Drawing.Size(75, 49);
            this.EqualButton.TabIndex = 32;
            this.EqualButton.Text = "=";
            this.EqualButton.UseVisualStyleBackColor = false;
            this.EqualButton.Click += new System.EventHandler(this.EqualButton_Click);
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 461);
            this.Controls.Add(this.EqualButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.MoveXToYButton);
            this.Controls.Add(this.DecimalPointButton);
            this.Controls.Add(this.Digit0Button);
            this.Controls.Add(this.PowerButton);
            this.Controls.Add(this.SubtractButton);
            this.Controls.Add(this.Digit3Button);
            this.Controls.Add(this.Digit2Button);
            this.Controls.Add(this.Digit1Button);
            this.Controls.Add(this.ReciprocalButton);
            this.Controls.Add(this.MultiplyButton);
            this.Controls.Add(this.Digit6Button);
            this.Controls.Add(this.Digit5Button);
            this.Controls.Add(this.Digit4Button);
            this.Controls.Add(this.SquareButton);
            this.Controls.Add(this.DivideButton);
            this.Controls.Add(this.Digit9Button);
            this.Controls.Add(this.Digit8Button);
            this.Controls.Add(this.Digit7Button);
            this.Controls.Add(this.PercentButton);
            this.Controls.Add(this.SqrtButton);
            this.Controls.Add(this.NegateButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ClearEntryButton);
            this.Controls.Add(this.BackspaceButton);
            this.Controls.Add(this.MemorySubtractButton);
            this.Controls.Add(this.MemoryAddButton);
            this.Controls.Add(this.MemoryRecallButton);
            this.Controls.Add(this.MemoryClearButton);
            this.Controls.Add(this.MemoryLabel);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.ExpressionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "CalculatorForm";
            this.Text = "Калькулятор";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalculatorForm_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ExpressionLabel;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Label MemoryLabel;
        private System.Windows.Forms.Button MemoryClearButton;
        private System.Windows.Forms.Button MemoryRecallButton;
        private System.Windows.Forms.Button MemoryAddButton;
        private System.Windows.Forms.Button MemorySubtractButton;
        private System.Windows.Forms.Button BackspaceButton;
        private System.Windows.Forms.Button ClearEntryButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button NegateButton;
        private System.Windows.Forms.Button SqrtButton;
        private System.Windows.Forms.Button PercentButton;
        private System.Windows.Forms.Button Digit7Button;
        private System.Windows.Forms.Button Digit8Button;
        private System.Windows.Forms.Button Digit9Button;
        private System.Windows.Forms.Button DivideButton;
        private System.Windows.Forms.Button SquareButton;
        private System.Windows.Forms.Button Digit4Button;
        private System.Windows.Forms.Button Digit5Button;
        private System.Windows.Forms.Button Digit6Button;
        private System.Windows.Forms.Button MultiplyButton;
        private System.Windows.Forms.Button ReciprocalButton;
        private System.Windows.Forms.Button Digit1Button;
        private System.Windows.Forms.Button Digit2Button;
        private System.Windows.Forms.Button Digit3Button;
        private System.Windows.Forms.Button SubtractButton;
        private System.Windows.Forms.Button PowerButton;
        private System.Windows.Forms.Button Digit0Button;
        private System.Windows.Forms.Button DecimalPointButton;
        private System.Windows.Forms.Button MoveXToYButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button EqualButton;
    }
}
