---
uid: terminology.md
---

# Terminology

Here are some definitions that will be helpful in understanding transactions:

## Thread of Control

Figaro is indifferent to the type or style of threads being used by the application; or, for that matter, if threads are being used at all — because Figaro supports multiprocess access. In the documentation, any time we refer to a _thread of control_, it can be read as a true thread (one of many in an application's address space) or a process.



## Free-Threaded

A Figaro object handle that can be used by multiple threads simultaneously without any application-level synchronization is called _free-threaded_



## Transaction

A _transaction_ is defined as one or more operations on one or more databases that should be treated as a single unit of work. For example, changes to a set of databases, in which either all of the changes must be applied to the database(s) or none of them should. Applications specify when each transaction starts, what database operations are included in it, and when it ends.



## Transaction Commit/Abort

Every transaction ends by _committing_ or _aborting_. If a transaction commits, Figaro guarantees that any database changes included in the transaction will never be lost, even after system or application failure. If a transaction aborts, or is uncommitted when the system or application fails, then the changes involved will never appear in the database.



## System or Application Failure

_System or application failure_ is the phrase we use to describe something bad happening near your data. It can be an application dumping core, being interrupted by a signal, the disk filling up, or the entire system crashing. In any case, for whatever reason, the application can no longer make forward progress, and its databases are left in an unknown state.



## Recovery

_Recovery_ is what makes the database consistent after a system or application failure. The recovery process includes review of log files and databases to ensure that the changes from each committed transaction appear in the database, and that no changes from an unfinished (or aborted) transaction do. Whenever system or application failure occurs, applications must usually run recovery.



## Deadlock

_Deadlock_, in its simplest form, happens when one thread of control owns resource A, but needs resource B; while another thread of control owns resource B, but needs resource A. Neither thread of control can make progress, and so one has to give up and release all its resources, at which time the remaining thread of control can make forward progress.



## Isolation

_Isolation_ means that container modifications made by one transaction will not normally be seen by readers from another transaction until the first commits its changes. Different threads use different transaction handles, so this mechanism is normally used to provide isolation between container operations performed by different threads.


Note that Figaro supports different isolation levels. For example, you can configure your application to see uncommitted reads, which means that one transaction can see data that has been modified but not yet committed by another transaction. Doing this might mean your transaction reads data "dirtied" by another transaction, but which subsequently might change before that other transaction commits its changes. On the other hand, lowering your isolation requirements means that your application can experience improved throughput due to reduced lock contention.

