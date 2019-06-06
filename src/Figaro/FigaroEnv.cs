// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedParameter.Global
// ReSharper disable EmptyConstructor
// ReSharper disable UnusedParameter.Local
#pragma warning disable 67
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable InconsistentNaming

namespace Figaro
{
    /// <summary>
    /// The handle for a database environment - a collection including support for some or all of caching,
    /// locking, logging and transaction subsystems, as well as databases and log files. Methods of
    /// the <see cref="FigaroEnv"/> handle are used to configure the environment as well as to operate
    /// on subsystems and databases in the environment.
    /// </summary>
    /// <remarks>
    /// <see cref="FigaroEnv"/> handles are free-threaded if the <see cref="EnvOpenOptions.Thread"/> flag is specified to
    /// the <see cref="FigaroEnv.Open"/> method when the environment is opened.
    /// The <see cref="FigaroEnv"/> handle should not be closed while any other
    /// handle remains open that is using it as a reference. Once either the <see cref="FigaroEnv.Close"/> or
    /// <see cref="FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction)"/> methods are called,
    /// the handle may not be accessed again.
    /// </remarks>
    public sealed class FigaroEnv : IDisposable
    {

        /// <summary>
        /// The class used to control High Availability (HA) replication for the database environment.
        /// </summary>
        public class FigaroReplicationManager
        {
            /// <summary>
            /// Displays the Replication Manager statistical information as displayed in the <c></c>
            /// </summary>
            /// <param name="options">Options for retrieving statistics.</param>
            /// <seealso cref="ReplicationStatisticsOptions"/>
            /// <seealso cref="GetReplicationManagerStatistics"/>
            /// <seealso cref="FigaroEnv.MessageFile"/>
            /// <seealso cref="FigaroEnv.MessageEventEnabled"/>
            public void PrintStatistics(ReplicationStatisticsOptions options) { }

            /// <summary>
            /// Retrieves statistical information about the Replication Manager.
            /// </summary>
            /// <param name="clear">If <c>true</c>, resets statistics after retrieving them.</param>
            /// <returns>A <c>ReplicationManagerStatistics</c> object containing statistical information.</returns>
            /// <seealso cref="ReplicationManagerStatistics"/>
            /// <seealso cref="ReplicationStatisticsOptions"/>
            public ReplicationManagerStatistics GetReplicationManagerStatistics(bool clear)
            {
                return null;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="threads"></param>
            /// <param name="options"></param>
            public void Start(UInt16 threads, ReplicationStartOptions options)
            {
            }

            /// <summary>
            /// Specifies how master and client sites will handle acknowledgment of replication messages 
            /// which are necessary for "permanent" records. 
            /// </summary>
            /// <remarks>
            /// The current implementation requires all sites in a replication group configure the same acknowledgment policy.
            /// </remarks>
            /// <param name="policy">The acknowledgment policy to specify.</param>
            /// <seealso cref="AcknowledgmentPolicy"/>
            public void SetAcknowledgmentPolicy(RecordAcknowledgmentPolicy policy) { }

            /// <summary>
            /// Specifies the database environment's priority in replication group elections. 
            /// </summary>
            /// <remarks>
            /// A special value of 0 indicates that this environment cannot be a replication group master.
            /// <para>
            /// Note that if the application never explicitly sets a priority, then a default value of 100 is used.
            /// </para>
            /// <para>This method may be called at any time during the life of the application.</para>
            /// </remarks>
            /// <param name="priority">The replication priority of the environment.</param>
            /// <seealso cref="Priority"/>
            public void SetPriority(UInt32 priority)
            {
            }

            /// <summary>
            /// Gets the configured acknowledgment policy for the replication management environment.
            /// </summary>
            /// <seealso cref="SetAcknowledgmentPolicy"/>
            public RecordAcknowledgmentPolicy AcknowledgmentPolicy { get;private set; }

            /// <summary>
            /// Gets the database environment's priority in replication group elections.
            /// </summary>
            /// <seealso cref="SetPriority"/>
            public UInt32 Priority { get;private set; }

            /// <summary>
            /// Adds a new site to the <c>FigaroReplicationManager</c>'s list of known sites. 
            /// </summary>
            /// <remarks>
            ///  It is not necessary for all sites in a replication group to know about all other sites in the group.
            /// <para>
            /// This method may be called at any time during the life of an application.
            /// </para>
            /// </remarks>
            /// <param name="host">The site's host identification string.</param>
            /// <param name="port">The port number on which the remote site is receiving.</param>
            /// <param name="peerToPeer">If <c>true</c>, configures client-to-client synchronization with the specified remote site.</param>
            public void AddRemoteHostSite(string host, UInt32 port, bool peerToPeer)
            {
            }

            /// <summary>
            /// Configures the host identification information for the local system.
            /// </summary>
            /// <remarks>
            /// This method may not be called after the <c>StartReplication</c> method has been called.
            /// </remarks>
            /// <param name="host">The site's host identification string.</param>
            /// <param name="port">The port number on which the local site is listening.</param>
            public void SetLocalSite(string host, UInt32 port)
            {
            }
        }


        /// <summary>
        /// Sets the path of a directory to be used as the location to create the access method
        /// database files. When the
        /// <see cref="Open"/> method is used to create a file, it will be created relative to this path.
        /// </summary>
        /// <remarks>
        /// If no database directories are specified, database files will be created either by absolute paths
        /// or relative to the environment home directory.
        /// <para>
        /// The <see cref="SetCreateDirectory"/> method may be called at any time.
        /// </para>
        /// </remarks>
        /// <param name="directory">A directory to be used to create database files.</param>
        public void SetCreateDirectory(string directory)
        {
        }

        /// <summary>
        /// Prints the transaction subsystem statistical information to the configured
        /// <see cref="FigaroEnv"/> output channel.
        /// </summary>
        /// <remarks>
        /// To capture the output, configure either message file (<see cref="SetMessageFile"/>) or
        /// event-driven (<see cref="MessageEventEnabled"/>) output.
        /// </remarks>
        /// <param name="clear">If <c>true</c>, resets the statistics after retrieval.</param>
        /// <seealso cref="TransactionStatistics"/>
        /// <seealso cref="GetTransactionStatistics"/>
        /// <seealso cref="SetMessageFile"/>
        /// <seealso cref="MessageEventEnabled"/>
        public void PrintTransactionStatistics(bool clear)
        {
        }

        /// <summary>
        /// Prints the mutex subsystem statistical information to the configured <see cref="FigaroEnv"/>
        /// output channel.
        /// </summary>
        /// <remarks>
        /// To capture the output, configure either message file (<see cref="SetMessageFile"/>) or
        /// event-driven (<see cref="MessageEventEnabled"/>) output.
        /// </remarks>
        /// <param name="clear">If <c>true</c>, resets the statistics after retrieval.</param>
        /// <seealso cref="MutexStatistics"/>
        /// <seealso cref="GetMutexStatistics"/>
        /// <seealso cref="SetMessageFile"/>
        /// <seealso cref="MessageEventEnabled"/>
        public void PrintMutexStatistics(bool clear)
        {
        }

        /// <summary>
        /// Gets the number of lock partitions in the environment.
        /// </summary>
        public uint LockPartitions { get;private set; }

        /// <summary>
        /// Set the number of lock table partitions in the environment.
        /// </summary>
        /// <remarks>
        /// The default value is 10 times the number of CPUs on the system if there is more than one CPU. Increasing
        /// the number of partitions can provide for greater throughput on a system with multiple CPUs and more than
        /// one thread contending for the lock manager. On single processor systems more than one partition may
        /// increase the overhead of the lock manager. Systems often report threading contexts as CPUs. If your
        /// system does this, set the number of partitions to 1 to get optimal performance.
        /// </remarks>
        /// <param name="partitions">The number of lock table partitions.</param>
        public void SetLockPartitions(uint partitions) { }

        /// <summary>
        /// Configures the underlying logging system.
        /// </summary>
        /// <param name="options">The logging options to configure.</param>
        /// <param name="enabled">If <c>true</c>, enables the <paramref name="options"/> specified.</param>
        /// <seealso cref="LogOptions"/>
        public void SetLogOptions(EnvLogOptions options, bool enabled) { }

        /// <summary>
        /// Gets the configured options of the underlying Figaro logging system.
        /// </summary>
        /// <seealso cref="SetLogOptions"/>
        public EnvLogOptions LogOptions { get;private set; }

        /// <summary>
        /// Gets the maximum number of file descriptors that may be concurrently opened by the library
        /// when flushing dirty pages from the cache.
        /// </summary>
        /// <seealso cref="SetMaxFileDescriptors"/>
        public uint MaxFileDescriptors { get;private set; }

        /// <summary>
        /// Gets the maximum number of file descriptors that may be concurrently opened by the library
        /// when flushing dirty pages from the cache.
        /// </summary>
        /// <param name="maxFileDescriptors">The maximum number of file descriptors.</param>
        /// <seealso cref="MaxFileDescriptors"/>
        public void SetMaxFileDescriptors(uint maxFileDescriptors) { }

