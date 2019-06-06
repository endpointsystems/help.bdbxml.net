// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedParameter.Local
// ReSharper disable CommentTypo
//#define _TDS
//#define _CDS
//#define _ENCRYPT

using System;

// using Figaro.Xml;



namespace Figaro
{
    /// <summary>
    /// The Figaro product edition in use.
    /// </summary>
    public enum FigaroProductEdition
    {
        /// <summary>
        /// Figaro Data Store (DS) edition.
        /// </summary>
        DataStore,
        /// <summary>
        /// Figaro High Availability (HA) edition.
        /// </summary>
        HighAvailability,
        /// <summary>
        /// Figaro Transactional Data Store (TDS) edition.
        /// </summary>
        TransactionalDataStore,
        /// <summary>
        /// Figaro Concurrent Data Store (CDS) edition.
        /// </summary>
        ConcurrentDataStore
    }
}



namespace Figaro
{
    /// <summary>
    /// Print options for displaying <c>Figaro.FigaroReplicationManager</c> statistics.
    /// </summary>
    /// <seealso cref="Figaro.FigaroReplicationManager"/>
    [Flags]
    public enum ReplicationStatisticsOptions
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Reset the statistics after displaying them.
        /// </summary>
        Clear = 1,
        /// <summary>
        /// Display detailed statistics.
        /// </summary>
        DisplayAll = 4
    }

    /// <summary>
    /// Replication Manager startup options.
    /// </summary>
    /// <seealso cref="Figaro.FigaroReplicationManager.Start"/>
    public enum ReplicationStartOptions
    {
        /// <summary>
        /// Starts as a client site, and does not call for an election.
        /// </summary>
        Client = 1,
        /// <summary>
        /// Starts as a master site, and does not call for 
        /// an election. <para>Note that there must never be more than a single 
        /// master in any replication group, and only one site at a 
        /// time should ever be started with this flag specified.</para>
        /// </summary>
        Master = 2,
        /// <summary>
        /// Start as a client and call for an election if no master is found.
        /// </summary>
        Election = 4
    }

    /// <summary>
    /// The status of a remote replication site.
    /// </summary>
    public enum ReplicationManagerStatus
    {
        /// <summary>
        /// The status of the remote replication site is unknown.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The remote replication site is connected and/or online.
        /// </summary>
        Connected = 1,
        /// <summary>
        /// The remote replication site is disconnected and/or offline.
        /// </summary>
        Disconnected = 2
    }

    
    /// <summary>
    /// Some acknowledgment policies use the concept of an electable peer, which is a client 
    /// capable of being subsequently elected master of the replication group. 
    /// </summary>
    public enum RecordAcknowledgmentPolicy
    {
        /// <summary>
        /// The master should wait until all replication clients have acknowledged each permanent replication message.
        /// </summary>
        All = 1,
        /// <summary>
        /// The master should wait until all currently connected replication
        /// clients have acknowledged each permanent replication message.
        /// </summary>
        AllAvailable = 2,
        /// <summary>
        /// The master should wait until all electable peers have acknowledged each permanent replication message.
        /// </summary>
        AllPeers = 3,
        /// <summary>
        /// The master should not wait for any client replication message acknowledgments.
        /// </summary>
        None = 4,
        /// <summary>
        /// The master should wait until at least one client site has acknowledged each permanent replication message.
        /// </summary>
        One = 5,
        /// <summary>
        /// The master should wait until at least one electable peer has acknowledged each permanent replication message.
        /// </summary>
        OnePeer = 6,
        /// <summary>
        /// The master should wait until it has received acknowledgments from the minimum number of electable
        /// peers sufficient to ensure that the effect of the permanent record remains durable if an election
        /// is held. This is the default acknowledgment policy.
        /// </summary>
        Quorum = 7
    }


    /// <summary>
    /// Configuration options that can be used throughout Figaro when using transactions.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="XmlTransaction"/>
    /// <seealso cref="XmlManager.CreateTransaction()"/>
    [Flags]
    public enum TransactionType
    {
        /// <summary>No options.</summary>
        None,
        /// <summary>
        /// This operation will have degree 2 isolation. This provides for cursor stability but not repeatable reads.
        /// <para>
        /// Data items which have been previously read by this transaction may be deleted or modified by other
        /// transactions before this transaction completes.
        /// </para>
        /// </summary>
        ReadCommitted,
        /// <summary>
        /// This operation will support degree 1 isolation; that is, read operations may return data that has
        /// been modified by other transactions but which has not yet been committed.
        /// <para>
        /// Silently ignored if the <see cref="GetAllDocumentOptions.ReadUncommitted"/> flag was not specified when the underlying container
        /// was opened.
        /// </para>
        /// </summary>
        ReadUncommitted,
        /// <summary>
        /// Do not synchronously flush the log when this transaction commits or prepares.
        /// <para>
        /// This means the transaction will exhibit the ACI (atomic, consistent, and isolated) properties,
        /// but not D (durable) {} that is, database integrity will be maintained but it is possible that this
        /// transaction may be undone during recovery.
        /// </para><para>
        /// This behavior may be set for a Figaro environment using the
        /// <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
        /// property. Any value specified to this method overrides that setting.
        /// </para>
        /// </summary>
        NoSyncTransaction,
        /// <summary>
        /// If a lock is unavailable for any Figaro operation performed in the context of this transaction,
        /// cause the operation to throw a <see cref="DBDeadlockException"/> immediately
        /// instead of blocking on the lock.
        /// </summary>
        NoWaitTransaction,
        /// <summary>
        /// This transaction will execute with snapshot isolation.
        /// <para>
        /// For containers with the <see cref="ContainerConfig.MultiVersion"/> flag
        /// set, data values will be read as they are when the transaction begins, without taking read locks.
        /// </para><para>
        /// Silently ignored for operations on databases with <see cref="ContainerConfig.MultiVersion"/> not set on the underlying
        /// container (read locks are acquired).
        /// </para><para>
        /// An error will be returned from update operations if a snapshot transaction attempts to update data
        /// which was modified after the snapshot transaction read it.
        /// </para>
        /// </summary>
        SnapshotTransaction,
        /// <summary>
        /// Synchronously flush the log when this transaction commits or prepares.
        /// This means the transaction will exhibit all of the ACID (atomic, consistent, isolated, and durable)
        /// properties.
        /// <para>
        /// This behavior is the default for Figaro environments unless the
        /// <see cref="EnvConfig.NoSyncTransaction"/> flag was specified to the
        /// <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
        /// method. Any value specified to this method overrides that setting.
        /// </para>
        /// </summary>
        SyncTransaction
    }

    /// <summary>
    /// The status of a transaction.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Indicates a transaction is running.
        /// </summary>
        Running,
        /// <summary>
        /// The transaction is being prepared.
        /// </summary>
        Prepared,
        /// <summary>
        /// The transaction has been committed.
        /// </summary>
        Committed,
        /// <summary>
        /// The transaction was aborted.
        /// </summary>
        Aborted
    }

    /// <summary>
    /// Options for printing memory pool statistics.
    /// </summary>
    /// <seealso cref="FigaroEnv.PrintMemoryPoolStatistics"/>
    [FlagsAttribute]
    public enum PrintMemoryPoolStatisticsOptions
    {
        /// <summary>
        /// Reset statistics after displaying their values.
        /// </summary>
        Clear,
        /// <summary>
        /// Display all available information.
        /// </summary>
        All,
        /// <summary>
        /// Display the buffers with hash chains.
        /// </summary>
        HashChains
    }

    /// <summary>
    /// The logging options used to configure a <see cref="FigaroEnv"/> instance.
    /// </summary>
    [Flags]
    public enum EnvLogOptions
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Turn off system buffering to avoid double caching.
        /// </summary>
        Direct = 0x00000002,
        /// <summary>
        /// Flush log writes to the backing disk before returning from the write system call as necessary, rather than
        /// flushing log writes explicitly in a separate system call.
        /// </summary>
        DSync = 0x00000004,
        /// <summary>
        /// If set, automatically removes log files that are no longer needed. Automatic log file removal is likely to make catastrophic recovery impossible.
        /// </summary>
        AutoRemove = 0x00000001,
        /// <summary>
        /// If set, maintain transaction logs in memory rather than on disk.
        /// </summary>
        InMemory = 0x00000008,
        
        /// <summary>
        /// Do not synchronize the log.This will require a manual flush/sync.
        /// </summary>
        NoSync = 0x00000020,
        /// <summary>
        /// If set, zero all pages of a log file when that log file is created. This has shown to provide greater
        /// transaction throughput in some environments. The log file will be zeroed by the thread which needs to
        /// re-create the new log file. Other threads may not write to the log file while this is happening.
        /// </summary>
        Zero = 0x00000010
    }


    /// <summary>
    /// The types of events that would generate progress events in
    /// the <see cref="FigaroEnv"/>.
    /// </summary>
    /// <seealso cref="FigaroEnv"/>
    public enum FeedbackEvent
    {
        /// <summary>A database upgrade is being performed.</summary>
        Upgrade,
        /// <summary>The environment is verifying the containers.</summary>
        Verify,
        /// <summary>A database recovery is being performed.</summary>
        Recover,
        /// <summary>An insurance policy against unknown events being caught.</summary>
        Other = 0
    }

    /// <summary>
    /// Salvage options to use when performing container verification.
    /// </summary>
    /// <seealso cref="XmlManager.VerifyContainer(string,SalvageOptions)"/>
    public enum SalvageOptions
    {
        /// <summary>No salvage flags.</summary>
        None,
        /// <summary>
        /// Write the key/data pairs from all databases in the file to the specified output stream.
        /// <para>
        /// The output format is the same as that specified for the <c>db_dump</c> utility, and can be used as
        /// input for the db_load utility.
        /// </para>
        /// </summary>
        Salvage, // Equates to DB_SALVAGE,
        /// <summary>
        /// Output all the key/data pairs in the file that can be found.
        /// <para>
        /// By default, Verify does not assume corruption. For example, if a key/data pair on a page is marked as
        /// deleted, it is not then written to the output file. When
        /// <see cref="SalvageAggressive"/> is specified,
        /// corruption is assumed, and any key/data pair that can be found is written. In this case,
        /// key/data pairs that are corrupted or have been deleted may appear in the
        /// output (even if the file being salvaged is in no way corrupt), and the output will
        /// almost certainly require editing before being loaded into a database.
        /// </para>
        /// </summary>
        SalvageAggressive // Equates to DB_SALVAGE | DB_AGGRESSIVE
    }

    /// <summary>
    /// Specifies the subsystems that are initialized and how the application's environment affects
    /// Figaro file naming, among other things.
    /// </summary>
    /// <remarks>
    /// The choice of subsystems initialized for a Figaro database environment is specified by the thread of control
    /// initially creating the environment. Any subsequent thread of control joining the environment will automatically be
    /// configured to use the same subsystems as were created in the environment (unless the thread of control requests
    /// a subsystem not available in the environment, which will fail). Applications joining an environment, able to
    /// adapt to whatever subsystems have been configured in the environment, should open the environment without
    /// specifying any subsystem flags. Applications joining an environment, requiring specific subsystems from their
    ///	environments, should open the environment specifying those specific subsystem flags.
    /// <para>
    ///	This enumeration can be bitwise OR'd together with multiple values where it is used.
    ///	</para>
    /// </remarks>
    /// <see cref="FigaroEnv.Open"/>
    /// <see cref="FigaroEnv"/>
    [Flags]
    public enum EnvOpenOptions
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Initialize the transaction subsystem. This subsystem should be used when recovery and atomicity of
        /// multiple operations are important. The <see cref="EnvOpenOptions.InitTransaction"/> flag implies the
        /// <see cref="InitLog"/> flag.
        /// </summary>
        InitTransaction,
        /// <summary>
        ///     Combines the most commonly used transaction flags:
        /// <see cref="InitLock"/>, <see cref="InitLog"/>, <see cref="InitMemoryBufferPool"/>, and <see cref="InitTransaction"/>.
        /// </summary>
        TransactionDefaults,
        /// <summary>
        /// Run normal recovery on this environment before opening it for normal use.
        /// If this flag is set, the <see cref="Create"/> and
        /// <see cref="EnvOpenOptions.InitTransaction"/> flags must also be set because the regions
        /// will be removed and re-created, and transactions are required for application recovery.
        /// </summary>
        Recover,
        /// <summary>
        /// Run catastrophic recovery on this environment before opening it for normal use.
        /// If this flag is set, the <see cref="Create"/> and <see cref="EnvOpenOptions.InitTransaction"/> flags must also
        /// be set, because the regions will be removed and re-created, and transactions are required for application recovery.
        /// </summary>
        RecoverFatal,
        /// <summary>
        /// Check to see if recovery needs to be performed before opening the database environment.
        /// </summary>
        Register,
        /// <summary>
        /// Initialize locking for the Figaro Concurrent Data Store subsystem.
        /// <para>
        /// In this mode, Figaro provides multiple reader/single writer access. The only other subsystem that should be
        /// specified with the <see cref="InitConcurrentDataStore"/>
        /// flag is <see cref="InitMemoryBufferPool"/>.
        /// </para>
        /// </summary>
        InitConcurrentDataStore,
        /// <summary>
        /// Initialize the Concurrent Data Store (CDS) defaults (<see cref="InitConcurrentDataStore"/> and <see cref="InitMemoryBufferPool"/>).
        /// Note that <see cref="EnvConfig.AllConcurrentDatabases"/> will automatically be set with this flag enabled.
        /// </summary>
        ConcurrentDataStoreDefaults,
        /// <summary>
        /// Initialize the locking subsystem.Use when multiple processes or threads are reading and writing a
        /// database so that they do not interfere with each other. If all threads are accessing the database(s)
        /// read-only, locking is unnecessary. When the <see cref="EnvOpenOptions.InitLog"/> flag
        /// is specified, it is usually necessary to run a deadlock detector, as well.
        /// See <see cref="FigaroEnv.DeadlockDetectPolicy"/> for more information.
        /// </summary>
        InitLock,
        /// <summary>
        /// Initialize the logging subsystem. Used when recovery from application or system failure is necessary. If the log region is
        /// being created and log files are already present, the log files are reviewed and writes are appended to the
        /// end of the log, rather than overwriting current log entries.
        /// </summary>
        InitLog,
        /// <summary>
        /// Initialize the shared memory buffer pool subsystem. Used whenever an application is using any Figaro access method.
        /// </summary>
        InitMemoryBufferPool,
        /// <summary>
        /// Cause the <see cref="FigaroEnv"/> handle returned by
        /// <see cref="FigaroEnv.Open"/> to be free-threaded;
        /// that is, concurrently usable by multiple threads in the address space. This flag should be specified if the
        /// <see cref="FigaroEnv"/> handle will be concurrently used by more than one thread in the process, or if any Db handles opened in the scope of
        /// the <see cref="FigaroEnv"/> handle will be concurrently used by more than one thread in the process.
        /// </summary>
        Thread,
        /// <summary>
        /// The Figaro process' environment may be permitted to specify information to be used when naming files; see @file-naming.md.
        /// Because permitting users to specify which files are used can create security problems, environment information will
        /// be used in file naming for all users only if the <see cref="UseEnvironment"/> flag is set.
        /// </summary>
        UseEnvironment,
        /// <summary>
        /// The Figaro process' environment may be permitted to specify information to be used when naming files; see @file-naming.md.
        /// Because permitting users to specify which files are used can create security problems, if the
        /// <see cref="UseEnvironmentRoot"/> flag is set,
        /// environment information will be used for file naming only for users with appropriate permissions
        /// (for example, users with a user-ID of 0 on UNIX systems).
        /// </summary>
        UseEnvironmentRoot,
        /// <summary>Cause Figaro subsystems to create any underlying files, as necessary.</summary>
        Create,
        /// <summary>Lock shared Figaro environment files and memory-mapped databases into memory.</summary>
        Lockdown,
        /// <summary>
        /// Allocate region memory from the heap instead of from memory backed by the file system or system shared memory.
        /// </summary>
        /// <remarks>
        /// This flag implies the environment will only be accessed by a single process (although that process may be multithreaded). This flag has two effects on the
        /// Figaro environment. First, all underlying data structures are allocated from per-process memory instead of from shared memory that is accessible to
        /// more than a single process. Second, mutexes are only configured to work between threads.
        /// <para>
        /// This flag should not be specified if more than a single process is accessing the environment because it is likely to cause database corruption and
        /// unpredictable behavior. For example, if both a server application and Figaro utilities (for example, db_archive, db_checkpoint or db_stat)
        /// are expected to access the environment, the <see cref="Private"/> flag should not be specified.
        /// </para>
        /// </remarks>
        /// <see href="@shared-memory-regions.md"/>
        Private,
        /// <summary>Allocate region memory from system shared memory instead of from heap memory or memory backed by
        /// the file system. See @shared-memory-regions.md for more information.</summary>
        SystemSharedMem,
        ///<summary>
        /// Initialize the replication subsystem. This subsystem should be used whenever an application 
        /// plans on using replication. This flag requires the <see cref="InitTransaction"/> 
        /// and <see cref="InitLock"/> flags also be configured. 
        /// </summary>
        Replication,
        /// <summary>
        /// Initializes the Replication subsystem with the <c>Create</c>, <c>InitLock</c>, <c>InitLog</c>, <c>InitMemoryBufferPool</c>, <c>InitTransaction</c>, <c>Recover</c>, and <c>Thread</c> values set.
        /// </summary>
        ReplicationDefaults,
        /// <summary>
        /// Run a failcheck operation against the environment, which checks for threads of control (either a true thread or a process) 
        /// that have exited while manipulating Berkeley DB library data structures, while holding a logical database lock, or 
        /// with an unresolved transaction (that is, a transaction that was never aborted or committed). 
        /// </summary>
        /// <remarks>
        /// If the failcheck determines a thread of control exited while holding database read locks, it will release those locks. If 
        /// the failcheck determines a thread of control exited with an unresolved transaction, the transaction will be aborted. In 
        /// either of these cases, the failcheck will return and the application may continue to use the database environment.
        /// </remarks>
        Failcheck
    }

    /// <summary>
    /// Used to select the types and attributes of log files returned.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="FigaroEnv.LogArchive(Figaro.LogArchiveOptions)"/>
    [FlagsAttribute]
    public enum LogArchiveOptions
    {
        /// <summary>All pathnames are returned as absolute pathnames, instead of relative to the database home directory.</summary>
        AbsolutePathNames, // = DB_ARCH_ABS,
        // <see cref="FigaroEnv.LogArchive(Figaro.LogArchiveOptions)"/> will not

        /// <summary>
        /// Return the database files that need to be archived in order to recover the database
        /// from catastrophic failure.
        /// <para>
        /// If any of the database files have not been accessed during the lifetime of the current log files,
        /// <see cref="FigaroEnv.LogArchive"/> will not
        /// include them in this list. It is also possible that some of the files referred to by the log have since been
        /// deleted from the system.
        /// </para><para>
        /// The <see cref="LogArchiveOptions.Data"/> and
        /// <see cref="LogArchiveOptions.Log"/> flags are mutually exclusive.
        /// </para>
        /// </summary>
        Data, // = DB_ARCH_DATA,
        /// <summary>Return all the log filenames, regardless of whether or not they are in use.
        /// The <see cref="LogArchiveOptions.Data"/> and
        /// <see cref="LogArchiveOptions.Log"/> flags are mutually exclusive.</summary>
        Log, // = DB_ARCH_LOG,
        /// <summary>
        /// Remove log files that are no longer needed; no filenames are returned. Automatic log
        /// file removal is likely to make catastrophic recovery impossible.
        /// </summary>
        Remove // = DB_ARCH_REMOVE
    }

    /// <summary>
    /// Used to describe the Figaro events raised by the <see cref="FigaroEnv.ProcessEventEnabled"/>
    /// property.
    /// </summary>
    /// <seealso cref="FigaroEnv.ProcessEventEnabled"/>
    /// <seealso cref="FigaroEnv"/>
    public enum EnvironmentEvent
    {
        /// <summary>
        /// The database environment has failed. All threads of control in the database environment
        /// should exit the environment, and recovery should be run.
        /// Errors can occur in the database library where the only solution is to shut down the application and run recovery
        /// (for example, if Figaro is unable to allocate heap memory). In such cases, the database methods will throw
        /// <see cref="RunRecoveryException"/>. It is often easier to simply exit the application
        /// when such errors occur rather than gracefully return up the stack.
        /// </summary>
        Panic = 0,
        /// <summary>
        /// The local site is now a replication client.
        /// </summary>
        ReplicationClient = 4,
        ///<summary>
        ///The local replication site has just won an election.
        ///An application using the Base replication API should arrange for a call to the DB_ENV->rep_start method after receiving this event
        ///to reconfigure the local environment as a replication master.
        ///Replication XmlManager applications may safely ignore this event. The Replication XmlManager calls <c>StartReplication</c>
        ///automatically on behalf of the application when appropriate (resulting in firing of the<c> ReplicationMaster</c> event).
        ///</summary>
        ReplicationSiteElected = 9,
        ///<summary>The local Replication Manager site has been removed from the group.</summary>
        ReplicationLocalSiteRemoved = 14,
        ///<summary>A new Replication Manager site has joined the replication group.</summary>
        ReplicationSiteAdded = 19,
        ///<summary>An existing Replication Manager site has been removed from the replication group.</summary>
        ReplicationSiteRemoved = 20,
        ///<summary>The local site is now the master site of its replication group. It is the application's responsibility to begin acting as the master environment.</summary>
        ReplicationMaster = 15,
        ///<summary>The replication group of which this site is a member has just established a new master; the local site is not the new master. The event_info parameter points to an integer containing the environment ID of the new master.</summary>
        ReplicationNewMaster = 17,
        ///<summary>The replication manager did not receive enough acknowledgments (based on the acknowledgment policy configured with DB_ENV->repmgr_set_ack_policy) to ensure a transaction's durability within the replication group. The transaction will be flushed to the master's local disk storage for durability. </summary>
        ReplicationAckFailure = 18,
        ///<summary>The client has completed startup synchronization and is now processing live log records received from the master. </summary>
        ReplicationStartupDone = 21,
        /// <summary>A database write to stable storage failed.</summary>
        WriteEventFailure = 23
    }

    /// <summary>
    /// Used in
    /// <see cref="FigaroEnv.GetLockStatistics(bool)"/>
    /// to determine which statistics to display as well as the option to reset values.
    /// </summary>
    /// <seealso cref="FigaroEnv.GetLockStatistics(bool)"/>
    public enum LockStatisticOptions
    {
        /// <summary>No options.</summary>
        None,
        /// <summary>Display all available information.</summary>
        AllStats,
        /// <summary>Reset statistics after displaying their values.</summary>
        ClearStats,
        /// <summary>Display the lock conflict matrix.</summary>
        ConflictMatrix,
        /// <summary>Display the lockers within hash chains.</summary>
        LockerHashChains,
        /// <summary>Display the locks within hash chains.</summary>
        LockHashChains,
        /// <summary>Display the locking subsystem parameters.</summary>
        LockParameters
    }

    /// <summary>
    ///    A list of lock requests to request in
    ///    <see cref="FigaroEnv.DeadlockDetectPolicy"/>.
    /// </summary>
    /// <seealso cref="FigaroEnv.DeadlockDetectPolicy"/>
    public enum DeadlockDetectType
    {
        /// <summary>
        /// Use whatever lock policy was specified when the database environment was created.
        ///	If no lock policy has yet been specified, set the lock policy to <see cref="DeadlockDetectType.Random"/>.
        /// </summary>
        Default,
        /// <summary>Reject lock requests which have timed out. No other deadlock detection is performed.</summary>
        Expire,
        /// <summary>Reject the lock request for the locker ID with the most locks.</summary>
        MaxLocks,
        /// <summary>Reject the lock request for the locker ID with the most write locks.</summary>
        MaxWrite,
        /// <summary>Reject the lock request for the locker ID with the fewest locks.</summary>
        MinLocks,
        /// <summary>Reject the lock request for the locker ID with the fewest write locks.</summary>
        MinWrite,
        /// <summary>Reject the lock request for the locker ID with the oldest lock. </summary>
        Oldest,
        /// <summary>Reject the lock request for a random locker ID.</summary>
        Random,
        /// <summary>Reject the lock request for the locker ID with the youngest lock.</summary>
        Youngest
    }

    /// <summary>Informational and debugging messages in the message output.</summary>
    /// <seealso cref="FigaroEnv.GetVerbose(Figaro.VerboseOption)"/>
    /// <seealso cref="FigaroEnv.SetVerbose(Figaro.VerboseOption,bool)"/>
    public enum VerboseOption
    {
        /// <summary>
        /// Display additional information when doing deadlock detection.
        /// </summary>
        Deadlock,
        /// <summary>
        /// Display additional information when performing file system operations such as open, close
        /// or rename. May not be available on all platforms.
        /// </summary>
        FileOps,
        /// <summary>
        /// Display additional information when performing all file system operations, including read and
        /// write. May not be available on all platforms.
        /// </summary>
        AllFileOps,
        /// <summary>
        /// Display additional information when performing recovery.
        /// </summary>
        Recovery,

        /// <summary>
        /// Display additional information concerning support for the
        /// <see cref="EnvOpenOptions.Register"/> flag to the <see cref="FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)"/> method.
        /// </summary>
        Register,

        /// <summary>Display detailed information when processing replication messages.</summary>
        Replication,

        /// <summary>
        /// Display the waits-for table when doing deadlock detection.
        /// </summary>
        WaitsFor
    }

    /// <summary>
    /// One or more actions to take against the statistics subsystem after retrieving system statistics.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="FigaroEnv.PrintDefaultStatistics(Figaro.StatPrintOptions)"/>
    [FlagsAttribute]
    public enum StatPrintOptions
    {
        /// <summary>
        /// No options.
        /// </summary>
        None,
        /// <summary>
        /// Display all available information.
        /// </summary>
        AllStats, // DB_STAT_ALL
        /// <summary>
        /// Reset statistics after displaying their values.
        /// </summary>
        ClearStats, // DB_STAT_CLEAR
        /// <summary>
        /// Display information for all configured subsystems.
        /// </summary>
        AllSubsystems // DB_STAT_SUBSYSTEM
    }

    /// <summary>
    /// Used to identify the type of <see cref="FigaroEnv"/> timeout setting.
    /// </summary>
    /// <seealso cref="FigaroEnv.SetTimeout(uint,Figaro.EnvironmentTimeoutType)"/>
    /// <seealso cref="FigaroEnv.GetTimeout(Figaro.EnvironmentTimeoutType)"/>
    public enum EnvironmentTimeoutType
    {
        /// <summary>The timeout value for locks in this database environment. </summary>
        Lock = 1,
        /// <summary>
        /// If fail check broadcasting has been configured, then set the timeout value 
        /// on how long a thread will wait for a mutex lock before checking whether 
        /// <see cref="FigaroEnv.FailCheck"/> has marked the mutex as failed. The default 
        /// is to check once every second.
        /// </summary>
        MutexFailcheck = 4,
        /// <summary>
        /// Set the timeout value on how long to wait for processes to exit the environment before 
        /// recovery is started when the <see cref="FigaroEnv.Open(string, EnvOpenOptions)"/> method was 
        /// called with the <see cref="EnvOpenOptions.Register"/> flag and recovery must be performed.
        /// </summary>
        Register = 8,

        /// <summary>The timeout value for transactions in this database environment.</summary>
        Transaction = 2
    }

    /// <summary>
    /// Used by
    /// <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
    /// to set the different
    /// <see cref="FigaroEnv"/> subsystems and
    /// settings for each of the following below.
    /// </summary>
    /// <seealso cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
    public enum EnvConfig
    {
        /// <summary>No flags set.</summary>
        None = 0,

        /// <summary>
        /// If set, Figaro CDS applications will perform locking on an environment-wide basis
        /// rather than on a per-database basis.
        /// Calling
        /// <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with
        /// the <see cref="AllConcurrentDatabases"/> flag only affects the specified
        /// <see cref="FigaroEnv"/> handle (and any other database handles
        /// opened within the scope of that handle). For consistent behavior across
        /// the environment, all <see cref="FigaroEnv"/> handles opened in the
        /// environment must also set the <see cref="AllConcurrentDatabases"/> flag.
        /// The <see cref="AllConcurrentDatabases"/> flag may be used only before the
        /// <see cref="FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)"/> method is called.
        /// </summary>
        /// <remarks>This flag is automatically set if the <see cref="EnvOpenOptions.InitConcurrentDataStore"/> flag is passed when 
        /// the environment is opened.</remarks>
        AllConcurrentDatabases = 0x00000040,

        /// <summary>
        /// Set this flag prior to creating a hot backup of a database environment.
        /// </summary>
        /// <remarks>
        /// If a transaction with the bulk insert optimization enabled is in progress, setting this flag forces a checkpoint in 
        /// the environment. After this flag is set in the environment, the bulk insert optimization is disabled until the flag 
        /// is reset. Using this protocol allows a hot backup procedure to make a consistent copy of the database even when bulk 
        /// transactions are ongoing.
        /// </remarks>
        HotBackupInProgress = 0x00000800,
        
        /// <summary>
        /// If set, operations with no explicit transaction handle specified that modifies databases in the database environment will 
        /// get a transaction handle automatically.
        /// </summary>
        /// <remarks>
        /// This flag may be configured at any time during the life of the application.
        /// </remarks>
        AutoCommit = 0x00000100,
        /// <summary>
        /// Turn off system buffering of database files to avoid double caching.
        /// Calling  <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the<see cref="DirectDB"/> flag only affects the
        /// specified <see cref="FigaroEnv"/> handle (and any other database handles opened within the scope of that handle).
        /// For consistent behavior across the environment, all <see cref="FigaroEnv"/> handles opened in the
        /// environment must set this flag.
        /// The<see cref="DirectDB"/> flag may be used at any time during the life of the application.
        /// </summary>
        DirectDB = 0x00000080,
        
        ///// <summary>
        ///// Turn off system buffering of log files to avoid double caching.
        ///// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the <see cref="DirectLog"/> flag only
        ///// affects the specified <see cref="FigaroEnv"/> handle
        ///// (and any other database handles opened within the scope of that handle). For consistent behavior across
        ///// the environment, all <see cref="FigaroEnv"/> handles opened in the
        ///// environment must also set this flag.
        ///// The <see cref="DirectLog"/> flag may be used at any time during the life of the application.
        ///// </summary>
        //DirectLog = 0x00000002,

        /// <summary>
        /// Configure Figaro to flush database writes to the backing disk before returning from the
        /// write system call, rather than flushing database writes explicitly in a separate system call,
        /// as necessary.
        /// This is only available on some systems (for example, systems supporting the
        /// IEEE/ANSI Std 1003.1 (POSIX) standard O_DSYNC flag, or systems supporting the Windows
        /// FILE_FLAG_WRITE_THROUGH flag). This flag may result in inaccurate file modification times and
        /// other file-level information for database files. This flag will almost certainly
        /// result in a performance decrease on most systems. This flag is only applicable to certain
        /// file systems (for example, the Veritas VxFS file system), where the file system's support for
        /// trickling writes back to stable storage behaves badly (or more likely, has been misconfigured).
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the <see cref="SyncDB"/> flag only affects the
        /// specified <see cref="FigaroEnv"/> handle (and any other handles opened within the scope of
        /// that handle). For consistent behavior across the environment, all <see cref="FigaroEnv"/> handles opened
        /// in the environment must set the <see cref="SyncDB"/> flag.
        /// The <see cref="SyncDB"/> flag may be used at any time during the life of
        /// the application.
        /// </summary>
        SyncDB = 0x00000200,
        
        ///// <summary>
        ///// Configure Figaro to flush log writes to the backing disk before returning from the
        ///// write system call, rather than flushing log writes explicitly in a separate system call, as
        ///// necessary.
        ///// This is only available on some systems (for example, systems supporting the IEEE/ANSI Std
        ///// 1003.1 (POSIX) standard O_DSYNC flag, or systems supporting the Windows <c>FILE_FLAG_WRITE_THROUGH</c>
        ///// flag). This flag may result in inaccurate file modification times and other file-level information
        ///// for Figaro log files. This flag may offer a performance increase on some systems and a
        ///// performance decrease on others.
        ///// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with
        ///// the <see cref="SyncLog"/> flag only affects the specified <see cref="FigaroEnv"/> handle
        ///// (and any other database handles opened within the scope of that handle). For consistent behavior across the environment,
        ///// all <see cref="FigaroEnv"/> handles opened in the environment must also set the <see cref="SyncLog"/> flag.
        ///// The <see cref="SyncLog"/> flag may be used at any time during the life of the application.
        ///// </summary>
        //SyncLog = 0x00000004,

        ///// <summary>
        ///// If set, Figaro will automatically remove log files that are no longer needed.
        ///// Automatic log file removal is likely to make catastrophic recovery impossible.
        ///// Replication applications will rarely want to configure automatic log file removal as it increases the likelihood a master will be unable
        ///// to satisfy a client's request for a recent log record.
        ///// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the <see cref="AutoLogRemove"/> flag
        ///// affects the database environment, including all threads of control accessing the database environment.
        ///// The <see cref="AutoLogRemove"/> flag may be used at any time during the life of the application.
        ///// </summary>
        //AutoLogRemove = 0x00000001,

        ///// <summary>
        ///// If set, maintain transaction logs in memory rather than on disk.
        ///// This means that transactions exhibit the ACI (atomicity,
        ///// consistency, and isolation) properties, but not D (durability) {}
        ///// that is, database integrity will be maintained, but if the
        ///// application or system fails, integrity will not persist. All
        ///// database files must be verified and/or restored from a replication
        ///// group master or archival backup after application or system failure.
        ///// When in-memory logs are configured and no more log buffer space is
        ///// available, Figaro methods may return an additional error value,
        ///// DB_LOG_BUFFER_FULL. When choosing log buffer and file sizes for
        ///// in-memory logs, applications should ensure the in-memory log buffer
        ///// size is large enough that no transaction will ever span the entire
        ///// buffer, and avoid a state where the in-memory buffer is full and no
        ///// space can be freed because a transaction that started in the first
        ///// log "file" is still active. Calling
        ///// <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
        ///// with the <see cref="LogInMemory"/> flag affects the database
        ///// environment, including all threads of control accessing the database
        ///// environment. The <see cref="LogInMemory"/> flag may be used only
        ///// before the
        ///// <see cref="FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)"/>
        ///// method is called.
        ///// </summary>
        //LogInMemory = 0x00000008,

        /// <summary>
        /// Open the database with support for multi-version concurrency control. This will cause updates to the
        /// database to follow a copy-on-write protocol, which is required to support snapshot isolation.This
        /// flag requires that the database be transactionally protected during its open and is not supported
        /// by the queue format.
        /// If set, all databases in the environment will be opened with this flag.
        /// This flag may be used at any time during the
        /// life of the application.
        /// </summary>
        MultiVersion = 0x00000004,
        /// <summary>
        /// If set, Figaro will grant all requested mutual exclusion mutexes and database locks without regard for their actual availability.
        /// This functionality should never be used for purposes other than debugging.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
        /// with the <see cref="NoLocking"/> flag only affects the
        /// specified <see cref="FigaroEnv"/> handle (and any other database handles opened within
        /// the scope of that handle).
        /// The <see cref="NoLocking"/> flag may be used at any time during the life of the application.
        /// </summary>
        NoLocking = 0x00000400,
        /// <summary>
        /// Figaro will copy read-only database files into the local cache instead of potentially mapping
        /// them into process memory.
        /// The <see cref="NoProcessMemoryMap"/> flag
        /// may be used at any time during the life of the application.
        /// </summary>
        NoProcessMemoryMap = 0x00000008,
        /// <summary>
        /// If set, Figaro will ignore any panic state in the database environment
        /// (Database environments in a panic state normally refuse all attempts to call Berkeley
        /// DB functions, throwing <see cref="RunRecoveryException"/>). This functionality should never be used for
        /// purposes other than debugging.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="NoPanic"/> flag only affects the specified <see cref="FigaroEnv"/>
        /// handle (and any other database handles opened within the scope of that handle).
        /// The <see cref="NoPanic"/> flag may be used at any time during the
        /// life of the application.
        /// </summary>
        NoPanic = 0x00000800,
        /// <summary>
        /// Overwrite files stored in encrypted formats before deleting them.
        /// Figaro overwrites files using alternating <c>0xff</c>, <c>0x00</c> and <c>0xff</c> byte patterns. For file
        /// overwriting to be effective, the underlying file must be stored on a fixed-block
        /// file system. Systems with journaling or logging file systems will require operating
        /// system support and probably modification of the database sources.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="Overwrite"/> flag only affects the specified <see cref="FigaroEnv"/>
        /// handle (and any other database handles opened within the scope of that handle).
        /// The <see cref="Overwrite"/> flag may be used at any time
        /// during the life of the application.
        /// </summary>
        Overwrite = 0x00001000,
        /// <summary>
        /// If set, Figaro will set the panic state for the database environment.
        /// (Database environments in a panic state normally refuse all attempts to call
        /// Figaro functions, throwing <see cref="RunRecoveryException"/>.)
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="PanicEnvironment"/> flag affects the database environment, including
        /// all threads of control accessing the database environment.
        /// The <see cref="PanicEnvironment"/> flag may be used only
        /// after the <see cref="FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)"/> method is called.
        /// </summary>
        PanicEnvironment = 0x00002000,
        /// <summary>
        /// In some applications, the expense of page-faulting the underlying shared memory
        /// regions can affect performance. (For example, if the page-fault occurs while holding
        /// a lock, other lock requests can convoy, and overall throughput may decrease.)
        /// If set, Figaro will page-fault shared regions into memory when initially creating
        /// or joining a database environment. In addition, Figaro will write the shared
        /// regions when creating an environment, forcing the underlying virtual memory and
        /// file systems to instantiate both the necessary memory and the necessary disk space.
        /// This can also avoid out-of-disk space failures later on.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="RegionInit"/> flag only affects the specified <see cref="FigaroEnv"/>
        /// handle (and any other database handles opened within the scope of that handle). For
        /// consistent behavior across the environment, all <see cref="FigaroEnv"/>
        /// handles opened in the environment must set the <see cref="RegionInit"/> flag.
        /// The <see cref="RegionInit"/> flag may be used at any time
        /// during the life of the application.
        /// </summary>
        RegionInit = 0x00004000,
        /// <summary>
        /// If set, database calls timing out based on lock or transaction timeout values will
        /// throw a <see cref="DBLockNotGrantedException"/> exception instead of
        /// <see cref="DBDeadlockException"/>. This
        /// allows applications to distinguish between operations which have deadlocked and
        /// operations which have exceeded their time limits.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="TimeNotGranted"/> flag only affects the specified <see cref="FigaroEnv"/>
        /// handle (and any other database handles opened within the scope of that handle).
        /// The <see cref="TimeNotGranted"/> flag may be used at any time
        /// during the life of the application.
        /// </summary>
        TimeNotGranted = 0x00008000,
        /// <summary>
        /// If set, Figaro will not write or synchronously flush the log on transaction
        /// commit. This means that transactions exhibit the ACI (atomicity, consistency, and
        /// isolation) properties, but not D (durability) {} that is, database integrity will be
        /// maintained, but if the application or system fails, it is possible some number of
        /// the most recently committed transactions may be undone during recovery.
        /// The number of transactions at risk is governed by how many log updates can fit into the log
        /// buffer, how often the operating system flushes dirty buffers to disk, and how often
        /// the log is checkpointed.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="NoSyncTransaction"/> flag only affects the
        /// specified <see cref="FigaroEnv"/>
        /// handle (and any other database handles opened within the scope of that handle).
        /// For consistent behavior across the environment, all <see cref="FigaroEnv"/>
        /// handles opened in the environment must set the <see cref="NoSyncTransaction"/> flag.
        /// The <see cref="NoSyncTransaction"/> flag may be used at any time during the
        /// life of the application.
        /// </summary>
        NoSyncTransaction = 0x00000001,
        /// <summary>
        /// If set and a lock is unavailable for any database operation performed in the
        /// context of a transaction, cause the operation to throw a <see cref="DBDeadlockException"/>
        /// exception (or throw a <see cref="DBLockNotGrantedException"/> exception if configured using
        /// the <see cref="TimeNotGranted"/> flag).
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="NoWaitTransaction"/> flag only affects the
        /// specified <see cref="FigaroEnv"/>
        /// handle (and any other database handles opened within the scope of that handle).
        /// For consistent behavior across the environment, all <see cref="FigaroEnv"/>
        /// handles opened in the environment must set the <see cref="NoWaitTransaction"/> flag.
        /// The <see cref="NoWaitTransaction"/> flag may be used at any time during
        /// the life of the application.
        /// </summary>
        NoWaitTransaction = 0x00000010,
        ///	<summary>
        /// If set, all transactions in the environment will be started as if it
        /// were passed to the
        /// <see cref="FigaroEnv"/> transaction
        /// subsystem.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption"/>
        /// with the this flag only affects the specified
        /// <see cref="FigaroEnv"/> handle (and any
        /// other database handles opened within the scope of that handle).
        /// This flag may be used at any time
        /// during the life of the application.
        /// </summary>
        SnapshotTransaction = 0x00000002,
        /// <summary>
        /// If set, Figaro will write, but will not synchronously flush, the log on
        /// transaction commit. This means that transactions exhibit the ACI (atomicity, consistency,
        /// and isolation) properties, but not D (durability) that is, database integrity will
        /// be maintained, but if the system fails, it is possible some number of the most
        /// recently committed transactions may be undone during recovery.
        /// The number of transactions at risk is governed by how often the system flushes dirty buffers to
        /// disk and how often the log is checkpointed.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> with the
        /// <see cref="WriteNoSyncTransaction"/> flag only affects the specified <see cref="FigaroEnv"/>
        /// handle (and any other database handles opened within the scope of that handle).
        /// For consistent behavior across the environment, all <see cref="FigaroEnv"/>
        /// handles opened in the environment must set the <see cref="WriteNoSyncTransaction"/> flag.
        /// The <see cref="WriteNoSyncTransaction"/> flag may be used at any time
        /// during the life of the application.
        /// </summary>
        WriteNoSyncTransaction = 0x00000020,
        /// <summary>
        /// If set, Figaro will yield the processor immediately after each page or mutex
        /// acquisition. This functionality should never be used for purposes other than stress
        /// testing.
        /// Calling <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
        /// with the <see cref="YieldCpu"/>
        /// flag only affects the specified <see cref="FigaroEnv"/> handle (and any other
        /// database handles opened within the scope of that handle). For consistent behavior
        /// the environment, all <see cref="FigaroEnv"/> handles opened in the environment
        /// must set the <see cref="YieldCpu"/> flag.
        /// The <see cref="YieldCpu"/> flag may be used at any time during the
        /// life of the application.
        /// </summary>
        YieldCpu = 0x00010000
    }

    /// <summary>
    /// One or more actions to perform when removing an environment.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction)"/>
    [FlagsAttribute]
    public enum EnvironmentRemoveAction
    {
        /// <summary>Perform no action.</summary>
        None,
        /// <summary>
        /// If the <see cref="Force"/> flag is set,
        /// the environment is removed, regardless of any processes that may
        /// still using it, and no locks are acquired during this process.
        /// <para>
        /// Generally, the <see cref="Force"/> flag is specified only
        /// when applications were unable to shut down cleanly, and
        /// there is a risk that an application may have died holding a database lock.)
        /// </para>
        /// </summary>
        Force,
        /// <summary>
        /// The database process' environment may be permitted to specify information to be used
        /// when naming files; see @file-naming.md.
        /// Because permitting users to specify which files are used can create security problems, environment information will be used in
        /// file naming for all users only if the <see cref="UseEnvironment"/> flag is set.
        /// </summary>
        UseEnvironment,
        /// <summary>
        /// The database process' environment may be permitted to specify information to be used
        /// when naming files; see @file-naming.md.
        /// Because permitting users to specify which files are used can create security problems, if the
        /// <see cref="UseEnvironmentRoot"/> flag is set, environment information will be used for file naming only for users with appropriate
        /// permissions.
        /// </summary>
        UseEnvironmentRoot
    }
    
    /// <summary>
    /// Used by <see cref="FigaroException"/> to determine errors and
    /// error codes encountered during the course of operation.
    /// </summary>
    /// <seealso cref="FigaroException"/>
    public enum FigaroExceptionCategory
    {
        /// <summary>An internal error occurred.</summary>
        InternalError,	//	Equates to XmlException.INTERNAL_ERROR
        /// <summary>The container is open.</summary>
        ContainerOpen,	//	Equates to XmlException.CONTAINER_OPEN
        /// <summary>The container is closed.</summary>
        ContainerClosed,	//	Equates to XmlException.CONTAINER_CLOSED
        /// <summary>Null pointer exception.</summary>
        NullPointer,	//	Equates to XmlException.NULL_POINTER
        /// <summary>Xml Indexer could not parse a document.</summary>
        IndexerParserError,	//	Equates to XmlException.INDEXER_PARSER_ERROR
        /// <summary>Figaro reported a database problem.</summary>
        DatabaseError,	//	Equates to XmlException.DATABASE_ERROR
        /// <summary>The query parser was unable to parse the expression.</summary>
        QueryParserError,	//	Equates to XmlException.QUERY_PARSER_ERROR
        /// <summary>The query evaluator was unable to execute the expression.</summary>
        QueryEvaluationError, //	Equates to XmlException.QUERY_EVALUATION_ERROR
        /// <summary><see cref="XmlResults"/> is lazily evaluated.</summary>
        LazyEvaluation,	//	Equates to XmlException.LAZY_EVALUATION
        /// <summary>The specified document could not be found.</summary>
        DocumentNotFound,	//	Equates to XmlException.DOCUMENT_NOT_FOUND
        /// <summary>The container already exists.</summary>
        ContainerExists,	//	Equates to XmlException.CONTAINER_EXISTS
        /// <summary>The indexing strategy name is unknown.</summary>
        UnknownIndex,	//	Equates to XmlException.UNKNOWN_INDEX
        /// <summary>An invalid parameter was passed.</summary>
        InvalidValue,	//	Equates to XmlException.INVALID_VALU
        /// <summary>The container version and the dbxml library version are not compatible.</summary>
        VersionMismatch,		//	Equates to XmlException.VERSION_MISMATCH
        /// <summary>Error using the event reader.</summary>
        EventError,		//	Equates to XmlException.EVENT_ERROR
        /// <summary>The specified container could not be found.</summary>
        ContainerNotFound,	//	Equates to XmlException.CONTAINER_NOT_FOUND
        /// <summary>An <see cref="XmlTransaction"/> has already been committed or aborted.</summary>
        TransactionError, //	Equates to XmlException.TRANSACTION_ERROR
        /// <summary>A uniqueness constraint has been violated.</summary>
        UniqueConstraintError, //	Equates to XmlException.UNIQUE_ERROR
        /// <summary>Unable to allocate memory.</summary>
        NoMemoryError,	//	Equates to XmlException.NO_MEMORY_ERROR
        /// <summary>An operation timed out.</summary>
        OperationTimeout,	//	Equates to XmlException.OPERATION_TIMEOUT
        /// <summary>An operation was explicitly interrupted.</summary>
        OperationInterrupted, //	Equates to XmlException.OPERATION_INTERRUPTED
        /// <summary>User memory too small for return. </summary>
        BufferSmall,
        /// <summary>"Null" return from secondary callback. </summary>
        DoNotIndex,
        /// <summary>Key/data deleted or never created. </summary>
        KeyEmpty,
        /// <summary>The key/data pair already exists. </summary>
        KeyExist,
        /// <summary> Deadlock condition exists. </summary>
        Deadlock,
        /// <summary>Lock unavailable. </summary>
        LockNotGranted,
        /// <summary> In-memory log buffer full. </summary>
        LogBufferFull,
        /// <summary>Server panic return. </summary>
        NoServerPanic,
        /// <summary>Bad home sent to server. </summary>
        NoServerHome,
        /// <summary>Bad ID sent to server. </summary>
        NoServerId,
        /// <summary>Key/data pair not found (EOF). </summary>
        DBNotFound,
        /// <summary>Out-of-date version. </summary>
        OldVersion,
        /// <summary>Requested page not found. </summary>
        PageNotFound,
        /// There are two masters.
        DuplicateMaster, //	= DB_REP_DUPMASTER,
        ///<summary>Rolled back a commit.</summary>
        RepHandleDead, //	= DB_REP_HANDLE_DEAD,
        /// <summary>Time to hold an election.</summary>
        RepHoldElection, //	= DB_REP_HOLDELECTION,
        /// <summary>This message should be ignored.</summary>
        RepIgnore,  //	= DB_REP_IGNORE	,
        /// <summary>Cached not written perm written.</summary>
        RepIsPerm,  //	= DB_REP_ISPERM,
        /// <summary>Unable to join replication group.</summary>
        RepJoinFailure, //	= DB_REP_JOIN_FAILURE,
        /// <summary>Master lease has expired.</summary>
        RepLeaseExpired, // = DB_REP_LEASE_EXPIRED,
        ///<summary>API/Replication lockout now.</summary>
        RepLockout, //	= DB_REP_LOCKOUT,
        /// <summary>New site entered system.</summary>
        RepNewSite, //	= DB_REP_NEWSITE,
        /// <summary>Permanent log record not written.</summary>
        RepNotPerm, //	= DB_REP_NOTPERM,
        /// <summary>Site cannot currently be reached.</summary>
        RepUnavailable, //	= DB_REP_UNAVAIL,
        /// <summary>Panic return. </summary>
        RunRecovery,	//	Equates to DB_RUNRECOVERY
        /// <summary>Secondary index corrupt. </summary>
        SecondaryBad, //	Equates to DB_SECONDARY_BAD
        /// <summary>Verify failed; bad format. </summary>
        VerifyBad, // Equates to DB_VERIFY_BAD
        /// <summary>Database version mismatch.</summary>
        DatabaseVersionMismatch // Equates to DB_VERSION_MISMATCH
    }


    //https://docs.oracle.com/cd/E17276_01/html/programmer_reference/rep_partview.html
    /// <summary>
    /// Flags for whether the replication site is a partial or full replication view, or not a replication view at all.
    /// </summary>		
    /// <remarks>
    /// A view is a replication site that maintains a copy of the replication group's data without 
    /// incurring other overhead from participating in the replication group. Views are useful for 
    /// applications that require read scalability, are geographically distributed, have slow 
    /// network connections to some sites, or require only a subset of replicated databases at some sites.
    /// <para>
    /// A client is a participant in the replication group because it helps to determine the master and 
    /// contributes to transaction durability. A view receives its copy of the replicated data without 
    /// participating in the replication group in these other ways. Specifically, a view cannot ever become 
    /// master, a view does not vote in elections, and a view does not contribute to transaction durability. 
    /// A view is similar to an unelectable client because neither can become master. They differ because an 
    /// unelectable client participates in the replication group by voting and contributing to transaction durability.
    /// </para>
    /// <para>
    /// Defining a site as a view is a permanent decision; once a site is defined as a view it can never 
    /// be transformed into a participant.
    /// </para>
    /// </remarks>
    /// <see cref="FigaroReplicationManager"/>
    public enum ReplicationViewOptions
    {
        /// <summary>
        ///Site is not a replication view.
        /// </summary>
        None,
        /// <summary>
        /// Site is a partial view, thereby containing a subset of the replicated databases.
        /// </summary>
        Partial,
        /// <summary>
        /// Site is a full view containing a copy of all replicated databases.
        /// </summary>
        Full
    }

    /// <summary>
    /// The connection status of the replication site.
    /// </summary>
    public enum ReplicationSiteConnectionStatus
    {
        /// <summary>
        /// Site connection status is not yet known.
        /// </summary>
        Unknown,
        /// <summary>
        /// Site is connected.
        /// </summary>
        Connected,
        /// <summary>
        /// Site is disconnected.
        /// </summary>
        Disconnected
    }

    /// <summary>
    /// Figaro backup options.
    /// </summary>
    /// <remarks>
    /// </remarks>
    [Flags]
    public enum BackupOptions
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Before performing the backup, first remove all files from the target backup directory tree.
        /// </summary>
        Clean = 2,
        /// <summary>
        /// Back up all ordinary files that might exist in the environment, and the environment's subdirectories.
        /// </summary>
        Files = 8,
        /// <summary>
        /// Back up only the database files. Do not backup the log files.
        /// </summary>
        NoLogs = 16,
        /// <summary>
        /// Regardless of the directory structure used by the source environment, place all back up files in a single directory 
        /// identified by the <see cref="BackupConfig.BackupPath"/> property. 
        /// <para>Use this option if absolute path names to your environment directory and the files within that directory are required by your application.</para>
        /// </summary>
        SingleDirectory = 32,
        /// <summary>
        /// Perform an incremental back up, instead of a full back up. When this option is specified, only log files are copied to the target directory.
        /// </summary>
        Incremental = 64,
        /// <summary>
        /// If the target directory does not exist, create it and any required subdirectories.
        /// </summary>
        Create = 1,
        /// <summary>
        /// Throw an exception if target backup file(s) already exist.
        /// </summary>
        Exclusive = 4,
        /// <summary>
        /// Run in verbose mode, listing operations as they are completed.
        /// </summary>
        Verbose = 1
    }

}

