using System;
using System.Net;
using System.Threading.Tasks;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using RedisBloom.Extensions;
using RedisBloom.LoadTests.Common;
using StackExchange.Redis;

namespace RedisBloom.LoadTests
{
    public abstract class BaseTest : IDisposable
    {
        protected static readonly IContainerService RedisContainer;
        protected static readonly IDatabaseAsync Database;
        protected static readonly IConfiguration Configuration;
        protected static readonly BloomFilterCfg BloomFilterCfg;

        static BaseTest()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            BloomFilterCfg = Configuration
                .GetSection("bloom")
                .Get<BloomFilterCfg>();

            RedisContainer = new Builder()
                .UseContainer()
                .UseImage("redislabs/rebloom:latest")
                .WithName("redis-bloom")
                .ExposePort(6379, 6379)
                .Build()
                .Start();

            Database = ConnectionMultiplexer
                .Connect(IPAddress.Loopback.ToString())
                .GetDatabase(0);
        }

        protected async Task ReserveBloomFilterAsync()
        {
            await Database.ReserveBloomFilterAsync(
                BloomFilterCfg.Name,
                BloomFilterCfg.ErrorRate,
                BloomFilterCfg.Capacity);
        }

        public void Dispose()
        {
            RedisContainer.Dispose();
        }
    }
}