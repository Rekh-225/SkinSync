using SkinSync.Cli.Core.Enums;
using SkinSync.Cli.Core.Models;
using SkinSync.Cli.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinSync.Cli.Core.Engine
{
    public class RecommendationEngine
    {
        readonly RoutineRepository _repo;
        public RecommendationEngine(RoutineRepository repo)
        {
               _repo = repo ?? throw new ArgumentNullException(nameof(repo));
           
        }
        public RecommendationResult Recommend(RecommendationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var concerns = request.Concerns ?? Array.Empty<SkinConcern>();

            var baseResult = _repo.GetBestRoutine(request.SkinType, request.Weather);

            var routine = new SkinRoutine
            {
                SkinType = baseResult.Routine.SkinType,
                Weather = baseResult.Routine.Weather,
                Cleanser = baseResult.Routine.Cleanser,
                Moisturizer = baseResult.Routine.Moisturizer,
                Sunscreen = baseResult.Routine.Sunscreen,
                Notes = baseResult.Routine.Notes,
            };
            var tips = new List<string>();

            foreach (var c in concerns)
            {
                if (c == SkinConcern.Acne)
                    tips.Add("Tip (Acne): Prefer non-comedogenic products.");

                if (c == SkinConcern.Sensitivity)
                    tips.Add("Tip (Sensitivity): Patch test new products before applying.");
            }

            // Apply tips to Notes (only once)
            if (tips.Count > 0)
            {
                routine.Notes = string.IsNullOrWhiteSpace(routine.Notes)
                    ? string.Join(" ", tips)
                    : $"{routine.Notes} {string.Join(" ", tips)}";
            }

            // Build explanation
            var explanation = baseResult.Explanation;

            if (tips.Count == 0)
                explanation += " No concern tips applied.";
            else
                explanation += $" Concern tips applied: {string.Join(", ", concerns.Distinct())}.";

            return new RecommendationResult(routine, explanation);



        }
    }
}
