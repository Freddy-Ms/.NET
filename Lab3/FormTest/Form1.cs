using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using API;
using static System.Net.WebRequestMethods;

namespace FormTest
{
    public partial class Form1 : Form
    {
        private HttpClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
            LoadISAsync();
        }

        private async void LoadISAsync()
        {
            await GlobalConfig.LoadIDAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string call = "https://openexchangerates.org/api/currencies.json";
            string response = await client.GetStringAsync(call);
            Dictionary<string, string> currDict = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
            List<Currencies> currencies = new List<Currencies>();
            foreach (var kvp in currDict)
            {
                currencies.Add(new Currencies { shortcut = kvp.Key, fullname = kvp.Value });
            }

            foreach (Currencies curr in currencies)
            {
                listBox1.Items.Add(curr.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           string data = dateTimePicker1.Value.ToString("yyyy-MM-dd");
           string call = $"https://openexchangerates.org/api/historical/{data}.json?app_id={GlobalConfig.ID}";
            string response = client.GetStringAsync(call).Result;
            var info = JsonSerializer.Deserialize<Dictionary<string,JsonElement>>(response);
            decimal rate = info["rates"].GetProperty("PLN").GetDecimal();

            decimal output = rate * decimal.Parse(textBox1.Text);
            textBox2.Text = output.ToString();
        }
    }
}
