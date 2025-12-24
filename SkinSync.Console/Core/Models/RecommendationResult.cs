using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinSync.Cli.Core.Models
{
    public sealed class RecommendationResult
    {
        public SkinRoutine Routine { get; }
        public string Explanation { get;  }

        public RecommendationResult(SkinRoutine routine, string explanation)
        {
            Routine = routine ?? throw new ArgumentNullException(nameof(routine));
            Explanation = explanation ?? string.Empty;
        }
    }
}