        /// <summary>
        /// Limits the number of sequential write operations scheduled by the library when flushing dirty pages
        /// from the cache.
        /// </summary>
        /// <param name="writes">The maximum number of write operations. Specify 0 for an unlimited number.</param>
        /// <param name="sleepMicroseconds">
        ///     The number of microseconds the thread of control should pause before scheduling further write operations. It must
        ///  be specified as an unsigned 32-bit number of microseconds, limiting the maximum pause to roughly 71 minutes.</param>
        /// <seealso cref="MaxSequentialWriteOperationsSleep"/>
        /// <seealso cref="SetMaxSequentialWriteOperations"/>
        public void SetMaxSequentialWriteOperations(uint writes, uint sleepMicroseconds) { }

        /// <summary>
        /// Gets the number of sequential write operations scheduled by the library when flushing dirty pages
        /// from the cache.
        /// </summary>
        /// <seealso cref="SetMaxSequentialWriteOperations"/>
        /// <seealso cref="MaxSequentialWriteOperationsSleep"/>
        public uint MaxSequentialWriteOperations { get;private set; }

        /// <summary>
        /// Gets the number of microseconds the thread of control should pause before scheduling further write operations.
        /// </summary>
        /// <seealso cref="SetMaxSequentialWriteOperations"/>
        public uint MaxSequentialWriteOperationsSleep { get;private set; }

        /// <summary>
        /// Specify that test-and-set mutexes should spin the specified number of times without blocking.
        /// The value defaults to 1 on uniprocessor systems and to 50 times the number of processors on
        /// multiprocessor systems.
        /// </summary>
        /// <param name="tasCount">The number of times a test-and-set mutex should spin without blocking.</param>
        /// <seealso cref="TestAndSetSpinCount"/>
        public void SetTestAndSetSpinCount(uint tasCount) { }

        /// <summary>
        /// Gets the configured spin count of test-and-set mutexes.
        /// </summary>
        /// <seealso cref="SetTestAndSetSpinCount"/>
        public uint TestAndSetSpinCount { get;private set; }

        /// <summary>
        /// Set the mutex alignment, in bytes.
        /// </summary>
        /// <remarks>
        /// It is sometimes advantageous to align mutexes on specific byte boundaries in order to minimize cache line
        /// collisions. The <see cref="SetMutexAlign"/> method specifies an alignment for mutexes allocated by the database
        /// subsystem.
        /// </remarks>
        /// <param name="align">The mutex alignment, in bytes. The mutex alignment must be a power-of-two value.</param>
        /// <seealso cref="MutexAlign"/>
        public void SetMutexAlign(uint align) { }

        /// <summary>
        /// Gets the mutex alignment, in bytes.
        /// </summary>
        /// <seealso cref="SetMutexAlign"/>
        public uint MutexAlign { get;private set; }

        /// <summary>
        /// Configures the maximum number of mutexes to allocate.
        /// </summary>
        /// <remarks>
        /// The underlying database system allocates a default number of mutexes based on the initial configuration of the
        /// database environment. That default calculation may be too small if the application has an unusual need for
        /// mutexes (for example, if the application opens an unexpectedly large number of databases) or too large (if
        /// the application is trying to minimize its memory footprint).
        /// <para>
        /// The <see cref="SetMaxMutexes"/> method is used to specify an absolute number of mutexes to allocate.
        /// </para>
        /// </remarks>
        /// <param name="mutexes">The maximum number of mutexes.</param>
        /// <seealso cref="MaxMutexes"/>
        public void SetMaxMutexes(uint mutexes) { }

        /// <summary>
        /// Gets the maximum number of mutexes to allocate.
        /// </summary>
        /// <seealso cref="SetMaxMutexes"/>
        public uint MaxMutexes { get;private set; }



        /// <summary>
        /// Gets a value indicating whether or not the object is configured for transactions.
        /// </summary>
        /// <value>Returns <c>true</c> if configured for transactions.</value>
        public bool Transactional { get;private set; }

        /// <summary>
        /// Writes log records to disk.
        /// </summary>
        public void FlushLogs() { }

        /// <summary>
        /// Raises the event when informational conditions are raised within the <see cref="FigaroEnv"/>.
        /// </summary>
        /// <remarks>
        /// There are interfaces in the Figaro library which either directly output informational messages or
        /// statistical information, or configure the library to output such messages when performing other operations,
        /// for example, <see cref="FigaroEnv.SetVerbose(Figaro.VerboseOption,bool)"/> and
        /// <see cref="FigaroEnv.PrintDefaultStatistics(Figaro.StatPrintOptions)"/>.
        /// <para>
        /// The <see cref="FigaroEnv.OnMessage"/> event is used to pass these
        /// messages to the application, and Figaro raise the event for each message. It is up to the
        /// event caller to display the message in an appropriate manner.
        /// </para><para>
        /// Setting <see cref="FigaroEnv.MessageEventEnabled"/> to <c>true</c> enables the
        /// event; setting it to <c>false</c> disables the event.
        /// </para><para>
        /// Alternatively, you can use the <see cref="FigaroEnv.MessageFile"/> property
        /// to display the messages via an output file.
        /// </para><para>
        /// The <see cref="FigaroEnv.OnMessage"/> event may be called at any
        /// time during the life of the application.
        /// </para>
        /// </remarks>
        public event EventHandler<MsgEventArgs> OnMessage;

        /// <summary>
        /// Raises an event when an error condition is raised within the Figaro library.
        /// </summary>
        /// <remarks>
        /// When an error occurs in the Figaro library, an exception is thrown or an error return value is
        /// returned by the interface. In some cases, however, the <c>errno</c> value may be insufficient to completely
        /// describe the cause of the error, especially during initial application debugging.
        /// <para>
        /// The <see cref="FigaroEnv.OnErr"/> event is used to enhance
        /// the mechanism for reporting error messages to the application. In some cases, when an error
        /// occurs, Figaro will raise the event with additional error information. It is up to the
        /// event caller to display the error message in an appropriate manner.
        /// </para>
        /// <para>
        /// Setting <see cref="FigaroEnv.ErrEventEnabled"/> to <c>false</c>
        /// disables this event.
        /// </para><para>
        /// Alternatively, you can use the <see cref="FigaroEnv.ErrorFile"/> property to
        /// to display the additional information via a file stream. You should not mix these approaches.
        /// </para><para>
        /// This error-logging enhancement does not slow performance or significantly increase application size,
        /// and may be run during normal operation as well as during application debugging.
        /// </para><para>
        /// The <see cref="FigaroEnv.OnErr"/> event configures operations
        /// performed using the specified <see cref="FigaroEnv"/> handle,
        /// not all operations performed on the underlying database environment.
        /// </para><para>
        /// The <see cref="FigaroEnv.OnErr"/> event may be called at any time
        /// during the life of the application.
        /// </para>
        /// </remarks>
        /// <seealso cref="FigaroEnv.ErrEventEnabled"/>
        /// <seealso cref="FigaroEnv.ErrorFile"/>
        /// <seealso cref="FigaroEnv"/>
        public event EventHandler<ErrEventArgs> OnErr;

        /// <summary>
        /// The event generated when the backup operation comes to a close.
        /// </summary>
        public event EventHandler<BackupCloseArgs> OnBackupClose;

        /// <summary>
        /// Raised to notify the process of specific Figaro events.
        /// </summary>
        /// <remarks>
        /// The <see cref="FigaroEnv.OnProcess"/> event configures operations performed
        /// using the specified <see cref="FigaroEnv"/> handle, not all operations
        /// performed on the underlying database environment.
        /// <para>
        /// Setting <see cref="FigaroEnv.ProcessEventEnabled"/> to <c>true</c> enables
        /// the event; setting <see cref="FigaroEnv.ProcessEventEnabled"/> to <c>false</c>
        /// disables the event.
        /// </para><para>
        /// The <see cref="FigaroEnv.OnProcess"/> event may be called at any time
        /// during the life of the application.
        /// </para>
        /// </remarks>
        /// <seealso cref="FigaroEnv.ProcessEventEnabled"/>
        /// <seealso cref="FigaroEnv"/>
        public event EventHandler<ProcessEventArgs> OnProcess;

        /// <summary>
        /// Gets or sets the prefix string that appears before error messages issued by Berkeley DB.
        /// </summary>
        /// <value>The application-specified error prefix for additional error messages.</value>
        /// <seealso cref="SetErrorFile"/>
        /// <seealso cref="ErrEventEnabled"/>
        public string ErrorPrefix { get; set; }

