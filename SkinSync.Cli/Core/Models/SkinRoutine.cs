using SkinSync.Cli.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinSync.Cli.Core.Models
{
    public class SkinRoutine
    {
        public SkinType SkinType { get; set; }
        public WeatherType Weather { get; set; }
        public string Cleanser { get; set; }
        public string Moisturizer { get; set; }
        public string Sunscreen { get; set; }
        public string Notes { get; set; }

    }
}
