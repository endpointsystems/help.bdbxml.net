---
uid: committing-a-transaction.md
---

# Committing a Transaction

In order to fully understand what is happening when you commit a transaction, you must first understand a little about what Figaro is doing with the logging subsystem. Logging causes all container write operations to be identified in logs, and by default these logs are backed by files on disk. These logs are used to restore your containers in the event of a system or application failure, so by performing logging, Figaro ensures the integrity of your data.

Moreover, Figaro performs _write-ahead_ logging. This means that information is written to the logs before the actual container is changed. This means that all write activity performed under the protection of the transaction is noted in the log before the transaction is committed. Be aware, however, that container maintains logs in-memory. If you are backing your logs on disk, the log information will eventually be written to the log files, but while the transaction is on-going the log data may be held only in memory.


When you commit a transaction, the following occurs:

A commit record is written to the log. This indicates that the modifications made by the transaction are now permanent. By default, this write is performed synchronously to disk so the commit record arrives in the log files before any other actions are taken.
Any log information held in memory is (by default) synchronously written to disk. Note that this requirement can be relaxed, depending on the type of commit you perform. See [Non-Durable Transactions](xref:non-durable-transactions.md) for more information. Also, if you are maintaining your logs entirely in-memory, then this step will of course not be taken. To configure your logging system for in-memory usage, see [Configuring In-Memory Logging](xref:configuring-in-memory-logging.md).
All locks held by the transaction are released. This means that read operations performed by other transactions or threads of control can now see the modifications without resorting to uncommitted reads (see [Reading Uncommitted Data](xref:reading-uncommitted-data.md) for more information).
To commit a transaction, you simply call @Figaro.XmlTransaction.Commit.


Notice that committing a transaction does not necessarily cause data modified in your memory cache to be written to the files backing your containers on disk. Dirtied database pages are written for a number of reasons, but a transactional commit is not one of them. The following are the things that can cause a dirtied database page to be written to the backing database file:

* **Checkpoints.** Checkpoints cause all dirtied pages currently existing in the cache to be written to disk, and a checkpoint record is then written to the logs. You can run checkpoints explicitly. For more information on checkpoints, see [Checkpoints](xref:checkpoints.md)
* **Cache is full.**If the in-memory cache fills up, then dirtied pages might be written to disk in order to free up space for other pages that your application needs to use. Note that if dirtied pages are written to the database files, then any log records that describe how those pages were dirtied are written to disk before the database pages are written.

Be aware that because your transaction commit caused container modifications recorded in your logs to be forced to disk, your modifications are by default "persistent" in that they can be recovered in the event of an application or system failure. However, recovery time is gated by how much data has been modified since the last checkpoint, so for applications that perform a lot of writes, you may want to run a checkpoint with some frequency.

Note that once you have committed a transaction, the transaction handle that you used for the transaction is no longer valid. To perform container activities under the control of a new transaction, you must obtain a fresh transaction handle.


## See Also
* [Non-Durable Transactions](xref:non-durable-transactions.md)
* [Reading Uncommitted Data](xref:reading-uncommitted-data.md)
* [Checkpoints](xref:checkpoints.md)
* [Configuring In-Memory Logging](xref:configuring-in-memory-logging.md)
