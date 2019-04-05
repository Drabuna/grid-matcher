namespace GridMatcher.Types
{
    public struct Size
    {
        private int _width;
        private int _height;

        public Size(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public int Width
        {
            get => _width;
            set => _width = value;
        }

        public int Height
        {
            get => _height;
            set => _height = value;
        }
    }
}