using SkinSync.Cli.Core.Engine;
using SkinSync.Cli.Core.Models;
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
            RoutinePrinter routinePrinter = new RoutinePrinter();

            var path =Path.Combine("Resources","routines.json"); 
             var routinesData = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            List<SkinRoutine>? routines = JsonSerializer.Deserialize<List<SkinRoutine>>(routinesData, options);
            Console.WriteLine($"Loaded routines: {routines?.Count ?? 0}");
            routinePrinter.Print(routines[0]);

        }
    }
}
