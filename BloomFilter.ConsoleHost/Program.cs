using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Ductus.FluentDocker.Builders;
using NBomber.Contracts;
using NBomber.CSharp;
using RedisBloom.Extensions;
using StackExchange.Redis;

namespace BloomFilter.ConsoleHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var container = new Builder()
                .UseContainer()
                .UseImage("redislabs/rebloom:latest")
                .WithName("redis-bloom")
                .ExposePort(6379, 6379)
                .Build()
                .Start();

            var setName = "test";
            var errorRate = 0.0001;
            var capacity = 50_000_000;
            
            var conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1");
            var db = conn.GetDatabase(0);

            await db.ReserveAsync(setName, errorRate, capacity);

            var insertGuidsStep = Step.Create("insert-guids", async context =>
            {
                var guid = Guid.NewGuid().ToString();
                await db.AddAsync(setName, guid);
               
                return Response.Ok();
            });

            var checkExistenceStep = Step.Create("check-existence", async context =>
            {
                var guid = Guid.NewGuid().ToString();
                var isExists = await db.ExistsAsync(setName, guid);
               
                return isExists ? Response.Fail() : Response.Ok();
            });

            var insertionScenario = ScenarioBuilder
                .CreateScenario("insertion-scenario", insertGuidsStep)
                .WithWarmUpDuration(TimeSpan.FromMinutes(2))
                .WithLoadSimulations(LoadSimulation.NewKeepConstant(100, TimeSpan.FromMinutes(3)));

            var checkExistenceScenario = ScenarioBuilder
                .CreateScenario("check-existence-scenario", checkExistenceStep)
                .WithoutWarmUp()
                .WithLoadSimulations(LoadSimulation.NewKeepConstant(200, TimeSpan.FromMinutes(3)));
            
            NBomberRunner
                .RegisterScenarios
                (
                    insertionScenario,
                    checkExistenceScenario
                )
                .Run();
        }
    }
}