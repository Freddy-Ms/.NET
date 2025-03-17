namespace GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SumbitButton = new Button();
            Seed = new TextBox();
            MaxValue = new TextBox();
            MaxWeight = new TextBox();
            Capacity = new TextBox();
            LabelMaxValue = new Label();
            LabelMaxWeight = new Label();
            LabelCapacity = new Label();
            SeedLabel = new Label();
            nrOfItems = new TextBox();
            NrOfItemsLabel = new Label();
            InstanceLabel = new Label();
            InstanceTextBox = new RichTextBox();
            ResultTextBox = new RichTextBox();
            ResultsLabel = new Label();
            SuspendLayout();
            // 
            // SumbitButton
            // 
            SumbitButton.Location = new Point(47, 322);
            SumbitButton.Name = "SumbitButton";
            SumbitButton.Size = new Size(140, 43);
            SumbitButton.TabIndex = 0;
            SumbitButton.Text = "Sumbit";
            SumbitButton.UseVisualStyleBackColor = true;
            SumbitButton.Click += SumbitButton_Click;
            // 
            // Seed
            // 
            Seed.Location = new Point(67, 60);
            Seed.Name = "Seed";
            Seed.Size = new Size(100, 23);
            Seed.TabIndex = 1;
            Seed.Text = "\r\n";
            Seed.TextChanged += isGreaterOrEqual0;
            // 
            // MaxValue
            // 
            MaxValue.Location = new Point(67, 116);
            MaxValue.Name = "MaxValue";
            MaxValue.Size = new Size(100, 23);
            MaxValue.TabIndex = 2;
            MaxValue.TextChanged += isGreaterThan0;
            // 
            // MaxWeight
            // 
            MaxWeight.Location = new Point(67, 174);
            MaxWeight.Name = "MaxWeight";
            MaxWeight.Size = new Size(100, 23);
            MaxWeight.TabIndex = 3;
            MaxWeight.TextChanged += isGreaterThan0;
            // 
            // Capacity
            // 
            Capacity.Location = new Point(67, 230);
            Capacity.Name = "Capacity";
            Capacity.Size = new Size(100, 23);
            Capacity.TabIndex = 4;
            Capacity.TextChanged += isGreaterOrEqual0;
            // 
            // LabelMaxValue
            // 
            LabelMaxValue.AutoSize = true;
            LabelMaxValue.Location = new Point(67, 98);
            LabelMaxValue.Name = "LabelMaxValue";
            LabelMaxValue.Size = new Size(60, 15);
            LabelMaxValue.TabIndex = 6;
            LabelMaxValue.Text = "Max Value";
            // 
            // LabelMaxWeight
            // 
            LabelMaxWeight.AutoSize = true;
            LabelMaxWeight.Location = new Point(67, 156);
            LabelMaxWeight.Name = "LabelMaxWeight";
            LabelMaxWeight.Size = new Size(70, 15);
            LabelMaxWeight.TabIndex = 7;
            LabelMaxWeight.Text = "Max Weight";
            // 
            // LabelCapacity
            // 
            LabelCapacity.AutoSize = true;
            LabelCapacity.Location = new Point(67, 212);
            LabelCapacity.Name = "LabelCapacity";
            LabelCapacity.Size = new Size(109, 15);
            LabelCapacity.TabIndex = 8;
            LabelCapacity.Text = "Knapsack Capacity:";
            // 
            // SeedLabel
            // 
            SeedLabel.AutoSize = true;
            SeedLabel.Location = new Point(67, 42);
            SeedLabel.Name = "SeedLabel";
            SeedLabel.Size = new Size(35, 15);
            SeedLabel.TabIndex = 9;
            SeedLabel.Text = "Seed:";
            // 
            // nrOfItems
            // 
            nrOfItems.Location = new Point(67, 282);
            nrOfItems.Name = "nrOfItems";
            nrOfItems.Size = new Size(100, 23);
            nrOfItems.TabIndex = 10;
            nrOfItems.TextChanged += isGreaterThan0;
            // 
            // NrOfItemsLabel
            // 
            NrOfItemsLabel.AutoSize = true;
            NrOfItemsLabel.Location = new Point(67, 264);
            NrOfItemsLabel.Name = "NrOfItemsLabel";
            NrOfItemsLabel.Size = new Size(100, 15);
            NrOfItemsLabel.TabIndex = 11;
            NrOfItemsLabel.Text = "Number of items:";
            // 
            // InstanceLabel
            // 
            InstanceLabel.AutoSize = true;
            InstanceLabel.Location = new Point(377, 42);
            InstanceLabel.Name = "InstanceLabel";
            InstanceLabel.Size = new Size(54, 15);
            InstanceLabel.TabIndex = 14;
            InstanceLabel.Text = "Instance:";
            // 
            // InstanceTextBox
            // 
            InstanceTextBox.Location = new Point(377, 75);
            InstanceTextBox.Name = "InstanceTextBox";
            InstanceTextBox.ReadOnly = true;
            InstanceTextBox.Size = new Size(226, 519);
            InstanceTextBox.TabIndex = 15;
            InstanceTextBox.Text = "";
            // 
            // ResultTextBox
            // 
            ResultTextBox.Location = new Point(47, 438);
            ResultTextBox.Name = "ResultTextBox";
            ResultTextBox.ReadOnly = true;
            ResultTextBox.Size = new Size(279, 156);
            ResultTextBox.TabIndex = 16;
            ResultTextBox.Text = "";
            // 
            // ResultsLabel
            // 
            ResultsLabel.AutoSize = true;
            ResultsLabel.Location = new Point(47, 411);
            ResultsLabel.Name = "ResultsLabel";
            ResultsLabel.Size = new Size(47, 15);
            ResultsLabel.TabIndex = 17;
            ResultsLabel.Text = "Results:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 615);
            Controls.Add(ResultsLabel);
            Controls.Add(ResultTextBox);
            Controls.Add(InstanceTextBox);
            Controls.Add(InstanceLabel);
            Controls.Add(NrOfItemsLabel);
            Controls.Add(nrOfItems);
            Controls.Add(SeedLabel);
            Controls.Add(LabelCapacity);
            Controls.Add(LabelMaxWeight);
            Controls.Add(LabelMaxValue);
            Controls.Add(Capacity);
            Controls.Add(MaxWeight);
            Controls.Add(MaxValue);
            Controls.Add(Seed);
            Controls.Add(SumbitButton);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SumbitButton;
        private TextBox Seed;
        private TextBox MaxValue;
        private TextBox MaxWeight;
        private TextBox Capacity;
        private Label LabelMaxValue;
        private Label LabelMaxWeight;
        private Label LabelCapacity;
        private Label SeedLabel;
        private TextBox nrOfItems;
        private Label NrOfItemsLabel;
        private Label InstanceLabel;
        private RichTextBox InstanceTextBox;
        private RichTextBox ResultTextBox;
        private Label ResultsLabel;
    }
}
