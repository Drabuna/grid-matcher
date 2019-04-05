using System.IO;
using GridMatcher.Matchers.Pattern;
using Newtonsoft.Json;

namespace GridMatcher.Loaders
{
    public class PatternsLoader
    {
        public Pattern[] Load(string filePath)
        {
            var data = File.ReadAllText(filePath);
            var patterns = JsonConvert.DeserializeObject<Pattern[]>(data);
            return patterns;
        }
    }
}