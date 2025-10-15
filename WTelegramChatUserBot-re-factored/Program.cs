using WTelegramChatUserBot_re_factored.Bot;

class Program
{
    static async Task Main()
    {
        var telegramService = new TelegramService();
        await telegramService.RunAsync();
    }
}
