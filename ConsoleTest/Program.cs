using GameUtils;

namespace ConsoleTest;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        BigNumber one = new BigNumber(1000000, 0);
        Console.WriteLine(one.ToString());
        

    }
}
