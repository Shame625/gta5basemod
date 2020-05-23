using Database;
using Database.Entity;
using MiddlewareNamespace.Classes.Player;
using Newtonsoft.Json;
using System.Linq;

namespace Middleware.Classes.Player
{
    public class PlayerManagerRepository
    {
        private readonly ApplicationDbContext _db;
        public PlayerManagerRepository(string cs)
        {
            _db = new ApplicationDbContext(cs);
        }

        public int SaveUserData(string id, PlayerExtension playerExtension)
        {
            var playerSession = _db.UserSession.FirstOrDefault(o => o.UserId == id);

            if(playerSession != null)
            {
                playerSession.SessionData = JsonConvert.SerializeObject(playerExtension);
            }
            else
            {
                var newSession = new Session { UserId = id, SessionData = JsonConvert.SerializeObject(playerExtension) };
                _db.UserSession.Add(newSession);
            }

            var result = _db.SaveChanges();
            return result;
        }

        public PlayerExtension LoadUserData(string id)
        {
            var playerSession = _db.UserSession.FirstOrDefault(o => o.UserId == id);
            if (playerSession != null)
                return JsonConvert.DeserializeObject<PlayerExtension>(playerSession.SessionData);

            return new PlayerExtension();
        }
    }
}
