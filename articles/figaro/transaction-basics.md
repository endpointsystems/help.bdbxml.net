---
uid: transaction-basics.md
---

# Transaction Basics

Once you have enabled transactions for your environment and your containers, you can use them to protect your container operations. You do this by acquiring a transaction handle and then using that handle for any database operation that you want to participate in that transaction.

You obtain a transaction handle using the @Figaro.XmlManager.CreateTransaction method.


Once you have completed all of the operations that you want to include in the transaction, you must commit the transaction using the @Figaro.XmlTransaction.Commit method.


If, for any reason, you want to abandon the transaction, you abort it using @Figaro.XmlTransaction.Abort. Note that if the last remaining @Figaro.XmlTransaction handle goes out of scope without being resolved, then the transaction is automatically aborted.


Any transaction handle that has been committed or aborted can no longer be used by your application.


Finally, you must make sure that all transaction handles are either committed or aborted before closing your containers and environment.


For example, the following example opens a transactional-enabled environment and container, obtains a transaction handle, and then performs a write operation under its protection. In the event of any failure in the write operation, the transaction is aborted and the container is left in a state as if no operations had ever been attempted in the first place.



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


## In This Section

* [Committing a Transaction](xref:committing-a-transaction.md)
* [Aborting a Transaction](xref:aborting-a-transaction.md)
* [Nested Transactions](xref:nested-transactions.md)
* [Configuring the Transaction Subsystem](xref:configuring-the-transaction-subsystem.md)


#### Other Resources
[Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)