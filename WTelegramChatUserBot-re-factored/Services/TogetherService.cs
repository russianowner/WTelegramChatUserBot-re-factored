using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WTelegramChatUserBot_re_factored.Services
{
    public class TogetherService
    {
        private readonly string _apiKey;
        private readonly HttpClient _http = new();

        public TogetherService(string apiKey)
        {
            _apiKey = apiKey;
        }
        public async Task<string?> GetReplyAsync(string prompt)
        {
            try
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                var payload = new
                {
                    model = "meta-llama/Llama-3.3-70B-Instruct-Turbo-Free",
                    messages = new[] { new { role = "user", content = prompt } },
                    temperature = 0.8,
                    max_tokens = 1000
                };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("https://api.together.xyz/v1/chat/completions", content);
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                using var doc = JsonDocument.Parse(body);
                var root = doc.RootElement;
                if (root.TryGetProperty("choices", out var choices) &&
                    choices.ValueKind == JsonValueKind.Array &&
                    choices.GetArrayLength() > 0)
                {
                    var msg = choices[0].GetProperty("message").GetProperty("content");
                    return msg.GetString()?.Trim();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
        }
    }
}
