using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Bored.GameService.GameSession
{
    public class GameSessionContext : IGameSession, IDisposable
    {
        readonly ConnectionMultiplexer muxer;
        private IDatabase conn;

        
        public GameSessionContext()
        {
            string connectionString = Startup.Configuration.GetConnectionString("redis");
            muxer = ConnectionMultiplexer.Connect(connectionString);
            //conn = muxer.GetDatabase();
            //conn.StringSet("foo", "bar");
            //var value = conn.StringGet("foo");
            //Console.WriteLine(value);
        }

        public void GetGameState()
        {
            throw new NotImplementedException();
        }

        public void AddGameState()
        {
            throw new NotImplementedException();
        }

        public void ReturnGameState()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            muxer.Dispose();
        }
    }
}
