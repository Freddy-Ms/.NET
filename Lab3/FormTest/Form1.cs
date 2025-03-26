using System.Text.Json;
using System.Threading.Tasks;
using API;

namespace FormTest
{
    public partial class Form1 : Form
    {
        private HttpClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
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
       
            foreach(Currencies curr in currencies)
            {
                listBox1.Items.Add(curr.ToString());
            }
        }
    }
}
