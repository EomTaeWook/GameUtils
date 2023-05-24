using System;
using System.Collections.Generic;

namespace GameUtils.Path
{
    public class AStartGenerator : IPathGenerator
    {
        internal class AStartNode : Node
        {
            public int G { get; set; }
            public int H { get; set; }

            public int F { get => G + H; }

            public AStartNode Parent { get; set; }

            public AStartNode(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        private readonly int[,] _map;
        private readonly int _width;
        private readonly int _height;
        private readonly int[] _dx = { -1, 0, 1, 0 };
        private readonly int[] _dy = { 0, 1, 0, -1 };
        public AStartGenerator(int[,] map)
        {
            _map = map;
            _width = map.GetLength(0);
            _height = map.GetLength(1);
        }
        public List<Node> FindPath(Node startNode, Node goalNode)
        {
            var openList = new List<AStartNode>(); // 탐색할 노드들의 목록
            var closedList = new List<AStartNode>(); // 탐색이 완료된 노드들의 목록
            openList.Add(new AStartNode(startNode.X, startNode.Y));

            while (openList.Count > 0)
            {
                AStartNode currentNode = openList[0];
                // 최소 F 값을 가지는 노드를 찾음
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].F < currentNode.F || openList[i].F == currentNode.F && openList[i].H < currentNode.H)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);
                if (currentNode.X == goalNode.X && currentNode.Y == goalNode.Y)
                {
                    // 목적지에 도착했을 때 경로를 반환
                    return GeneratePath(currentNode);
                }

                for (int i = 0; i < 4; i++)
                {
                    int neighborX = currentNode.X + _dx[i];
                    int neighborY = currentNode.Y + _dy[i];

                    if (neighborX >= 0 && neighborX < _width && neighborY >= 0 && neighborY < _height)
                    {
                        // 이웃 노드가 벽이 아닌 경우에만 처리
                        if (_map[neighborX, neighborY] == 0)
                        {
                            AStartNode neighborNode = new AStartNode(neighborX, neighborY);
                            int cost = 1;

                            if (closedList.Contains(neighborNode))
                            {
                                continue;
                            }

                            int score = currentNode.G + cost; // 출발지에서 현재 노드까지의 이동 비용
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

        private List<Node> GeneratePath(AStartNode goalNode)
        {
            List<Node> path = new List<Node>();

            AStartNode currentNode = goalNode;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }
        private int CalculateHeuristic(Node node, Node goalNode)
        {
            // Manhattan 거리를 휴리스틱으로 사용 (가로/세로 이동만 고려)
            return Math.Abs(node.X - goalNode.X) + Math.Abs(node.Y - goalNode.Y);
        }
    }
}
