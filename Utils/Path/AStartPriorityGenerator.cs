using System;
using System.Collections.Generic;
using System.Linq;

namespace GameUtils.Path
{
    public class AStartPriorityGenerator : IPathGenerator
    {
        private readonly int[,] _map;
        private readonly int _width;
        private readonly int _height;
        private readonly int[] _dx = { -1, 0, 1, 0 };
        private readonly int[] _dy = { 0, 1, 0, -1 };

        public AStartPriorityGenerator(int width, int height)
        {
            _map = new int[width, height];
            _width = width;
            _height = height;
        }
        public AStartPriorityGenerator(int[,] map)
        {
            _map = map;
            _width = map.GetLength(0);
            _height = map.GetLength(1);
        }
        public List<PathNode> Generate(PathNode startNode,
            PathNode goalNode,
            HashSet<ValueTuple<int, int>> excludedNodes,
            params int[] walkableValues)
        {
            var openList = new List<AStartNode>();
            var closedList = new HashSet<(int, int)>();
            var startAStarNode = new AStartNode(startNode.X, startNode.Y)
            {
                G = 0,
                H = CalculateHeuristic(startNode, goalNode)
            };
            openList.Add(startAStarNode);
            excludedNodes.Remove((goalNode.X, goalNode.Y));

            while (openList.Count > 0)
            {
                AStartNode currentNode = openList.OrderBy(node => node.F)
                    .ThenBy(node => node.G).First();
                openList.Remove(currentNode);
                closedList.Add((currentNode.X, currentNode.Y));

                if (currentNode.Equals(goalNode))
                {
                    return GeneratePath(currentNode);
                }

                for (int i = 0; i < 4; i++)
                {
                    int neighborX = currentNode.X + _dx[i];
                    int neighborY = currentNode.Y + _dy[i];

                    if (neighborX >= 0 && neighborX < _width && neighborY >= 0 && neighborY < _height)
                    {
                        int mapValue = _map[neighborX, neighborY];
                        var neighbor = (neighborX, neighborY);
                        if (walkableValues.Contains(mapValue)
                            && !closedList.Contains(neighbor)
                            && !excludedNodes.Contains(neighbor))
                        {
                            int index = Array.IndexOf(walkableValues, mapValue);

                            if (index == -1)
                            {
                                continue;
                            }
                            AStartNode neighborNode = new AStartNode(neighborX, neighborY)
                            {
                                Parent = currentNode,
                                G = currentNode.G + index * 2 + 1,
                                H = CalculateHeuristic(new PathNode(neighborX, neighborY), goalNode)
                            };
                            if (!openList.Any(n => n.X == neighborX && n.Y == neighborY))
                            {
                                openList.Add(neighborNode);
                            }
                        }
                    }
                }
            }
            return null;
        }
        private List<PathNode> GeneratePath(AStartNode goalNode)
        {
            List<PathNode> path = new List<PathNode>();
            AStartNode currentNode = goalNode;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }
        private int CalculateHeuristic(PathNode node, PathNode goalNode)
        {
            return System.Math.Abs(node.X - goalNode.X) + System.Math.Abs(node.Y - goalNode.Y);
        }
    }
}
