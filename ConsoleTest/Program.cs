using GameUtils;
using GameUtils.Crypto;
using GameUtils.Math;

namespace ConsoleTest;

internal class Program
{

    static void Main(string[] args)
    {
        //int width = 50;
        //int height = 50;

        //var map = new ProceduralMapGeneration(new RandomGenerator(-319073866), width, height, 10);
        //var grid = map.Generate();
        //map.Print(grid);

        var t = TimeHelper.CalculateStartOfWeek(DateTime.Now);

        t = TimeHelper.CalculateStartOfWeek(DateTime.Now.AddDays(-1));
        t = TimeHelper.CalculateStartOfWeek(DateTime.Now.AddDays(-2));
        t = TimeHelper.CalculateStartOfWeek(DateTime.Now.AddDays(-3));
        t = TimeHelper.CalculateStartOfWeek(DateTime.Now.AddDays(-4));
        t = TimeHelper.CalculateStartOfWeek(DateTime.Now.AddDays(-5));




        var pair = Cryptogram.GenerateKeyPair();
        var cryptogram = new Cryptogram();
        cryptogram.InitializeWithPublicKey(pair.Item1);
        cryptogram.InitializeWithPrivateKey(pair.Item2);
        var tt = cryptogram.EncryptString("test");
        var text = cryptogram.DecryptString(tt);



        var num1 = new BigNumber(11);
        var num2 = new BigNumber(999);

        Console.WriteLine(num1);
        Console.WriteLine(num2);

        Console.WriteLine(num1 + num2);
        Console.WriteLine(num2 - num1);
        Console.WriteLine(num1 - num2);
    }
}

