using System;
using GridMatcher.Types;

namespace GridMatcher.Core
{
    public class Grid
    {
        private Size _size;
        private Tile[,] _tiles;

        public Grid(int width, int height)
        {
            _size = new Size(width,  height);
            CreateEmptyGrid();
        }

        public Grid(Size size)
        {
            _size = new Size(size.Width, size.Height);
            CreateEmptyGrid();
        }

        private void CreateEmptyGrid()
        {
            _tiles = new Tile[_size.Width,_size.Height];
            
            for (var x = 0; x < _size.Width; x++)
            {
                for (var y = 0; y < _size.Height; y++)
                {
                    _tiles[x, y] = new Tile(x, y);
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < _size.Width && y >= 0 && y < _size.Height)
            {
                return _tiles[x, y];
            }
            throw new IndexOutOfRangeException("Tile is out of bounds");
        }

        public void SetTileType(int x, int y, TileType type)
        {
            if (x < 0 || x >= _size.Width || y < 0 || y >= _size.Height)
                throw new IndexOutOfRangeException("Tile is out of bounds");
            _tiles[x, y].Type = type;
        }

        public Size Size => _size;
    }
}