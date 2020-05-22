using CitizenFX.Core;
using CitizenFX.Core.Native;
using MiddlewareNamespace.Classes.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Classes.Counter
{
    public class CounterCommands : BaseScript
    {
        public static void RegisterCounterCommands()
        {
            API.RegisterCommand("counter", new Action<int, List<object>, string>((source, args, rawCommand) =>
            {
                var extendedPlayer = PlayerManager.Instance.GetExtendedPlayer(source);

                if (args.Count == 0)
                {
                    extendedPlayer.Extensions.Counter.Number++;
                    TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { $"Current number: {extendedPlayer.Extensions.Counter.Number}" } });
                    return;
                }
                return;

            }), true);
        }
    }
}
