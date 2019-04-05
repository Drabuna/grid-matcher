using GridMatcher.Core;
using GridMatcher.Generators;
using NUnit.Framework;

namespace GridMatcher.Tests.generators
{
    [TestFixture]
    public class InputGridGeneratorTest
    {
        [TestCase]
        public  void When_ProvidedValidInput_Expect_CorrectlyConfiguredGrid()
        {
            var data = @"[
                        [1, 2, 3],
                        [2, 1, 2],
                        [1, 3, 1],
                        [2, 1, 2]
            ]";

            var grid = new Grid(3, 4);
            var generator = new InputGridGenerator();
            generator.Input = data;
            generator.GenerateGrid(grid);
            
            //first column
            Assert.That(grid.GetTile(0,0).Type == TileType.Red);
            Assert.That(grid.GetTile(0,1).Type == TileType.Green);
            Assert.That(grid.GetTile(0,2).Type == TileType.Red);
            Assert.That(grid.GetTile(0,3).Type == TileType.Green);
            
            //second column
            Assert.That(grid.GetTile(1,0).Type == TileType.Green);
            Assert.That(grid.GetTile(1,1).Type == TileType.Red);
            Assert.That(grid.GetTile(1,2).Type == TileType.Yellow);
            Assert.That(grid.GetTile(1,3).Type == TileType.Red);
            
            //third column
            Assert.That(grid.GetTile(2,0).Type == TileType.Yellow);
            Assert.That(grid.GetTile(2,1).Type == TileType.Green);
            Assert.That(grid.GetTile(2,2).Type == TileType.Red);
            Assert.That(grid.GetTile(2,3).Type == TileType.Green);
        }
    }
}