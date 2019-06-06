---
uid: transaction-faq.md
---

# Transaction FAQ

## What should a transactional program do when an error occurs?

Any time an error occurs, such that a transactionally protected set of operations cannot complete successfully, the transaction must be aborted. While deadlock is by far the most common of these errors, there are other possibilities, such as running out of disk space for the file system.


In transactional applications, there are three classes of error returns: "expected" errors, "unexpected but recoverable" errors, and a single "unrecoverable" error. Expected errors are @Figaro.FigaroException exceptions containing the @Figaro.FigaroExceptionCategory.ContainerNotFound category, which indicates that a searched-for key item is not present in the database. Applications may want to explicitly test for and handle this exception or, in the case where the absence of a key implies the enclosing transaction should fail, simply call 
@Figaro.XmlTransaction.Abort. Unexpected but recoverable errors are errors like @Figaro.DBDeadlockException, which indicates that an operation has been selected to resolve a deadlock, or a system error which likely indicates that the file system has no available disk space. Applications must immediately call @Figaro.XmlTransaction.Abort when these returns occur, as it is not possible to proceed otherwise. The only unrecoverable error is @Figaro.RunRecoveryException, which indicates that the system must stop and recovery must be run.


## How do hot backups work? Can't you get an inconsistent picture of the database when you copy it?

First, Figaro's Berkeley DB database subsystem is based on the technique of "write-ahead logging", which means that before any change is made to a database, a log record is written that describes the change. Further, Berkeley DB guarantees that the log record that describes the change will always be written to stable storage (that is, disk) before the database page where the change was made is written to stable storage. Because of this guarantee, we know that any change made to a database will appear either in just a log file, or both the database and a log file, but never in just the database.


Second, you can always create a consistent and correct database based on the log files and the databases from a database environment. So, during a hot backup, we first make a copy of the databases and then a copy of the log files. The tricky part is that there may be pages in the database that are related for which we won't get a consistent picture during this copy. For example, let's say that we copy pages 1-4 of the database, and then are swapped out. For whatever reason (perhaps because we needed to flush pages from the cache, or because of a checkpoint), the database pages 1 and 5 are written. Then, the hot backup process is re-scheduled, and it copies page 5. Obviously, we have an inconsistent database snapshot, because we have a copy of page 1 from before it was written by the other thread of control, and a copy of page 5 after it was written by the other thread. What makes this work is the order of operations in a hot backup.


Because of the write-ahead logging guarantees, we know that any page written to the database will first be referenced in the log. If we copy the database first, then we can also know that any inconsistency in the database will be described in the log files, and so we know that we can fix everything up during recovery.



## My application is throwing deadlock exceptions. Is the normal, and what should I do?

It is quite common for deadlock issues to occur. All applications should be prepared to handle deadlock returns, because even if the application is deadlock free when deployed, future changes to the application or the database implementation might introduce deadlocks.


Practices which _reduce_ the chance of deadlock include:

* Not navigating backwards through the database (via @Figaro.XmlResults), as backward navigation creates backward-scanning database cursors which can deadlock with page splits.
* Not configuring @Figaro.TransactionType.ReadUncommitted as that flag requires write transactions upgrade their locks when aborted, which can lead to deadlock. Generally, @Figaro.TransactionType.ReadCommitted or non-transactional read operations are less prone to deadlock than @Figaro.TransactionType.ReadUncommitted.

## How can I move a database from one transactional environment into another?

Because database pages contain references to log records, databases cannot be simply moved into different database environments. To move a database into a different environment, dump and reload the database (using the [dbxml_dump](xref:dbxml_dump.md) and [dbxml_load](xref:dbxml_load.md) utilities, respectively)before moving it. If the database is too large to dump and reload, the database may be prepared in place using the @Figaro.FigaroEnv.LSNReset(System.String,System.Boolean) method or the `-r` argument to the [dbxml_load](xref:dbxml_load.md) utility.



## I'm getting an exception that says `log_flush: LSN past current end-of-log`. What does that mean?

The most common cause of this exception is that a system administrator has removed all of the log files from a database environment. You should shut down your database environment as gracefully as possible, first flushing the database environment cache to disk, if that's possible. Then, dump and reload your databases. If the database is too large to dump and reload, the database may be reset in place using the @Figaro.FigaroEnv.LSNReset(System.String,System.Boolean) method or the `-r` argument to the [dbxml_load](xref:dbxml_load.md) utility. However, if you reset the database in place, you should verify your databases before using them again.

>[!NOTE]
>It is possible for the databases to be corrupted by running after all of the log files have been removed, and the longer the application runs, the worse it can get.

## See Also

* @Figaro.RunRecoveryException
* @Figaro.FigaroException
* @Figaro.FigaroExceptionCategory
* @Figaro.XmlResults
* @Figaro.TransactionType
* @Figaro.FigaroEnv.LSNReset(System.String,System.Boolean)
* [dbxml_dump](xref:dbxml_dump.md)
* [dbxml_load](xref:dbxml_load.md)