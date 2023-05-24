using GameUtils;
using System.Text.Json;

namespace ConsoleTest;

internal class Program
{
    class TT
    {
        public string Prompt { get; set; }
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        BigNumber one = new BigNumber(1000000, 0);
        Console.WriteLine(one.ToString());

        var requester = new HttpRequestHelper();

        var json = JsonSerializer.Serialize(new TT()
        {
            Prompt = "portrait of a boy holding an apple, gustave climpt, oil paint, high quality, concept art"
        });

        

    }

}