#region Xml-Based Enums

namespace Figaro    //namespace Figaro    
{
    /// <summary>
    /// Container configuration state settings.
    /// </summary>
    /// <seealso cref="ContainerConfig.IndexNodes"/>
    /// <seealso cref="ContainerConfig.Statistics"/>
    /// <seealso cref="ContainerConfig"/>
    public enum ConfigurationState
    {
        /// <summary>
        /// Use the default setting.
        /// </summary>
        UseDefault,
        /// <summary>
        /// Turn the setting on.
        /// </summary>
        Off,
        /// <summary>
        /// Turn the setting off.
        /// </summary>
        On
    }

    /// <summary>
    /// Specifies the query evaluation type.
    /// </summary>
    /// <seealso cref="QueryContext.EvaluationType"/>
    public enum EvaluationType
    {
        /// <summary>Retrieve the results immediately.</summary>
        Eager,
        /// <summary>Retrieve the document lazily; that is, retrieve document content and document metadata
        /// only on an as-needed basis when reading the document.</summary>
        Lazy
    }

    /// <summary>
    /// Specifies the action to take when retrieving documents from a <see cref="Container"/>.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="Container"/>
    /// <seealso cref="Container.GetAllDocuments()"/>
    [Flags]
    public enum GetAllDocumentOptions
    {
        ///<summary>No options or settings.</summary>
        None,
        /// <summary>Retrieve the document lazily; that is, retrieve document content and document
        /// metadata only on an as-needed basis when reading the document.</summary>
        LazyDocs,
        /// <summary>This operation will support degree 1 isolation; that is, read operations may return data
        /// that has been modified by other transactions but which has not yet been committed.
        /// Silently ignored if the <see cref="GetAllDocumentOptions.ReadUncommitted"/> flag was not specified when the
        /// underlying container was opened.</summary>
        ReadUncommitted,
        /// <summary>This operation will have degree 2 isolation. This provides for cursor stability
        /// but not repeatable reads. Data items which have been previously read by this
        /// transaction may be deleted or modified by other transactions before this transaction completes. </summary>
        ReadCommitted,
        /// <summary>Acquire write locks instead of read locks when doing the read, if locking is configured.
        /// Setting this flag can eliminate deadlock during a read-modify-write cycle by
        /// acquiring the write lock during the read part of the cycle so that another thread of
        /// control acquiring a read lock for the same item, in its own read-modify-write cycle,
        /// will not result in deadlock.</summary>
        ReadModifyWrite,
        /// <summary>Return results in reverse order relative to the sort of the index.</summary>
        ReverseOrder
    }
    /// <summary>
    /// The type of high bound operation to be performed against a range lookup for an
    /// <see cref="XmlIndexLookup"/> object.
    /// </summary>
    /// <seealso cref="XmlIndexLookup"/>
    /// <seealso cref="XmlIndexLookup.SetHighBound(Figaro.XmlValue,Figaro.HighboundOperationLookup)"/>
    public enum HighboundOperationLookup
    {
        /// <summary>Less than the specified value for the high bound index lookup.</summary>
        LessThan,
        /// <summary>Less than or equal to the specified value for the high bound index lookup.</summary>
        LessThanOrEqual
    }

