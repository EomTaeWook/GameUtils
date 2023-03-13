using ShareLogic;

namespace ConsoleTest;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");


        BigNumber one = new BigNumber()
        {
            NumValue = 10,
            UnitValue = ""
        };
        BigNumber two = new BigNumber()
        {
            NumValue = 1000,
        };

        var result = one + two;

    }
}
