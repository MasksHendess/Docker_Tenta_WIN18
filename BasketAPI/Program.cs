using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace BasketAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ConnectionMultiplexer RedisConn = ConnectionMultiplexer.Connect("redis:6379,allowAdmin=true");
            if (RedisConn == null)
            {
                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false
                };
                RedisConn = ConnectionMultiplexer.Connect(option);
            }
            IDatabase db = RedisConn.GetDatabase();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
