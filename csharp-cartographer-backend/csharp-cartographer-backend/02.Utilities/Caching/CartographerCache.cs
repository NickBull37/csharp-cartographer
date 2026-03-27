using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using System.Collections.Concurrent;

namespace csharp_cartographer_backend._02.Utilities.Caching
{
    public class CartographerCache : ICartographerCache
    {
        private record CacheEntry(MapText MapText, DateTime CreatedDateUtc);
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
        private readonly ConcurrentDictionary<string, SemaphoreSlim> CacheLocks = new();
        private readonly ConcurrentDictionary<string, CacheEntry> Cache = new();

        public CartographerCache()
        {
        }

        public MapText? TryGetCacheEntry(string key)
        {
            if (Cache.TryGetValue(key, out var entry) && NotExpired(entry))
            {
                return entry.MapText;
            }

            return null;
        }

        public SemaphoreSlim GetSemaphore(string key)
        {
            return CacheLocks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        }

        public async Task<bool> IsLockAcquired(SemaphoreSlim @lock)
        {
            return await @lock.WaitAsync(TimeSpan.FromSeconds(5));
        }

        public void SaveCacheEntry(string key, MapText entry)
        {
            Cache.AddOrUpdate(key,
                _ => new CacheEntry(entry, DateTime.UtcNow),
                (_, _) => new CacheEntry(entry, DateTime.UtcNow));
        }

        private bool NotExpired(CacheEntry entry)
        {
            return DateTime.UtcNow - entry.CreatedDateUtc < CacheDuration;
        }
    }
}
