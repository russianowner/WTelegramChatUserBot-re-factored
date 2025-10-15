using System.Diagnostics;
using WTelegram;
using WTelegramChatUserBot_re_factored.Services;

namespace WTelegramChatUserBot_re_factored.Bot
{
    public class TelegramService
    {
        private Client? _client;
        private readonly ConfigService _config;
        private readonly MessageProcessor _processor;

        public TelegramService()
        {
            _config = new ConfigService();
            var together = new TogetherService(_config.TogetherApiKey);
            var history = new HistoryService();
            _processor = new MessageProcessor(together, history);
        }
        public async Task RunAsync()
        {
            _client = new Client(Config);
            var me = await _client.LoginUserIfNeeded();
            Console.WriteLine($"влогинил {me.username ?? me.first_name}");

            while (true)
            {
                var sw = Stopwatch.StartNew();
                var tasks = _config.TargetUsernames.Select(username => _processor.CheckAndReply(_client, username));
                await Task.WhenAll(tasks);
                sw.Stop();
                Console.WriteLine($"Цикл завершён за {sw.ElapsedMilliseconds} мс");
                await Task.Delay(TimeSpan.FromSeconds(_config.DelaySeconds));
            }
        }
        private string? Config(string what) => what switch
        {
            "api_id" => _config.ApiId,
            "api_hash" => _config.ApiHash,
            "phone_number" => _config.PhoneNumber,
            _ => null
        };
    }
}
