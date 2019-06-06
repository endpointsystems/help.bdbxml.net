---
uid: recoverability-and-deadlock-handling.md
---

# Recoverability and Deadlock Handling

The primary reason for using transactions is recoverability. Any logical change to a database may require multiple changes to underlying data structures. For example, a single write operation call can potentially require that multiple physical database pages be written. If only some of those pages are written and then the system or application fails, the database is left inconsistent and cannot be used until it has been recovered; that is, until the partially completed changes have been undone.


_Write-ahead logging_ is the term that describes the underlying implementation that Figaro uses to ensure recoverability. What it means is that before any change is made to a database, information about the change is written to a database log. During recovery, the log is read, and databases are checked to ensure that changes described in the log for committed transactions appear in the database. Changes that appear in the database but are related to aborted or unfinished transactions in the log are undone from the database.


For recoverability after application or system failure, operations that modify the database must be protected by transactions. More specifically, operations are not recoverable unless a transaction is begun and each operation is associated with the transaction via the Figaro interfaces, and then the transaction successfully committed. This is true even if logging is turned on in the database environment.


Figaro also uses transactions to recover from deadlock. Database operations are usually performed on behalf of a unique locker. Transactions can be used to perform multiple calls on behalf of the same locker within a single thread of control. For example, consider the case in which an application uses a cursor scan to locate a record and then the application accesses another other item in the database, based on the key returned by the cursor, without first closing the cursor. If these operations are done using default locker IDs, they may conflict. If the locks are obtained on behalf of a transaction, using the transaction's locker ID instead of the database handle's locker ID, the operations will not conflict.


In transactional (not Concurrent Data Store) applications supporting both readers and writers, or just multiple writers, Figaro functions have an additional possible exception: @Figaro.DBDeadlockException. This means two threads of control deadlocked, and the thread receiving the @Figaro.DBDeadlockException has been selected to discard its locks in order to resolve the problem. When an application receives a @Figaro.DBDeadlockException, the correct action is to close any cursors involved in the operation and abort any enclosing transaction. In the sample code, any time the database operation throws @Figaro.DBDeadlockException, @Figaro.XmlTransaction.Abort is called (which releases the transaction's resources and undoes any partial changes to the databases), and then the transaction is retried from the beginning.


There is no requirement that the transaction be attempted again, but that is a common course of action for applications. Applications may want to set an upper bound on the number of times an operation will be retried because some operations on some data sets may simply be unable to succeed. For example, updating all of the pages on a large Web site during prime business hours may simply be impossible because of the high access rate to the database.


The @Figaro.XmlTransaction.Abort method is called in error cases other than deadlock. Any time an error occurs, such that a transactionally protected set of operations cannot complete successfully, the transaction must be aborted. While deadlock is by far the most common of these errors, there are other possibilities; for example, running out of disk space for the file system. In transactional applications, there are three classes of error returns: "expected" errors, "unexpected but recoverable" errors, and a single "unrecoverable" error. Applications may want to explicitly test for and handle Figaro exceptions, or, in the case where logic implies the enclosing transaction should fail, simply call @Figaro.XmlTransaction.Abort. Applications must immediately call @Figaro.XmlTransaction when these returns occur, as it is not possible to proceed otherwise. The only unrecoverable exception is @Figaro.RunRecoveryException</a>, which indicates that the system must stop and recovery must be run.


Figaro also uses transactions to recover from deadlock. Database operations (that is, any call to a function underlying the container handles returned by @Figaro.XmlManager.OpenContainer are usually performed on behalf of a unique locker. Transactions can be used to perform multiple calls on behalf of the same locker within a single thread of control. For example, consider the case in which an application uses a cursor scan to locate a record and then the application accesses another other item in the database, based on the key returned by the cursor, without first closing the cursor. If these operations are done using default locker IDs, they may conflict. If the locks are obtained on behalf of a transaction, using the transaction's locker ID instead of the database handle's locker ID, the operations will not conflict.

## See Also

* @Figaro.DBDeadlockException
* @Figaro.XmlTransaction

#### Other Resources
* [Managing Database Files](xref:managing-database-files.md)
