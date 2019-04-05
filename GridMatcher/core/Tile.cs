namespace GridMatcher.Core
{
    public struct Tile
    {
        private int _x;
        private int _y;

        private TileType _type;
        
        public Tile(int x, int y)
        {
            _x = x;
            _y = y;
            _type = TileType.Empty;
        }

        public Tile(int x, int y, TileType type)
        {
            _x = x;
            _y = y;
            _type = type;
        }

        public override string ToString()
        {
            return @"[" + _x + ", " + _y + ", " + _type + "]";
        }

        public int X => _x;

        public int Y => _y;

        public TileType Type
        {
            get => _type;
            set => _type = value;
        }
    }
}