    /// <summary>
    /// The type of high bound operation to be performed against a range lookup for an
    /// <see cref="XmlIndexLookup"/> object.
    /// </summary>
    /// <seealso cref="XmlIndexLookup"/>
    /// <seealso cref="XmlIndexLookup.SetLowBound(Figaro.XmlValue,Figaro.IndexLookupOperation)"/>
    public enum IndexLookupOperation
    {
        /// <summary>Equal to the specified value for the low bound index lookup.</summary>
        Equal,
        /// <summary>Greater than the specified value for the low bound index lookup.</summary>
        GreaterThan,
        /// <summary>Greater than or equal to the specified value for the low bound index lookup.</summary>
        GreaterThanOrEqual,
        /// <summary>Less than the specified value for the low bound index lookup.</summary>
        LessThan,
        /// <summary>Less than or equal to the specified value for the low bound index lookup.</summary>
        LessThanOrEqual,
        /// <summary>No operation for the specified value.</summary>
        None
    }

    /// <summary>The type of lookup operation.</summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="XmlIndexLookup.Execute(QueryContext)"/>
    [FlagsAttribute]
    public enum IndexLookupOptions
    {
        /// <summary>
        /// Retrieve the document lazily. That is, retrieve document content and document metadata only on an
        /// as-needed basis when reading the document.
        /// </summary>
        LazyDocs,
        /// <summary>
        /// This operation will support degree 1 isolation; that is, read operations may return data that has
        /// been modified by other transactions but which has not yet been committed.
        /// <para>
        /// Silently ignored if the <see cref="ContainerConfig.ReadUncommitted"/> flag
        /// was not specified when the underlying container was opened.
        /// </para>
        /// </summary>
        ReadUncommitted,
        /// <summary>
        /// This operation will have degree 2 isolation. This provides for cursor stability but not repeatable reads.
        /// <para>
        /// Data items which have been previously read by this transaction may be deleted or modified by other
        /// transactions before this transaction completes.
        /// </para>
        /// </summary>
        ReadCommitted,
        /// <summary>
        /// Acquire write locks instead of read locks when doing the read, if locking is configured.
        /// <para>
        /// Setting this flag can eliminate deadlock during a read-modify-write cycle by acquiring the write lock
        /// during the read part of the cycle so that another thread of control acquiring a read lock for the same
        /// item, in its own read-modify-write cycle, will not result in deadlock.
        /// </para>
        /// </summary>
        ReadModifyWrite,
        /// <summary>
        /// Return results in reverse order relative to the sort of the index.
        /// </summary>
        ReverseOrder,
        /// <summary>
        /// Relevant for node storage containers with node indices only. Causes the
        /// <see cref="XmlIndexLookup.Execute(QueryContext)"/> operations
        /// to return document nodes rather than direct pointers to the interior nodes. This is more
        /// efficient if all that is desired is a reference to target documents.
        /// </summary>
        NoIndexNodes,
        /// <summary>
        /// Enables use of a cache mechanism that optimizes
        /// <see cref="XmlIndexLookup.Execute(QueryContext)"/> operations
        /// that a large number of nodes from the same document.
        /// </summary>
        CacheDocuments
    }

