using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddlewareNamespace
{
    public class Middleware : BaseScript
    {
        static PlayerList pl = new PlayerList();
        public static bool gameState = false;
        public static int guessNumber;

        public static void MessageClients(string input)
        {
            TriggerClientEvent("chat:addMessage", new { color = new[] { 255, 255, 0 }, multiline = true, args = new string[] { $"[TestMod]{input}" } });
        }

        public static void MessageClient(Player player, string input)
        {
            TriggerClientEvent(player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { $"[TestMod]{input}" } });
        }

        public static void DebugMessage(string input)
        {
            Debug.WriteLine($"[TestMod][{DateTime.Now.ToString("HH:mm:ss")}] {input}");
        }

        public static Player GetPlayer(int index)
        {
            return pl[index];
        }

        private static Dictionary<Player, PlayerExtension> playerExtendedList = new Dictionary<Player, PlayerExtension>();
        public static ExtendendedPlayer GetExtendedPlayer(int source)
        {
            var player = GetPlayer(source);

            if(playerExtendedList.ContainsKey(player))
            {
                var current = playerExtendedList[player];
                if(current != null)
                {
                    return new ExtendendedPlayer { player = player, extension = current };
                }
                else
                {
                    playerExtendedList[player] = new PlayerExtension();
                    return new ExtendendedPlayer { player = player, extension = playerExtendedList[player] };
                }
            }
            else
            {
                playerExtendedList.Add(player, new PlayerExtension());
                return new ExtendendedPlayer { player = player, extension = playerExtendedList[player] };
            }
        }

        public static void Gamble(Player player, int number)
        {
            playerExtendedList[player].didGamble = true;
            playerExtendedList[player].guessNumber = number;
        }

        public static void ResetPlayers()
        {
            DebugMessage("Reseting players!");

            foreach(var v in playerExtendedList)
            {
                DebugMessage(v.Key.Name + " " + v.Value.ToString());
                v.Value.didGamble = false;
                v.Value.guessNumber = null;
            }
        }
        public static List<Player> GetWinners()
        {
            return playerExtendedList.Where(o => o.Value.guessNumber == guessNumber)
                .Select(o => o.Key)
                .ToList();
        }

        public class PlayerExtension
        {
            public bool? didGamble { get; set; }
            public int? guessNumber { get; set; }

            public override string ToString()
            {
                return $"didGamble: {didGamble} guessNumber: {guessNumber}";
            }
        }

        public class ExtendendedPlayer
        {
            public Player player { get; set; }
            public PlayerExtension extension { get; set; }
        }
    }
}
