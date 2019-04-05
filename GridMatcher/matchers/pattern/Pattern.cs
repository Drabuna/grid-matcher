namespace GridMatcher.Matchers.Pattern
{
    public struct Pattern
    {
        private string _name;
        private TemplateTile[]_template;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public TemplateTile[] Template
        {
            get => _template;
            set => _template = value;
        }
    }
}