    /// <summary>
    /// The log category to enable or disable.
    /// </summary>
    /// <remarks>
    /// Figaro can be configured to generate a stream of messages to help application
    /// debugging. The messages are categorized by subsystem, and by importance. The messages
    /// are sent to the output stream that is configured in the <see cref="FigaroEnv"/>
    /// associated with the <see cref="XmlManager"/> generating the message. The output is
    /// sent to <c>std.cerr</c> if no environment is associated with <see cref="XmlManager"/>.
    /// </remarks>
    /// <seealso cref="LogConfiguration.SetCategory(Figaro.LogConfigurationCategory,bool)"/>
    [Flags]
    public enum LogConfigurationCategory
    {
        /// <summary>Enable indexer messages.</summary>
        Indexer,
        /// <summary>Enable query processor messages.</summary>
        Query,
        /// <summary>Enable optimizer messages.</summary>
        Optimizer,
        /// <summary>Enable dictionary messages.</summary>
        Dictionary,
        /// <summary>Enable <see cref="Container"/> messages.</summary>
        Container,
        /// <summary>Enable node storage messages.</summary>
        NodeStore,
        /// <summary>Enable <see cref="  XmlManager"/> messages.</summary>
        XmlManager,
        /// <summary>Enable all messages.</summary>
        All,
        /// <summary> Disable all messages.</summary>
        None
    }
    /// <summary>The log level to enable or disable.</summary>
    /// <remarks>
    /// Figaro can be configured, at a global level, to generate a stream of messages to help application
    /// debugging. The messages are categorized by subsystem, and by importance. The messages
    /// are sent to the output stream that is configured in the <see cref="FigaroEnv"/>
    /// associated with the <see cref="XmlManager"/> generating the message. The output is
    /// sent to <c>std.cerr</c> if no environment is associated with <see cref="XmlManager"/>.
    /// </remarks>
    /// <seealso cref="LogConfiguration.SetLogLevel(Figaro.LogConfigurationLevel,bool)"/>
    public enum LogConfigurationLevel
    {
        /// <summary>
        /// Set debug level information.
        /// </summary>
        Debug,
        /// <summary>
        /// Set the log level to record informational messages.
        /// </summary>
        Info,
        /// <summary>
        /// Set the log level to record warning messages.
        /// </summary>
        Warning,
        /// <summary>
        /// Set the log level to record error messages.
        /// </summary>
        Error,
        /// <summary>
        /// Log all messages.
        /// </summary>
        All,
        /// <summary>
        /// Log no messages.
        /// </summary>
        None
    }

