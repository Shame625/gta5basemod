using Middleware.Classes.Counter;
using Middleware.Classes.Player;
using MiddlewareNamespace.Classes.Lottery;
using Newtonsoft.Json;
using System;

namespace MiddlewareNamespace.Classes.Player
{
    public class ExtendendedPlayer
    {
        private readonly PlayerManagerRepository _playerManagerRepositroy;
        public ExtendendedPlayer(CitizenFX.Core.Player player, string esxId)
        {
            _playerManagerRepositroy = new PlayerManagerRepository(PlayerManager.Instance.ConnectionString);
            Player = player;
            EsxId = esxId;
            Extensions = _playerManagerRepositroy.LoadUserData(EsxId);

            MessagesManager.Instance.DebugMessage("ExtendedPlayer added to ExtendedPlayerList");
        }
        public CitizenFX.Core.Player Player { get; set; }
        public PlayerExtension Extensions { get; set; }
        public string EsxId { get; set; }
        public void Save()
        {
            MessagesManager.Instance.DebugMessage("Saving changes");
            var result = _playerManagerRepositroy.SaveUserData(EsxId, Extensions);
            MessagesManager.Instance.DebugMessage(result + " changes saved");
        }
    }
    public class PlayerExtension
    {
        public PlayerExtension()
        {
            Lottery = new LotteryData();
            Counter = new CounterData { Number = 0 };
        }

        [JsonIgnore]
        public LotteryData Lottery { get; set; }
        public CounterData Counter { get; set; }
    }
}
