namespace ImageProcessing
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
            original_IMG = new PictureBox();
            GrayScale = new PictureBox();
            Negative = new PictureBox();
            Threshold = new PictureBox();
            Edges = new PictureBox();
            LoadIMG = new Button();
            Proccess = new Button();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)original_IMG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GrayScale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Negative).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Threshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Edges).BeginInit();
            SuspendLayout();
            // 
            // original_IMG
            // 
            original_IMG.Location = new Point(12, 12);
            original_IMG.Name = "original_IMG";
            original_IMG.Size = new Size(362, 354);
            original_IMG.SizeMode = PictureBoxSizeMode.StretchImage;
            original_IMG.TabIndex = 0;
            original_IMG.TabStop = false;
            // 
            // GrayScale
            // 
            GrayScale.Location = new Point(407, 12);
            GrayScale.Name = "GrayScale";
            GrayScale.Size = new Size(181, 174);
            GrayScale.SizeMode = PictureBoxSizeMode.StretchImage;
            GrayScale.TabIndex = 1;
            GrayScale.TabStop = false;
            // 
            // Negative
            // 
            Negative.Location = new Point(594, 12);
            Negative.Name = "Negative";
            Negative.Size = new Size(181, 174);
            Negative.SizeMode = PictureBoxSizeMode.StretchImage;
            Negative.TabIndex = 2;
            Negative.TabStop = false;
            // 
            // Threshold
            // 
            Threshold.Location = new Point(407, 192);
            Threshold.Name = "Threshold";
            Threshold.Size = new Size(181, 174);
            Threshold.SizeMode = PictureBoxSizeMode.StretchImage;
            Threshold.TabIndex = 3;
            Threshold.TabStop = false;
            // 
            // Edges
            // 
            Edges.Location = new Point(594, 192);
            Edges.Name = "Edges";
            Edges.Size = new Size(181, 174);
            Edges.SizeMode = PictureBoxSizeMode.StretchImage;
            Edges.TabIndex = 4;
            Edges.TabStop = false;
            // 
            // LoadIMG
            // 
            LoadIMG.Location = new Point(133, 372);
            LoadIMG.Name = "LoadIMG";
            LoadIMG.Size = new Size(100, 29);
            LoadIMG.TabIndex = 5;
            LoadIMG.Text = "Load Image";
            LoadIMG.UseVisualStyleBackColor = true;
            LoadIMG.Click += LoadIMG_Click;
            // 
            // Proccess
            // 
            Proccess.Location = new Point(542, 370);
            Proccess.Name = "Proccess";
            Proccess.Size = new Size(100, 31);
            Proccess.TabIndex = 6;
            Proccess.Text = "Proccess";
            Proccess.UseVisualStyleBackColor = true;
            Proccess.Click += Proccess_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Pliki JPG (*.jpg)|*.jpg";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Proccess);
            Controls.Add(LoadIMG);
            Controls.Add(Edges);
            Controls.Add(Threshold);
            Controls.Add(Negative);
            Controls.Add(GrayScale);
            Controls.Add(original_IMG);
            Name = "Form1";
            Text = "Parallel Filters";
            ((System.ComponentModel.ISupportInitialize)original_IMG).EndInit();
            ((System.ComponentModel.ISupportInitialize)GrayScale).EndInit();
            ((System.ComponentModel.ISupportInitialize)Negative).EndInit();
            ((System.ComponentModel.ISupportInitialize)Threshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)Edges).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox original_IMG;
        private PictureBox GrayScale;
        private PictureBox Negative;
        private PictureBox Threshold;
        private PictureBox Edges;
        private Button LoadIMG;
        private Button Proccess;
        private OpenFileDialog openFileDialog1;
    }
}
