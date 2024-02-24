using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameUtils
{
    public class HttpRequester
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> PostByJsonAsync(string url, string json, int millisecondsDelay = 5000)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var tokenSource = new CancellationTokenSource(millisecondsDelay);

            var response = await _httpClient.PostAsync(url, content, tokenSource.Token);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
        public async Task<string> GetAsync(string url, Dictionary<string, string> parameters = null, int millisecondsDelay = 5000)
        {
            var tokenSource = new CancellationTokenSource(millisecondsDelay);

            var sb = new StringBuilder();
            sb.Append(url);

            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            if (parameters.Count > 0)
            {
                sb.Append('?');
                foreach (var item in parameters)
                {
                    string key = Uri.EscapeDataString(item.Key);
                    string value = Uri.EscapeDataString(item.Value);
                    sb.Append($"{key}={value}&");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            var response = await _httpClient.GetAsync(sb.ToString(), tokenSource.Token);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }

}
