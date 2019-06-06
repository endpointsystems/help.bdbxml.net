---
uid: transaction-tuning.md
---

# Transaction Tuning

There are a few different issues to consider when tuning the performance of Figaro transactional applications. First, you should review [Performance Tuning](xref:performance-tuning.md), as the tuning issues for access method applications are applicable to transactional applications as well. The following are additional tuning issues for transactional applications:

#### Read Locks

Performing all read operations outside of transactions or at [Snapshot Isolation](xref:snapshot-isolation.md) can often significantly increase application throughput. In addition, limiting the lifetime of non-transactional cursors will reduce the length of times locks are held, thereby improving concurrency.



#### DirectDB and DirectLog

Consider using the @Figaro.EnvConfig.DirectDB and @Figaro.EnvConfig.DirectLog flags. On some systems, avoiding caching in the operating system can improve write throughput and allow the creation of larger Figaro caches.



#### Read Committed and Uncommitted

Consider decreasing the level of isolation of transaction using the @Figaro.TransactionType.ReadUncommitted or @Figaro.TransactionType.ReadCommitted flags for transactions or cursors or the @Figaro.TransactionType.ReadUncommitted flag on individual read operations. The @Figaro.TransactionType.ReadCommitted flag will release read locks on cursors as soon as the data page is no longer referenced. This is also called degree 2 isolation. This will tend to block write operations for shorter periods for applications that do not need to have repeatable reads for cursor operations.


The @Figaro.TransactionType.ReadUncommitted flag will allow read operations to potentially return data which has been modified but not yet committed, and can significantly increase application throughput in applications that do not require data be guaranteed to be permanent in the database. This is also called _degree 1 isolation_, or _dirty reads_.



#### Read-Modify-Write

If there are many deadlocks, consider using the @Figaro.QueryOptions.ReadModifyWrite flag to immediate acquire write locks when reading data items that will subsequently be modified. Although this flag may increase contention (because write locks are held longer than they would otherwise be), it may decrease the number of deadlocks that occur.



#### Sync and No-Sync Transactions

By default, transactional commit in Figaro implies durability; that is, all committed operations will be present in the database after recovery from any application or system failure. For applications not requiring that level of certainty, specifying the @Figaro.EnvConfig.NoSyncTransaction flag will often provide a significant performance improvement. In this case, the database will still be fully recoverable, but some number of committed transactions might be lost after application or system failure.



#### Access Databases in Order

When modifying multiple databases in a single transaction, always access physical files and databases within physical files, in the same order where possible. In addition, avoid returning to a physical file or database, that is, avoid accessing a database, moving on to another database and then returning to the first database. This can significantly reduce the chance of deadlock between threads of control.



#### Log Buffer Size

Figaro internally maintains a buffer of log writes. The buffer is written to disk at transaction commit, by default, or, whenever it is filled. If it is consistently being filled before transaction commit, it will be written multiple times per transaction, costing application performance. In these cases, increasing the size of the log buffer can increase application throughput.

#### Log File Location

If the database environment's log files are on the same disk as the databases, the disk arms will have to seek back-and-forth between the two. Placing the log files and the databases on different disk arms can often increase application throughput.

## See Also

* @Figaro.QueryOptions
* @Figaro.EnvConfig
* [Performance Tuning](xref:performance-tuning.md)
* [Snapshot Isolation](xref:snapshot-isolation.md)