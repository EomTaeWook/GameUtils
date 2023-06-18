using GameUtils.Path;
using System;

namespace GameUtils.Map
{
    public class BSPMapGenerator : IMapGenerator
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


        private readonly int[,] _map;
        private readonly int MinRoomSize;
        private readonly RandomGenerator _randomGenerator;
        private const int CorridorWidth = 2;
        private Node<RoomContainer> _root;

        private RoomContainer _rootConatiner;
        private readonly AStartPathGenerator _pathGenerator;
        public BSPMapGenerator(int width, int height, int minRoomSize) : this(
            new RandomGenerator(DateTime.Now.GetHashCode()),
            width,
            height,
            minRoomSize)
        {
        }
        public BSPMapGenerator(RandomGenerator randomGenerator, int width, int height, int minRoomSize)
        {
            Console.WriteLine(randomGenerator.RandomSeed);

            _randomGenerator = randomGenerator;
            _map = new int[width, height];
            MinRoomSize = minRoomSize + 2;
            _rootConatiner = new RoomContainer()
            {
                X = 0,
                Y = 0,
                Width = width,
                Height = height
            };
        }
        private Room RoomGenerator(RoomContainer node)
        {
            return new Room();
        }
        private void AAAAA(RoomContainer container)
        {
            for (int i = 0; i < container.Height; ++i)
            {
                _map[container.X, container.Y + i] = 1;
                _map[container.X + container.Width - 1, container.Y + i] = 1;

            }
            for (int i = 0; i < container.Width; ++i)
            {
                _map[container.X + i, container.Y] = 1;
                _map[container.X + i, container.Y + container.Height - 1] = 1;
            }

            for (int y = 0; y < _rootConatiner.Height; ++y)
            {
                for (int x = 0; x < _rootConatiner.Width; ++x)
                {
                    Console.Write(_map[x, y] == 1 ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        
        private Node<RoomContainer> SplitContainer(RoomContainer roomContainer)
        {
            bool splitHorizontally = _randomGenerator.Next(0, 1) == 0;

            if (roomContainer.Width <= MinRoomSize * 2 && roomContainer.Height <= MinRoomSize * 2)
            {
                return null;
            }
            var node = new Node<RoomContainer>(roomContainer);
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
                var topContainer = new RoomContainer()
                {
                    X = roomContainer.X,
                    Y = roomContainer.Y,
                    Width = roomContainer.Width,
                    Height = splitHeight
                };

                var bottomContainer = new RoomContainer()
                {
                    X = roomContainer.X,
                    Y = roomContainer.Y + splitHeight,
                    Width = roomContainer.Width,
                    Height = roomContainer.Height - splitHeight
                };
                AAAAA(topContainer);
                AAAAA(bottomContainer);
                
                node.Left = SplitContainer(topContainer);
                node.Right = SplitContainer(bottomContainer);
            }
            else
            {
                int minValue = Convert.ToInt32(node.Item.Width * 0.4);
                int maxValue = Convert.ToInt32(node.Item.Width * 0.5);

                int splitWidth = _randomGenerator.Next(minValue, maxValue);

                var leftContainer = new RoomContainer()
                {
                    X = node.Item.X,
                    Y = node.Item.Y,
                    Width = splitWidth,
                    Height = node.Item.Height
                };
                var rightContainer = new RoomContainer()
                {
                    X = node.Item.X + splitWidth,
                    Y = node.Item.Y,
                    Width = node.Item.Width - splitWidth,
                    Height = node.Item.Height
                };

                AAAAA(leftContainer);
                AAAAA(rightContainer);

                node.Left = new Node<RoomContainer>(leftContainer);
                node.Right = new Node<RoomContainer>(rightContainer);

                node.Left = SplitContainer(leftContainer);
                node.Right = SplitContainer(rightContainer);
            }

            return node;
        }
        
        public int[,] Generate()
        {
            _root = SplitContainer(_rootConatiner);

            return _map;
        }
    }
}
