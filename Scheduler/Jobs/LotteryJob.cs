using Quartz;
using System;
using MiddlewareNamespace;
using System.Linq;

namespace Scheduler.Jobs
{
    
    class LotteryJob : IJob
    {
        private static int currentLuckyNumber;
        private static bool guessing = false;
        readonly Random random = new Random();
        public void Execute(JobExecutionContext context)
        {
            if(!guessing)
            {
                GenerateLuckyNumber();
            }
            else
            {
                GetWinners();
            }
        }

        public void GenerateLuckyNumber()
        {
            var number = random.Next(1, 100);
            currentLuckyNumber = number;
            guessing = true;
           
            Middleware.gameState = true;
            Middleware.guessNumber = number;

            Middleware.DebugMessage($"Guessing started. Lucky Number: {currentLuckyNumber}");
            Middleware.MessageClients("Welcome! Lottery is on! Guess the number between 1-100 by typing /lottery [1-100]");
        }

        public void GetWinners()
        {
            guessing = false;
            Middleware.gameState = false;
            var winners = Middleware.GetWinners();
            Middleware.ResetPlayers();

            Middleware.DebugMessage($"Lottery ended! Waiting 1 minute for the next game!");
            Middleware.MessageClients($"Congratulations: {String.Join(", ",  winners.Select(o => o.Name).ToList())} Lucky number was NUMBER " + currentLuckyNumber);

            winners.ForEach(o => Middleware.MessageClient(o, "Congratulations! You are a winner!"));
        }
    }
}
