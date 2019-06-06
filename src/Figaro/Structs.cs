// using Figaro.Xml;

using System;

namespace Figaro    
{
    /// <summary>
    /// Information about the sites configured within the replication manager.
    /// </summary>
    public struct ReplicationSite
    {
        /// <summary>
        /// The environment identification number of the remote replication manager.
        /// </summary>
        public int EnvironmentID;
        /// <summary>
        /// The host name of the remote replication manager.
        /// </summary>
        public string Host;
        /// <summary>
        /// The listening port of the remote replication manager.
        /// </summary>
        public UInt16 Port;
        /// <summary>
        /// The status of the remote replication manager.
        /// </summary>
        public ReplicationManagerStatus Status;
    }
    /// <summary>
    /// A log sequence number which specifies a unique location in a 
    /// log file. 
    /// </summary>
    /// <remarks>
    /// A <see cref="LogSequenceNumber"/> consists of two unsigned 32-bit integers -- one specifies the log file number, and the other specifies an offset in the log file.
    /// </remarks>
    public struct LogSequenceNumber
    {
        /// <summary>
        /// The log file number.
        /// </summary>
        public uint FileID;
        /// <summary>
        /// An offset in the log file.
        /// </summary>
        public uint Offset;
    }

    /// <summary>
    /// Individual transaction information.
    /// </summary>
    public struct TransactionInfo
    {
        /// <summary>
        /// The global ID.
        /// </summary>
        public string GlobalID;
        /// <summary>
        /// The local ID.
        /// </summary>
        public uint ID;
        /// <summary>
        /// The log sequence number of the last checkpoint.
        /// </summary>
        public LogSequenceNumber LSN;
        /// <summary>
        /// The number of buffer copies created by this transaction that remain in cache.
        /// </summary>
        public uint MvccReferenceCount;
        /// <summary>
        /// The specified name for the transaction (up to the first 50 bytes).
        /// </summary>
        public string Name;
        /// <summary>
        /// The transaction ID of the parent transaction (or 0, if no parent)
        /// </summary>
        public uint ParentID;
        /// <summary>
        /// The process ID of the originator of the transaction.
        /// </summary>
        public uint ProcessID;
        /// <summary>
        /// The log sequence number of reads for snapshot transactions.
        /// </summary>
        public LogSequenceNumber ReadLSN;
        /// <summary>
        /// The transaction status.
        /// </summary>
        public TransactionStatus Status;
        /// <summary>
        /// The thread of control ID of the originator of the transaction.
        /// </summary>
        public uint ThreadID;
    }

    /// <summary>
    /// Version information of the Berkeley DB release used by the library.
    /// </summary>
    /// <remarks>
    /// This structure is created by the
    /// <see cref="LogConfiguration.EnvironmentVersion"/> property.
    /// </remarks>
    /// <threadsafety static="true" instance="true"/>
    /// <seealso cref="LogConfiguration.EnvironmentVersion"/>
    public struct EnvVersion
    {
        /// <summary>Gets the major version.</summary><value>The major version of the Berkeley DB library.</value>
        public int Major { get; private set; }

        /// <summary>Gets the minor version.</summary><value>The minor version of the Berkeley DB library.</value>
        public int Minor { get; private set; }

        /// <summary>Gets the patch version.</summary><value>The patch version of the Berkeley DB Library.</value>
        public int Patch { get; private set; }
        /// <summary>Gets the <see cref="System.String"/> representation of the Berkeley DB version.</summary>
        /// <returns>The <see cref="System.String"/> representation of the Berkeley DB version.</returns>
        public override string ToString()
        {
            return string.Empty; 
        }
    }

    /// <summary>
    /// The size of the <see cref="FigaroEnv"/> cache.
    /// </summary>
    /// <remarks>
    /// This structure is created by a call to the <see cref="FigaroEnv.GetCacheSize"/> method.
    /// </remarks>
    /// <threadsafety static="true" instance="true"/>
    /// <seealso cref="FigaroEnv.GetCacheSize"/>
    /// <seealso cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
    public struct EnvCacheSize
    {
        /// <summary>
        /// Initializes a new instance of the EnvCacheSize structure.
        /// </summary>
        /// <param name="gigabytes">The number of gigabytes.</param>
        /// <param name="bytes">The number of bytes.</param>
        public EnvCacheSize(int gigabytes, int bytes) : this()
        {
            Gigabytes = gigabytes;
            Bytes = bytes;
        }

        /// <summary>Gets or sets the cache size, in gigabytes.</summary><value>The size, in gigabytes, of the cache size.</value>
        /// <seealso cref="FigaroEnv.GetCacheSize"/>
        /// <seealso cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
        public int Gigabytes { get; set; }

        /// <summary>Gets or sets the cache size, in bytes.</summary><value>The size, in bytes, of the cache size.</value>
        /// <seealso cref="FigaroEnv.GetCacheSize"/>
        /// <seealso cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
        public int Bytes { get; set; }

        /// <summary>Return a <see cref="System.String"/> representation of the cache size.</summary><returns>A <see cref="System.String"/> representation of the cache size.</returns>
        /// <seealso cref="FigaroEnv.GetCacheSize"/>
        /// <seealso cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
        /// <returns>The string representation of the Figaro subsystem.</returns>
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
