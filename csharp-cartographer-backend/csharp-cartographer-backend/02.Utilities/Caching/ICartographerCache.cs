using csharp_cartographer_backend._03.Models.Shared;

namespace csharp_cartographer_backend._02.Utilities.Caching
{
    public interface ICartographerCache
    {
        StyledText? TryGetCacheEntry(string key);

        SemaphoreSlim GetSemaphore(string key);

        Task<bool> IsLockAcquired(SemaphoreSlim @lock);

        void SaveCacheEntry(string key, StyledText entry);
    }
}
