using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bored.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        public string AddGameState(string gameID, IGameState state)
        {
            var serializedState = JsonConvert.SerializeObject(state);
            return _db.StringSet(gameID, serializedState) ? serializedState : null;
        }
    }
}
