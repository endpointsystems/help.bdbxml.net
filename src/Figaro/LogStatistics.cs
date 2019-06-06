using System;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro    
{
    /// <summary>
    /// Log statistics data.
    /// </summary>
    /// <remarks>
    /// This object is created using the
    /// <see cref="FigaroEnv.GetLogStatistics(bool)"/>
    /// method of the <see cref="FigaroEnv"/> object.
    /// </remarks>
    /// <seealso cref="FigaroEnv.GetLogStatistics(bool)"/>
    /// <seealso cref="FigaroEnv"/>
    /// <threadsafety static="true" instance="true"/>
    public sealed class LogStatistics: IDisposable
    {
        /// <summary>
        /// Gets the magic number that identifies a file as a log file.
        /// </summary>
        /// <value>The magic number that identifies a file as a log file, or -1 if an error occurs.</value>
        public uint Magic { get; private set; }
        /// <summary>
        /// Gets the version of the log file type.
        /// </summary>
        /// <value>The version of the log file type, or -1 if an error occurs.</value>
        public uint Version { get; private set; }
        /// <summary>
        /// Gets the mode of any created log files.
        /// </summary>
        /// <value>The mode of any created log files, or -1 if an error occurs.</value>
        public uint Mode { get; private set; }
        /// <summary>
        /// Gets the in-memory log record cache size.
        /// </summary>
        /// <value>The in-memory log record cache size, or -1 if an error occurs.</value>
        public uint LogRecordCacheSize { get; private set; }
        /// <summary>
        /// Gets the log file size.
        /// </summary>
        /// <value>The log file size, or -1 if an error occurs.</value>
        public uint LogFileSize { get; private set; }

        // <summary>
        // Gets the greatest number of lock objects.
        // </summary>
        // public uint LockObjectsHighWater { get; private set; }

        /// <summary>
        /// Gets the number of records written to this log.
        /// </summary>
        /// <value>The number of records written to this log, or -1 if an error occurs.</value>
        public ulong LogRecords { get; private set; }
        /// <summary>
        /// Gets the number of megabytes written to this log.
        /// </summary>
        /// <value>The number of megabytes written to this log, or -1 if an error occurs.</value>
        public uint MegabytesWritten { get; private set; }
        /// <summary>
        /// Gets the number of bytes written in addition to the megabytes written.
        /// </summary>
        /// <value>
        /// The number of bytes written in addition to the megabytes written, or -1 if an error occurs.
        /// </value>
        public uint BytesWritten { get; private set; }
        /// <summary>
        /// Gets the number of megabytes written to this log since the last checkpoint.
        /// </summary>
        /// <value>
        /// The number of megabytes written to this log since the last checkpoint, or -1 if an error occurs.
        /// </value>
        public uint CheckpointMegabytes { get; private set; }
        /// <summary>
        /// Gets the number of bytes written in addition to the megabytes written to this log since the last checkpoint.
        /// </summary>
        /// <value>
        /// The number of bytes written in addition to the megabytes written to this log since the last checkpoint, or -1 if an error occurs.
        /// </value>
        public uint CheckpointBytes { get; private set; }
        /// <summary>
        /// Gets the number of times the log has been written to disk.
        /// </summary>
        /// <value>The number of times the log has been written to disk, or -1 if an error occurs.</value>
        public ulong WriteCount { get; private set; }
        /// <summary>
        /// Gets the number of times the log has been written to disk because the in-memory log record cache filled up.
        /// </summary>
        /// <value>
        /// The number of times the log has been written to disk because the in-memory log record cache filled up, or -1 if an error occurs.
        /// </value>
        public ulong FullCacheWriteCount { get; private set; }
        /// <summary>
        /// Gets the number of times the log has been read from disk.
        /// </summary>
        /// <value>The number of times the log has been read from disk, or -1 if an error occurs.</value>
        public ulong ReadCount { get; private set; }
        /// <summary>
        /// Gets the number of times the log has been flushed to disk.
        /// </summary>
        /// <value>The number of times the log has been flushed to disk, or -1 if an error occurs.</value>
        public ulong FlushCount { get; private set; }
        /// <summary>
        /// Gets the current log file number.
        /// </summary>
        /// <value>The current log file number, or -1 if an error occurs.</value>
        public uint CurrentLogNumber { get; private set; }
        /// <summary>
        /// Gets the byte offset in the current log file.
        /// </summary>
        /// <value>The byte offset in the current log file, or -1 if an error occurs.</value>
        public uint ByteOffset { get; private set; }
        /// <summary>
        /// Gets the log file number of the last record known to be on disk.
        /// </summary>
        /// <value>The log file number of the last record known to be on disk, or -1 if an error occurs.</value>
        public uint LastKnownRecord { get; private set; }
        
        /// <summary>
        /// Gets the byte offset of the last record known to be on disk.
        /// </summary>
        /// <value>The byte offset of the last record known to be on disk, or -1 if an error occurs.</value>
        public uint LastKnownByteOffset { get; private set; }

        /// <summary>
        /// Gets the maximum number of commits contained in a single log flush.
        /// </summary>
        /// <value>The maximum number of commits contained in a single log flush, or -1 if an error occurs.</value>
        public uint MaxFlushCommits { get; private set; }

        /// <summary>
        /// Gets the minimum number of commits contained in a single log flush that contained a commit.
        /// </summary>
        /// <value>
        /// The minimum number of commits contained in a single log flush that contained a commit, or -1 if an error occurs.
        /// </value>
        public uint MinFlushCommits { get; private set; }

        /// <summary>
        /// Gets the size of the log region, in bytes.
        /// </summary>
        /// <value>The size of the log region, in bytes, or -1 if an error occurs.</value>
        public ulong LogRegionByteSize { get; private set; }

        /// <summary>
        /// Gets the number of times that a thread of control was forced to wait before obtaining the log region mutex.
        /// </summary>
        /// <value>
        /// The number of times that a thread of control was forced to wait before obtaining the log region mutex, or -1 if an error occurs.
        /// </value>
        public ulong LogRegionWaitCount { get; private set; }

        /// <summary>
        /// Gets the number of times that a thread of control was able to obtain the log region mutex without waiting.
        /// </summary>
        /// <value>
        /// The number of times that a thread of control was able to obtain the log region mutex without waiting, or -1 if an error occurs.
        /// </value>
        public ulong LogRegionNoWaitCount { get; private set; }
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
