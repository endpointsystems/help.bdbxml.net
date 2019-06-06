---
uid: opening-a-transactional-environment-and-container.md
---

# Opening a Transactional Environment and Container

To enable transactions for your environment, you must initialize the transactional subsystem. Note that doing this also initializes the logging subsystem. In addition, you must initialize the memory pool (in-memory cache). Frequently, but not always, you will also initialize the locking subsystem.


Creating transaction-protected applications using Figaro is quite easy. Applications first use @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) to initialize the database environment. Transaction-protected applications normally require all four subsystems, so the @Figaro.EnvOpenOptions.InitMemoryBufferPool, @Figaro.EnvOpenOptions.InitLock, @Figaro.EnvOpenOptions.InitLog, and @Figaro.EnvOpenOptions.InitTransaction flags should be specified. Or, you can also use the @Figaro.EnvOpenOptions.TransactionDefaults flag which includes all four of these subsystems.


Once the application has called @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions), it opens its databases within the environment. Once the databases are opened, the application makes changes to the databases inside of transactions. Each set of changes that entails a unit of work should be surrounded by the appropriate @Figaro.XmlTransaction calls. The access methods will make the appropriate calls into the Lock, Log and Memory Pool subsystems in order to guarantee transaction semantics. When the application is ready to exit, all outstanding transactions should have been committed or aborted.


Databases accessed by a transaction must not be closed during the transaction. Once all outstanding transactions are finished, all open containers should be closed. When the containers have been closed, the environment should be closed by calling @Figaro.FigaroEnv.Close.


Notice in the example below that you first create the environment handle, and then you provide the handle to the @Figaro.XmlManager constructor. You do this because you cannot use transactions with the @Figaro.XmlManager instance's default internal environment.


Notice in the following example that you first create the environment handle, and then you provide the handle to the @Figaro.XmlManager constructor. You do this because you cannot use transactions with the @Figaro.XmlManager instance's default internal environment.


``` C#
const EnvOpenOptions options = 
                                // create the environment
                                EnvOpenOptions.Create |
                                // initialize locking
                                EnvOpenOptions.InitLock | 
                                // initialize logging
                                EnvOpenOptions.InitLog |
                                // initialize memory cache
                                EnvOpenOptions.InitMemoryBufferPool |
                                // initialize transactions
                                EnvOpenOptions.InitTransaction;

    var env = new FigaroEnv();
    // open the environment home directory
    env.Open(homePath, options);
    // initialize the manager with the specified environment
    var mgr = new XmlManager(env);

    // your database operations go here

    // close our un-adopted environment
    env.Close();
```

You then create and/or open your containers as normal. The only difference is that you must pass the @Figaro.ContainerConfig.Transactional flag to the @Figaro.XmlManager.OpenContainer(Figaro.ContainerConfig) or Figaro.XmlManager.CreateContainer(Figaro.ContainerConfig) method. For example:


``` C#
const EnvOpenOptions options = 
                                // create the environment
                                EnvOpenOptions.Create |
                                // initialize locking
                                EnvOpenOptions.InitLock | 
                                // initialize logging
                                EnvOpenOptions.InitLog |
                                // initialize memory cache
                                EnvOpenOptions.InitMemoryBufferPool |
                                // initialize transactions
                                EnvOpenOptions.InitTransaction;

    // configure a container for transactions
    var cfg = new ContainerConfig
                  {
                      AllowCreate = true,
                      Transactional = true
                  };
    var env = new FigaroEnv();
    // open the environment home directory
    env.Open(homePath, options);
    // initialize the manager with the specified environment
    var mgr = new XmlManager(env);

    // open a transactional container
    var trans = mgr.CreateTransaction();
    var container = mgr.OpenContainer(trans, cfg);
    trans.Commit();

    // your database operations go here

    // close our un-adopted environment
    env.Close();
```

After running this initial program, we can use the @db_stat.md utility to display the contents of the environment directory:


