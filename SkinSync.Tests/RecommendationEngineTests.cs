using System;
using System.IO;
using System.Collections.Generic;
using SkinSync.Cli.Core.Engine;
using SkinSync.Cli.Core.Enums;
using SkinSync.Cli.Core.Models;
using SkinSync.Cli.Data;
using Xunit;

namespace SkinSync.Tests
{
    public class RecommendationEngineTests
    {
        private static RecommendationEngine CreateEngine()
        {
            var path = TestHelpers.GetTestJsonPath();
            Assert.True(File.Exists(path), $"Test JSON not found at: {path}");

            var repo = new RoutineRepository(path);
            return new RecommendationEngine(repo);
        }

        [Fact]
        public void Recommend_NoConcerns_DoesNotAddTips()
        {
            // Arrange
            var engine = CreateEngine();

            var request = new RecommendationRequest
            {
                SkinType = SkinType.Oily,
                Weather = WeatherType.Hot,
                Concerns = null
            };

            // Act
            var result = engine.Recommend(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Routine);
            Assert.DoesNotContain("Tip (Acne)", result.Routine.Notes ?? string.Empty, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Tip (Sensitivity)", result.Routine.Notes ?? string.Empty, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("no concern", result.Explanation, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Recommend_Acne_AddsAcneTip()
        {
            // Arrange
            var engine = CreateEngine();

            var request = new RecommendationRequest
            {
                SkinType = SkinType.Oily,
                Weather = WeatherType.Hot,
                Concerns = new List<SkinConcern> { SkinConcern.Acne }
            };

            // Act
            var result = engine.Recommend(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Routine);
            Assert.Contains("Tip (Acne)", result.Routine.Notes ?? string.Empty, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("acne", result.Explanation, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Recommend_AcneAndSensitivity_AddsBothTips()
        {
            // Arrange
            var engine = CreateEngine();

            var request = new RecommendationRequest
            {
                SkinType = SkinType.Oily,
                Weather = WeatherType.Hot,
                Concerns = new List<SkinConcern> { SkinConcern.Acne, SkinConcern.Sensitivity }
            };

            // Act
            var result = engine.Recommend(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Routine);

            var notes = result.Routine.Notes ?? string.Empty;

            Assert.Contains("Tip (Acne)", notes, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("Tip (Sensitivity)", notes, StringComparison.OrdinalIgnoreCase);

            Assert.Contains("acne", result.Explanation, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("sensitivity", result.Explanation, StringComparison.OrdinalIgnoreCase);
        }
    }
}