        /// <summary>
        /// Trace custom information into the <see cref="FigaroEnv"/> error trace output.
        /// </summary>
        /// <remarks>
        /// This function allows developers to trace
        /// <see cref="string.Format(string,object[])"/>-style formatted strings into the
        /// Figaro error output stream configured  for the <see cref="FigaroEnv"/>instance.
        /// These streams can be either file-based (<see cref="SetErrorFile"/>),
        /// event-driven (<see cref="ErrEventEnabled"/>) or the default (<c>stderr</c>).
        /// You can also set a custom error prefix  (<see cref="ErrorPrefix"/>) to help
        /// catalog and categorize error output.
        /// <note class="implementnotes">
        /// There is no equivalent method for the message output.
        /// </note>
        /// </remarks>
        /// <seealso cref="ErrorPrefix"/>
        /// <seealso cref="ErrEventEnabled"/>
        /// <seealso cref="SetErrorFile"/>
        /// <seealso cref="FigaroEnv"/>
        /// <param name="message">The format string of the error message.</param>
        /// <param name="args">Format string arguments.</param>
        public void TraceError(string message, params object[] args) { }

        /// <summary>
        /// An event raised to report Figaro operation progress.
        /// </summary>
        /// <remarks>
        /// Some operations performed by the database library can take non-trivial amounts of time.
        ///  <see cref="E:Figaro.FigaroEnv.OnProgress"/> event can be used by
        ///  applications to monitor progress within these operations. When an operation is likely to take
        ///  a long time, Figaro will raise the event with progress information.
        /// <para>
        /// It is up to the event caller to display this information in an appropriate manner.
        /// </para>
        /// <para>
        /// Setting <see cref="P:Figaro.FigaroEnv.ProgressEventEnabled"/> to <c>true</c>
        ///  enables this event; setting <see cref="P:Figaro.FigaroEnv.ProgressEventEnabled"/>
        ///  to <c>false</c> disables this event.
        /// </para>
        /// <para>
        /// The <see cref="E:Figaro.FigaroEnv.OnProgress"/> method configures
        ///  operations performed using the specified handle.
        /// </para>
        /// <para>
        /// This event may be called at any time during the life of the application.
        /// </para>
        /// </remarks>
        /// <seealso cref="P:Figaro.FigaroEnv.ProgressEventEnabled"/>
        public event EventHandler<ProgressEventArgs> OnProgress;

        /// <summary>
        /// Raised if a thread of control (either a <c>true</c> thread or a process) is still running.
        /// </summary>
        /// <seealso cref="FigaroEnv"/>
        public event EventHandler<IsAliveEventArgs> OnIsAlive;

        /// <summary>Gets the maximum supported transactions in a Figaro database environment.</summary>
        /// <remarks>
        ///    To set this value, use the <see cref="SetMaxTransactions"/> method.
        /// </remarks>
        /// <seealso cref="SetMaxTransactions"/>
        /// <value>The maximum number of supported transactions.</value>
        public ushort MaxTransactions => 0;

        /// <summary>Sets the maximum supported transactions in a Figaro database environment.</summary>
        /// <remarks>
        ///    Configure the Figaro database environment to support maximum specified active transactions. This
        ///    value bounds the size of the memory allocated for transactions. Child transactions are counted as active
        ///    until they either commit or abort.
        ///    <para>
        ///        Transactions that update multi-version databases are not freed until the last page version that the
        ///        transaction created is flushed from cache. This means that applications using multi-version concurrency
        ///        control may need a transaction for each page in cache, in the extreme case.
        ///    </para><para>
        ///        When all of the memory available in the database environment for transactions is in use, calls
        ///        to begin transactions will fail (until some active transactions complete). If this interface is
        ///        never called, the database environment is configured to support at least 100 active transactions.
        ///    </para>
        /// </remarks>
        /// <param name="value">The maximum number of transactions.</param>
        /// <seealso cref="MaxTransactions"/>
        public void SetMaxTransactions(ushort value) { }

        /// <summary>
        /// Opens a Figaro environment. It provides a structure for creating a consistent environment for processes using one or more of
        /// the features of Berkeley DB.
        /// </summary>
        /// <param name="dbHome">The database environment's home directory. For more information on the home directory, and filename resolution in general,
        /// see @file-naming.md. The environment variable <c>DB_HOME</c> may be used as the path of the database home, as described in @file-naming.md.
        /// When using a Unicode build on Windows (the default), the <c>dbHome</c> argument will be interpreted as a UTF-8 string, which is equivalent to
        /// ASCII for Latin characters.</param>
        /// <param name="envOpenOptions">The <see cref="EnvOpenOptions"/> parameter.</param>
        /// <seealso cref="EnvOpenOptions"/>
        /// <seealso cref="EnvConfig.AllConcurrentDatabases"/>
        /// <remarks>
        /// <note type="implementnotes">
        /// If the <see cref="EnvOpenOptions.InitConcurrentDataStore"/> flag is specified, the <see cref="EnvConfig.AllConcurrentDatabases"/> flag is automatically set.
        /// This flag is required for the underlying Berkeley DB XML database configuration.
        /// </note>
        /// </remarks>
        public void Open(string dbHome, EnvOpenOptions envOpenOptions)
        {
        }

        /// <summary>
        /// Gets the path of a directory to be used as the location of logging files.
        /// </summary>
        /// <value>The log directory for the environment.</value>
        /// <seealso cref="SetLogDirectory"/>
        public string LogDirectory { get;private set; }

        /// <summary>
        /// Sets the path of a directory to be used as the location of logging files. Log files
        /// created by the Log Manager subsystem will be created in this directory.
        /// </summary>
        /// <remarks>
        /// If no logging directory is specified, log files are created in the environment
        /// home directory. See @file-naming.md for more information.
        /// <para>
        /// For the greatest degree of recoverability from system or application failure, database files
        /// and log files should be located on separate physical devices.
        /// </para><para>
        /// <c>SetLogDirectory</c> configures operations performed using the
        /// specified <see cref="FigaroEnv"/> handle, not all operations
        /// performed on the underlying database environment.
        /// </para><para>
        /// This method should not be called after
        /// the <seealso cref="Open"/> method. If the
        /// database environment already exists when <seealso cref="Open"/>
        /// is called, the information specified to <see cref="SetLogDirectory"/> must
        /// be consistent with the existing environment or corruption can occur.
        /// </para>
        /// </remarks>
        /// <param name="path">The path of the directory for the location of log files.</param>
        public void SetLogDirectory(string path) { }



        /// <summary>
        /// Gets a value indicating whether the automatic transaction checkpoint feature has been enabled.
        /// </summary>
        /// <seealso cref="SetEnvironmentTransactionCheckpoint"/>
        public bool CheckpointEnabled { get;private set; }

        /// <summary>
        /// Gets a value indicating whether force option of the automatic transaction checkpoint feature has been enabled.
        /// </summary>
        /// <remarks>
        /// This value is set to <c>true</c> when you enable the automatic checkpoint feature. See <see cref="SetEnvironmentTransactionCheckpoint"/> for more information.
        /// </remarks>
        /// <seealso cref="SetEnvironmentTransactionCheckpoint"/>
        public bool CheckpointForced { get;private set; }

        /// <summary>
        /// Gets the number of minutes before a transaction checkpoint is set.
        /// </summary>
        /// <remarks>
        /// This value is set when you enable the automatic checkpoint feature. See <see cref="SetEnvironmentTransactionCheckpoint"/> for more information.
        /// </remarks>
        /// <seealso cref="SetEnvironmentTransactionCheckpoint"/>
        public uint CheckpointMinutes { get;private set; }

        /// <summary>
        /// Gets the log size, in kilobytes, before a transaction checkpoint is set.
        /// </summary>
        /// <remarks>
        /// This value is set when you enable the automatic checkpoint feature. See <see cref="SetEnvironmentTransactionCheckpoint"/> for more information.
        /// </remarks>
        /// <seealso cref="SetEnvironmentTransactionCheckpoint"/>
        public uint CheckpointLogSize { get;private set; }



        /// <summary>
        /// Write a checkpoint to and flush the log at these set intervals.
        /// </summary>
        /// <param name="force">If <c>true</c>, forces a checkpoint record, even if there has been no activity since the last checkpoint.</param>
        /// <param name="minutes">If nonzero, a checkpoint will be performed if more than the specified <c>minutes</c> have passed since
        /// the last checkpoint.</param>
        /// <param name="logKilobytes">If nonzero, a checkpoint will be performed if more than the specified kilobytes have been written
        /// to the log file since the last checkpoint.</param>
        /// <remarks>
        /// If there has been any logging activity in the database environment since the last checkpoint,
        /// the <see cref="SetEnvironmentTransactionCheckpoint"/>
        /// method flushes the underlying memory pool, writes a checkpoint record to the log, and then flushes the log.
        /// </remarks>
        public void SetEnvironmentTransactionCheckpoint(bool force, uint minutes, uint logKilobytes) { }

