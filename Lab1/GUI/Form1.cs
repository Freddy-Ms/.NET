using Knapsack;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void isGreaterThan0(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (int.TryParse(textBox.Text, out int number))
            {
                if (number <= 0)
                {
                    textBox.BackColor = Color.Red;


                    // MessageBox.Show("Wartoœæ musi byæ wiêksza od 0!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textBox.BackColor = Color.White;
                }
            }
            else
            {
                textBox.BackColor = Color.Red;

                // MessageBox.Show("Proszê wprowadziæ prawid³ow¹ liczbê!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void isGreaterOrEqual0(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (int.TryParse(textBox.Text, out int number))
            {
                if (number < 0)
                {
                    textBox.BackColor = Color.Red;
                    // MessageBox.Show("Wartoœæ musi byæ wiêksza lub równa 0!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textBox.BackColor = Color.White;
                }
            }
            else
            {
                textBox.BackColor = Color.Red;
                // MessageBox.Show("Proszê wprowadziæ prawid³ow¹ liczbê!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SumbitButton_Click(object sender, EventArgs e)
        {
            if (Seed.BackColor == Color.Red || MaxValue.BackColor == Color.Red || MaxWeight.BackColor == Color.Red || Capacity.BackColor == Color.Red || nrOfItems.BackColor == Color.Red || Seed.Text == "" || MaxValue.Text == "" || MaxWeight.Text == "" || Capacity.Text == "" || nrOfItems.Text == "")
            {
                MessageBox.Show("Proszê wprowadziæ prawid³owe wartoœci!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int seed = int.Parse(Seed.Text);
                int numberOfItems = int.Parse(nrOfItems.Text);
                int capacity = int.Parse(Capacity.Text);
                int maxWeight = int.Parse(MaxWeight.Text);
                int maxValue = int.Parse(MaxValue.Text);

                Problem problem = new Problem(numberOfItems, maxWeight, maxValue, seed);
                InstanceTextBox.Text = problem.ToString();
                Result result = problem.Solve(capacity);
                ResultTextBox.Text = result.ToString();

            }
        }

    }
}
