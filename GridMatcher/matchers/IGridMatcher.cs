using System.Collections.Generic;
using GridMatcher.Core;

namespace GridMatcher.Matchers
{
    public interface IGridMatcher
    {
        List<Match> FindMatches(Grid grid);
    }
}