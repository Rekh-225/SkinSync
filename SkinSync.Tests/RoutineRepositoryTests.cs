using SkinSync.Cli.Core.Enums;
using SkinSync.Cli.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinSync.Tests
{

    public class RoutineRepositoryTests
    {
        private static RoutineRepository CreateRepo()
        {
            var path = TestHelpers.GetTestJsonPath();
            Assert.True(File.Exists(path), $"Test JSON not found at: {path}");

            return new RoutineRepository(path);
        }

        [Fact]
        public void GetBestRoutine_ExactMatch_ReturnsExactRoutine()
        {
            // Arrange
            var repo = CreateRepo();

            // Act
            var result = repo.GetBestRoutine(SkinType.Oily, WeatherType.Hot);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Routine);
            Assert.Equal(SkinType.Oily, result.Routine.SkinType);
            Assert.Equal(WeatherType.Hot, result.Routine.Weather);
            Assert.Contains("exact", result.Explanation, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetBestRoutine_SkinTypeFallback_ReturnsSameSkinType()
        {
            // Arrange
            var repo = CreateRepo();

            // Act (Dry+Cold does not exist in test JSON, but Dry exists for Hot)
            var result = repo.GetBestRoutine(SkinType.Dry, WeatherType.Cold);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Routine);
            Assert.Equal(SkinType.Dry, result.Routine.SkinType);
            Assert.Contains("skintype", result.Explanation, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("fallback", result.Explanation, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetBestRoutine_WeatherFallback_ReturnsSameWeather()
        {
            // Arrange
            var repo = CreateRepo();

            // Act (Humid exists only for Normal in test JSON; AcneProne doesn't exist)
            var result = repo.GetBestRoutine(SkinType.AcneProne, WeatherType.Humid);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Routine);
            Assert.Equal(WeatherType.Humid, result.Routine.Weather);
            Assert.Contains("weather", result.Explanation, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("fallback", result.Explanation, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetBestRoutine_DefaultFallback_ReturnsNormalModerate()
        {
            // Arrange
            var repo = CreateRepo();

            // Act (Sensitive+Cold does not exist in test JSON)
            var result = repo.GetBestRoutine(SkinType.Sensitive, WeatherType.Cold);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Routine);
            Assert.Equal(SkinType.Normal, result.Routine.SkinType);
            Assert.Equal(WeatherType.Moderate, result.Routine.Weather);
            Assert.Contains("default", result.Explanation, StringComparison.OrdinalIgnoreCase);
        }
    }
}

