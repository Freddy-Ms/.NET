using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Logic
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
    }
}
