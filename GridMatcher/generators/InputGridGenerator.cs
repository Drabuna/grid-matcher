using System.Collections.Generic;
using GridMatcher.Core;
using Newtonsoft.Json;

namespace GridMatcher.Generators
{
    /*
     * Given a proper two dimensional JSON array, containing one-letter colors, fills the grid accordingly.
     */
    public class InputGridGenerator:IGridGenerator
    {
        private string _input;
        
        public void GenerateGrid(Grid grid)
        {
            //TODO: input string requires validation, but let's skip it for now
            var tiles = JsonConvert.DeserializeObject<List<List<int>>>(_input);
            
            //we swap how we read the data, since we want to get data in, the way it was printed
            for (var y = 0; y < tiles[0].Count; y++)
            {
                for (var x = 0; x < tiles.Count; x++)
                {
                    grid.SetTileType(y, x, (TileType)tiles[x][y]);
                }
            }       
        }

        public string Input
        {
            get => _input;
            set => _input = value;
        }
    }
}