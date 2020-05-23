using CitizenFX.Core;
using Middleware.Classes.Player;
using System;
using System.Collections.Generic;

namespace MiddlewareNamespace.Classes.Player
{
    public sealed class PlayerManager : BaseScript
    {
        public Dictionary<int, ExtendendedPlayer> playerExtendedList = new Dictionary<int, ExtendendedPlayer>();
        PlayerList pl = new PlayerList();
        public string ConnectionString;
        dynamic ESX;
        private CitizenFX.Core.Player GetPlayer(int index)
        {
            return pl[index];
        }

        public ExtendendedPlayer GetExtendedPlayer(int source)
        {
            if(!playerExtendedList.ContainsKey(source))
            {
                var player = GetPlayer(source);
                var xPlayerIdentifier = ESX.GetPlayerFromId(source).getIdentifier();
                playerExtendedList.Add(source, new ExtendendedPlayer(player, xPlayerIdentifier));

                return playerExtendedList[source];
            }

            return playerExtendedList[source];
        }

        public bool DestroyExtendedPlayer(CitizenFX.Core.Player player)
        {
            if (playerExtendedList.ContainsKey(int.Parse(player.Handle)))
            {
                playerExtendedList[int.Parse(player.Handle)].Save();
                playerExtendedList.Remove(int.Parse(player.Handle));

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
        public void Init(string cs, dynamic esx, EventHandlerDictionary ev)
        {
            ConnectionString = cs;
            ESX = esx;
            ev["onResourceStop"] += new Action<string>(OnResourceStop);
        }
        private void OnResourceStop(string resourceName)
        {
            MessagesManager.Instance.DebugMessage(resourceName);
            if ("TestMod" != resourceName) return;

            MessagesManager.Instance.DebugMessage("Saving all players!");
            foreach (var v in playerExtendedList)
            {
                v.Value.Save();
            }
            MessagesManager.Instance.DebugMessage("Saving dong!");
        }
        public static PlayerManager Instance { get; } = new PlayerManager();
    }
}
