using Middleware.Classes.Counter;
using MiddlewareNamespace.Classes.Lottery;

namespace MiddlewareNamespace.Classes.Player
{
    public class ExtendendedPlayer
    {
        public CitizenFX.Core.Player Player { get; set; }
        public PlayerExtension Extensions { get; set; }
    }
    public class PlayerExtension
    {
        public PlayerExtension()
        {
            Lottery = new LotteryData();
            Counter = new CounterData { Number = 0 };
        }
        public LotteryData Lottery { get; set; }
        public CounterData Counter { get; set; }
    }
}
