using System;

namespace GridMatcher.Core
{
    public struct Match
    {
        private Tile[] _tiles;
        private string _name;

        public Match(Tile[] tiles, string name)
        {
            _tiles = tiles;
            _name = name;
        }

        public override string ToString()
        {
           return @"Match - " + _name + " " + string.Join(", ", _tiles);
        }

        public bool TilesColorsMatch(TileType color)
        {
            return Array.TrueForAll(_tiles, tile => tile.Type == color);
        }

        public Tile[] Tiles => _tiles;

        public string Name => _name;
    }
}