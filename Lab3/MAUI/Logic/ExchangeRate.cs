using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Logic
{
    public class ExchangeRate
    {
        [Required]
        public int TargetCurrencyId { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime Date { get; set; }

        public decimal Rate { get; set; }

        [NotMapped]
        public decimal ReverseRate => 1 / Rate;

        public Currency TargetCurrency { get; set; } = null!;
    }
}
