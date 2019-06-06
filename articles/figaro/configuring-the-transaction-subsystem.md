---
uid: configuring-the-transaction-subsystem.md
---

# Configuring the Transaction Subsystem

Most of the configuration activities that you need to perform for your transactional Figaro application will involve the locking and logging subsystems. See [Concurrency](xref:concurrency.md) and [Managing Database Files](xref:managing-database-files.md) for details.


However, there are a couple of things that you can do to configure your transaction subsystem directly. These things are:

Configure the maximum number of simultaneous transactions needed by your application. In general, you should not need to do this unless you use deeply nested transactions or you have many threads all of which have active transactions. In addition, you may need to a higher maximum number of transactions if you are using snapshot isolation. See [Snapshot Isolation](xref:snapshot-isolation.md) for details.


By default, your application can support 20 active transactions.


You can set the maximum number of simultaneous transactions supported by your application using the @Figaro.FigaroEnv.MaxTransactions property. Note that this method must be called before the environment has been opened.


If your application has exceeded this maximum value, then any attempt to begin a new transaction will fail.
Configure the timeout value for your transactions. This value represents the longest period of time a transaction can be active. Note, however, that transaction timeouts are checked only when Figaro examines its lock tables for blocked locks (see [Locks, Blocks and Deadlocks](xref:locks-blocks-and-deadlocks.md) for more information). Therefore, a transaction's timeout can have expired, but the application will not be notified until Figaro has a reason to examine its lock tables.


Be aware that some transactions may be inappropriately timed out before the transaction has a chance to complete. You should therefore use this mechanism only if you know your application might have unacceptably long transactions and you want to make sure your application will not stall during their execution. (This might happen if, for example, your transaction blocks or requests too much data.)
>[!NOTE]
>Transaction timeouts default to 0 seconds, which means that they never time out.

To set the maximum timeout value for your transactions, use the @Figaro.FigaroEnv.SetTimeout(System.UInt32,Figaro.EnvironmentTimeoutType) method. This method configures the entire environment, not just the handle used to set the configuration. Further, this value may be set at any time during the application's lifetime.

## See Also

* @Figaro.FigaroEnv
* @Figaro.FigaroEnv.SetTimeout(System.UInt32,Figaro.EnvironmentTimeoutType)
* [Locks, Blocks and Deadlocks](xref:locks-blocks-and-deadlocks.md)
* [Managing Database Files](xref:managing-database-files.md) 
* [Concurrency](xref:concurrency.md)
* [Snapshot Isolation](xref:snapshot-isolation.md)

