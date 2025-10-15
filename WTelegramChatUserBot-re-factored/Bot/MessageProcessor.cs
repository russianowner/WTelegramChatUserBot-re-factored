using TL;
using WTelegram;
using WTelegramChatUserBot_re_factored.Services;

namespace WTelegramChatUserBot_re_factored.Bot
{
    public class MessageProcessor
    {
        private readonly TogetherService _together;
        private readonly HistoryService _history;
        private readonly Dictionary<string, int> _lastMsgIds = new();

        public MessageProcessor(TogetherService together, HistoryService history)
        {
            _together = together;
            _history = history;
        }
        public async Task CheckAndReply(Client client, string username)
        {
            try
            {
                var me = await client.LoginUserIfNeeded();
                var resolved = await client.Contacts_ResolveUsername(username);
                var user = resolved.users.Values.OfType<User>().FirstOrDefault();
                if (user == null || user.access_hash == 0) return;
                var peer = new InputPeerUser(user.id, user.access_hash);
                var historyMsgs = await client.Messages_GetHistory(peer, limit: 1);
                var msg = historyMsgs.Messages.OfType<Message>().FirstOrDefault();
                if (msg == null || msg.from_id is PeerUser fromUser && fromUser.user_id == me.id)
                    return;
                if (_lastMsgIds.TryGetValue(username, out var lastId) && msg.id == lastId)
                    return;
                Console.WriteLine($"Новое сообщение от @{username}: {msg.message}");
                var prompt = _history.BuildPrompt(user.id, msg.message ?? "");
                var reply = await _together.GetReplyAsync(prompt);
                if (!string.IsNullOrWhiteSpace(reply))
                {
                    await client.SendMessageAsync(peer, reply);
                    Console.WriteLine($"Ответ @{username}: {reply}");
                    _history.Update(user.id, "user", msg.message ?? "");
                    _history.Update(user.id, "assistant", reply);
                    _lastMsgIds[username] = msg.id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"@{username}: {ex.Message}");
            }
        }
    }
}
