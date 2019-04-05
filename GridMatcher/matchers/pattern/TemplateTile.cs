namespace GridMatcher.Matchers.Pattern
{
    public struct TemplateTile
    {
        private int _x;
        private int _y;
        private TemplateTileType _color;

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public TemplateTileType Color
        {
            get => _color;
            set => _color = value;
        }
    }
}