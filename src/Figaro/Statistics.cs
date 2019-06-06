using System;
using System.Collections.ObjectModel;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable InconsistentNaming

namespace Figaro    
{
    /// <summary>
    /// Memory pool statistics for each of the cache files.
    /// </summary>
    public sealed class MemoryPoolStatisticsInfo
    {
        /// <summary>
        /// Gets memory pool subsystem statistics.
        /// </summary>
        public MemoryPoolStatistics MemoryPoolInfo { get; private set; }
        /// <summary>
        /// Gets file statistics for the cache files that make up the memory pool.
        /// </summary>
        public ReadOnlyCollection<MemoryPoolFileStatistics> FileStatistics { get; private set; }
    }

    /// <summary>
    /// Mutex statistics.
    /// </summary>
    public class MutexStatistics: IDisposable
    {
        /// <summary>
        /// Gets the maximum number of mutexes.
        /// </summary>
        public uint MaxMutexes { get; private set; }
        /// <summary>
        /// Gets the Mutex alignment, in bytes.
        /// </summary>
        public uint MutexAlignment { get; private set; }
        /// <summary>
        /// Gets the total number of configured mutexes.
        /// </summary>
        public uint MutexesConfigured { get; private set; }
        /// <summary>
        /// Gets the number of mutexes currently available.
        /// </summary>
        public uint MutexesFree { get; private set; }
        /// <summary>
        /// Gets the number of mutexes in use.
        /// </summary>
        public uint MutexesInUse { get; private set; }
        /// <summary>
        ///  Gets the number of times test-and-set mutexes will spin without blocking.
        /// </summary>
        public uint TestAndSpinCount { get; private set; }
        /// <summary>
        /// Gets the size of the mutex region, in bytes.
        /// </summary>
        public ulong MutexRegionBytes { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was able to obtain the mutex region mutex without waiting.
        /// </summary>
        public ulong MutexRegionNoWait { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was forced to wait before obtaining the mutex region mutex.
        /// </summary>
        public ulong MutexRegionWait { get; private set; }
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
    /// Transaction statistics.
    /// </summary>
    public class TransactionStatistics
    {
        /*

        /// <summary>
        /// Initializes a new instance of the TransactionStatistics class.
        /// </summary>
        public TransactionStatistics(uint lastAllocatedTransactionId, uint maxActiveTransactions, uint maximumActiveTransactionsConfigured, uint maxTransactionsOnSnapshotList, uint transactionsOnSnapshotList, uint transactionsRestored, ulong lastCompletedCheckpoint, ulong regionNoWait, ulong regionWait, ulong transactionRegionSize, ulong transactionsAborted, ulong transactionsBegun, ulong transactionsCommitted)
        {
            TransactionsCommitted = transactionsCommitted;
            TransactionsBegun = transactionsBegun;
            TransactionsAborted = transactionsAborted;
            TransactionRegionSize = transactionRegionSize;
            RegionWait = regionWait;
            RegionNoWait = regionNoWait;
            LastCompletedCheckpoint = lastCompletedCheckpoint;
            TransactionsRestored = transactionsRestored;
            TransactionsOnSnapshotList = transactionsOnSnapshotList;
            MaxTransactionsOnSnapshotList = maxTransactionsOnSnapshotList;
            MaximumActiveTransactionsConfigured = maximumActiveTransactionsConfigured;
            MaxActiveTransactions = maxActiveTransactions;
            LastAllocatedTransactionID = lastAllocatedTransactionId;
        }
        */

        /// <summary>
        /// Gets the LSN of the last checkpoint.
        /// </summary>
        public LogSequenceNumber CheckpointLSN { get; private set; }
        /// <summary>
        /// Gets a list of currently active transactions.
        /// </summary>
        public ReadOnlyCollection<TransactionInfo> ActiveTransactions { get; private set; }
        /// <summary>
        /// Gets the number of transactions that are currently active.
        /// </summary>
        public uint CurrentlyActiveTransactions { get; private set; }
        /// <summary>
        /// Gets the last transaction ID active.
        /// </summary>
        public uint LastAllocatedTransactionID { get; private set; }
        /// <summary>
        /// Gets the maximum number of active transactions at any one time.
        /// </summary>
        public uint MaxActiveTransactions { get; private set; }
        /// <summary>
        /// Gets the maximum number of active transactions configured.
        /// </summary>
        public uint MaximumActiveTransactionsConfigured { get; private set; }
        /// <summary>
        /// Gets the maximum number of transactions on the snapshot list at any one time.
        /// </summary>
        public uint MaxTransactionsOnSnapshotList { get; private set; }
        /// <summary>
        /// Gets the number of transactions on the snapshot list. These are transactions 
        /// which modified a database opened with <see cref="EnvConfig.MultiVersion"/>, 
        /// and which have committed or aborted, but the copies of pages they 
        /// created are still in the cache.
        /// </summary>
        public uint TransactionsOnSnapshotList { get; private set; }
        /// <summary>
        /// Gets the number of transactions that have been restored.
        /// </summary>
        public uint TransactionsRestored { get; private set; }
        /// <summary>
        /// Gets the time the last completed checkpoint finished (as the number of 
        /// seconds since the Epoch, returned by the IEEE/ANSI Std 1003.1 (POSIX) 
        /// time function).
        /// </summary>
        public ulong LastCompletedCheckpoint { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was able to obtain 
        /// the transaction region mutex without waiting.
        /// </summary>
        public ulong RegionNoWait { get; private set; }
        /// <summary>
        /// Gets the number of times that a thread of control was 
        /// forced to wait before obtaining the transaction region mutex.
        /// </summary>
        public ulong RegionWait { get; private set; }
        /// <summary>
        /// Gets the size of the transaction region, in bytes.
        /// </summary>
        public ulong TransactionRegionSize { get; private set; }
        /// <summary>
        /// Gets the number of transactions that have been aborted.
        /// </summary>
        public ulong TransactionsAborted { get; private set; }
        /// <summary>
        /// Gets the number of transactions that have begun.
        /// </summary>
        public ulong TransactionsBegun { get; private set; }
        /// <summary>
        /// Gets the number of committed transactions.
        /// </summary>
        public ulong TransactionsCommitted { get; private set; }
    }

    /// <summary>
    /// Replication management statistics for <see cref="FigaroReplicationManager"/>.
    /// </summary>
    public class ReplicationManagerStatistics
    {
        /// <summary>
        /// he number of times an attempt to open a new connection failed.
        /// </summary>
        public ulong ConnectionFailures { get; private set; }
        /// <summary>
        /// The number of times an existing connection failed.
        /// </summary>
        public ulong ConnectionsDropped { get; private set; }
        /// <summary>
        /// The number of outgoing messages that were completely dropped,
        /// because the outgoing message queue was full. (Berkeley DB
        /// replication is tolerant of dropped messages, and will automatically
        /// request retransmission of any missing messages as needed.)
        /// </summary>
        public ulong MessagesDropped { get; private set; }
        /// <summary>
        /// The number of outgoing messages which could not be transmitted
        /// immediately, due to a full network buffer, and had to be queued for
        /// later delivery.
        /// </summary>
        public ulong MessagesQueued { get; private set; }
        /// <summary>
        /// The number of times a message critical for maintaining database
        /// integrity (for example, a transaction commit), originating at this
        /// site, did not receive sufficient acknowledgment from clients,
        /// according to the configured acknowledgment policy and acknowledgment
        /// timeout.
        /// </summary>
        public ulong PermMessagesFailed { get; private set; }
    }
}