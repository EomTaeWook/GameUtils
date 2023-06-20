using System;
using System.Collections.Generic;

namespace GameUtils.Path
{
    public class AStartPathGenerator : IPathGenerator
    {
        internal sealed class AStartNode : PathNode, IEquatable<AStartNode>
        {
            public int G { get; set; }
            public int H { get; set; }

            public int F { get => G + H; }

            public AStartNode Parent { get; set; }

            public AStartNode(int x, int y) : base(x, y)
            {
            }

            public bool Equals(AStartNode other)
            {
                return X == other.X && Y == other.Y;
            }
        }

        private readonly int[,] _map;
        private readonly int _width;
        private readonly int _height;
        private readonly int[] _dx = { -1, 0, 1, 0 };
        private readonly int[] _dy = { 0, 1, 0, -1 };

        public AStartPathGenerator(int width, int height)
        {
            _map = new int[width, height];
            _width = width;
            _height = height;
        }
        public AStartPathGenerator(int[,] map)
        {
            _map = map;
            _width = map.GetLength(0);
            _height = map.GetLength(1);
        }
        public List<PathNode> Generate(PathNode startNode, PathNode goalNode)
        {
            var openList = new List<AStartNode>();
            var closedList = new List<AStartNode>();
            openList.Add(new AStartNode(startNode.X, startNode.Y));

            while (openList.Count > 0)
            {
                AStartNode currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].F < currentNode.F || openList[i].F == currentNode.F && openList[i].H < currentNode.H)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);
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
                        if (_map[neighborX, neighborY] == 0)
                        {
                            AStartNode neighborNode = new AStartNode(neighborX, neighborY);
                            
                            if (closedList.Contains(neighborNode))
                            {
                                continue;
                            }

                            int score = currentNode.G + 1;
                            bool isBestScore = false;

                            if (!openList.Contains(neighborNode))
                            {
                                // 새로운 노드라면 추가
                                openList.Add(neighborNode);
                                isBestScore = true;
                            }
                            else if (score < neighborNode.G)
                            {
                                isBestScore = true;
                            }

                            if (isBestScore == true)
                            {
                                neighborNode.Parent = currentNode;
                                neighborNode.G = score;
                                neighborNode.H = CalculateHeuristic(neighborNode, goalNode);
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
            return Math.Abs(node.X - goalNode.X) + Math.Abs(node.Y - goalNode.Y);
        }
    }
}
