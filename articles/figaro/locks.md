---
uid: locks.md
---

# Locks

When one thread of control wants to obtain access to an object, it requests a lock for that object. This lock is what allows Figaro to provide your application with its transactional isolation guarantees by ensuring that:

* no other thread of control can read that object (in the case of an exclusive lock)
* no other thread of control can modify that object (in the case of an exclusive or non-exclusive lock)

## Lock Resources

When locking occurs, there are three resources in use:

* **The locker.** This is the thing that holds the lock. In a transactional application, the locker is a transaction handle. For non-transactional operations, the locker is a cursor or a Container, Database, or some document management handle.
* **The lock.** This is the actual data structure that locks the object. In Figaro, a locked object structure in the lock manager is representative of the object that is locked.
( **The locked object.** The thing that your application actually wants to lock. In a Figaro application, the locked object is usually a database page, which in turn contains multiple database entries (key and data). Note that if you use node-level containers, your documents are split into multiple database records. Therefore, when you read or write an XML document that is stored in a node container, you may be locking multiple database pages.

You can configure how many total lockers, locks, and locked objects your application is allowed to support. See [Configuring the Locking Subsystem](xref:configuring-the-locking-subsystem.md) for details.


The following figure shows a transaction handle, `Txn A`, that is holding a lock on database page 002. In this graphic, `Txn A` is the locker, and the locked object is page 002. Only a single lock is in use in this operation.

**A sample lock**
![a sample lock](/images/simplelock.jpg)

## Types of Locks

Figaro applications support both exclusive and non-exclusive locks. Exclusive locks are granted when a locker wants to write to an object. For this reason, exclusive locks are also sometimes called write locks.


An exclusive lock prevents any other locker from obtaining any sort of a lock on the object. This provides isolation by ensuring that no other locker can observe or modify an exclusively locked object until the locker is done writing to that object.


Non-exclusive locks are granted for read-only access. For this reason, non-exclusive locks are also sometimes called _read locks_. Since multiple lockers can simultaneously hold read locks on the same object, read locks are also sometimes called _shared locks_.


A non-exclusive lock prevents any other locker from modifying the locked object while the locker is still reading the object. This is how transactional cursors are able to achieve repeatable reads; by default, the cursor's transaction holds a read lock on any object that the cursor has examined until such a time as the transaction is committed or aborted. You can avoid these read locks by using snapshot isolation. See [Snapshot Isolation](xref:snapshot-isolation.md) for details.


In the following figure, `Txn A` and `Txn B` are both holding read locks on page 002, while `Txn C` is holding a write lock on page 003:

![Read write locks](/images/rwlocks1.jpg)

## Lock Lifetime

A locker holds its locks until such a time as it does not need the lock any more. What this means is:

A transaction holds any locks that it obtains until the transaction is committed or aborted.
All non-transaction operations hold locks until such a time as the operation is completed. For cursor operations, the lock is held until the cursor is moved to a new position or closed.

## See Also

* [Locks, Blocks and Deadlocks](xref:locks-blocks-and-deadlocks.md)
* [Concurrency](xref:concurrency.md)
* [Snapshot Isolation](xref:snapshot-isolation.md)
* [Configuring the Locking Subsystem](xref:configuring-the-locking-subsystem.md)
