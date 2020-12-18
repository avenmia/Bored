using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Bored.GameService.GameSession
{
    public class RedisContext
    {

        readonly ConnectionMultiplexer muxer;
        
        public RedisContext(IConfiguration _configuration)
        {
            string redisConnectionString = _configuration.GetConnectionString("redis");
            muxer = ConnectionMultiplexer.Connect(redisConnectionString);
            IDatabase conn = muxer.GetDatabase();
            conn.StringSet("foo", "bar");
            var value = conn.StringGet("foo");
            Console.WriteLine(value);
        }
    }
}
