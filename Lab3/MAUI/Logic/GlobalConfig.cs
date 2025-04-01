using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Logic
{
    public static class GlobalConfig
    {
        public static string ID { get; set; } = string.Empty;

        public static async Task LoadIDAsync()
        {
            if (File.Exists("E:\\KEY.txt"))
            {
                ID = await File.ReadAllTextAsync("E:\\KEY.txt");
            }
        }
    }
}
