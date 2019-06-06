---
uid: architecting-figaro-tds-applications.md
---

# Architecting Figaro TDS Applications


When building Transactional Data Store applications, the architecture decisions involve application startup (running recovery) and handling system or application failure. For details on performing recovery, see the Recovery procedures.

Recovery in a database environment is a single-threaded procedure; that is, one thread of control or process must complete database environment recovery before any other thread of control or process operates in the Figaro environment. It may simplify matters that Figaro serializes recovery and creation of a new database environment.


Performing recovery first marks any existing database environment as "failed" and then removes it, causing threads of control running in the database environment to fail and return to the application. This feature allows applications to recover environments without concern for threads of control that might still be running in the removed environment. The subsequent re-creation of the database environment is serialized, so multiple threads of control attempting to create a database environment will serialize behind a single creating thread.


One consideration in removing (as part of recovering) a database environment which may be in use by another thread, is the type of mutex being used by the Figaro library. In the case of database environment failure when using test-and-set mutexes, threads of control waiting on a mutex when the environment is marked "failed" will quickly notice the failure and will return an error from the Figaro API. In the case of environment failure when using blocking mutexes, where the underlying system mutex implementation does not unblock mutex waiters after the thread of control holding the mutex dies, threads waiting on a mutex when an environment is recovered might hang forever. Applications blocked on events (for example, an application blocked on a network socket, or a GUI event) may also fail to notice environment recovery within a reasonable amount of time. Systems with such mutex implementations are rare, but do exist; applications on such systems should use an application architecture where the thread recovering the database environment can explicitly terminate any process using the failed environment, or configure Figaro for test-and-set mutexes, or incorporate some form of long-running timer or watchdog process to wake or kill blocked processes should they block for too long.


Regardless, it makes little sense for multiple threads of control to simultaneously attempt recovery of a database environment, since the last one to run will remove all database environments created by the threads of control that ran before it. However, for some applications, it may make sense for applications to have a single thread of control that performs recovery and then removes the database environment, after which the application launches a number of processes, any of which will create the database environment and continue forward.


There are three common ways to architect Figaro Transactional Data Store applications. The one chosen is usually based on whether or not the application is comprised of a single process or group of processes descended from a single process (for example, a server started when the system first boots), or if the application is comprised of unrelated processes (for example, processes started by web connections or users logged into the system).



## Single-Process Applications

The first way to architect Transactional Data Store applications is as a single process (the process may or may not be multithreaded.)


When this process starts, it runs recovery on the database environment and then opens its databases. The application can subsequently create new threads of control as it chooses. Those threads of control can either share already open @Figaro.FigaroEnv handles, or create their own. In this architecture, databases are rarely opened or closed when more than a single thread of control is running; that is, they are opened when only a single thread is running, and closed after all threads but one have exited. The last thread of control to exit closes the databases and the database environment.


This architecture is simplest to implement because thread serialization is easy and failure detection does not require monitoring multiple processes.


If the application's thread model allows processes to continue after thread failure, the @Figaro.FigaroEnv.FailCheck method can be used to determine if the database environment is usable after thread failure. If the application does not call @Figaro.FigaroEnv.FailCheck, or @Figaro.FigaroEnv.FailCheck returns `true`, the application must behave as if there has been a system failure, performing recovery and re-creating the database environment. Once these actions have been taken, other threads of control can continue (as long as all existing Figaro handles are first discarded), or restarted.



## Related Processes

The second way to architect Transactional Data Store applications is as a group of related processes (the processes may or may not be multithreaded).


This architecture requires the order in which threads of control are created be controlled to serialize database environment recovery.


In addition, this architecture requires that threads of control be monitored. If any thread of control exits with open Figaro handles, the application may call the @Figaro.FigaroEnv.FailCheck method to detect lost mutexes and locks and determine if the application can continue. If the application does not call @Figaro.FigaroEnv.FailCheck, or @Figaro.FigaroEnv.FailCheck throws an exception that the database environment can no longer be used, the application must behave as if there has been a system failure, performing recovery and creating a new database environment. Once these actions have been taken, other threads of control can be continued (as long as all existing Figaro handles are first discarded), or restarted.


The easiest way to structure groups of related processes is to first create a single "watcher" process (often a script) that starts when the system first boots, runs recovery on the database environment and then creates the processes or threads that will actually perform work. The initial thread has no further responsibilities other than to wait on the threads of control it has started, to ensure none of them unexpectedly exit. If a thread of control exits, the watcher process optionally calls the @Figaro.FigaroEnv.FailCheck method. If the application does not call @Figaro.FigaroEnv.FailCheck or if @Figaro.FigaroEnv.FailCheck throws an exception that the environment can no longer be used, the watcher kills all of the threads of control using the failed environment, runs recovery, and starts new threads of control to perform work.



## Unrelated Processes

The third way to architect Transactional Data Store applications is as a group of unrelated processes (the processes may or may not be multithreaded). This is the most difficult architecture to implement because of the level of difficulty in some systems of finding and monitoring unrelated processes.


One solution is to log a thread of control ID when a new Figaro handle is opened. For example, an initial "watcher" process could run recovery on the database environment and then create a sentinel file. Any "worker" process wanting to use the environment would check for the sentinel file. If the sentinel file does not exist, the worker would fail or wait for the sentinel file to be created. Once the sentinel file exists, the worker would register its process ID with the watcher (via shared memory, IPC or some other registry mechanism), and then the worker would open its @Figaro.FigaroEnv handles and proceed. When the worker finishes using the environment, it would unregister its process ID with the watcher. The watcher periodically checks to ensure that no worker has failed while using the environment. If a worker fails while using the environment, the watcher removes the sentinel file, kills all of the workers currently using the environment, runs recovery on the environment, and finally creates a new sentinel file.


The weakness of this approach is that, on some systems, it is difficult to determine if an unrelated process is still running. For example, POSIX systems generally disallow sending signals to unrelated processes. The trick to monitoring unrelated processes is to find a system resource held by the process that will be modified if the process dies. On POSIX systems, flock- or `fcntl`-style locking will work, as will LockFile on Windows systems. Other systems may have to use other process-related information such as file reference counts or modification times. In the worst case, threads of control can be required to periodically re-register with the watcher process: if the watcher has not heard from a thread of control in a specified period of time, the watcher will take action, recovering the environment.


The Figaro library includes one built-in implementation of this approach, the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method's @Figaro.EnvOpenOptions.Register flag.


If the @Figaro.EnvOpenOptions.Register flag is set, each process opening the database environment first checks to see if recovery needs to be performed. If recovery needs to be performed for any reason (including the initial creation of the database environment), and @Figaro.EnvOpenOptions.Register is also specified, recovery will be performed and then the open will proceed normally. If recovery needs to be performed and @Figaro.EnvOpenOptions.Register is not specified, the 
@Figaro.RunRecoveryException exception will be thrown.


There are two additional requirements for the @Figaro.EnvOpenOptions.Register architecture to work: First, all applications using the database environment must specify the @Figaro.EnvOpenOptions.Register flag when opening the environment. However, there is no additional requirement the application choose a single process to recover the environment, as the first process to open the database environment will know to perform recovery. Second, there can only be a single @Figaro.FigaroEnv handle per database environment in each process. As the @Figaro.EnvOpenOptions.Register locking is per-process, not per-thread, multiple @Figaro.FigaroEnv handles in a single environment could race with each other, potentially causing data corruption.



## See Also

* @Figaro.RunRecoveryException
* @Figaro.EnvOpenOptions
* [A Note on System Failure](xref:a-note-on-system-failure.md)
* @Figaro.FigaroEnv
