using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Bored.GameService.GameSession
{
    public class GameSessionContext : IGameSessionContext
    {
        private IDatabase _db;
        private readonly IConnectionMultiplexer _muxer;

        
        public GameSessionContext(IConnectionMultiplexer muxer)
        {
            _muxer = muxer;
            _db = _muxer.GetDatabase();
        }

        public string GetGameState(string gameID)
        {
            return _db.StringGet(gameID);
        }

        public void AddGameState()
        {
            _db.StringSet("foo", "Here's game state");
            throw new NotImplementedException();
        }
    }
}