    /// <summary>
    /// One or more options for putting whole Xml documents in a
    /// <see cref="Container"/> object.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="Container.PutDocument(string,UpdateContext)"/>
    [Flags]
    public enum PutDocumentOptions
    {
        /// <summary>Use this option if you plan to pass a file name to the container.</summary>
        None,
        /// <summary>
        /// Generate a unique name.
        /// <para>
        /// If no name is set for the <see cref="XmlDocument"/> in
        /// a <see cref="XmlContainerType.WholeDocContainer"/> container type, a
        /// system-defined unique name is generated. If a name is specified, a unique string is appended to that
        /// name to ensure uniqueness.
        /// </para>
        /// </summary>
        GenerateFileName,
        /// <summary>
        /// Force the use of a scanner that will neither validate nor read schema or DTDs associated
        /// with the document during parsing. This is efficient, but can cause parsing errors if
        /// the document references information that might have come from a schema or DTD, such as
        /// entity references.
        /// </summary>
        WellFormedOnly
    }

    /// <summary>
    /// One or more options for retrieving nodes and documents from the containers.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="Container.GetNode(System.String,Figaro.RetrievalModes)"/>
    /// <seealso cref="Container.GetDocument(string)"/>
    /// <seealso cref="XmlDocument"/>
    [Flags]
    public enum RetrievalModes
    {
        /// <summary>
        /// Use no options.
        /// </summary>
        None,
        /// <summary>
        /// Retrieve the document lazily. That is, retrieve document content and document metadata only
        /// on an as needed-basis when reading the document.
        /// </summary>
        LazyDocs,
        /// <summary>
        /// This operation will support degree 1 isolation; that is, read operations may return data that
        /// has been modified by other transactions but which has not yet been committed. Silently ignored
        /// if the <see cref="ContainerConfig.ReadUncommitted"/> flag
        /// was not specified when the underlying container was opened.
        /// </summary>
        ReadUncommitted,
        /// <summary>
        /// This operation will have degree 2 isolation. This provides for cursor stability but
        /// not repeatable reads. Data items which have been previously read by this transaction
        /// may be deleted or modified by other transactions before this transaction completes.
        /// </summary>
        ReadCommitted,
        /// <summary>
        /// Acquire write locks instead of read locks when doing the read, if locking is
        /// configured. Setting this flag can eliminate deadlock during a read-modify-write
        /// cycle by acquiring the write lock during the read part of the cycle so that another
        /// thread of control acquiring a read lock for the same item, in its own read-modify-write
        /// cycle, will not result in deadlock.
        /// </summary>
        ReadModifyWrite
    }

