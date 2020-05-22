using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareNamespace
{
    public class MessagesManager : BaseScript
    {
        public void MessageClients(string input)
        {
            TriggerClientEvent("chat:addMessage", new { color = new[] { 255, 255, 0 }, multiline = true, args = new string[] { $"[TestMod]{input}" } });
        }

        public void MessageClient(Player player, string input)
        {
            TriggerClientEvent(player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { $"[TestMod]{input}" } });
        }

        public void DebugMessage(string input)
        {
            Debug.WriteLine($"[TestMod][{DateTime.Now.ToString("HH:mm:ss")}] {input}");
        }

        static MessagesManager()
        {
        }

        private MessagesManager()
        {
        }
        public static MessagesManager Instance { get; } = new MessagesManager();
    }
}
