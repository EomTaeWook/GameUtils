using GameUtils.Internals;
using GameUtils.Path;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameUtils.Map
{
    public class ProceduralMapGeneration : IMapGenerator
    {
        private class RoomContainer
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int CenterX { get => X + Width / 2; }
            public int CenterY { get => Y + Height / 2; }
        }
        private class Room
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        private readonly int MinRoomSize;
        private readonly RandomGenerator _randomGenerator;

        private readonly RoomContainer _rootConatiner;
        private readonly AStartPriorityGenerator _allPathGenerator;
        public ProceduralMapGeneration(int width, int height, int minRoomSize) : this(
            new RandomGenerator(DateTime.Now.GetHashCode()),
            width,
            height,
            minRoomSize)
        {
        }
        public ProceduralMapGeneration(RandomGenerator randomGenerator, int width, int height, int minRoomSize)
        {
            Console.WriteLine(randomGenerator.RandomSeed);

            _randomGenerator = randomGenerator;

            MinRoomSize = minRoomSize + 2;
            _rootConatiner = new RoomContainer()
            {
                X = 0,
                Y = 0,
                Width = width,
                Height = height
            };

            _allPathGenerator = new AStartPriorityGenerator(width, height);

        }
        private void Fill(int[,] array, int value)
        {
            Parallel.For(0, array.GetLength(0), (x) =>
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = value;
                }
            });
        }
        private List<RoomContainer> GetRoomContainer(Node<RoomContainer> node)
        {
            var list = new List<RoomContainer>();
            if (node.Left == null && node.Right == null)
            {
                list.Add(node.Item);
                return list;
            }
            list.AddRange(GetRoomContainer(node.Left));
            list.AddRange(GetRoomContainer(node.Right));
            return list;
        }

        private List<Room> MakeRoom(Node<RoomContainer> node, int[,] map)
        {
            var rooms = new List<Room>();

            if (node.Left == null && node.Right == null)
            {
                rooms.Add(RoomGenerator(node.Item, map));
                return rooms;
            }
            rooms.AddRange(MakeRoom(node.Left, map));
            rooms.AddRange(MakeRoom(node.Right, map));
            return rooms;

        }
        private Room RoomGenerator(RoomContainer node, int[,] map)
        {
            var nodeWidth = node.Width - 1;
            var nodeHeight = node.Height - 1;
            var nodeX = node.X + 1;
            var nodeY = node.Y + 1;
            var room = new Room
            {
                X = nodeX + _randomGenerator.Next(0, node.Width / 3),
                Y = nodeY + _randomGenerator.Next(0, node.Height / 3)
            };

            room.Width = nodeWidth - (room.X - node.X);
            room.Height = nodeHeight - (room.Y - node.Y);

            room.Width -= _randomGenerator.Next(0, node.Width / 3);
            room.Height -= _randomGenerator.Next(0, node.Height / 3);

            for (int y = 0; y < room.Height; ++y)
            {
                for (int x = 0; x < room.Width; ++x)
                {
                    if (map[room.X + x, room.Y + y] == 0)
                    {
                        continue;
                    }
                    map[room.X + x, room.Y + y] = 0;
                }
            }
            return room;
        }
        private void MakePath(Node<RoomContainer> node, int[,] map, IPathGenerator[] pathGenerators)
        {
            if (node.Left == null || node.Right == null)
                return;

            var paths = ConnectRoom(node.Left.Item, node.Right.Item, pathGenerators);
            if (paths == null)
            {
                throw new InvalidOperationException("not found path");
            }

            for (int i = 0; i < paths.Count; ++i)
            {
                map[paths[i].X, paths[i].Y] = 0;
            }

            MakePath(node.Left, map, pathGenerators);
            MakePath(node.Right, map, pathGenerators);
        }
        private List<PathNode> ConnectRoom(RoomContainer start, RoomContainer end, IPathGenerator[] pathGenerators)
        {
            var startNode = new PathNode(start.CenterX, start.CenterY);
            var endNode = new PathNode(end.CenterX, end.CenterY);

            foreach (var pathGenerator in pathGenerators)
            {
                var paths = pathGenerator.Generate(
                startNode,
                endNode,
                null,
                0);

                if (paths != null)
                {
                    return paths;
                }
            }
            return null;
        }
        private Tuple<RoomContainer, RoomContainer> GetRandomSplitContainer(RoomContainer roomContainer)
        {
            bool splitHorizontally = _randomGenerator.Next(0, 1) == 0;

            if (roomContainer.Width <= MinRoomSize * 2 && roomContainer.Height <= MinRoomSize * 2)
            {
                return null;
            }

            RoomContainer roomContainer1, roomContainer2;
            if (roomContainer.Width <= MinRoomSize)
            {
                splitHorizontally = true;
            }
            else if (roomContainer.Height <= MinRoomSize)
            {
                splitHorizontally = false;
            }

            if (splitHorizontally)
            {
                int minValue = Convert.ToInt32(roomContainer.Height * 0.4);
                int maxValue = Convert.ToInt32(roomContainer.Height * 0.5);
                int splitHeight = _randomGenerator.Next(minValue, maxValue);
                roomContainer1 = new RoomContainer()
                {
                    X = roomContainer.X,
                    Y = roomContainer.Y,
                    Width = roomContainer.Width,
                    Height = splitHeight
                };

                roomContainer2 = new RoomContainer()
                {
                    X = roomContainer.X,
                    Y = roomContainer.Y + splitHeight,
                    Width = roomContainer.Width,
                    Height = roomContainer.Height - splitHeight
                };
            }
            else
            {
                int minValue = Convert.ToInt32(roomContainer.Width * 0.4);
                int maxValue = Convert.ToInt32(roomContainer.Width * 0.5);

                int splitWidth = _randomGenerator.Next(minValue, maxValue);

                roomContainer1 = new RoomContainer()
                {
                    X = roomContainer.X,
                    Y = roomContainer.Y,
                    Width = splitWidth,
                    Height = roomContainer.Height
                };
                roomContainer2 = new RoomContainer()
                {
                    X = roomContainer.X + splitWidth,
                    Y = roomContainer.Y,
                    Width = roomContainer.Width - splitWidth,
                    Height = roomContainer.Height
                };
            }
            return Tuple.Create(roomContainer1, roomContainer2);
        }

        private Node<RoomContainer> SplitContainer(RoomContainer roomContainer)
        {
            var node = new Node<RoomContainer>(roomContainer);

            var sc = GetRandomSplitContainer(roomContainer);
            if (sc != null)
            {
                node.Left = SplitContainer(sc.Item1);
                node.Right = SplitContainer(sc.Item2);
            }

            return node;
        }

        public int[,] Generate()
        {
            var map = new int[_rootConatiner.Width, _rootConatiner.Height];

            Fill(map, 1);
            var rootNode = SplitContainer(_rootConatiner);
            MakePath(rootNode, map, new IPathGenerator[] { new AStartPriorityGenerator(map), _allPathGenerator });
            MakeRoom(rootNode, map);

            return map;
        }
        public void Print(int[,] map)
        {
            for (int y = 0; y < _rootConatiner.Height; ++y)
            {
                for (int x = 0; x < _rootConatiner.Width; ++x)
                {
                    Console.Write(map[x, y] == 1 ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
