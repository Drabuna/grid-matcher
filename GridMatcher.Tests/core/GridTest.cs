using NUnit.Framework;
using GridMatcher.Core;

namespace Tests.core
{
    [TestFixture]
    public class GridTest
    {
        [TestCase]
        public void When_NewGridCreated_Expect_CorrectSize()
        {
            var grid = new Grid(5, 5);
            Assert.That(grid.Size.Width == 5 && grid.Size.Height == 5);
        }

        [TestCase]
        public void When_NewGridCreated_Expect_EmptyPopulatedGrid()
        {
            var grid = new Grid(5, 5);
            for (var x = 0; x < grid.Size.Width; x++)
            {
                for (var y = 0; y < grid.Size.Height; y++)
                {
                    Assert.That(grid.GetTile(x, y).Type == TileType.Empty);
                }
            }
        }
        
    }
}