using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NBomber.Contracts;
using NBomber.CSharp;
using NUnit.Framework;
using RedisBloom.Extensions;

namespace RedisBloom.LoadTests
{
    [TestFixture]
    public class BloomFilterScenarios : BaseTest
    {
        private static IStep CheckExistenceStep => Step.Create("check-existence", async context =>
        {
            var guid = Guid.NewGuid().ToString();
            var isExists = await Database.ExistsInBloomFilterAsync(BloomFilterCfg.Name, guid);

            return isExists ? Response.Fail() : Response.Ok();
        });
        
        private static IStep InsertGuidsStep => Step.Create("insert-guids", async context =>
        {
            var guid = Guid.NewGuid().ToString();
            await Database.InsertToBloomFilterAsync(BloomFilterCfg.Name, guid);

            return Response.Ok();
        });
        
        private static Scenario InsertionScenario => ScenarioBuilder
            .CreateScenario("insertion-scenario", InsertGuidsStep)
            .WithWarmUpDuration(TimeSpan.FromMinutes(1))
            .WithLoadSimulations(LoadSimulation.NewKeepConstant(20, TimeSpan.FromSeconds(1)));
        
        private static Scenario CheckExistenceScenario => ScenarioBuilder
            .CreateScenario("check-existence-scenario", CheckExistenceStep)
            .WithoutWarmUp()
            .WithLoadSimulations(LoadSimulation.NewKeepConstant(20, TimeSpan.FromMinutes(1)));

        [SetUp]
        public async Task SetUpAsync()
        {
            await ReserveBloomFilterAsync();
        }
        
        [Test]
        public async Task InsertionWithCheckExistenceScenario()
        {
            NBomberRunner
                .RegisterScenarios
                (
                    InsertionScenario,
                    CheckExistenceScenario
                )
                .Run();

            var debugInfo = await Database.DebugInfoFromBloomFilterAsync(BloomFilterCfg.Name);
            Console.WriteLine($"\n\n> BF.DEBUG {BloomFilterCfg.Name} \n{debugInfo}");
        }

        [TearDown]
        public void TearDown() => base.Dispose();
    }
}