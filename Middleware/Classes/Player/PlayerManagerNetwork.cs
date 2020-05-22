using CitizenFX.Core;
using MiddlewareNamespace;
using MiddlewareNamespace.Classes.Player;
using System;

namespace Middleware.Classes.Player
{
    public sealed class PlayerManagerNetwork : BaseScript
    {
        static PlayerManagerNetwork()
        {
        }

        private PlayerManagerNetwork()
        {
        }

        public void RegisterPlayerManagerNetworkEvents(EventHandlerDictionary ev)
        {
            ev["PlayerDropped"] += new Action<CitizenFX.Core.Player, string>(OnPlayerDropped);
        }

        private void OnPlayerDropped([FromSource]CitizenFX.Core.Player player, string reason)
        {
            if (PlayerManager.Instance.DestroyExtendedPlayer(player))
                MessagesManager.Instance.DebugMessage(player.Name + " disconnected! Removing from ExtendedPlayerList!");
        }

        public static PlayerManagerNetwork Instance { get; } = new PlayerManagerNetwork();
    }
}
