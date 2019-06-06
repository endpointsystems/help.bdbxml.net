using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// Locking subsystem statistical data.
    /// </summary>
    /// <remarks>
    /// This object is created using the
    /// <see cref="FigaroEnv.GetLockStatistics(bool)"/>
    /// method of the <see cref="FigaroEnv"/> object.
    /// </remarks>
    /// <seealso cref="FigaroEnv.GetLockStatistics(bool)"/>
    /// <seealso cref="FigaroEnv"/>
    /// <threadsafety static="true" instance="true"/>
public sealed class LockStatistics: IDisposable
    {
        /// <summary>
        /// Gets the last allocated locker ID.
        /// </summary>
        /// <value>The last allocated locker ID, or -1 if an error occurs..</value>        
        public ulong LastAllocatedLockerId { get; private set; }

        /// <summary>
        /// Gets the current maximum unused locker ID.
        /// </summary>
        /// <value>The current maximum unused locker ID, or -1 if an error occurs.</value>
        public ulong CurrentMaxUnusedLockerId { get; private set; }

        /// <summary>
        /// Gets the number of lock modes.
        /// </summary>
        /// <value>The number of lock modes, or -1 if an error occurs.</value>
        public ulong LockModesCount { get; private set; }

        /// <summary>
        /// Gets the maximum number of locks possible.
        /// </summary>
        /// <value>The maximum number of locks possible, or -1 if an error occurs.</value>
        public ulong MaxLocks { get; private set; }

        /// <summary>
        /// Gets the maximum number of lockers possible.
        /// </summary>
        /// <value>The maximum number of lockers possible, or -1 if an error occurs.</value>
        public ulong MaxPossibleLockers { get; private set; }

        /// <summary>
        /// Gets the maximum number of lock objects possible.
        /// </summary>
        /// <value>The maximum number of lock objects possible, or -1 if an error occurs.</value>
        public ulong MaxPossibleLock { get; private set; }

        /// <summary>
        /// Gets the number of current locks.
        /// </summary>
        /// <value>The number of current locks, or -1 if an error occurs.</value>
        public ulong CurrentLockCount { get; private set; }

        /// <summary>
        /// Gets the maximum number of locks at any one time.
        /// </summary>
        /// <value>The maximum number of locks at any one time, or -1 if an error occurs.</value>
        public ulong LockHighWater { get; private set; }

        /// <summary>
        /// Gets the number of current lockers.
        /// </summary>
        /// <value>The number of current lockers, or -1 if an error occurs.</value>
        public ulong CurrentLockerCount { get; private set; }

        /// <summary>
        /// Gets the maximum number of lockers at any one time.
        /// </summary>
        /// <value>The maximum number of lockers at any one time, or -1 if an error occurs.</value>
        public ulong LockerHighWater { get; private set; }

        /// <summary>
        /// Gets the number of current lock objects.
        /// </summary>
        /// <value>The number of current lock objects, or -1 if an error occurs.</value>
        public ulong CurrentLockObjectCount { get; private set; }

        /// <summary>
        /// Gets the maximum number of lock objects at any one time.
        /// </summary>
        /// <value>The maximum number of lock objects at any one time, or -1 if an error occurs.</value>
        public ulong LockObjectsHighWater { get; private set; }

        /// <summary>
        /// Gets the total number of locks requested.
        /// </summary>
        /// <value>The total number of locks requested.</value>
        public ulong TotalLocksRequested { get; private set; }

        /// <summary>
        /// Gets the total number of locks released.
        /// </summary>
        /// <value>The total number of locks released, or -1 if an error occurs.</value>
        public ulong TotalReleasedLocks { get; private set; }

        /// <summary>
        /// Gets the total number of locks upgraded.
        /// </summary>
        /// <value>The total number of locks upgraded, or -1 if an error occurs.</value>
        public ulong TotalUpgradedLocks { get; private set; }

        /// <summary>
        /// Gets the total number of locks downgraded.
        /// </summary>
        /// <value>The total number of locks downgraded, or -1 if an error occurs.</value>
        public ulong TotalDowngradedLocks { get; private set; }

        /// <summary>
        /// Gets the number of lock requests not immediately available due to conflicts, for which the thread of control waited.
        /// </summary>
        /// <value>
        /// The number of lock requests not immediately available due to conflicts, for which the thread of control waited, or -1 if an error occurs.
        /// </value>
        public ulong TotalLockWait { get; private set; }

        /// <summary>
        /// Gets the number of lock requests not immediately available due to conflicts, for which the thread of control did not wait.
        /// </summary>
        /// <value>
        /// The number of lock requests not immediately available due to conflicts, for which the thread of control did not wait, or -1 if an error occurs.
        /// </value>
		public ulong TotalLockNoWait { get; private set; }

        /// <summary>
        /// Gets the number of deadlocks.
        /// </summary>
        /// <value>The number of deadlocks, or -1 if an error occurs.</value>
        public ulong Deadlocks { get; private set; }
        /// <summary>
        /// Gets the lock timeout value.
        /// </summary>
        /// <value>Lock timeout value, or -1 if an error occurs.</value>
        public ulong Lock{ get; private set; }

        /// <summary>
        /// Gets the number of lock requests that have timed out.
        /// </summary>
        /// <value>The number of lock requests that have timed out, or -1 if an error occurs.</value>
        public ulong TimedOutLockRequests { get; private set; }

        /// <summary>
        /// Gets the transaction timeout value.
        /// </summary>
        /// <value>Transaction timeout value, or -1 if an error occurs.</value>
        public ulong TransactionTimeout { get; private set; }

        /// <summary>
        /// Gets the number of transactions that have timed out. This value is also a component of <c>st_ndeadlocks</c>, an internal structure that contains the total number of deadlocks detected.
        /// </summary>
        /// <value>
        /// The number of transactions that have timed out, or -1 if an error occurs.
        /// </value>
		public ulong TimedOutTransactions { get; private set; }

        /// <summary>
        /// Gets the number of requests to allocate or deallocate an object for which the thread of control waited.
        /// </summary>
        /// <value>
        /// The number of requests to allocate or deallocate an object for which the thread of control waited, or -1 if an error occurs.
        /// </value>
        public ulong ObjectWaits { get; private set; }

        /// <summary>
        /// Gets the number of requests to allocate or deallocate an object for which the thread of control did not wait.
        /// </summary>
        /// <value>
        /// The number of requests to allocate or deallocate an object for which the thread of control did not wait, or -1 if an error occurs.
        /// </value>
        public ulong ObjectNoWaits { get; private set; }

        /// <summary>
        /// Gets the number of requests to allocate or deallocate a locker for which the thread of control waited.
        /// </summary>
        /// <value>
        /// The number of requests to allocate or deallocate a locker for which the thread of control waited, or -1 if an error occurs.
        /// </value>
        public ulong LockersWait { get; private set; }

        /// <summary>
        /// Gets the number of requests to allocate or deallocate a locker for which the thread of control did not wait.
        /// </summary>
        /// <value>
        /// The number of requests to allocate or deallocate a locker for which the thread of control did not wait, or -1 if an error occurs.
        /// </value>
        public ulong LockersNoWait { get; private set; }

        /// <summary>
        /// Gets the number of requests to allocate or deallocate a lock structure for which the thread of control waited.
        /// </summary>
        /// <value>
        /// The number of requests to allocate or deallocate a lock structure for which the thread of control waited, or -1 if an error occurs.
        /// </value>
        public ulong LocksWait { get; private set; }

        /// <summary>
        /// Gets the number of requests to allocate or deallocate a lock structure for which the thread of control did not wait.
        /// </summary>
        /// <value>
        /// The number of requests to allocate or deallocate a lock structure for which the thread of control did not wait, or -1 if an error occurs.
        /// </value>
        public ulong LocksNoWait { get; private set; }

        /// <summary>
        /// Gets the maximum length of a lock hash bucket.
        /// </summary>
        /// <value>Maximum length of a lock hash bucket, or -1 if an error occurs.</value>
        public ulong HashLength { get; private set; }

        /// <summary>
        /// Gets the size of the lock region, in bytes.
        /// </summary>
        /// <value>The size of the lock region, in bytes, or -1 if an error occurs.</value>
        public ulong LockRegionSize { get; private set; }

        /// <summary>
        /// Gets the number of times that a thread of control was forced to wait before obtaining the lock region mutex.
        /// </summary>
        /// <value>
        /// The number of times that a thread of control was forced to wait before obtaining the lock region mutex, or -1 if an error occurs.
        /// </value>
        public ulong RegionWait { get; private set; }

        /// <summary>
        /// Gets the number of times that a thread of control was able to obtain the lock region mutex without waiting.
        /// </summary>
        /// <value>
        /// The number of times that a thread of control was able to obtain the lock region mutex without waiting, or -1 if an error occurs.
        /// </value>
        public ulong RegionNoWait { get; private set; }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">If disposing equals <c>true</c>, the method has been called directly or 
        /// indirectly by a user's code. Managed and unmanaged resources can be disposed. If disposing equals 
        /// <c>false</c>, the method has been called by the runtime from inside the 
        /// finalizer and you should not reference other objects. Only unmanaged resources can be disposed. </param>
        protected void Dispose(bool disposing)
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
}


