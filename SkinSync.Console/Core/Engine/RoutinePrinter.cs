using SkinSync.Cli.Core.Enums;
using SkinSync.Cli.Core.Models;

namespace SkinSync.Cli.Core.Engine
{
    public class RoutinePrinter
    {
        public void Print(SkinRoutine routine)
        {
            
            Console.WriteLine($"Skin Type: {routine.SkinType}");
            Console.WriteLine($"Weather: {routine.Weather}");
            Console.WriteLine($"Cleanser: {routine.Cleanser}");
            Console.WriteLine($"Moisturizer: {routine.Moisturizer}");
            Console.WriteLine($"Sunscreen: {routine.Sunscreen}");
            Console.WriteLine($"Notes: {routine.Notes}");
        }
    }
}
