using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisBloom.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InsertToBloomFilterAsync(this IDatabaseAsync db, string key, string value)
        {
            await db.ExecuteAsync("BF.ADD", key, value);
        }
        
        public static async Task ReserveBloomFilterAsync(this IDatabaseAsync db, string key, double errorRate, int capacity)
        {
            await db.ExecuteAsync("BF.RESERVE", key, errorRate, capacity);
        }

        public static async Task<bool> ExistsInBloomFilterAsync(this IDatabaseAsync db, string key, string value)
        {
            return (bool)await db.ExecuteAsync("BF.EXISTS", key, value);
        }

        public static async Task<string> DebugInfoFromBloomFilterAsync(this IDatabaseAsync db, string key)
        {
            var result = (string[])await db.ExecuteAsync("BF.DEBUG", key);
            return string.Join("\n", result);
        }
    }
}