    /// <summary>
    /// Indicates what kind of object is to be returned from an <see cref="XmlResults"/> object.
    /// </summary>
    /// <seealso cref="XmlResults.NextValue"/>
    /// <seealso cref="XmlResults"/>
    public enum ResultsType
    {
        /// <summary>Returns an <see cref="XmlValue"/> object.</summary>
        Value,
        /// <summary>Returns an <see cref="XmlDocument"/> object.</summary>
        Document,
        /// <summary>Returns an <see cref="XmlData"/> object.</summary>
        Data
    }

    ///// <summary>
    ///// Synchronization options when committing a transaction.
    ///// </summary>
    ///// <seealso cref="XmlTransaction.Commit()"/>
    ///// <seealso cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
    //public enum TransactionCommitOption
    //{
    //   /// <summary>
    //   /// Do not synchronously flush the log. This means the transaction will exhibit the ACI
    //   /// (atomicity, consistency, and isolation) properties, but not D (durability) {} that is,
    //   /// database integrity will be maintained, but it is possible that this transaction may be undone
    //   /// during recovery.
    //   /// <para>
    //   /// This behavior may also be set for a database environment using the
    //   /// <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/>
    //   /// method or for a single transaction using the
    //   /// <see cref="XmlManager.CreateTransaction()"/> method. Any
    //   /// value specified to this method overrides both of those settings.
    //   /// </para>
    //   /// </summary>
    //   NoSyncTransaction,
    //   /// <summary>
    //   /// Synchronously flush the log. This means the transaction will exhibit all of the ACID
    //   /// (atomicity, consistency, isolation, and durability) properties.
    //   /// <para>
    //   /// This behavior is the default for database environments unless the <see cref="TransactionCommitOption.NoSyncTransaction"/> flag was
    //   /// specified to the <see cref="FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,bool)"/> method. This behavior may also be set for
    //   /// a single transaction using the <see cref="XmlManager.CreateTransaction()"/> method.
    //   /// Any value specified to this method overrides both of those settings.
    //   /// </para>
    //   /// </summary>
    //   SyncTransaction
    //};

