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
        private IDatabase conn;
        private readonly IConnectionMultiplexer _muxer;

        
        public GameSessionContext(IConnectionMultiplexer muxer)
        {
            _muxer = muxer;
            conn = _muxer.GetDatabase();
        }

        public string GetGameState(string gameID)
        {
            return conn.StringGet(gameID);
        }

        public void AddGameState()
        {
            // conn.StringSet("foo", "Here's game state");
            throw new NotImplementedException();
        }
    }
}
