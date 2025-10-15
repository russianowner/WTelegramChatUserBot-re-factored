using System.Text;
using WTelegramChatUserBot_re_factored.Models;

namespace WTelegramChatUserBot_re_factored.Services
{
    public class HistoryService
    {
        private readonly Dictionary<long, List<MessageHistory>> _history = new();

        public void Update(long userId, string role, string text)
        {
            if (!_history.ContainsKey(userId))
                _history[userId] = new();
            _history[userId].Add(new MessageHistory(role, text));
            if (_history[userId].Count > 10)
                _history[userId].RemoveAt(0);
        }
        public string BuildPrompt(long userId, string newMsg)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Ваш промт на настроение");
            if (_history.ContainsKey(userId))
            {
                foreach (var msg in _history[userId])
                    sb.AppendLine($"{msg.Role}: {msg.Text}");
            }
            sb.AppendLine($"user: {newMsg}");
            sb.AppendLine("assistant:");
            return sb.ToString();
        }
    }
}
