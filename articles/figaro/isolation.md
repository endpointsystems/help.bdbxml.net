---
uid: isolation.md
---

# Isolation

While a transaction is in progress, your containers will appear to the transaction as if there are no other operations occurring outside of the transaction. That is, operations wrapped inside a transaction will always have a clean and consistent view of your databases. They never have to see updates currently in progress under the protection of another transaction. Note, however, that isolation guarantees can be relaxed from the default setting.


Transactions can be isolated from each other to different degrees. Serializable provides the most isolation, and means that, for the life of the transaction, every time a thread of control reads a data item, it will be unchanged from its previous value (assuming, of course, the thread of control does not itself modify the item). By default, Figaro enforces serializability whenever database reads are wrapped in transactions. This is also known as degree 3 isolation.


Most applications do not need to enclose all reads in transactions, and when possible, transactionally protected reads at serializable isolation should be avoided as they can cause performance problems. For example, a serializable cursor sequentially reading each key/data pair in a database, will acquire a read lock on most of the pages in the database and so will gradually block all write operations on the databases until the transaction commits or aborts. Note, however, that if there are update transactions present in the application, the read operations must still use locking, and must be prepared to repeat any operation (possibly closing and reopening a cursor) that fails with a 
@Figaro.DBDeadlockException thrown. Applications that need repeatable reads are ones that require the ability to repeatedly access a data item knowing that it will not have changed (for example, an operation modifying a data item based on its existing value).


Snapshot isolation also guarantees repeatable reads, but avoids read locks by using multi-version concurrency control (MVCC). This makes update operations more expensive, because they have to allocate space for new versions of pages in cache and make copies, but avoiding read locks can significantly increase throughput for many applications. Snapshot isolation is discussed in detail below.


A transaction may only require cursor stability; that is, only be guaranteed that cursors see committed data that does not change so long as it is addressed by the cursor, but may change before the reading transaction completes. This is also called degree 2 isolation. Figaro provides this level of isolation when a transaction is started with the @Figaro.TransactionType.ReadCommitted flag. This flag may also be specified when opening a cursor within a fully isolated transaction.


Figaro optionally supports reading uncommitted data; that is, read operations may request data which has been modified but not yet committed by another transaction. This is also called degree 1 isolation. This is done by first specifying the @Figaro.TransactionType.ReadUncommitted flag when opening the underlying database, and then specifying the @Figaro.TransactionType.ReadUncommitted flag when beginning a transaction or performing a read operation. The advantage of using @Figaro.TransactionType.ReadUncommitted is that read operations will not block when another transaction holds a write lock on the requested data; the disadvantage is that read operations may return data that will disappear should the transaction holding the write lock abort.



## Snapshot Isolation

To make use of snapshot isolation, databases must first be configured for multi-version access by calling @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) with the @Figaro.EnvConfig.MultiVersion flag. Then transactions must be configured with the @Figaro.TransactionType.SnapshotTransaction flag.


When configuring an environment for snapshot isolation, it is important to realize that having multiple versions of pages in cache means that the working set will take up more of the cache. As a result, snapshot isolation is best suited for use with larger cache sizes.


If the cache becomes full of page copies before the old copies can be discarded, additional I/O will occur as pages are written to temporary "freezer" files. This can substantially reduce throughput, and should be avoided if possible by configuring a large cache and keeping snapshot isolation transactions short. The amount of cache required to avoid freezing buffers can be estimated by taking a checkpoint followed by a call to @Figaro.FigaroEnv.LogArchive(Figaro.LogArchiveOptions). The amount of cache required is approximately double the size of logs that remains.


The environment should also be configured for sufficient transactions using @Figaro.FigaroEnv.SetMaxTransactions(System.UInt16). The maximum number of transactions needs to include all transactions executed concurrently by the application plus all cursors configured for snapshot isolation. Further, the transactions are retained until the last page they created is evicted from cache, so in the extreme case, an additional transaction may be needed for each page in the cache.


So when should applications use snapshot isolation?
* There is a large cache relative to size of updates performed by concurrent transactions; and
* Read/write contention is limiting the throughput of the application; or
* The application is all or mostly read-only, and contention for the lock manager mutex is limiting throughput.

The simplest way to take advantage of snapshot isolation is for queries: keep update transactions using full read/write locking and set @Figaro.TransactionType.SnapshotTransaction on read-only transactions. This should minimize blocking of snapshot isolation transactions and will avoid introducing new @Figaro.DBDeadlockException exceptions.


If the application has update transactions which read many items and only update a small set (for example, scanning until a desired record is found, then modifying it), throughput may be improved by running some updates at snapshot isolation as well.

## See Also

* [Why Transactions?](xref:why-transactions.md)