namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        private Bitmap? img;
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadIMG_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            if (file != null)
            {
                img = new Bitmap(file);
                original_IMG.Image = img;
            }
        }

        private void Proccess_Click(object sender, EventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("Load image first!");
            }
            else
            {
                var imgClones = new Bitmap[]
                {
                    (Bitmap)img.Clone(),
                    (Bitmap)img.Clone(),
                    (Bitmap)img.Clone(),
                    (Bitmap)img.Clone()
                };
                Action[] filters = new Action[]
                {
                    () =>  {GrayScale.Image = Filters.GrayscaleFilter(imgClones[0]); },
                    () =>  {Negative.Image = Filters.NegativeFilter(imgClones[1]); },
                    () =>  {Threshold.Image = Filters.ThresholdFilter(imgClones[2]); },
                    () =>  {Edges.Image = Filters.EdgeDetectionFilter(imgClones[3]); }
                };

                Parallel.For(0, filters.Length, i =>
                {
                    filters[i]();
                });
            }
        }
    }
}
