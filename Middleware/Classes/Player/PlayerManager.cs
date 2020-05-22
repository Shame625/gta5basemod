using CitizenFX.Core;
using Middleware.Classes.Player;
using System.Collections.Generic;

namespace MiddlewareNamespace.Classes.Player
{
    public sealed class PlayerManager
    {
        public Dictionary<CitizenFX.Core.Player, PlayerExtension> playerExtendedList = new Dictionary<CitizenFX.Core.Player, PlayerExtension>();
        PlayerList pl = new PlayerList();
        private CitizenFX.Core.Player GetPlayer(int index)
        {
            return pl[index];
        }

        public ExtendendedPlayer GetExtendedPlayer(int source)
        {
            var player = GetPlayer(source);

            if (playerExtendedList.ContainsKey(player))
            {
                var current = playerExtendedList[player];
                if (current != null)
                {
                    return new ExtendendedPlayer { Player = player, Extensions = current };
                }
                else
                {
                    playerExtendedList[player] = new PlayerExtension();
                    return new ExtendendedPlayer { Player = player, Extensions = playerExtendedList[player] };
                }
            }
            else
            {
                playerExtendedList.Add(player, new PlayerExtension());
                return new ExtendendedPlayer { Player = player, Extensions = playerExtendedList[player] };
            }
        }

        public bool DestroyExtendedPlayer(CitizenFX.Core.Player player)
        {
            if (playerExtendedList.ContainsKey(player))
            {
                playerExtendedList.Remove(player);
                return true;
            }
            return false;
        }
        static PlayerManager()
        {
        }

        private PlayerManager()
        {
        }
        public static PlayerManager Instance { get; } = new PlayerManager();
    }
}
