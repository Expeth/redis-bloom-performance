namespace RedisBloom.LoadTests.Common
{
    public class BloomFilterCfg
    {
        public string Name { get; set; }
        public double ErrorRate { get; set; }
        public int Capacity { get; set; }
    }
}