        /// <summary>
        /// Checks for threads of control (either a <c>true</c> thread or a process)
        /// that have exited while manipulating data structures, while holding a logical database lock, or
        /// with an unresolved transaction
        /// (that is, a transaction that was never aborted or committed).
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if recovery needs to be run against the environment; otherwise, <c>false</c> is returned.
        /// </returns>
        /// <remarks>
        /// Applications calling the <see cref="FigaroEnv.FailCheck"/>
        /// method must have already enabled the <see cref="FigaroEnv.IsAliveEnabled"/> property,
        /// and must have configured the database environment using
        /// the <see cref="FigaroEnv.ThreadCount"/> property.
        /// <para>
        /// If <see cref="FigaroEnv.FailCheck"/> determines a thread of control exited while holding database read locks,
        /// it will release those locks. If <see cref="FigaroEnv.FailCheck"/> determines a thread of control exited
        /// with an unresolved transaction, the transaction will be aborted. In either of these cases,
        /// <see cref="FigaroEnv.FailCheck"/> will return <c>false</c> and the application may continue to use the database
        /// environment.
        /// </para><para>
        /// In either of these cases, the <see cref="FigaroEnv.FailCheck"/> method will also report the process
        /// and thread IDs associated with any released locks or aborted transactions. The information is printed to a specified output
        /// channel (see the <see cref="FigaroEnv.MessageEventEnabled"/> method for more information).
        /// </para><para>
        /// If <see cref="FigaroEnv.FailCheck"/> determines a thread of control has exited such that database
        /// environment recovery is required, it will return <c>true</c>. In this case, the application should not continue to use the database
        /// environment. For a further description as to the actions the application should take when this failure occurs, see Handling failure in
        /// Data Store and Concurrent Data Store applications, and Handling failure in Transactional Data Store applications.
        /// </para><para>
        /// The <see cref="FigaroEnv.FailCheck"/> method may not be called before the
        /// <see cref="Open"/> method has been called.
        /// </para>
        /// </remarks>
        /// <seealso cref="Open"/>
        /// <seealso cref="FigaroEnv.IsAliveEnabled"/>
        /// <seealso cref="FigaroEnv.ThreadCount"/>
        public bool FailCheck()
        {
            return false;
        }

        /// <summary>
        /// Returns the names of all of the log files that are no longer in use (for example, that are no longer
        /// involved in active transactions), and that may safely be archived for catastrophic recovery and then
        /// removed from the system.
        /// </summary>
        /// <param name="logArchiveOptions">One or more options for detecting and returning log archive records.</param>
        /// <returns>
        /// A list of database or log files, or <c>null</c> if none exist.
        /// </returns>
        public ReadOnlyCollection<string> LogArchive(LogArchiveOptions logArchiveOptions)
        {
            return new ReadOnlyCollection<string>(new List<string>());
        }

        /// <summary>
        /// Returns logging system statistics. This may not be called before the
        /// <seealso cref="Open"/> method has been called.
        /// </summary>
        /// <param name="clearAfterRetrieval">If <c>true</c>, clears the log statistics subsystem after retrieving the data.</param>
        /// <returns>Logging system statistics.</returns>
        /// <remarks>
        /// This method cannot be called before the
        /// <see cref="Open"/>
        /// method has been called.
        /// </remarks>
        /// <seealso cref="LogStatistics"/>
        public LogStatistics GetLogStatistics(bool clearAfterRetrieval)
        {
            return new LogStatistics();
        }

        /// <summary>
        /// Displays the logging subsystem statistical information, as described for
        /// the <see cref="FigaroEnv.GetLogStatistics(bool)"/> method.
        /// The information is printed to either the <see cref="FigaroEnv.OnMessage"/>
        /// event or file output specified in <see cref="FigaroEnv.SetMessageFile(System.String)"/>.
        /// </summary>
        /// <param name="clearAfterRetrieval">If <c>true</c>, clears the log statistics subsystem after retrieving the data.</param>
        /// <seealso cref="LogStatistics"/>
        public void PrintLogStatistics(bool clearAfterRetrieval) { }

        /// <summary>
        ///    Gets the size of the in-memory log buffer, in bytes.
        /// </summary>
        /// <seealso cref="SetLogBufferSize"/>
        /// <value>The size (in bytes) of the log buffer.</value>
        public uint LogBufferSize { get;private set; }

        /// <summary>
        ///    Sets the size of the in-memory log buffer, in bytes.
        /// </summary>
        /// <remarks>
        ///    When the logging subsystem is configured for on-disk logging, the default size of the in-memory log buffer is
        ///    approximately 32KB. Log information is stored in-memory until the storage space fills up or transaction commit
        ///    forces the information to be flushed to stable storage. In the presence of long-running transactions or
        ///    transactions producing large amounts of data, larger buffer sizes can increase throughput.
        /// <para>
        ///    When the logging subsystem is configured for in-memory logging, the default size of the in-memory log buffer is
        ///    1MB. Log information is stored in-memory until the storage space fills up or transaction abort or commit frees
        ///    up the memory for new transactions. In the presence of long-running transactions or transactions producing large
        ///    amounts of data, the buffer size must be sufficient to hold all log information that can accumulate during the
        ///    longest running transaction. When choosing log buffer and file sizes for in-memory logs, applications should
        ///    ensure the in-memory log buffer size is large enough that no transaction will ever span the entire buffer, and
        ///    avoid a state where the in-memory buffer is full and no space can be freed because a transaction that started in
        ///    the first log "file" is still active.
        /// </para><para>
        ///    <see cref="SetLogBufferSize"/>  configures a database environment, not only operations performed
        ///    using the specified <see cref="FigaroEnv"/> handle.
        /// </para><para>
        ///    This method should not be called after the <seealso cref="Open"/> method
        ///    is called. If the database environment already exists when <see cref="Open"/> is set, any
        ///    changes to this property will be ignored.
        /// </para>
        /// </remarks>
        /// <param name="size">The new in-memory log buffer size, in bytes.</param>
        public void SetLogBufferSize(uint size) { }

        /// <summary>
        ///     Gets or sets the maximum size of a single file in the log, in bytes.
        /// </summary>
        /// <remarks>
        ///        Because log sequence number (LSN) file offsets are unsigned four-byte values, the set value
        ///        may not be larger than the maximum unsigned four-byte value.
        ///    <para>
        ///        When the logging subsystem is configured for on-disk logging, the default size of a log file is 10MB.
        ///    </para><para>
        ///        When the logging subsystem is configured for in-memory logging, the default size of a log file is 256KB.
        ///        In addition, the configured log buffer size must be larger than the log file size. (The logging subsystem
        ///        divides memory configured for in-memory log records into "files", as database environments configured for
        ///        in-memory log records may exchange log records with other members of a replication group, and those members
        ///        may be configured to store log records on-disk.) When choosing log buffer and file sizes for in-memory logs,
        ///        applications should ensure the in-memory log buffer size is large enough that no transaction will ever span
        ///        the entire buffer, and avoid a state where the in-memory buffer is full and no space can be freed
        ///        because a transaction that started in the first log "file" is still active.
        ///    </para><para>
        ///        See @configuring-the-logging-subsystem.md for more information.
        ///    </para><para>
        ///        The <see cref="FigaroEnv.MaxLogSize"/> property sets a database environment, not only
        ///        operations performed using the specified <see cref="FigaroEnv"/> handle.
        ///    </para><para>
        ///        The <see cref="FigaroEnv.MaxLogSize"/> property may be set at any time during the life of the application.
        ///    </para><para>
        ///        If no size is specified by the application, the size last specified for the database
        ///        region will be used, or if no database region previously existed, the default will be used.
        ///    </para>
        /// </remarks>
        /// <value>The maximum size of a single file in the log, in bytes. </value>
        public uint MaxLogSize { get; set; }

        /// <summary>
        ///    Gets the size of the underlying logging area of the Figaro environment, in bytes.
        /// </summary>
        /// <seealso cref="SetMaxLogRegion"/>
        /// <value>The maximum log region size, in bytes.</value>
        public uint MaxLogRegion { get;private set; }

        /// <summary>
        ///    Set the size of the underlying logging area of the Figaro environment, in bytes.
        /// </summary>
        /// <remarks>
        ///    By default, or if the value is set to 0, the default size is approximately 60KB. The log
        ///    region is used to store filenames, and so may need to be increased in size if a large
        ///    number of files will be opened and registered with the specified Figaro BDB
        ///    environment's log manager.
        /// <para>
        ///    <see cref="SetMaxLogRegion"/> configures a
        ///    database environment, not only operations performed using the specified
        ///    <see cref="FigaroEnv"/> handle.
        /// </para><para>
        ///    This method may not be set after the
        ///    <seealso cref="Open"/> method is called. If the database environment already exists when <seealso cref="Open"/>
        ///    is called, the information specified to <see cref="SetMaxLogRegion"/> will be ignored.
        /// </para></remarks>
        /// <param name="size">The maximum log region size, in bytes.</param>
        public void SetMaxLogRegion(uint size) { }

        /// <summary>
        /// Initializes a new instance of the FigaroEnv class.
        /// </summary>
        /// <remarks>
        /// The constructor allocates memory internally; calling the
        /// <see cref="FigaroEnv.Close"/> or
        /// <see cref="FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction)"/> methods
        /// will free that memory.
        /// </remarks>
        public FigaroEnv() { }

