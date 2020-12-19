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
        }

        public string GetGameState()
        {
            conn = _muxer.GetDatabase();
            conn.StringSet("foo", "Here's game state");
            return conn.StringGet("foo");
        }

        public void AddGameState()
        {
            throw new NotImplementedException();
        }
    }
}
