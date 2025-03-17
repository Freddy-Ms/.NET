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
            label1 = new Label();
            LabelMaxValue = new Label();
            LabelMaxWeight = new Label();
            LabelCapacity = new Label();
            SeedLabel = new Label();
            nrOfItems = new TextBox();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            InstanceTextBox = new RichTextBox();
            richTextBox1 = new RichTextBox();
            label5 = new Label();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(81, 40);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 5;
            label1.Text = "label1";
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 264);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 11;
            label2.Text = "Number of items:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(377, 42);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 14;
            label3.Text = "Instance:";
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
            // richTextBox1
            // 
            richTextBox1.Location = new Point(47, 438);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(279, 156);
            richTextBox1.TabIndex = 16;
            richTextBox1.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(47, 411);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 17;
            label5.Text = "Results:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 615);
            Controls.Add(label5);
            Controls.Add(richTextBox1);
            Controls.Add(InstanceTextBox);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
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
        private Label label1;
        private Label LabelMaxValue;
        private Label LabelMaxWeight;
        private Label LabelCapacity;
        private Label SeedLabel;
        private TextBox nrOfItems;
        private Label label2;
        private Label label4;
        private Label label3;
        private RichTextBox InstanceTextBox;
        private RichTextBox richTextBox1;
        private Label label5;
    }
}
