using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinSync.Tests
{
    public static class TestHelpers
    {
        public static string GetTestJsonPath()
        {
            
            var baseDir = AppContext.BaseDirectory;

            var path = Path.Combine(baseDir, "TestData", "routines_test.json");
            return path;
        }
        
    }
}
