using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameUtils
{
    public class HttpRequestHelper
    {
        private static readonly HttpClient _webClient = new HttpClient();

        public async Task<string> PostByJsonAsync(string url, string json, int millisecondsDelay = 5000)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var tokenSource = new CancellationTokenSource(millisecondsDelay);

            var response = await _webClient.PostAsync(url, content, tokenSource.Token);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }

}
