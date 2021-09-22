using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using RedisBloom.Extensions;
using StackExchange.Redis;

namespace BloomFilter.ConsoleHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var conn = await ConnectionMultiplexer.ConnectAsync("127.0.0.1");
            var db = conn.GetDatabase(0);


            //await CheckErrorRate(db);
            await GenerateGuids("guids.txt", 1000000);
            await WriteGuids(db, "guids.txt");

            // Console.WriteLine(await db.ExistsAsync("1"));
        }

        static async Task GenerateGuids(string filename, int size)
        {
            var guids = new List<string>();

            for (int i = 0; i < size; i++)
            {
                guids.Add(Guid.NewGuid().ToString());
            }
            
            await File.WriteAllLinesAsync(filename, guids);
        }

        static async Task WriteGuids(IDatabaseAsync db, string filename)
        {
            var guids = await File.ReadAllLinesAsync(filename);
            
            foreach (var guid in guids)
            {
                await db.AddAsync(guid);
            }
        }

        static async Task CheckErrorRate(IDatabaseAsync db)
        {
            var errors = 0;
            var size = 10000;
            
            await GenerateGuids("fake-guids.txt", size);
            var guids = await File.ReadAllLinesAsync("fake-guids.txt");

            var timer = new Stopwatch();
            timer.Start();
            foreach (var guid in guids)
            {
                var isExist = await db.ExistsAsync(guid);
                if (isExist)
                {
                    //Console.WriteLine($"This GUID already exists - {fakeGuid}");
                    errors++;
                }
            }
            timer.Stop();

            Console.WriteLine($"Elapsed ms - {timer.ElapsedMilliseconds}ms");
            Console.WriteLine($"Errors found - {errors}");
            Console.WriteLine($"Error rate - {(double)size / (size - errors)}");
        }
    }
}