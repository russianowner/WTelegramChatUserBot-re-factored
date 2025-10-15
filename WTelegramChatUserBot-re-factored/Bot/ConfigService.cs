using Microsoft.Extensions.Configuration;

namespace WTelegramChatUserBot_re_factored.Bot
{
    public class ConfigService
    {
        private readonly IConfiguration _config;

        public ConfigService()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
        public string ApiId => _config["Telegram:ApiId"] ?? throw new Exception("апи ида нет");
        public string ApiHash => _config["Telegram:ApiHash"] ?? throw new Exception("хеша нет");
        public string PhoneNumber => _config["Telegram:PhoneNumber"] ?? throw new Exception("мобилы нет");
        public string TogetherApiKey => _config["Together:ApiKey"] ?? throw new Exception("апи кей отстуствует");
        public string[] TargetUsernames => _config.GetSection("Targets").Get<string[]>() ?? Array.Empty<string>();
        public int DelaySeconds => int.TryParse(_config["DelaySeconds"], out var v) ? v : 10;
    }
}
