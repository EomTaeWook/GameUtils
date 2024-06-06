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


        var pair = Cryptogram.GenerateKeyPair();

        Cryptogram.InitializeWithPublicKey(pair.Item1);
        Cryptogram.InitializeWithPrivateKey(pair.Item2);
        var tt = Cryptogram.EncryptString("test");
        var text = Cryptogram.DecryptString(tt);



        var num1 = new BigNumber(11);
        var num2 = new BigNumber(999);

        Console.WriteLine(num1);
        Console.WriteLine(num2);

        Console.WriteLine(num1 + num2);
        Console.WriteLine(num2 - num1);
        Console.WriteLine(num1 - num2);
    }
}

