using GameUtils.Path;
using System.Collections.Generic;

namespace GameUtils
{
    public interface IPathGenerator
    {
        List<Node> FindPath(Node start, Node goal);
    }
    public class PathGenerator
    {
        private readonly IPathGenerator _pathGenerator;
        public PathGenerator(IPathGenerator pathGenerator)
        {
            _pathGenerator = pathGenerator;
        }
        public List<Node> FindPath(Node start, Node goal)
        {
            return _pathGenerator.FindPath(start, goal);
        }
    }
}
