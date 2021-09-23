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

        private static Scenario InsertionScenario(int threads, TimeSpan duration) => ScenarioBuilder
            .CreateScenario("insertion-scenario", InsertGuidsStep)
            .WithLoadSimulations(LoadSimulation.NewKeepConstant(threads, duration))
            .WithoutWarmUp();
        
        private static Scenario CheckExistenceScenario(int threads, TimeSpan duration) => ScenarioBuilder
            .CreateScenario("check-existence-scenario", CheckExistenceStep)
            .WithLoadSimulations(LoadSimulation.NewKeepConstant(threads, duration))
            .WithoutWarmUp();

        [SetUp]
        public async Task SetUpAsync()
        {
            await ReserveBloomFilterAsync();
        }
        
        [Test]
        public async Task Insertion_And_CheckExistence_Simultaneously_Scenario()
        {
            NBomberRunner
                .RegisterScenarios
                (
                    InsertionScenario(200, TimeSpan.FromSeconds(5)),
                    CheckExistenceScenario(200, TimeSpan.FromSeconds(5))
                )
                .Run();

            await PrintDebugInfoAsync();
        }
        
        [Test]
        public async Task Insertion_Then_CheckExistence_Sequentially_Scenario()
        {
            NBomberRunner
                .RegisterScenarios
                (
                    InsertionScenario(200, TimeSpan.FromMinutes(10))
                )
                .Run();
            
            NBomberRunner
                .RegisterScenarios
                (
                    CheckExistenceScenario(200, TimeSpan.FromMinutes(5))
                )
                .Run();

            await PrintDebugInfoAsync();
        }
        
        [Test]
        public async Task Insertion_Then_Insertion_And_CheckExistence_Sequentially_Scenario()
        {
            NBomberRunner
                .RegisterScenarios
                (
                    InsertionScenario(200, TimeSpan.FromMinutes(15))
                )
                .Run();
            
            NBomberRunner
                .RegisterScenarios
                (
                    InsertionScenario(5, TimeSpan.FromMinutes(20)),
                    CheckExistenceScenario(300, TimeSpan.FromMinutes(20))
                )
                .Run();

            await PrintDebugInfoAsync();
        }

        private async Task PrintDebugInfoAsync()
        {
            var debugInfo = await Database.DebugInfoFromBloomFilterAsync(BloomFilterCfg.Name);
            Console.WriteLine($"\n\n> BF.DEBUG {BloomFilterCfg.Name} \n{debugInfo}");
        }

        [TearDown]
        public void TearDown() => base.Dispose();
    }
}