        /// <summary>
        /// Closes the Figaro environment, freeing any allocated resources and closing any underlying subsystems.
        /// </summary>
        /// <remarks>
        /// The <see cref="FigaroEnv"/> handle should not be closed while any other handle that refers to it is
        /// not yet closed; for example, database environment handles must not be closed while database handles remain open, or transactions in the
        /// environment have not yet been committed or aborted.
        /// <para>
        /// Where the environment was initialized with the <see cref="EnvOpenOptions.InitLock"/> flag,
        /// calling <see cref="FigaroEnv.Close"/> does not release any locks still held
        /// by the closing process, providing functionality for long-lived locks.
        /// </para>
        /// <para>
        /// Where the environment was initialized with the <see cref="EnvOpenOptions.InitMemoryBufferPool"/> flag,
        /// calling <see cref="FigaroEnv.Close"/> implies calls
        /// to <c>DbMpoolFile.close</c> for any remaining open files in the memory pool that were returned to this
        /// process by calls to <c>DbMpoolFile.open</c>. It does not imply a call to
        /// <c>DbMpoolFile.sync</c> for those files.
        /// <note type="info">
        /// The <c>DbMPoolFile</c> referenced is used internally by the underlying environment object, and not explicitly implemented as a .NET object
        /// in this library version.
        /// </note>
        /// 	</para>
        /// <para>
        /// Where the environment was initialized with the <see cref="EnvOpenOptions.InitTransaction"/> or
        /// <see cref="EnvOpenOptions.TransactionDefaults"/> flags, calling
        /// <see cref="FigaroEnv.Close"/> aborts any unresolved transactions. Applications should not depend on
        /// this behavior for transactions involving Figaro databases; all such transactions should be explicitly resolved. The problem with
        /// depending on this semantic is that aborting an unresolved transaction involving database operations requires a database handle. Because
        /// the database handles should have been closed before calling <see cref="FigaroEnv.Close"/>, it will
        /// not be possible to abort the transaction, and recovery will have to be run on the Figaro environment before further operations are done.
        /// </para>
        /// <para>
        /// In multi-threaded applications, only a single thread may
        /// call <see cref="FigaroEnv.Close"/>.
        /// </para>
        /// <para>
        /// After <see cref="FigaroEnv.Close"/> has been called, regardless of its
        /// return, the Figaro environment handle may not be accessed again.
        /// </para>
        /// </remarks>
        /// <seealso cref="EnvOpenOptions"/>
        public void Close() { }

        /// <summary>
        /// Allows database files to be copied, and then the copy used in the same database environment as the original.
        /// </summary>
        /// <param name="file">The name of the physical file in which new file IDs are to be created.</param>
        /// <param name="encrypted">If <c>true</c>, indicates the file contains encrypted databases.</param>
        /// <remarks>
        /// All databases contain an ID string used to identify the database in the database environment cache. If a physical database file is copied,
        /// and used in the same environment as another file with the same ID strings, corruption can occur.
        /// The <see cref="FigaroEnv.FileIdReset(System.String,bool)"/> method
        /// creates new ID strings for all of the databases in the physical file.
        /// <para>
        /// 		<see cref="FigaroEnv.FileIdReset(System.String,bool)"/> modifies the
        /// physical file, in-place. Applications should not reset IDs in files that are currently in use.
        /// </para><para>
        /// The <see cref="FigaroEnv.FileIdReset(System.String,bool)"/> method
        /// may be called at any time during the life of the application.
        /// </para>
        /// </remarks>
        public void FileIdReset(string file, bool encrypted) { }

        /// <summary>Gets the database environment home directory.</summary><value>The database environment home directory.</value>
        public string Home { get;private set; }

        /// <summary>Gets the collection of <see cref="EnvOpenOptions"/>  flags.</summary>
        /// <returns>The bitwise OR'd <see cref="EnvOpenOptions"/> parameter value flags.</returns>
        /// <seealso cref="EnvOpenOptions"/>
        public EnvOpenOptions GetOpenOptions()
        {
            return EnvOpenOptions.TransactionDefaults;
        }

        /// <summary>
        /// The <see cref="FigaroEnv.LSNReset(System.String,bool)"/>
        /// method allows database files to be moved from one transactional
        /// database environment to another.
        /// </summary>
        /// <param name="file">The name of the physical file in which LSNs are
        /// to be cleared.</param>
        /// <param name="dbEncrypt">Set to <c>true</c> if the file contains
        /// encrypted databases.</param>
        /// <remarks>
        /// Database pages in transactional database environments contain
        /// references to the environment's log files (that is, log sequence
        /// numbers, or LSNs). Copying or moving a database file from one
        /// database environment to another, and then modifying it, can result
        /// in data corruption if the LSNs are not first cleared.
        /// <para>
        /// Note that LSNs should be reset before moving or copying the database
        /// file into a new database environment, rather than moving or copying
        /// the database file and then resetting the LSNs. Figaro has
        /// consistency checks that may be triggered if an application calls
        /// <see cref="FigaroEnv.LSNReset(System.String,bool)"/> on a
        /// database in a new environment when the database LSNs still reflect
        /// the old environment.
        /// </para><para>The
        /// <see cref="FigaroEnv.LSNReset(System.String,bool)"/>
        /// method modifies the physical file, in-place. Applications should not
        /// reset LSNs in files that are currently in use.
        /// </para><para>The
        /// <see cref="FigaroEnv.LSNReset(System.String,bool)"/>
        /// method may be called at any time during the life of the application.
        /// </para>
        /// </remarks>
        public void LSNReset(string file, bool dbEncrypt) { }

        /// <summary>
        /// Destroys a Figaro environment if it is not currently in use.
        /// </summary>
        /// <param name="dbHome">The database environment's home directory. For more information on the home directory, and filename resolution in general,
        /// see @file-naming.md. The environment variable <c>DB_HOME</c> may be used as the path of the database home, as described in @file-naming.md.
        /// When using a Unicode build on Windows (the default), the <c>dbHome</c> argument will be interpreted as a UTF-8 string, which is equivalent to
        /// ASCII for Latin characters.</param>
        /// <param name="removeActions">One or more <see cref="EnvironmentRemoveAction"/> actions to take against the environment while removing.</param>
        /// <remarks>
        /// The environment regions, including any backing files, are removed.
        /// Any log or database files and the environment directory are not removed.
        /// </remarks>
        /// <seealso cref="EnvironmentRemoveAction"/>
        public void Remove(string dbHome, EnvironmentRemoveAction removeActions) { }

        /// <summary>
        /// Displays the default statistical information. The information is printed to
        /// the <see cref="FigaroEnv.OnMessage"/> event.
        /// </summary>
        /// <param name="statPrintOptions">One or more configuration settings and/or actions to perform against
        /// the statistics subsystem while printing the information.</param>
        /// <seealso cref="FigaroEnv.SetMessageFile(System.String)"/>
        /// <seealso cref="FigaroEnv.MessageEventEnabled"/>
        public void PrintDefaultStatistics(StatPrintOptions statPrintOptions) { }

        ///// <summary>
        ///// Returns an <see cref="EnvVersion"/> structure containing version information of the underlying Figaro system.
        ///// </summary>
        ///// <returns>
        ///// An <see cref="EnvVersion"/> containing major, minor, and patch version information of the Figaro release used by the library.
        ///// </returns>
        ///// <seealso cref="EnvVersion"/>
        // public EnvVersion GetVersion()
        // {
        //    return new EnvVersion();
        // }

        /// <summary>
        /// Get the size of the shared memory buffer pool - that is, the cache.
        /// </summary>
        /// <returns>
        /// The <see cref="EnvCacheSize"/> structure specifying the size of the file to be mapped into the
        /// process address space.
        /// </returns>
        /// <seealso cref="SetCacheSize"/>
        public EnvCacheSize GetCacheSize() { return new EnvCacheSize(); }

        /// <summary>
        /// Set the size of the shared memory buffer pool - that is, the cache.
        /// The cache should be the size of the normal working data set of the
        /// application, with some small amount of additional memory for unusual
        /// situations. (<em>Note:</em> the working set is not the same as the
        /// number of pages accessed simultaneously, and is usually much
        /// larger.)
        /// </summary>
        /// <param name="size">The <see cref="EnvCacheSize"/> structure
        /// specifying the size of the file to be mapped into the process
        /// address space.</param>
        /// <param name="cacheCount">The number of caches to create. Each cache
        /// specified will have a size equal to the <paramref name="size"/>
        /// parameter divided by this number.</param>
        /// <remarks>
        /// <para>
        /// The default cache size is 256KB, and may not be specified as less
        /// than 20KB. Any cache size less than 500MB is automatically increased
        /// by 25% to account for buffer pool overhead; cache sizes larger than
        /// 500MB are used as specified. The maximum size of a single cache is
        /// 4GB on 32-bit systems and 10TB on 64-bit systems. (All sizes are in
        /// powers-of-two, that is, 256KB is 218 not 256,000.) For information
        /// on tuning the database cache size, see @selecting-a-cache-size.md.
        /// </para>
        /// <para>
        /// It is possible to specify caches to Figaro large enough they
        /// cannot be allocated contiguously on some architectures. If the cache count (set through
        /// <see cref="FigaroEnv.SetCacheSize"/>) is 0 or 1, the cache will be
        /// allocated contiguously in memory. If it is greater than 1, the cache
        /// will be split across <c>n</c>separate regions, where the region size
        /// is equal to the initial cache size divided by the cache count.
        /// </para>
        /// <para>
        /// The memory pool may be resized by calling
        /// <see cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
        /// after the environment is open. The supplied size will be rounded to
        /// the nearest multiple of the region size and may not be larger than
        /// the maximum size configured with
        /// <see cref="FigaroEnv.SetCacheMax(Figaro.EnvCacheSize)"/>.
        /// </para>
        /// <para>
        /// The
        /// <see cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
        /// method configures a database environment, not only operations
        /// performed using the specified <seealso cref="FigaroEnv"/> handle.
        /// </para>
        /// <para>
        /// The
        /// <see cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
        /// method may be called at any time during the life of the application.
        /// </para>
        /// </remarks>
        /// <seealso cref="FigaroEnv.GetCacheCount"/>
        public void SetCacheSize(EnvCacheSize size, int cacheCount) { }