```
>db_stat -e -h .
Sun Dec 13 23:04:11 2009        Local time
0x120897        Magic number
0       Panic value
4.8.24  Environment version
9       Btree version
9       Hash version
1       Lock version
15      Log version
4       Queue version
2       Sequence version
1       Txn version
Sun Dec 13 23:02:46 2009        Creation time
0xfa8593b       Environment ID
2       Primary region allocation and reference count mutex [0/2 0% !Own]
1       References
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
0x40988 Log magic number
15      Log version number
31KB 256B       Log record cache size
0       Log file mode
10Mb    Current log file size
249     Records entered into the log
671KB 372B      Log bytes written
671KB 372B      Log bytes written since last checkpoint
22      Total log file I/O writes
21      Total log file I/O writes due to overflow
5       Total log file flushes
0       Total log file I/O reads
1       Current log file number
687476  Current log file offset
1       On-disk log file number
687476  On-disk log file offset
1       Maximum commits in a log flush
1       Minimum commits in a log flush
160KB   Log region size
0       The number of region locks that required waiting (0%)
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
42      Last allocated locker ID
0x7fffffff      Current maximum unused locker ID
9       Number of lock modes
1000    Maximum number of locks possible
1000    Maximum number of lockers possible
1000    Maximum number of lock objects possible
20      Number of lock object partitions
0       Number of current locks
7       Maximum number of locks at any one time
6       Maximum number of locks in any one bucket
0       Maximum number of locks stolen by for an empty partition
0       Maximum number of locks stolen for any one partition
41      Number of current lockers
45      Maximum number of lockers at any one time
0       Number of current lock objects
5       Maximum number of lock objects at any one time
4       Maximum number of lock objects in any one bucket
0       Maximum number of objects stolen by for an empty partition
0       Maximum number of objects stolen for any one partition
130     Total number of locks requested
130     Total number of locks released
0       Total number of locks upgraded
0       Total number of locks downgraded
0       Lock requests not available due to conflicts, for which we waited
0       Lock requests not available due to conflicts, for which we did not wait
0       Number of deadlocks
0       Lock timeout value
0       Number of locks that have timed out
0       Transaction timeout value
0       Number of transactions that have timed out
800KB   The size of the lock region
0       The number of partition locks that required waiting (0%)
0       The maximum number of times any partition lock was waited for (0%)
0       The number of object queue operations that required waiting (0%)
0       The number of locker allocations that required waiting (0%)
0       The number of region locks that required waiting (0%)
4       Maximum hash bucket length
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
260KB 792B      Total cache size
1       Number of caches
1       Maximum number of caches
264KB   Pool individual cache size
0       Maximum memory-mapped file size
0       Maximum open file descriptors
0       Maximum sequential buffer writes
0       Sleep after writing maximum sequential buffers
0       Requested pages mapped into the process' address space
130     Requested pages found in the cache (76%)
41      Requested pages not found in the cache
82      Pages created in the cache
0       Pages read into the cache
53      Pages written from the cache to the backing file
0       Clean pages forced from the cache
53      Dirty pages forced from the cache
0       Dirty pages written by trickle-sync thread
29      Current total page count
0       Current clean page count
29      Current dirty page count
37      Number of hash buckets used for page location
4096    Assumed page size used
294     Total number of times hash chains searched for a page
5       The longest hash chain searched for a page
514     Total number of hash chain entries checked for page
0       The number of hash bucket locks that required waiting (0%)
0       The maximum number of times any hash bucket lock was waited for (0%)
0       The number of region locks that required waiting (0%)
0       The number of buffers frozen
0       The number of buffers thawed
0       The number of frozen buffers freed
205     The number of page allocations
403     The number of hash buckets examined during allocations
12      The maximum number of hash buckets examined for an allocation
53      The number of pages examined during allocations
1       The max number of pages examined for an allocation
0       Threads waited on page I/O
0       The number of times a sync is interrupted
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
1       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
1       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
1       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
Pool File: unknown
8192    Page size
0       Requested pages mapped into the process' address space
1       Requested pages found in the cache (50%)
1       Requested pages not found in the cache
2       Pages created in the cache
0       Pages read into the cache
0       Pages written from the cache to the backing file
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
0/0     No checkpoint LSN
Sun Dec 13 23:02:46 2009        Checkpoint timestamp
0x80000014      Last transaction ID allocated
100     Maximum number of active transactions configured
0       Active transactions
3       Maximum active transactions
20      Number of transactions begun
0       Number of transactions aborted
20      Number of transactions committed
0       Snapshot transactions
0       Maximum snapshot transactions
0       Number of transactions restored
48KB    Transaction region size
0       The number of region locks that required waiting (0%)
Active transactions:
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
80KB    Mutex region size
0       The number of region locks that required waiting (0%)
4       Mutex alignment
100     Mutex test-and-set spins
1500    Mutex total count
1363    Mutex free count
137     Mutex in-use count
162     Mutex maximum in-use count
Mutex counts
1363    Unallocated
1       env region
23      lock region
7       logical lock
1       log filename
1       log flush
1       log region
16      mpoolfile handle
29      mpool buffer
17      mpool file bucket
37      mpool hash bucket
1       mpool region
1       mutex region
1       transaction checkpoint
1       txn region
```

## See Also
@Figaro.ContainerConfig