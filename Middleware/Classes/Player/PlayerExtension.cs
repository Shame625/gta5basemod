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
        }
        public LotteryData Lottery { get; set; }
    }
}
