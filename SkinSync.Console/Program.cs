using SkinSync.Cli.Core.Engine;
using SkinSync.Cli.Core.Enums;
using SkinSync.Cli.Core.Models;
using SkinSync.Cli.Data;
using System;
using System.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SkinSync.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("SkinSync ready");
            //  RoutinePrinter routinePrinter = new RoutinePrinter();

            //  var path = Path.Combine("Resources", "routines.json");
            //  var routinesData = File.ReadAllText(path);

            //  var options = new JsonSerializerOptions
            //  {
            //      PropertyNameCaseInsensitive = true
            //  };
            //  options.Converters.Add(new JsonStringEnumConverter());
            //  List<SkinRoutine>? routines = JsonSerializer.Deserialize<List<SkinRoutine>>(routinesData, options);
            //  Console.WriteLine($"Loaded routines: {routines?.Count ?? 0}");
            ////  routinePrinter.Print(routines[0]);

            //  var jsonpath = Path.Combine("Resources", "routines1.json");
            //  var repo = new RoutineRepository(jsonpath);

            //var printer = new RoutinePrinter();

            //var result = repo.GetBestRoutine(Core.Enums.SkinType.Oily, Core.Enums.WeatherType.Hot);
            //var result1 = repo.GetBestRoutine(Core.Enums.SkinType.Dry, Core.Enums.WeatherType.Humid);
            //var result2 = repo.GetBestRoutine(Core.Enums.SkinType.AcneProne, Core.Enums.WeatherType.Humid);
            //var result3 = repo.GetBestRoutine(Core.Enums.SkinType.Sensitive, Core.Enums.WeatherType.Cold);


            //Console.WriteLine($"Explanation: {result.Explanation}");
            //printer.Print(result.Routine);
            //Console.WriteLine("--------");

            //Console.WriteLine($"Explanation: {result1.Explanation}");
            //printer.Print(result1.Routine);
            //Console.WriteLine("--------");

            //Console.WriteLine($"Explanation: {result2.Explanation}");
            //printer.Print(result2.Routine);
            //Console.WriteLine("--------");

            //Console.WriteLine($"Explanation: {result3.Explanation}");
            //printer.Print(result3.Routine);

            //RecommendationRequest recommendationRequest = new RecommendationRequest();
            //RecommendationEngine recommendationEngine = new RecommendationEngine(repo);
            //recommendationEngine.Recommend(recommendationRequest);
            //routinePrinter.Print


            var repo = new RoutineRepository(Path.Combine("Resources", "routines1.json"));
            var engine = new RecommendationEngine(repo);
            var printer = new RoutinePrinter();

            // 1) No concerns
            var r1 = engine.Recommend(new RecommendationRequest
            {
                SkinType = SkinType.Oily,
                Weather = WeatherType.Hot,
                Concerns = null
            });
            Console.WriteLine("\n--- No concerns ---");
            Console.WriteLine(r1.Explanation);
            printer.Print(r1.Routine);

            // 2) Acne
            var r2 = engine.Recommend(new RecommendationRequest
            {
                SkinType = SkinType.Oily,
                Weather = WeatherType.Hot,
                Concerns = new List<SkinConcern> { SkinConcern.Acne }
            });
            Console.WriteLine("\n--- Acne ---");
            Console.WriteLine(r2.Explanation);
            printer.Print(r2.Routine);

            // 3) Acne + Sensitivity
            var r3 = engine.Recommend(new RecommendationRequest
            {
                SkinType = SkinType.Oily,
                Weather = WeatherType.Hot,
                Concerns = new List<SkinConcern> { SkinConcern.Acne, SkinConcern.Sensitivity }
            });
            Console.WriteLine("\n--- Acne + Sensitivity ---");
            Console.WriteLine(r3.Explanation);
            printer.Print(r3.Routine);


        }
    }
}
