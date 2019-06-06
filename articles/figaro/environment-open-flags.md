---
uid: environment-open-flags.md
---

# Environment Open Flags

In order to use an environment, you must first open it. When you do this, there are a series of @Figaro.EnvOpenOptions flags that you can optionally specify. These flags are bitwise OR'd together, and they have the effect of enabling important subsystems (such as transactional support).

There are a great many environment open flags and these are described in the Berkeley DB documentation. However, there are a few that you are likely to want to use with your Figaro application, so we describe them here:

Regardless of the flags you decide to set at creation time, it is important to use the same ones on all subsequent environment opens (the exception to this is Create which is only required to create an environment). In particular, avoid using flags to open environments that were not used at creation time. This is because different subsystems require different data structures on disk, and it is therefore illegal to attempt to use subsystems that were not initialized when the environment was first created.

## Create

If the environment does not exist at the time that it is opened, then create it. It is an error to attempt to open a database environment that has not been created.

## InitLock

Initializes the locking subsystem. This subsystem is used when an application employs multiple threads or processes that are concurrently reading and writing Berkeley DB databases. In this situation, the locking subsystem, along with a deadlock detector, helps to prevent concurrent readers/writers from interfering with each other.


Remember that under the covers Figaro containers are using Berkeley DB databases, so if you want your containers to be accessible by multiple threads and/or multiple processes, then you should enable this subsystem.

## InitLog

Initializes the logging subsystem. This subsystem is used for database recovery from application or system failures. For more information on normal and catastrophic recovery, see the [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md) guide.


## InitMemoryBufferPool

Initializes the shared memory pool subsystem. This subsystem is required for multithreaded Figaro applications, and it provides an in-memory cache that can be shared by all threads and processes participating in this environment.


## InitTransaction

Initializes the transaction subsystem. This subsystem provides atomicity for multiple database access operations. When transactions are in use, recovery is possible if an error condition occurs for any given operation within the transaction. If this subsystem is turned on, then the logging subsystem must also be turned on.


## Recover

Causes normal recovery to be run against the underlying database. Normal recovery ensures that the database files are consistent relative to the operations recorded in the log files. This is useful if, for example, your application experienced an ungraceful shut down and there is consequently an possibility that some write operations were not flushed to disk.


Recovery can only be run if the logging subsystem is turned on. Also, recovery must only be run by a single thread of control; typically it is run by the application's master thread before any other database operations are performed.



## See Also

* @Figaro.EnvOpenOptions
* @Figaro.FigaroEnv

#### Other Resources
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)
* [XML Manager and Environments](xref:xml-manager-and-environments.md)