        /// <summary>
        /// Gets the maximum cache size and stores it in the <see cref="EnvCacheSize"/>
        /// structure, containing the total size in gigabytes and bytes.
        /// </summary>
        /// <returns>
        /// An <see cref="EnvCacheSize"/>
        /// structure containing the configured size of the environment cache.
        /// </returns>
        /// <remarks>
        /// The specified size is rounded to the nearest multiple of the cache region
        /// size, which is the initial cache size divided by the number of regions
        /// specified to the <see cref="FigaroEnv.GetCacheSize"/> method. If no
        /// value is specified, it defaults to the initial cache size.
        /// <para> The <see cref="FigaroEnv.GetCacheMax"/> method may be called at
        /// any time during the life of the application. </para>
        /// </remarks>
        public EnvCacheSize GetCacheMax() { return new EnvCacheSize(); }

        /// <summary>
        /// Sets the maximum cache size.
        /// </summary>
        /// <param name="size">The desired maximum cache size.</param>
        /// <remarks>
        /// The specified size is rounded to the nearest multiple of the cache region
        /// size, which is the initial cache size divided by the number of regions
        /// specified to the
        /// <see cref="FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,int)"/>
        /// method. If no value is specified, it defaults to the initial cache size.
        /// <para>
        /// This method configures a database environment, not only operations performed using the specified
        /// <see cref="FigaroEnv"/> handle.
        /// </para>
        /// <alert class="warn">
        ///  Setting this value in an empty <see cref="FigaroEnv"/> may throw an exception. If this occurs, set the cache size first
        ///  with a call to <see cref="SetCacheSize"/>.
        /// </alert>
        /// <para>
        ///     This method may be called at any time during the life of the application.
        /// </para>
        /// </remarks>
        public void SetCacheMax(EnvCacheSize size) { }

        /// <summary>
        /// Gets the number of caches created.
        /// </summary>
        /// <returns>The number of created caches.</returns>
        public int GetCacheCount()
        {
            return 0;
        }

        /// <summary>
        /// Gets or sets the deadlock detector to be run whenever a lock
        /// conflict occurs, and specify what lock request(s) should be rejected.
        /// </summary>
        /// <value>
        /// The enumeration indicating the type of deadlock policy set in place.
        /// </value>
        /// <remarks>
        /// As transactions acquire locks on behalf of a single locker ID, rejecting
        /// a lock request associated with a transaction normally requires the
        /// transaction be aborted.
        /// <para>
        /// The <see cref="FigaroEnv.DeadlockDetectPolicy"/> property configures a database
        /// environment, not only operations performed using the specified
        /// <see cref="FigaroEnv"/> handle.
        /// </para>
        /// <para>
        /// This property may be set at any
        /// time during the life of the application.
        /// </para>
        /// </remarks>
        public DeadlockDetectType DeadlockDetectPolicy { get; set; }

        /// <summary>
        /// Runs one iteration of the deadlock detector. The deadlock detector traverses the
        /// lock table and marks one of the participating lock requesters for rejection in
        /// each deadlock it finds.
        /// <para>
        /// The method either throws an exception that encapsulates a non-zero error value
        /// on failure, and returns the number of lock requests that were rejected.
        /// </para>
        /// </summary>
        /// <param name="detectType">The parameter specifying which lock requests to reject.</param>
        /// <returns>The number of rejected lock requests.</returns>
        public uint LockDetect(DeadlockDetectType detectType)
        {
            return 0;
        }

        /// <summary>Set the password used by the Figaro library to perform encryption and decryption. </summary>
        /// <param name="password">The password to set.</param>
        /// <param name="encrypt">If <c>true</c>, uses the <c>Rijndael</c>/AES algorithm for encryption or decryption. If <c>false</c>, disables encryption on an encrypted environment. </param>
        /// <seealso cref="IsEncrypted"/>
        public void SetEncryption(string password, bool encrypt) { }

        /// <summary>Gets a value indicating whether the containers is encrypted.</summary>
        public bool IsEncrypted { get;private set; }

        /// <summary>
        /// Gets a list of data directories specified within the <see cref="FigaroEnv"/> instance.
        /// </summary>
        /// <returns>A list of data directories.</returns>
        public ReadOnlyCollection<string> GetDataDirectories()
        {
            return new ReadOnlyCollection<string>(new List<string>());
        }

        /// <summary>
        /// Configure a database environment.
        /// </summary>
        /// <param name="envConfig">The environment setting to enable or disable.</param>
        /// <param name="enabled">If <c>true</c>, enable the environment setting.</param>
        /// <remarks>
        /// See <see cref="EnvConfig"/> for more information about the configuration settings.
        /// </remarks>
        /// <seealso cref="EnvConfig"/>
        public void SetEnvironmentOption(EnvConfig envConfig, bool enabled) { }

        /// <summary>
        /// Get the <see cref="EnvConfig"/> flags set in the
        /// <see cref="FigaroEnv"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="EnvConfig"/> flags set in the
        /// <see cref="FigaroEnv"/>.
        /// </returns>
        public EnvConfig GetEnvironmentOptions()
        {
            return EnvConfig.None;
        }

        /// <summary>
        /// Gets the approximate number of threads in the database environment. This value is used to determine memory sizing
        /// and thread control block reclamation policy in the <see cref="FigaroEnv"/>.
        /// </summary>
        /// <value>The approximate number of threads in the database environment.</value>
        /// <seealso cref="SetThreadCount"/>
        public byte ThreadCount { get;private set; }

        /// <summary>
        /// Sets the approximate number of threads in the database environment.
        /// </summary>
        /// <value>The approximate number of threads in the database environment.</value>
        /// <remarks>
        /// Must be set prior to opening the database environment if the <see cref="FigaroEnv.FailCheck"/>
        /// method will be used. Setting the thread count does not set the
        /// maximum number of threads but is used to determine memory sizing and the thread control
        /// block reclamation policy.
        /// <para>
        /// This property cannot be set after the <see cref="Open"/> method has been called.
        /// </para>
        /// </remarks>
        /// <param name="threadCount">The number of database environment threads.</param>
        /// <seealso cref="ThreadCount"/>
        public void SetThreadCount(byte threadCount) { }

        /// <summary>
        /// Sets the timeout values for the requested <see cref="EnvironmentTimeoutType"/>.
        /// </summary>
        /// <param name="microseconds">The number of microseconds to set the timeout to.</param>
        /// <param name="timeoutType">The timeout type to set.</param>
        /// <remarks>
        /// All timeouts are checked whenever a thread of control blocks on a lock or when deadlock
        /// detection is performed.
        /// <para>
        /// In the case of
        /// <see cref="EnvironmentTimeoutType.Lock"/>, the lock is one
        /// requested explicitly through the Lock subsystem interfaces. In the case of
        /// <see cref="EnvironmentTimeoutType.Transaction"/>, the lock is one
        /// requested on behalf of a transaction. In either case, it may be a lock requested by the database
        /// access methods underlying the application.
        /// </para><para>
        /// As timeouts are only checked when the lock request first blocks or when deadlock detection
        /// is performed, the accuracy of the timeout depends on how often deadlock detection is performed.
        /// </para>
        /// </remarks>
        /// <seealso cref="EnvironmentTimeoutType"/>
        /// <seealso cref="GetTimeout"/>
        public void SetTimeout(uint microseconds, EnvironmentTimeoutType timeoutType) { }

        /// <summary>
        /// Gets the timeout values for the specified <see cref="EnvironmentTimeoutType"/>.
        /// </summary>
        /// <param name="timeoutType">The timeout type to look up.</param>
        /// <returns>The timeout value, in microseconds.</returns>
        /// <remarks>
        /// All timeouts are checked whenever a thread of control blocks on a lock or when deadlock
        /// detection is performed.<para>
        /// In the case of <see cref="EnvironmentTimeoutType.Lock"/>, the lock
        /// is one requested explicitly through the Lock subsystem interfaces. In the case
        /// of <see cref="EnvironmentTimeoutType.Transaction"/>, the lock
        /// is one requested on behalf of a transaction. In either case, it may be a lock requested by the
        /// database access methods underlying the application.
        /// </para><para>
        /// As timeouts are only checked when the lock request first blocks or when deadlock detection is performed, the accuracy of the timeout
        /// depends on how often deadlock detection is performed.
        /// </para>
        /// </remarks>
        /// <seealso cref="EnvironmentTimeoutType"/>
        /// <seealso cref="SetTimeout"/>
        public uint GetTimeout(EnvironmentTimeoutType timeoutType)
        {
            return 0;
        }

