using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.CompilerServices;
namespace API
{
    public class APITest
    {
        public HttpClient client;
        public async Task GetData()
        {
            client = new HttpClient();
            string call = "https://openexchangerates.org/api/currencies.json";
            string response = await client.GetStringAsync(call);
            Console.WriteLine(response);
            Console.WriteLine("-----------");
            Dictionary<string, string> currDict = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
            List < Currencies > currencies = new List<Currencies>();
            foreach(var kvp in currDict)
            {
                currencies.Add(new Currencies { shortcut = kvp.Key, fullname = kvp.Value });
            }
            currencies.ForEach(Console.WriteLine);
        }
    }
    internal class Currencies
    {
        public string shortcut { get; set; }
        public string fullname { get; set; }
        public override string ToString()
        {
            return $"{shortcut} - {fullname}";
        }
    }
}
