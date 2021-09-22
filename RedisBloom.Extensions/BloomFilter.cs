using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisBloom.Extensions
{
    public static class BloomFilter
    {
        public static async Task AddAsync(this IDatabaseAsync db, string key, string value)
        {
            await db.ExecuteAsync("BF.ADD", key, value);
        }
        
        public static async Task ReserveAsync(this IDatabaseAsync db, string key, double errorRate, int capacity)
        {
            await db.ExecuteAsync("BF.RESERVE", key, errorRate, capacity);
        }

        public static async Task<bool> ExistsAsync(this IDatabaseAsync db, string key, string value)
        {
            return (bool)await db.ExecuteAsync("BF.EXISTS", key, value);
        }
    }
}