    // <summary>
    // Flags to use for opening a container.
    // </summary>
    // <remarks>
    // This enumeration can be bitwise OR'd together with multiple values where it is used.
    // </remarks>
    // <seealso cref="XmlManager.OpenContainer(string)"/>
    // <seealso cref="XmlManager"/>
    // [Flags]
    //    public enum ContainerSettings
    //    {
    //        /// <summary>
    //        /// No settings.
    //        /// </summary>
    //        None = 0,
    //        /// <summary>If the container does not currently exist, create it.</summary>
    //        Create ,
    //        /// <summary>This operation will support degree 1 isolation; that is, read operations may return data that has been
    //        /// modified by other transactions but which has not yet been committed. Silently
    //        /// ignored if the <see cref="ContainerSettings.ReadUncommitted"/> flag was not specified when the underlying container was opened.</summary>
    //        ReadUncommitted ,
    //        /// <summary>Return an error if the container already exists. The <see cref="ContainerSettings.Excl"/> flag
    //        /// is only meaningful when specified with the <see cref="ContainerSettings.Create"/> flag. </summary>
    //        Excl ,
    //        /// <summary>
    //        /// Open the database with support for multi-version concurrency control. This will cause updates to the container
    //        /// to follow a copy-on-write protocol, which is required to support snapshot isolation. The <see cref="MultiVersion"/>
    //        /// flag requires that the container be transactionally protected during its open.
    //        /// </summary>
    //        MultiVersion ,
    //        /// <summary>Do not map this container into process memory. </summary>
    //        NoMmap ,
    //        /// <summary>
    //        /// Open the container for reading only. Any attempt to modify items in the container
    //        /// will fail, regardless of the actual permissions of any underlying files.
    //        /// </summary>
    //        ReadOnly,
    //        /// <summary>Cause the container handle to be free-threaded; that is,
    //        /// concurrently usable by multiple threads in the address space. </summary>
    //        Thread ,
    //        /// <summary>Do checksum verification of pages read into the cache from the backing file store.
    //        /// Figaro uses the SHA1 Secure Hash Algorithm if encryption is configured and a
    //        /// general hash algorithm if it is not.</summary>
    //        Checksum,
    //        /// <summary>Encrypt the database using the cryptographic password specified to <see cref="FigaroEnv.SetEncrypt(System.String,bool)"/>.</summary>
    //        Encrypt ,
    // 
    //        /// <summary>
    //        /// If set, Figaro will not write log records for this database. This means that updates
    //        /// of this database exhibit the ACI (atomicity, consistency, and isolation) properties,
    //        /// but not D (durability) {} that is, database integrity will be maintained, but if the
    //        /// application or system fails, integrity will not persist. The database file must be verified
    //        /// and/or restored from backup after a failure.
    //        /// </summary>
    //        TransactionNotDurable,
    //        /// <summary>
    //        /// Causes the indexer to create index targets that reference documents rather than nodes. This can
    //        /// be more desirable for simple queries that only need to return documents and do relatively
    //        /// little navigation during querying. It can apply to both container types, and is the
    //        /// default for containers of type <see cref="XmlContainerType.WholeDocContainer"/>.
    //        /// </summary>
    //        NoIndexNodes,
    //        /// <summary>
    //        /// Causes the indexer to create index targets that reference nodes rather than documents. This allows
    //        /// index lookups during query processing to more efficiently find target nodes and avoid walking
    //        /// the document tree. It can apply to both container types, and is the default
    //        /// for containers of type <see cref="XmlContainerType.NodeContainer"/>.
    //        /// </summary>
    //        IndexNodes,
    //        /// <summary>
    //        /// Causes the container to be created to include structural statistics information, which is very
    //        /// useful for cost based query optimization. Containers created with these statistics will take
    //        /// longer to load and update, since the statistics must also be updated. This is the default.
    //        /// </summary>
    //        Statistics ,
    //        /// <summary>
    //        /// Causes the container to be created without structural statistics information - by default
    //        /// structural statistics are created.
    //        /// </summary>
    //        NoStatistics ,
    //        /// <summary>
    //        /// Cause the container to support transactions. If this flag is set, an <see cref="XmlTransaction"/>
    //        /// object may be used with any method that supports transactional protection. Also, if this
    //        /// flag is used, and if an <see cref="XmlTransaction"/> object is not provided to a method that modifies a
    //        /// <see cref="Container"/> object, then auto commit is automatically used for the operation.
    //        /// </summary>
    //        Transactional ,
    //        /// <summary>
    //        /// When loading documents into the container, validate the Xml if it refers to a DTD or Xml Schema.
    //        /// </summary>
    //        AllowValidation
    //    }

    /// <summary>
    /// Specifies the storage type of the container.
    /// </summary>
    /// <seealso cref="Container.ContainerType"/>
    public enum XmlContainerType
    {
        /// <summary>
        /// Documents are broken down into their component nodes, and these nodes are stored individually
        /// in the container. This is the preferred container storage type.
        /// </summary>
        NodeContainer,
        /// <summary>Documents are stored intact; all white space and formatting is preserved.</summary>
        WholeDocContainer
    }

    /// <summary>
    /// Indicates what type of path the index should take with respect to identifying nodes to index.
    /// </summary>
    /// <seealso cref="IndexingStrategy"/>
    /// <seealso cref="IndexingStrategy.PathType"/>
    public enum IndexPathType
    {
        /// <summary>There is no index path.</summary>
        NoPath, // Equates to PATH_NONE
        /// <summary>Index the node regardless of its location.</summary>
        NodePath, // Equates to PATH_NODE
        /// <summary>Index the node according to a specific path to the node. This value is preferred for indices, and yields better performance.</summary>
        EdgePath // Equates to PATH_EDGE
    }
    /// <summary>
    /// Identifies what sort of queries the index supports.
    /// </summary>
    /// <seealso cref="IndexingStrategy.KeyType"/>
    /// <seealso cref="IndexingStrategy"/>
    public enum IndexKeyType
    {
        /// <summary>No index key type.</summary>
        NoKey, // Equates to 0
        /// <summary>Improves the performance of queries looking for the existence of an node, regardless of its value.</summary>
        Presence, // Equates to KEY_PRESENCE
        /// <summary>Improves the performances of queries looking for nodes with a specific value.</summary>
        Equality, // Equates to KEY_EQUALITY
        /// <summary>Improves the performance of queries looking for a node whose value contains a given substring. This key type is best used when your queries use the XQuery <c>contains()</c> substring function. </summary>
        Substring, // Equates to KEY_SUBSTRING
    }
    /// <summary>
    /// Used to determine the type of data indexed in the container.
    /// </summary>
    /// <seealso cref="IndexingStrategy.NodeType"/>
    /// <seealso cref="IndexingStrategy"/>
    public enum IndexNodeType
    {
        /// <summary>No node type.</summary>
        NoNodeType, // Equates to 0
        /// <summary>Index an Xml element.</summary>
        Element, // Equates to NODE_ELEMENT
        /// <summary>Index an attribute found in the Xml document.</summary>
        Attribute, // Equates to NODE_ATTRIBUTE
        /// <summary>
        /// If specified,
        /// <see cref="IndexingStrategy.PathType"/> must be set
        /// to <see cref="IndexPathType.NodePath"/>.
        /// </summary>
        Metadata // Equates to NODE_METADATA
    }