        /// <summary>
        /// Gets the directory files created to back in-memory access method databases will be created relative to this path.
        /// These temporary files can be quite large, depending on the size of the database.
        /// </summary>
        /// <remarks>
        /// If no directories are specified, the following alternatives are checked in the specified order. The first existing directory path is used for all temporary files.
        /// <list type="numbered">
        /// <item>The value of the environment variable <em>TMPDIR</em>.</item>
        /// <item>The value of the environment variable <em>TEMP</em>.</item>
        /// <item>The value of the environment variable <em>TMP</em>.</item>
        /// <item>The value of the environment variable <em>TempFolder</em>.</item>
        /// <item>The value returned from the <see href="http://msdn.microsoft.com/en-us/library/aa364992(VS.85).aspx">GetTempPath</see> Win32 API call.</item>
        /// <item>The directory <em>C:\temp</em>.</item>
        /// <item>The directory <em>C:\tmp</em>.</item>
        /// </list>
        /// <para>
        /// <em>Note:</em> environment variables are checked only if <see cref="EnvOpenOptions.UseEnvironment"/> or <see cref="EnvOpenOptions.UseEnvironmentRoot"/> to <see cref="Open"/>.
        /// </para>
        /// </remarks>
        /// <value>
        /// The path of a directory to be used as the location of temporary files.
        /// </value>
        public string TempDirectory { get;private set; }

        /// <summary>
        /// Sets a database environment's temporary directory path.
        /// </summary>
        /// <remarks>
        /// <see cref="SetTempDirectory"/> configures operations for the specified <see cref="FigaroEnv"/> instance.
        /// database environment.
        /// <para>
        /// This method should not be called after the <see cref="Open"/> method. If the database environment already exists when <see cref="Open"/> is called, the
        /// information specified to <see cref="SetTempDirectory"/> must be consistent with the existing environment or database corruption can occur.
        /// </para>
        /// </remarks>
        /// <param name="dir">The new temp directory for the specified <see cref="FigaroEnv"/> instance.</param>
        public void SetTempDirectory(string dir) { }

        /// <summary>
        /// Turns specific additional informational and debugging messages in the Figaro message
        /// output on and off. To see the additional messages, verbose messages must also be configured for
        /// the application. For more information on verbose messages, see the
        /// <see cref="SetMessageFile"/> method.
        /// </summary>
        /// <param name="which">The verbose option to enable or disable.</param>
        /// <param name="enabled">If <c>true</c>, enables the setting.</param>
        /// <remarks>
        /// 	<see cref="FigaroEnv.SetVerbose(Figaro.VerboseOption,bool)"/>
        /// may be called at any time during the life of the application.
        /// </remarks>
        /// <seealso cref="FigaroEnv.MessageFile"/>
        /// <seealso cref="FigaroEnv.MessageEventEnabled"/>
        public void SetVerbose(VerboseOption which, bool enabled) { }

        /// <summary>
        /// Returns whether the specified which parameter is currently set or not.
        /// </summary>
        /// <param name="which">The <see cref="VerboseOption"/> you want to
        /// confirm the setting for.</param>
        /// <returns>
        /// This method returns <c>true</c> if the
        /// <see cref="VerboseOption"/> parameter has been set.
        /// </returns>
        /// <remarks>
        /// This method may be called at any time during the life of the application.
        /// </remarks>
        public bool GetVerbose(VerboseOption which)
        {
            return true;
        }

        /// <summary>
        /// Gets the maximum number of locking entities supported by the underlying Figaro environment.
        /// </summary>
        /// <value>
        /// The maximum number of lockers.
        /// </value>
        /// <seealso cref="SetMaxLockers"/>
        public uint MaxLockers { get { return 0; } }

        /// <summary>
        /// Sets the maximum number of lockers.
        /// </summary>
        /// <remarks>
        /// This value is used by <see cref="Open"/> to estimate how much space to allocate for
        /// various lock-table data structures. The default value is
        /// 1000 lockers. For specific information on configuring the size of the lock subsystem, see
        /// Configuring locking: sizing the system.
        /// <para>
        /// <see cref="SetMaxLockers"/> configures a database environment, not only operations performed
        /// using the specified <see cref="FigaroEnv"/> handle.
        /// </para><para>
        /// This method may not be called after the <see cref="Open"/> method. If the database
        /// environment already exists when <see cref="Open"/> is called, the information specified to
        /// <see cref="SetMaxLockers"/> will be ignored.
        /// </para>
        /// </remarks>
        /// <param name="lockers">The maximum number of lockers.</param>
        /// <seealso cref="MaxLockers"/>
        public void SetMaxLockers(uint lockers) { }

        /// <summary>
        /// Gets the maximum number of locks supported by the underlying Figaro Environment.
        /// </summary>
        /// <value>
        /// The maximum number of locks supported by the database environment.
        /// </value>
        /// <seealso cref="SetMaxLocks"/>
        public uint MaxLocks
        {
            get { return 0; }
        }

        /// <summary>
        /// This value is used by <see cref="Open"/> to estimate how much space to allocate for various lock-table data structures.
        /// The default value is 1000 locks. For specific information on configuring the size of the lock subsystem, see
        /// Configuring locking: sizing the system.
        /// </summary>
        /// <remarks>
        /// The <see cref="MaxLocks"/> property configures a database environment, not only operations performed using the
        /// specified <see cref="FigaroEnv"/> handle.
        /// <para>
        /// This method should not be called after the <see cref="Open"/> method.
        /// If the database environment already exists when <see cref="Open"/> is called, the information
        /// specified to <see cref="MaxLocks"/> will be ignored.
        /// </para>
        /// </remarks>
        /// <param name="locks">The maximum number of locks.</param>
        /// <seealso cref="MaxLocks"/>
        public void SetMaxLocks(uint locks) { }

        /// <summary>
        /// Gets the maximum number of locked objects supported by the Figaro environment.
        /// </summary>
        /// <value>
        /// The maximum number of locked objects.
        /// </value>
        /// <seealso cref="SetMaxLockedObjects"/>
        public uint MaxLockedObjects { get;private set; }

        /// <summary>
        /// Sets the maximum number of locked objects in the database environment.
        /// </summary>
        /// <remarks>
        /// This value is used by <see cref="Open"/>
        /// to estimate how much space to allocate for various lock-table data structures.
        /// The default value is 1000 objects. For specific information on configuring the size of the lock subsystem,
        /// see Configuring locking: sizing the system.
        /// <para>
        /// <see cref="SetMaxLockedObjects"/> configures the
        /// database environment, not only operations performed using the specified <see cref="FigaroEnv"/> handle.
        /// </para><para>
        /// This method should not be called after the <see cref="Open"/> method. If the database environment already exists
        /// when <see cref="Open"/> is called, the information specified to <see cref="SetMaxLockedObjects"/> will be ignored.
        /// </para>
        /// </remarks>
        /// <param name="objects">The maximum number of objects.</param>
        /// <seealso cref="MaxLockedObjects"/>
        public void SetMaxLockedObjects(uint objects) { }

        /// <summary>
        /// Gets or sets the maximum file size, in bytes, for a file to be mapped into the process address space. If no value is specified, it defaults to 10MB.
        /// </summary>
        /// <value>
        /// The maximum size for a file to be mapped into the process address space.
        /// </value>
        /// <remarks>
        /// Files that are opened read-only in the pool (that also satisfy a few other criteria) are, by default, mapped into the process address
        /// space instead of being copied into the local cache. This can result in better-than-usual performance because available virtual memory
        /// is normally much larger than the local cache, and page faults are faster than page copying on many systems. However, it can cause resource
        /// starvation in the presence of limited virtual memory, and it can result in immense process sizes in the presence of large databases.
        /// <para>
        /// The <see cref="FigaroEnv.MaxFileMapSize"/> property configures a database environment, not only operations performed
        /// using the specified <see cref="FigaroEnv"/> handle.
        /// </para>
        /// <para>
        /// The <see cref="FigaroEnv.MaxFileMapSize"/> property may be set at any time during the life of the application.
        /// </para>
        /// </remarks>
        public ulong MaxFileMapSize { get; set; }

        /// <summary>
        /// Returns the locking subsystem statistics.
        /// </summary>
        /// <param name="clearStatistics">If <c>true</c>, clears (resets) statistics after returning their values.</param>
        /// <returns>
        /// The <see cref="LockStatistics"/> structure containing details of the locking subsystem.
        /// </returns>
        /// <remarks>
        /// The <see cref="FigaroEnv.GetLockStatistics(bool)"/> method
        /// creates a statistical structure of type <see cref="LockStatistics"/> and
        /// copies a pointer to it into a user-specified memory location.
        /// </remarks>
        /// <seealso cref="LockStatistics"/>
        public LockStatistics GetLockStatistics(bool clearStatistics)
        {
            return new LockStatistics();
        }

