using CitizenFX.Core;
using CitizenFX.Core.Native;
using Database;
using MiddlewareNamespace;
using SchedulerNamespace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestModServer
{ 
    public class Start : BaseScript
    {
        string connectionString = API.GetConvar("sql_connection_string", "");
        dynamic ESX;
        private readonly ApplicationDbContext _dbContext;
        private static MainScheduler _scheduler;
        public Start()
        {
            _dbContext = new ApplicationDbContext(connectionString);
    
            _scheduler = new MainScheduler(connectionString);
    
            if (connectionString == "")
            {
                Debug.WriteLine("[TestMod] sql_connection_string convar not set!");
            }
            else
            {
                Debug.WriteLine("[TestMod] Connection: " + connectionString);
            }
    
            TriggerEvent("esx:getSharedObject", new object[] { new Action<dynamic>(esx => {
                    ESX = esx;
                })
            });

            RegisterLotteryCommand();
        }

        void RegisterLotteryCommand()
        {
            API.RegisterCommand("lottery", new Action<int, List<object>, string>((source, args, rawCommand) =>
            {
                var extendedPlayer = Middleware.GetExtendedPlayer(source);

                if(Middleware.gameState == false)
                {
                    TriggerClientEvent(extendedPlayer.player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Game has not started yet!" } });
                    return;
                }

                if(Middleware.gameState == true && extendedPlayer.extension.didGamble.HasValue && extendedPlayer.extension.didGamble == true)
                {
                    TriggerClientEvent(extendedPlayer.player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "You have already gambled! You picked number: " + extendedPlayer.extension.guessNumber } });
                    return;
                }

                if (args.Count == 0)
                {
                    TriggerClientEvent(extendedPlayer.player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Please pick a number." } });
                    return;
                }
                else if(args.Count == 1)
                {
                    int guess;
                    bool valid = int.TryParse(args[0].ToString(), out guess);
                    if(valid)
                    {
                        if(guess > 0 && guess <= 100)
                        {
                            Middleware.Gamble(extendedPlayer.player, guess);
                            TriggerClientEvent(extendedPlayer.player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { $"You picked number {guess}." } });
                            return;
                        }

                        TriggerClientEvent(extendedPlayer.player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Please enter number between 1 and 100." } });
                        return;
                    }
                    TriggerClientEvent(extendedPlayer.player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Wrong arguments. Please try again." } });
                    return;
                }
                else
                {
                    TriggerClientEvent(extendedPlayer.player, "chat:addMessage", new { color = new[] { 255, 0, 0 }, multiline = true, args = new string[] { "Wrong arguments. Please try again." } });
                    return;
                }

            }), true);
        }
    }

}

