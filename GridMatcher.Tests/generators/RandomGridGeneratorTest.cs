using NUnit.Framework;
using GridMatcher.Core;
using GridMatcher.Generators;

namespace GridMatcher.Tests.generators
{
    [TestFixture]
    public class RandomGridGeneratorTest
    {
        [TestCase]
        public void When_RandomGridGenerated_Expect_NoEmptyTiles()
        {
            var grid = new Grid(5, 5);
            var generator = new RandomGridGenerator();
            generator.Seed = 45678;
            generator.GenerateGrid(grid);

            for (var x = 0; x < grid.Size.Width; x++)
            {
                for (var y = 0; y < grid.Size.Height; y++)
                {
                    Assert.That(grid.GetTile(x, y).Type != TileType.Empty);
                }
            }
        }
        
    }
}