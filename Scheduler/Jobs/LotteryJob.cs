using Quartz;
using MiddlewareNamespace.Classes.Lottery;

namespace Scheduler.Jobs
{
    class LotteryJob : IJob
    {
        public void Execute(JobExecutionContext context)
        {
            if(!LotteryManager.Instance.gameState)
            {
                LotteryManager.Instance.StartGame();
            }
            else
            {
                LotteryManager.Instance.EndGame();
            }
        }
    }
}
