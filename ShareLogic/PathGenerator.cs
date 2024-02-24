using GameUtils.Path;
using System;
using System.Collections.Generic;

namespace GameUtils
{
    public interface IPathGenerator
    {
        List<PathNode> Generate(PathNode startNode, PathNode goalNode, HashSet<ValueTuple<int, int>> excludedNodes = null, params int[] walkableValues);
    }
    public class PathGenerator
    {
        private readonly IPathGenerator _pathGenerator;
        public PathGenerator(IPathGenerator pathGenerator)
        {
            _pathGenerator = pathGenerator;
        }
        public List<PathNode> FindPath(PathNode startNode, PathNode goalNode, HashSet<ValueTuple<int, int>> excludedNodes, params int[] walkableValues)
        {
            if (excludedNodes == null)
            {
                excludedNodes = new HashSet<ValueTuple<int, int>>();
            }
            if (walkableValues == null)
            {
                walkableValues = Array.Empty<int>();
            }
            return _pathGenerator.Generate(startNode, goalNode, excludedNodes, walkableValues);
        }
    }
}
