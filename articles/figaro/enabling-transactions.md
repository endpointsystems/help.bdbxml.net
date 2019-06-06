---
uid: enabling-transactions.md
---

# Enabling Transactions

In order to use transactions with your application, you must turn them on. To do this you must:


**Use an externally-managed environment** (see [Environments](xref:environments.md) for details).


**Turn on transactions for your environment.** You do this by providing the @Figaro.EnvOpenOptions.InitTransaction flag to the @Figaro.FigaroEnv.Open method. Note that initializing the transactional subsystem implies that the logging subsystem is also initialized. Also, note that if you do not initialize transactions when you first create your environment, then you cannot use transactions for that environment after that. This is because Figaro allocates certain structures needed for transactional locking that are not available if the environment is created without transactional support.


**Initialize the in-memory cache.** You do this by passing the @Figaro.EnvOpenOptions.InitMemoryBufferPool flag to the @Figaro.FigaroEnv.Open method.


**Initialize the locking subsystem.** This is what provides locking for concurrent applications. It also is used to perform deadlock detection. See Concurrency for more information.


You initialize the locking subsystem by passing the @Figaro.EnvOpenOptions.InitLock flag to the @Figaro.FigaroEnv.Open method.


**Initialize the logging subsystem.** While this is enabled by default for transactional applications, we suggest that you explicitly initialize it anyway for the purposes of code readability. The logging subsystem is what provides your transactional application its durability guarantee, and it is required for recoverability purposes. See [Managing Database Files](xref:managing-database-files.md) for more information.


You initialize the logging subsystem by passing the @Figaro.EnvOpenOptions.InitLog flag to the @Figaro.FigaroEnv.Open method.


**Transaction-enable your containers.** You do this by passing the @Figaro.ContainerConfig.Transactional flag when you open or create the container.



## In This Section
* [Environments](xref:environments.md)
* [Opening a Transactional Environment and Container](xref:opening-a-transactional-environment-and-container.md)


## See Also

* @Figaro.EnvOpenOptions.InitLog
* @Figaro.FigaroEnv.Open
* @Figaro.ContainerConfig

#### Other Resources
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)
* [Managing Database Files](xref:managing-database-files.md)