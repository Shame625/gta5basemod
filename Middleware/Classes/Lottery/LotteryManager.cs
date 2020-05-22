using MiddlewareNamespace.Classes.Player;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddlewareNamespace.Classes.Lottery
{
    public sealed class LotteryManager
    {
        readonly Random random = new Random();
        public bool gameState = false;
        public int guessNumber;
        public void StartGame()
        {
            var number = random.Next(1, 100);
            guessNumber = number;
            gameState = true;

            MessagesManager.Instance.DebugMessage($"Guessing started. Lucky Number: {guessNumber}");
            MessagesManager.Instance.MessageClients("Welcome! Lottery is on! Guess the number between 1-100 by typing /lottery [1-100]");
        }
        public void EndGame()
        {
            var winners = GetWinners();
            winners.ForEach(o => MessagesManager.Instance.MessageClient(o, "Congratulations! You are a winner!"));
            MessagesManager.Instance.MessageClients($"Congratulations: {String.Join(", ", winners.Select(o => o.Name).ToList())} Lucky number was NUMBER " + guessNumber);
            gameState = false;
            ResetPlayers();

            MessagesManager.Instance.DebugMessage($"Lottery ended! Waiting for the next game!");
        }
        public void Gamble(ExtendendedPlayer player, int number)
        {
            player.Extensions.Lottery.Gamble(number);
        }
        public void ResetPlayers()
        {
            MessagesManager.Instance.DebugMessage("Reseting players!");

            foreach (var v in PlayerManager.Instance.playerExtendedList)
            {
                MessagesManager.Instance.DebugMessage(v.Key.Name + " " + v.Value.Lottery.ToString());
                v.Value.Lottery.Reset();
            }
        }
        private List<CitizenFX.Core.Player> GetWinners()
        {
            return PlayerManager.Instance.playerExtendedList.Where(o => o.Value.Lottery.guessNumber == guessNumber)
                .Select(o => o.Key)
                .ToList();
        }

        static LotteryManager()
        {
        }

        private LotteryManager()
        {
        }
        public static LotteryManager Instance { get; } = new LotteryManager();
    }
}
