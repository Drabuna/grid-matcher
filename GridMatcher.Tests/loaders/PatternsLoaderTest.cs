using GridMatcher.Loaders;
using GridMatcher.Matchers.Pattern;
using NUnit.Framework;

namespace GridMatcher.Tests.loaders
{
    [TestFixture]
    public class PatternsLoaderTest
    {
        [TestCase]
        public void When_GivenValidPatternsFile_Expect_CorrectlyLoadedPatterns()
        {
            var loader = new PatternsLoader();
            var patterns = loader.Load(@"resources\test_patterns.json");
            Assert.That(patterns.Length == 2);
            
            Assert.That(patterns[0].Name == "Vertical Match 3");
            Assert.That(patterns[0].Template[0].X == 0 && patterns[0].Template[0].Y == 0 &&  patterns[0].Template[0].Color == TemplateTileType.Any);
            Assert.That(patterns[0].Template[1].X == 0 && patterns[0].Template[1].Y == 1 &&  patterns[0].Template[1].Color == TemplateTileType.LikeFirst);
            Assert.That(patterns[0].Template[2].X == 0 && patterns[0].Template[2].Y == 2 &&  patterns[0].Template[2].Color == TemplateTileType.LikeFirst);
            
            Assert.That(patterns[1].Name == "Horizontal Match 4");
            Assert.That(patterns[1].Template[0].X == 0 && patterns[1].Template[0].Y == 0 &&  patterns[1].Template[0].Color == TemplateTileType.Any);
            Assert.That(patterns[1].Template[1].X == 1 && patterns[1].Template[1].Y == 0 &&  patterns[1].Template[1].Color == TemplateTileType.LikeFirst);
            Assert.That(patterns[1].Template[2].X == 2 && patterns[1].Template[2].Y == 0 &&  patterns[1].Template[2].Color == TemplateTileType.LikeFirst);
            Assert.That(patterns[1].Template[3].X == 3 && patterns[1].Template[3].Y == 0 &&  patterns[1].Template[3].Color == TemplateTileType.LikeFirst);
        }
    }
}