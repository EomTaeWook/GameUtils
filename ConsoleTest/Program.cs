using GameUtils;
using GameUtils.Map;

namespace ConsoleTest;

internal class Program
{

    static void Main(string[] args)
    {
        int width = 50;
        int height = 50;

        var map = new ProceduralMapGeneration(new RandomGenerator(-319073866), width, height, 10);
        var grid = map.Generate();
        map.Print(grid);
    }
}

