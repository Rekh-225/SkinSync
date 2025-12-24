using SkinSync.Cli.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinSync.Cli.Core.Models
{
    public class RecommendationRequest
    {
         public SkinType SkinType {  get; set; }
        public WeatherType Weather {  get; set; }
      public   IReadOnlyList<SkinConcern> Concerns;

    }
}
