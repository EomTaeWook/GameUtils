using GameUtils.Path;
using System.Collections.Generic;

namespace GameUtils
{
    public interface IPathGenerator
    {
        List<PathNode> Generate(PathNode start, PathNode goal);
    }
    public class PathGenerator
    {
        private readonly IPathGenerator _pathGenerator;
        public PathGenerator(IPathGenerator pathGenerator)
        {
            _pathGenerator = pathGenerator;
        }
        public List<PathNode> FindPath(PathNode start, PathNode goal)
        {
            return _pathGenerator.Generate(start, goal);
        }
    }
}