    /// <summary>
    /// An enumerated list of possible node types that can be
    /// contained within an <see cref="XmlValue"/> object.
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// An attribute in an Xml document.
        /// </summary>
        AttributeNode, // Equates to XmlValue.ATTRIBUTE_NODE,
        /// <summary>
        /// The <c>CDATA</c> section in an Xml document.
        /// </summary>
        CDataSectionNode, // Equates to XmlValue.CDATA_SECTION_NODE,
        /// <summary>
        /// Comments found in an Xml document.
        /// </summary>
        CommentNode, // Equates to XmlValue.COMMENT_NODE,
        /// <summary>
        /// A document fragment within an Xml document.
        /// </summary>
        DocumentFragmentNode, // Equates to XmlValue.DOCUMENT_FRAGMENT_NODE,
        /// <summary>
        /// A node containing an entire document.
        /// </summary>
        DocumentNode, // Equates to XmlValue.DOCUMENT_NODE,
        /// <summary>
        /// A document type.
        /// </summary>
        DocumentTypeNode, // Equates to XmlValue.DOCUMENT_TYPE_NODE,
        /// <summary>
        /// An Xml document element.
        /// </summary>
        ElementNode, // Equates to XmlValue.ELEMENT_NODE,
        /// <summary>
        /// An entity node found in an Xml document.
        /// </summary>
        EntityNode, // Equates to XmlValue.ENTITY_NODE,
        /// <summary>
        /// An entity reference node found in an Xml document.
        /// </summary>
        EntityReferenceNode, // Equates to XmlValue.ENTITY_REFERENCE_NODE
        /// <summary>
        /// A notation node found in an Xml document.
        /// </summary>
        NotationNode, // Equates to XmlValue.NOTATION_NODE
        /// <summary>
        /// A processing instruction node found in an Xml document.
        /// </summary>
        PINode, //	Equates to XmlValue.PROCESSING_INSTRUCTION_NODE
        /// <summary>
        /// A text node found in an Xml document.
        /// </summary>
        TextNode//	Equates to XmlValue.TEXT_NODE,
    }

    /// <summary>
    /// The XML data types of an <see cref="XmlValue"/> object.
    /// </summary>
    /// <seealso cref="XmlValue"/>
    public enum XmlValueType
    {
        /// <summary>
        /// Built-in simple type definition.
        /// </summary>
        AnySimpleType, // Equates to XmlValue.ANY_SIMPLE_TYPE,
        /// <summary>
        /// A Uniform Resource Identifier Reference (URI). Can be
        /// absolute or relative, and may have an optional fragment
        /// identifier.
        /// </summary>
        AnyUri, // Equates to XmlValue.ANY_URI,
        /// <summary>
        /// Base64-encoded arbitrary binary data.
        /// </summary>
        Base64Binary, // Equates to XmlValue.BASE_64_BINARY,
        /// <summary>
        /// Arbitrary binary data.
        /// </summary>
        Binary, // Equates to XmlValue.BINARY,
        /// <summary>
        /// Binary-valued logic legal literals <c>{true,false,0,1}</c>.
        /// </summary>
        Boolean, // Equates to XmlValue.BOOLEAN,
        /// <summary>
        /// Calendar date. Format: <c>CCYY-MM-DD</c>.
        /// </summary>
        Date, // Equates to XmlValue.DATE,
        /// <summary>
        /// Specific instance of time. ISO 8601 extended format: <c>CCYY-MM-DDThh:mm:ss</c>.
        /// </summary>
        DateTime, // Equates to XmlValue.DATE_TIME,
        /// <summary>
        /// A duration of time. ISO 8601 of extended format <c>PnYn MnDTnH nMn S</c>.
        /// </summary>
        DaytimeDuration, // Equates to XmlValue.DAY_TIME_DURATION,
        /// <summary>
        /// Arbitrary precision decimal numbers.
        /// Sign omitted, “+” is assumed. Leading and trailing
        /// zeros are optional. If the fractional part is
        /// zero, the period and following zero(es) can be
        /// omitted.
        /// </summary>
        Decimal, // Equates to XmlValue.DECIMAL,
        /// <summary>
        /// Double-precision 64-bit floating point type - legal literals <c>{0, -0, INF, -INF and NaN}</c>. For example: <c>-1E4, 12.78e-2, 12</c> and <c>INF</c>.
        /// </summary>
        Double, // Equates to XmlValue.DOUBLE,
        /// <summary>
        /// A duration of time, in ISO 8601 extended format <c>PnYn MnDTnH nMn S</c>.
        /// </summary>
        Duration, // Equates to XmlValue.DURATION,
        /// <summary>
        /// 32-bit floating point type - legal literals <c>{0, -0,INF, -INF and NaN}</c>.
        /// Example: <c> -1E4, 1267.43233E12, 12.78e-2, 12 and INF</c>.
        /// </summary>
        Float, // Equates to XmlValue.FLOAT,
        /// <summary>
        /// Gregorian day. For example, a day such as the 5th of the month is <c>--05</c>.
        /// </summary>
        GDay, // Equates to XmlValue.G_DAY,
        /// <summary>
        /// Gregorian month. For example: the month of May is expressed as <c>--05--</c>.
        /// </summary>
        GMonth, // Equates to XmlValue.G_MONTH,
        /// <summary>
        /// Gregorian calendar year. For example, express year 1999 as: <c>1999</c>.
        /// </summary>
        GYear, // Equates to XmlValue.G_YEAR,
        /// <summary>
        /// Specific Gregorian month and year. For example, to express May 1999, write: <c>1999-05</c>.
        /// </summary>
        GYearMonth, // Equates to XmlValue.G_YEAR_MONTH,
        /// <summary>
        /// Arbitrary hex-encoded binary data. For example, <c>0FB7</c> is a hex encoding for 16-bit int <c>4023</c> (binary <c>111110110111</c>).
        /// </summary>
        HexBinary, // Equates to XmlValue.HEX_BINARY,
        /// <summary>
        /// An XML node element.
        /// </summary>
        Node, // Equates to XmlValue.NODE,
        /// <summary>
        /// No XML data type.
        /// </summary>
        None, // Equates to XmlValue.NONE,
        /// <summary>
        /// XML notation.
        /// </summary>
        Notation, // Equates to XmlValue.NOTATION,
        /// <summary>
        /// XML qualified name.
        /// </summary>
        QName, // Equates to XmlValue.QNAME,
        /// <summary>
        /// Character strings in XML.
        /// </summary>
        String, // Equates to XmlValue.STRING,
        /// <summary>
        /// An instant of time that recurs every day. For example, 1:20 pm for Eastern Standard Time,
        ///  which is 5 hours behind Coordinated Universal Time (UTC), would
        /// be <c>13:20:00-05:00</c>.
        /// </summary>
        Time, // Equates to XmlValue.TIME,
        /// <summary>
        /// An arbitrary, generic data type that can be used as other types.
        /// </summary>
        UntypedAtomic, // Equates to XmlValue.UNTYPED_ATOMIC,
        /// <summary>
        /// A year month duration derived from <see cref="Duration"/> in the format <c>PnYnM</c>.
        /// For example, to indicate a year month duration of 2 years and 3 months, you would
        /// write: <c>P2Y3M</c>.
        /// </summary>
        YearMonthDuration       // Equates to XmlValue.YEAR_MONTH_DURATION
    }

    /// <summary>
    /// The basic data types recognized by <see cref="IndexingStrategy"/>.
    /// </summary>
    /// <remarks>
    /// Note that if  <see cref="IndexingStrategy.KeyType"/> is set to
    /// <see cref="IndexKeyType.Presence"/>, then
    /// <see cref="XmlDatatype"/> must be set to <see cref="XmlDatatype.None"/>.
    /// </remarks>
    /// <seealso cref="IndexingStrategy.IndexValueSyntax"/>
    /// <seealso cref="IndexingStrategy"/>
    public enum XmlDatatype
    {
        /// <summary>
        /// No XML data type.
        /// </summary>
        None,
        /// <summary>
        /// Base64-encoded arbitrary binary data.
        /// </summary>
        Base64Binary,
        /// <summary>
        /// Binary-valued logic legal literals <c>{true,false,0,1}</c>.
        /// </summary>
        Boolean,
        /// <summary>
        /// Calendar date. Format: <c>CCYY-MM-DD</c>.
        /// </summary>
        Date,
        /// <summary>
        /// Specific instance of time. ISO 8601 extended format: <c>CCYY-MM-DDThh:mm:ss</c>.
        /// </summary>
        DateTime,
        /// <summary>
        /// A duration of time. ISO 8601 of extended format <c>PnYn MnDTnH nMn S</c>.
        /// </summary>
        DayTimeDuration,
        /// <summary>
        /// Arbitrary precision decimal numbers.
        /// Sign omitted, “+” is assumed. Leading and trailing
        /// zeros are optional. If the fractional part is
        /// zero, the period and following zero(es) can be
        /// omitted.
        /// </summary>
        Decimal,
        /// <summary>
        /// Double-precision 64-bit floating point type - legal literals <c>{0, -0, INF, -INF and NaN}</c>. For example: <c>-1E4, 12.78e-2, 12</c> and <c>INF</c>.
        /// </summary>
        Double,
        /// <summary>
        /// A duration of time, in ISO 8601 extended format <c>PnYn MnDTnH nMn S</c>.
        /// </summary>
        Duration,
        /// <summary>
        /// 32-bit floating point type - legal literals <c>{0, -0,INF, -INF and NaN}</c>.
        /// Example: <c> -1E4, 1267.43233E12, 12.78e-2, 12 and INF</c>.
        /// </summary>
        Float,
        /// <summary>
        /// Gregorian day. For example, a day such as the 5th of the month is <c>--05</c>.
        /// </summary>
        GDay,
        /// <summary>
        /// Gregorian month. For example: the month of May is expressed as <c>--05--</c>.
        /// </summary>
        GMonth,
        /// <summary>
        /// Gregorian specific day in a month. For example, Feb 5 is expressed as <c>--02-05</c>.
        /// </summary>
        GMonthDay,
        /// <summary>
        /// Gregorian calendar year. For example, for year <c>1999</c> write: <c>1999</c>.
        /// </summary>
        GYear,
        /// <summary>
        /// Specific Gregorian month and year. For example, to express May 1999 write: <c>1999-05</c>.
        /// </summary>
        GYearMonth,
        /// <summary>
        /// Arbitrary hex-encoded binary data. For example, <c>0FB7</c> is a hex encoding for 16-bit int <c>4023</c> (binary <c>111110110111</c>).
        /// </summary>
        HexBinary,
        /// <summary>
        /// Integer type. Note: This data type maps to Untyped Atomic in the Berkeley DB XML library.
        /// </summary>
        Integer,
        /// <summary>
        /// Character strings in XML.
        /// </summary>
        String,
        /// <summary>
        /// An instant of time that recurs every day. For example, 1:20 pm for Eastern Standard Time,
        ///  which is 5 hours behind Coordinated Universal Time (UTC), would
        /// be <c>13:20:00-05:00</c>.
        /// </summary>
        Time,
        /// <summary>
        /// A year month duration derived from <see cref="Duration"/> in the format <c>PnYnM</c>.
        /// For example, to indicate a year month duration of 2 years and 3 months, you would
        /// write: <c>P2Y3M</c>.
        /// </summary>
        YearMonthDuration,
        /// <summary>
        /// An arbitrary, generic data type that can be used as other types.
        /// </summary>
        UntypedAtomic
    }

    /// <summary>
    /// One or more values to set during <see cref="XmlManager"/> instantiation.
    /// </summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="XmlManager"/>
    [FlagsAttribute]
    public enum ManagerInitOptions
    {
        /// <summary>
        /// If set, this flag allows XQuery queries to access data sources
        /// external to the container, such as files on disk or HTTP URIs. By default,
        /// such access is not allowed.
        /// </summary>
        AllowExternalAccess, // Equates to DBXML_ALLOW_EXTERNAL_ACCESS
        /// <summary>
        /// If set, XQuery queries that reference unopened containers
        /// will automatically open those containers, and close them when references
        /// resulting from the query are released. By default, a query will fail if it
        /// refers to containers that are not open.
        /// </summary>
        AllowAutoOpen, // Equates to DBXML_ALLOW_AUTO_OPEN

        /// <summary>
        /// If set, the <see cref="XmlManager"/> object will
        /// close and delete the underlying
        /// <see cref="FigaroEnv"/> object at the end
        /// of the <see cref="XmlManager"/>'s life.
        /// </summary>
        AdoptFigaroEnv, // Equates to DBXML_ADOPT_DBENV
        /// <summary>
        /// Use all available flags.
        /// </summary>
        AllOptions,
        /// <summary>
        /// No options.
        /// </summary>
        None = 0
    }

    /// <summary> One or more values to set for re-indexing containers.</summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="XmlManager.ReindexContainer(string,UpdateContext,ReindexOptions)"/>
    [FlagsAttribute]
    public enum ReindexOptions
    {
        /// <summary>Change the container's index type to node indexes.</summary>
        IndexNodes, // Equates to DBXML_INDEX_NODES
        /// <summary>Change the container's index type to document indexes.</summary>
        NoIndexNodes, // Equates to DBXML_NO_INDEX_NODES
        /// <summary>Add a structural statistics database to the container during re-indexing.</summary>
        Statistics, // Equates to DBXML_STATISTICS
        /// <summary>Remove an existing structural statistics database.</summary>
        NoStatistics // Equates to DBXML_NO_STATISTICS
    }

    /// <summary>One or more options for querying containers.</summary>
    /// <remarks>
    /// This enumeration can be bitwise OR'd together with multiple values where it is used.
    /// </remarks>
    /// <seealso cref="XmlManager.Query(string,QueryContext,QueryOptions)"/>
    /// <seealso cref="XmlManager"/>
    [FlagsAttribute]
    public enum QueryOptions
    {
        /// <summary>
        /// No settings.
        /// </summary>
        None,
        /// <summary>
        /// Retrieve the document lazily. That is, retrieve document content and document metadata only on an as needed basis when reading the document.
        /// </summary>
        LazyDocs, // Equates to DBXML_LAZY_DOCS

        /// <summary>
        /// When parsing a document in order to execute a query, use static analysis of the query to
        /// materialize only those portions of the document relevant to the query.
        /// This can significantly enhance performance of queries against documents from containers of
        /// type <see cref="XmlContainerType.WholeDocContainer"/> and documents not in a container. It should not be used if
        /// arbitrary navigation of the resulting nodes is to be performed, as not all nodes in the
        /// original document will be present and unexpected results could be returned. This flag
        /// has no effect on documents in containers of type <see cref="XmlContainerType.NodeContainer"/>.
        /// </summary>
        DocumentProjection, // Equates to DBXML_DOCUMENT_PROJECTION

        /// <summary>
        /// This operation will have degree 2 isolation. This provides for cursor stability but not
        /// repeatable reads. Data items which have been previously read by this transaction may be deleted
        /// or modified by other transactions before this transaction completes.
        /// </summary>
        ReadCommitted, // Equates to DB_READ_COMMITTED

        /// <summary>
        /// This operation will support degree 1 isolation; that is, read operations may return data
        /// that has been modified by other transactions but which has not yet been committed.
        /// Silently ignored if the <see cref="ContainerConfig.ReadUncommitted"/>
        /// flag was not specified when the underlying container was opened.
        /// </summary>
        ReadUncommitted, // Equates to DB_READ_UNCOMMITTED
        /// <summary>
        /// Acquire write locks instead of read locks when doing the read, if locking is configured.
        /// Setting this flag can eliminate deadlock during a read-modify-write cycle by
        /// acquiring the write lock during the read part of the cycle so that another thread of
        /// control acquiring a read lock for the same item, in its own read-modify-write cycle,
        /// will not result in deadlock.
        /// </summary>
        ReadModifyWrite, // Equates to DB_RMW
        /// <summary>
        /// Force the use of a scanner that will neither validate nor read schema or DTDs associated with the
        /// document during parsing.
        /// <para>
        /// This is efficient, but can cause parsing errors if the document references
        /// information that might have come from a schema or DTD, such as entity references.
        /// </para>
        /// </summary>
        WellFormedOnly, // Equates to DBXML_WELL_FORMED_ONLY
    }

    /// <summary>
    /// The state of the container.
    /// </summary>
    public enum ContainerState
    {
        /// <summary>
        /// The container is open.
        /// </summary>
        Open,
        /// <summary>
        /// The container is closed.
        /// </summary>
        Closed
    }
}

#endregion Xml