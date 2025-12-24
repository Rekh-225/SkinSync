using SkinSync.Cli.Core.Enums;
using SkinSync.Cli.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkinSync.Cli.Data
{
    public class RoutineRepository
    {
        private readonly IReadOnlyList<SkinRoutine> _routines;

        public RoutineRepository(string jsonPath)
        {
            if (string.IsNullOrWhiteSpace(jsonPath))
                throw new ArgumentException("json path cannot be null or empty", nameof(jsonPath));

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("Routines json file not found", jsonPath);

            var json = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var routines = JsonSerializer.Deserialize<List<SkinRoutine>>(json, options);

            if (routines == null || routines.Count == 0)
            {
                throw new ArgumentException("No routines were loaded from the json file.");
            }
            _routines = routines;

        }

        public RecommendationResult GetBestRoutine(SkinType skintype, WeatherType weather)
        {
            //CASE 1 Exact match
            var exact = _routines.FirstOrDefault(r => r.SkinType == skintype && r.Weather == weather);
            if (exact is not null)
             return new RecommendationResult(exact, "Exact match is found");

            //Case2 skintype-only fallback 
            var skinFallback = _routines.FirstOrDefault(r => r.SkinType == skintype);
            if (skinFallback is not null)
                return new RecommendationResult(skinFallback, "No exact match; using SkinType fallback.");

            //Weather-only Fallback
            var weatherFallback = _routines.FirstOrDefault(r => r.Weather == weather);
            if (weatherFallback is not null)
                return new RecommendationResult(weatherFallback, "No exact match; using Weather fallback.");

            //Default routine (must exist in dataset)
            var @default = _routines.FirstOrDefault( r=> r.SkinType == SkinType.Normal  && r.Weather == WeatherType.Moderate);
            if (@default is not null)
                return new RecommendationResult(@default, "No match found; using default routine (Normal + Moderate).");

            throw new InvalidOperationException("Default routine (Normal + Moderate) is missing from the dataset.");
        }

    }
}

