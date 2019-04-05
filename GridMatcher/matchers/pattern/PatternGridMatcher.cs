using System.Collections.Generic;
using GridMatcher.Core;

namespace GridMatcher.Matchers.Pattern
{
    public class PatternGridMatcher:IGridMatcher
    {
        private Pattern[] _patterns;

        private Dictionary<TileType, TemplateTileType> _colorMap = new Dictionary<TileType, TemplateTileType>
            {
                {TileType.Red, TemplateTileType.Red},
                {TileType.Green, TemplateTileType.Green},
                {TileType.Yellow, TemplateTileType.Yellow}
            };
            
        public List<Match> FindMatches(Grid grid)
        {
            var foundMatches = new List<Match>();
            for (var x = 0; x < grid.Size.Width; x++)
            {
                for (var y = 0; y < grid.Size.Height; y++)
                {
                    var tile = grid.GetTile(x, y);
                    if(tile.Type != TileType.Empty) foundMatches.AddRange(FindMatchesFromTile(tile, grid));
                }
            }
            return foundMatches;
        }

        private List<Match> FindMatchesFromTile(Tile tile, Grid grid)
        {
            var allMatches = new List<Match>();
            //we'll go though all the patterns we have
            foreach (var pattern in _patterns)
            {
                //create a list of tiles
                var matchedTiles = new List<Tile>();
                
                //keep track of the first color
                var firstColor = _colorMap[tile.Type];
                //loop through all the specific pattern template tiles
                //we will use template tiles positions as an offset to the input tile
                //using that offset we will get those tiles from the grid, and see if they meet color requirements
                foreach (var templateTile in pattern.Template)
                {
                    var xPos = tile.X + templateTile.X;
                    var yPos = tile.Y + templateTile.Y;
                    
                    if (xPos < 0 || xPos >= grid.Size.Width || yPos < 0 || yPos >= grid.Size.Height) break;

                    var gridTile = grid.GetTile(xPos, yPos);

                    if(gridTile.Type == TileType.Empty) break;
                    if (!MeetsColorRequirements(_colorMap[gridTile.Type], templateTile.Color, firstColor)) break;
                    
                    matchedTiles.Add(gridTile);
                }
                
                //check if list of tiles equals length of pattern
                if (matchedTiles.Count == pattern.Template.Length)
                {
                    allMatches.Add(new Match(matchedTiles.ToArray(), pattern.Name));
                }
            }
            return allMatches;
        }

        private bool MeetsColorRequirements(TemplateTileType originalColor, TemplateTileType requiredColor,
            TemplateTileType firstColor = TemplateTileType.Any)
        {
            switch (requiredColor)
            {
                case TemplateTileType.Any:
                    return true;
                case TemplateTileType.LikeFirst when firstColor == TemplateTileType.Any:
                    return true;
                case TemplateTileType.LikeFirst when firstColor != TemplateTileType.Any:
                    return originalColor == firstColor;
                default:
                    return originalColor == requiredColor;
            }
        }

        public Pattern[] Patterns
        {
            get => _patterns;
            set => _patterns = value;
        }
    }
}