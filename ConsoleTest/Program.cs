using GameUtils;
using GameUtils.Map;
using System.Text.Json;

namespace ConsoleTest;

internal class Program
{

    static void Main(string[] args)
    {
        int width = 50;
        int height = 50;

        var map = new BSPMapGenerator(new RandomGenerator(-319073866), width, height, 7);
        var grid = map.Generate();

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Console.Write(grid[x, y] == 1 ? "#" : ".");
            }
            Console.WriteLine();
        }
    }
}

