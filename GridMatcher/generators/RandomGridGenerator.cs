using System;
using GridMatcher.Core;

namespace GridMatcher.Generators
{
    public class RandomGridGenerator: IGridGenerator
    {
        private int _seed;
        
        public void GenerateGrid(Grid grid)
        {
            var random = new Random(_seed);
            TileType[] possibleTypes = {TileType.Red, TileType.Green, TileType.Yellow};
            
            for (var x = 0; x < grid.Size.Width; x++)
            {
                for (var y = 0; y < grid.Size.Height; y++)
                {
                    var randomType = possibleTypes[random.Next(possibleTypes.Length)];
                    grid.SetTileType(x, y, randomType);
                }
            }
        }

        public int Seed
        {
            get => _seed;
            set => _seed = value;
        }
    }
}