using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisBloom.Extensions
{
    public static class BloomFilter
    {
        public static async Task AddAsync(this IDatabaseAsync db, string data)
        {
            await db.ExecuteAsync("BF.ADD", "test", data);
        }

        public static async Task<bool> ExistsAsync(this IDatabaseAsync db,string data)
        {
            return (bool)await db.ExecuteAsync("BF.EXISTS", "test", data);
        }
    }
}