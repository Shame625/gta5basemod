﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using Database;
using Middleware.Classes.Counter;
using Middleware.Classes.Lottery;
using Middleware.Classes.Player;
using MiddlewareNamespace.Classes.Player;
using SchedulerNamespace;
using System;

namespace TestModServer
{ 
    public class Start : BaseScript
    {
        string connectionString = API.GetConvar("sql_connection_string", "");
        private readonly ApplicationDbContext _dbContext;
        private static MainScheduler _scheduler;
        dynamic ESX;
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

            //Registers network events
            PlayerManagerNetwork.Instance.RegisterPlayerManagerNetworkEvents(EventHandlers);

            //get esx
            TriggerEvent("esx:getSharedObject", new object[] { new Action<dynamic>(esx => {
                    ESX = esx;
                })
            });

            PlayerManager.Instance.Init(connectionString, ESX, EventHandlers);
            //Register commands here
            LotteryCommands.RegisterLotteryCommands();
            CounterCommands.RegisterCounterCommands();
        }
    }

}

