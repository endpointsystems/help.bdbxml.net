// ReSharper disable EmptyConstructor
using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// Cache information for each file that makes up the memory pool.
    /// </summary>
    public struct MemoryPoolFileStatistics: IDisposable
    {
        /// <summary>
        /// Gets the number of requested pages found in the cache.
        /// </summary>
        public ulong CacheHit { get; private set; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// Gets the page size in bytes.
        /// </summary>
        public uint PageSize { get; private set; }
        /// <summary>
        /// Gets the pages created in the cache.
        /// </summary>
        public ulong CachePagesCreated { get; private set; }
        /// <summary>
        /// Gets the requested pages found in the cache.
        /// </summary>
        public ulong CachePagesFound { get; private set; }
        /// <summary>
        /// Gets the requested pages not found in the cache.
        /// </summary>
        public ulong CachePagesNotFound { get; private set; }
        /// <summary>
        /// Gets the pages read into the cache.
        /// </summary>
        public ulong CachePagesRead { get; private set; }
        /// <summary>
        /// Gets the pages written from the cache to the backing file.
        /// </summary>
        public ulong CachePagesWritten { get; private set; }
        /// <summary>
        /// Gets the requested pages mapped to the process address space.
        /// </summary>
        public ulong MappedPages { get; private set; }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">If disposing equals <c>true</c>, the method has been called directly or 
        /// indirectly by a user's code. Managed and unmanaged resources can be disposed. If disposing equals 
        /// <c>false</c>, the method has been called by the runtime from inside the 
        /// finalizer and you should not reference other objects. Only unmanaged resources can be disposed. </param>
        private void Dispose(bool disposing)
        {

        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

    }

    /// <summary>
    /// Memory pool (buffer cache) subsystem statistics.
    /// </summary>
    public sealed class MemoryPoolStatistics : IDisposable 
    {        
        /// <summary>
        /// Initializes a new instance of the MemoryPoolStatistics class.
        /// </summary>
        public MemoryPoolStatistics() {}

        /// <summary>
        /// Gets the bytes of cache (total cache size is <see cref="CacheBytes"/> + <see cref="CacheGigabytes"/>).
        /// </summary>
        public uint CacheBytes { get; private set; }
        /// <summary>
        /// Gets the number of caches.
        /// </summary>
        public uint CacheCount { get; private set; }
        /// <summary>
        /// Gets the maximum number of caches.
        /// </summary>
        public uint CacheCountMax { get; private set; }
        /// <summary>
        /// Gets the gigabytes of cache (total cache size is <see cref="CacheBytes"/> + <see cref="CacheGigabytes"/>).
        /// </summary>
        public uint CacheGigabytes { get; private set; }
        /// <summary>
        /// Gets the requested pages mapped into the process' address space.
        /// </summary>
        public uint MappedRequestPages { get; private set; }
        /// <summary>
        /// Gets the maximum open file descriptors.
        /// </summary>
        public uint MaxOpenFileDescriptors { get; private set; }
        /// <summary>
        /// Gets the maximum sequential buffer writes.
        /// </summary>
        public uint MaxSequentialBufferWrites { get; private set; }
        /// <summary>
        /// Gets the microseconds to pause after writing maximum sequential buffers.
        /// </summary>
        public uint MaxSequentialBufferWritesSleep { get; private set; }
        /// <summary>
        /// Gets the maximum memory-mapped file size.
        /// </summary>
        public ulong MemoryMapFileSize { get; private set; }
        /// <summary>
        /// Gets the requested pages found in the cache.
        /// </summary>
        public uint PagesInCache { get; private set; }
        /// <summary>
        /// Gets the number of operations blocked waiting for I/O to complete.
        /// </summary>
        public ulong BlockedOperations { get; private set; }
        /// <summary>
        /// Gets the longest chain ever encountered in buffer hash table lookups.
        /// </summary>
        public ulong BufferHashLongestChain { get; private set; }
        /// <summary>
        /// Gets the total number of buffer hash table lookups.
        /// </summary>
        public ulong BufferHashTableLookups { get; private set; }
        /// <summary>
        /// Gets the number of frozen buffers freed.
        /// </summary>
        public ulong BuffersFreed { get; private set; }
        /// <summary>
        /// Gets the number of buffers frozen.
        /// </summary>
        public ulong BuffersFrozen { get; private set; }
        /// <summary>
        /// Gets the number of buffers thawed.
        /// </summary>
        public ulong BuffersThawed { get; private set; }
        /// <summary>
        /// Gets the individual cache size, in bytes.
        /// </summary>
        public ulong CacheSize { get; private set; }
        /// <summary>
        /// Gets the number of hash buckets checked during allocation.
        /// </summary>
        public ulong HashBucketsCheckedAtAllocation { get; private set; }
        /// <summary>
        /// Gets the number of hash buckets in buffer hash table.
        /// </summary>
        public ulong HashBucketsInBufferHashTable { get; private set; }
        /// <summary>
        /// Gets the total number of hash elements traversed during hash table lookups.
        /// </summary>
        public ulong HashElementsTraversed { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was able to obtain a hash bucket lock without waiting.
        /// </summary>
        public ulong HashLockNoWait { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was forced to wait before obtaining a hash bucket lock.
        /// </summary>
        public ulong HashLockWait { get; private set; }
        /// <summary>
        /// Gets the maximum number of hash buckets checked during an allocation.
        /// </summary>
        public ulong MaxHashBucketsCheckedAtAllocation { get; private set; }
        /// <summary>
        /// Gets the maximum number of times any hash bucket lock was waited for by a thread of control.
        /// </summary>
        public ulong MaxHashBucketWait { get; private set; }
        /// <summary>
        /// Gets the number of times a thread of control was able to obtain the hash 
        /// bucket lock without waiting on the bucket which had the maximum 
        /// number of times that a thread of control needed to wait.
        /// </summary>
        public ulong MaxHashLockNoWait { get; private set; }
        /// <summary>
        /// Gets the maximum number of hash buckets checked during an allocation.
        /// </summary>
        public ulong MaxPagesCheckedAtAllocation { get; private set; }
        /// <summary>
        /// Gets the number of memory pool sync operations interrupted.
        /// </summary>
        public ulong MemoryPoolSyncOperationsInterrupted { get; private set; }
        /// <summary>
        /// Gets the number of page allocations.
        /// </summary>
        public ulong PageAllocations { get; private set; }
        /// <summary>
        /// Gets the number of pages checked at allocation.
        /// </summary>
        public ulong PagesCheckedAtAllocation { get; private set; }
        /// <summary>
        /// Gets the clean pages forced from the cache.
        /// </summary>
        public ulong PagesCleanForcedFromCache { get; private set; }
        /// <summary>
        /// Gets the pages created in the cache.
        /// </summary>
        public ulong PagesCreatedInCache { get; private set; }
        /// <summary>
        /// Gets the dirty pages forced from the cache.
        /// </summary>
        public ulong PagesDirtyForcedFromCache { get; private set; }
        
        // <summary>
        // Gets the dirty pages written using the memory buffer trickle method.
        // </summary>
        // public ulong PagesDirtyTrickle { get; private set; }
        
        /// <summary>
        /// Gets the clean pages currently in the cache.
        /// </summary>
        public ulong PagesInCacheClean { get; private set; }
        /// <summary>
        /// Gets the dirty pages currently in the cache.
        /// </summary>
        public ulong PagesInCacheDirty { get; private set; }
        /// <summary>
        /// Gets the pages read into the cache.
        /// </summary>
        public ulong PagesReadIntoCache { get; private set; }
        /// <summary>
        /// Gets the pages written from the cache to the backing file.
        /// </summary>
        public ulong PagesWrittenFromCache { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was able to obtain a cache region mutex without waiting.
        /// </summary>
        public ulong RegionNoWait { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was forced to wait before obtaining a cache region mutex.
        /// </summary>
        public ulong RegionWait { get; private set; }
        /// <summary>
        /// Gets the requested pages found in the cache.
        /// </summary>
        public ulong RequestedPagesFoundInCache { get; private set; }
        /// <summary>
        /// Gets the requested pages not found in the cache.
        /// </summary>
        public ulong RequestedPagesNotFoundInCache { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {}
    }
}