        /// <summary>
        /// Displays the locking subsystem statistical information found in the <see cref="LockStatistics"/>
        /// structure.
        /// </summary>
        /// <param name="lockStatisticOptions">One or more flag values indicating what information to print.</param>
        /// <remarks>
        /// The information is printed to a specified output channel
        /// (see the <see cref="FigaroEnv.SetMessageFile(System.String)"/> method for more information), or passed to the
        /// <see cref="FigaroEnv.OnMessage"/> event.
        /// </remarks>
        /// <seealso cref="FigaroEnv.SetMessageFile(System.String)"/>
        /// <seealso cref="FigaroEnv.OnMessage"/>
        /// <seealso cref="LockStatisticOptions"/>
        /// <seealso cref="LockStatistics"/>
        public void PrintLockStatistics(LockStatisticOptions lockStatisticOptions) { }

        /// <summary>
        /// Configures <see cref="FigaroEnv"/> message information for output to the file system. Setting to <c>null</c>
        /// reconfigures the output setting.
        /// </summary>
        /// <param name="pathAndFile">fully-qualified path the file to write message event information into.</param>
        /// <remarks>
        /// Alternatively, you could set <see cref="FigaroEnv.MessageEventEnabled"/> to <c>true</c> and capture
        /// the output from the <see cref="FigaroEnv.OnMessage"/> event.
        /// However, you can only use one or the other - if you try to set both, unexpected results may occur including
        /// performance issues.
        /// </remarks>
        public void SetMessageFile(string pathAndFile) { }

        /// <summary>
        /// Gets the file configured for message output.
        /// </summary>
        /// <value>
        /// The message file configured for <see cref="FigaroEnv"/> output.
        /// </value>
        public string MessageFile { get;private set; }

        /// <summary>
        /// Gets the file configured for error output.
        /// </summary>
        /// <value>
        /// The file configured for <see cref="FigaroEnv"/> error output.
        /// </value>
        public string ErrorFile { get;private set; }

        /// <summary>
        /// Adds the path of a directory to be used as the location of the access method
        /// database files. Paths specified to the <see cref="FigaroEnv.Open"/> function
        /// will be searched relative to this path. Paths set using this method are
        /// additive, and specifying more than one will result in each specified directory being
        /// searched for database files. If any directories are specified, created database
        /// files will always be created in the first path specified.
        /// <para>
        /// If no database directories are specified, database files must be named
        /// either by absolute paths or relative to the environment home directory.
        /// This method configures operations performed using the specified <see cref="FigaroEnv"/>
        /// handle, not all operations performed on the underlying database environment.
        /// </para>
        /// <para>
        /// This method may not be called after the
        /// <see cref="FigaroEnv.Open"/> method is called. If the database
        /// environment already exists when <see cref="FigaroEnv.Open"/> is called,
        /// the information specified to this method must be consistent with the
        /// existing environment or corruption can occur.
        /// </para>
        /// </summary>
        /// <param name="dataDirectory">
        /// A directory to be used as a location for database files.
        /// </param>
        public void AddDataDirectory(string dataDirectory) { }

        /// <summary>
        /// When set, additional information regarding error information will be streamed
        /// to this location.
        /// </summary>
        /// <param name="pathAndFile">fully-qualified path the file to write error event information into.</param>
        /// <remarks>
        /// When an error occurs in the Figaro library, an exception is
        /// thrown or an error return value is returned by the interface. In
        /// some cases, however, the exception or returned value may be insufficient
        /// to completely describe the cause of the error, especially during initial
        /// application debugging.
        /// <para>
        /// This error logging enhancement does not slow performance or significantly
        /// increase application size, and may be run during normal operation as well
        /// as during application debugging.
        /// </para>
        /// <para>
        /// This method can be set at any time during the life of the application.
        /// </para>
        /// </remarks>
        public void SetErrorFile(string pathAndFile) { }

        /// <summary>
        /// Gets the number of additional mutexes to allocate.
        /// </summary>
        /// <value>The number of additional mutexes to allocate.</value>
        /// <seealso cref="SetMutexIncrement"/>
        public uint MutexIncrement { get;private set; }

        /// <summary>
        /// Configure the number of additional mutexes to allocate.
        /// </summary>
        /// <remarks>
        /// Additionally, an application may want to allocate mutexes for its own use. The
        /// <see cref="SetMutexIncrement"/> method is used to add a
        /// number of mutexes to the default allocation.
        /// <para>
        /// The <see cref="FigaroEnv.MutexIncrement"/> property configures a database environment, not only operations
        /// performed using the specified <see cref="FigaroEnv"/> handle.
        /// </para>
        /// <para>
        /// <see cref="SetMutexIncrement"/> should not be called
        /// after the <see cref="Open"/> method is called. If the database
        /// environment already exists when <see cref="Open"/>
        /// is called, the information specified to <see cref="SetMutexIncrement"/>
        /// will be ignored.
        /// </para>
        /// </remarks>
        /// <param name="size">The mutex increment size.</param>
        /// <seealso cref="MutexIncrement"/>
        public void SetMutexIncrement(uint size) { }

        /// <summary>Gets or sets a value indicating whether to toggle the <see cref="FigaroEnv.OnMessage"/> event.</summary>
        /// <value>The boolean value determining if <see cref="FigaroEnv.OnMessage"/> is enabled.</value>
        /// <seealso cref="FigaroEnv.OnMessage"/>
        public bool MessageEventEnabled { get; set; }

        /// <summary>Gets or sets a value indicating whether to toggle the <see cref="FigaroEnv.OnErr"/> event.</summary>
        /// <value>The boolean value determining if <see cref="FigaroEnv.OnErr"/> is enabled.</value>
        /// <seealso cref="FigaroEnv.OnErr"/>
        public bool ErrEventEnabled { get; set; }

        /// <summary>Gets or sets a value indicating whether to toggle the <see cref="FigaroEnv.OnProcess"/> event.</summary>
        /// <value>The boolean value determining if <see cref="FigaroEnv.OnProcess"/> is enabled.</value>
        /// <seealso cref="FigaroEnv.OnProcess"/>
        public bool ProcessEventEnabled { get; set; }

        /// <summary>Gets or sets a value indicating whether to toggle the <see cref="FigaroEnv.OnProgress"/> event.</summary>
        /// <value>The boolean value determining if <see cref="FigaroEnv.OnProgress"/> is enabled.</value>
        /// <seealso cref="FigaroEnv.OnProgress"/>
        public bool ProgressEventEnabled { get; set; }

        /// <summary>Gets or sets a value indicating whether to toggle the <see cref="FigaroEnv.OnIsAlive"/> event.</summary>
        /// <remarks>You must enable the threading subsystem before enabling this event, or a <see cref="FigaroEnvException"/> will occur.</remarks>
        /// <value>The boolean value determining if <see cref="FigaroEnv.OnIsAlive"/> is enabled.</value>
        /// <seealso cref="FigaroEnv.OnIsAlive"/>
        /// <seealso cref="EnvOpenOptions.Thread"/>
        /// <exception cref="FigaroEnvException">If the threading subsystem is not enabled.</exception>
        public bool IsAliveEnabled { get; set; }

        /// <summary>
        /// Gets the memory pool statistics.
        /// </summary>
        /// <param name="clear">
        /// If <c>true</c>, clears the statistics from the environment after
        /// retrieval.</param>
        /// <returns>A <see cref="MemoryPoolStatisticsInfo"/> structure containing memory pool statistics.</returns>
        /// <seealso cref="MemoryPoolStatisticsInfo"/>
        public MemoryPoolStatisticsInfo GetMemoryPoolStatistics(bool clear)
        { return new MemoryPoolStatisticsInfo(); }

        /// <summary>
        /// Retrieves mutex statistics.
        /// </summary>
        /// <param name="clear">If <c>true</c>, resets statistics after returning
        /// their values.</param>
        /// <returns>Mutex system statistics.</returns>
        /// <seealso cref="MutexStatistics"/>
        public MutexStatistics GetMutexStatistics(bool clear)
        {
            return new MutexStatistics();
        }

        /// <summary>
        /// Retrieves the transaction subsystem statistics.
        /// </summary>
        /// <param name="clear">If <c>true</c>, resets statistics after returning
        /// their values.</param>
        /// <returns>Transaction system statistics.</returns>
        /// <seealso cref="TransactionStatistics"/>
        public TransactionStatistics GetTransactionStatistics(bool clear)
        { return new TransactionStatistics(); }

        /// <summary>
        /// Displays cache subsystem
        /// </summary>
        /// <param name="options">Behavior options for statistics retrieval.</param>
        public void PrintMemoryPoolStatistics(PrintMemoryPoolStatisticsOptions options)
        { }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

        #endregion IDisposable Members
    }
}