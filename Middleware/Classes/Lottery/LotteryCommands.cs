using CitizenFX.Core;
using CitizenFX.Core.Native;
using MiddlewareNamespace.Classes.Lottery;
using MiddlewareNamespace.Classes.Player;
using System;
using System.Collections.Generic;

namespace Middleware.Classes.Lottery
{
    public class LotteryCommands : BaseScript
    {
        public static void RegisterLotteryCommands()
        {
            API.RegisterCommand("lottery", new Action<int, List<object>, string>((source, args, rawCommand) =>
            {
                var extendedPlayer = PlayerManager.Instance.GetExtendedPlayer(source);

                if (LotteryManager.Instance.gameState == false)
                {
                    TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Game has not started yet!" } });
                    return;
                }

                if (LotteryManager.Instance.gameState == true && extendedPlayer.Extensions.Lottery.didGamble.HasValue && extendedPlayer.Extensions.Lottery.didGamble == true)
                {
                    TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "You have already gambled! You picked number: " + extendedPlayer.Extensions.Lottery.guessNumber } });
                    return;
                }

                if (args.Count == 0)
                {
                    TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Please pick a number." } });
                    return;
                }
                else if (args.Count == 1)
                {
                    int guess;
                    bool valid = int.TryParse(args[0].ToString(), out guess);
                    if (valid)
                    {
                        if (guess > 0 && guess <= 100)
                        {
                            LotteryManager.Instance.Gamble(extendedPlayer, guess);
                            TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { $"You picked number {guess}." } });
                            return;
                        }

                        TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Please enter number between 1 and 100." } });
                        return;
                    }
                    TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Wrong arguments. Please try again." } });
                    return;
                }
                else
                {
                    TriggerClientEvent(extendedPlayer.Player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Wrong arguments. Please try again." } });
                    return;
                }

            }), true);
        }
    }
}
