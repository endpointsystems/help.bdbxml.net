---
uid: deadlocks.md
---

# Deadlocks

A deadlock occurs when two or more threads of control are blocked, each waiting on a resource held by the other thread. When this happens, there is no possibility of the threads ever making forward progress unless some outside agent takes action to break the deadlock.


For example, if `Txn A` is blocked by `Txn B` at the same time `Txn B` is blocked by `Txn A` then the threads of control containing `Txn A` and `Txn B` are deadlocked; neither thread can make any forward progress because neither thread will ever release the lock that is blocking the other thread.

**Deadlocking**
![deadlock](/images/deadlock.jpg)
When two threads of control deadlock, the only solution is to have a mechanism external to the two threads capable of recognizing the deadlock and notifying at least one thread that it is in a deadlock situation. Once notified, a thread of control must abandon the attempted operation in order to resolve the deadlock. Figaro's locking subsystem offers a deadlock notification mechanism. See [Configuring Deadlock Detection](xref:configuring-deadlock-detection.md) for more information.


Note that when one locker in a thread of control is blocked waiting on a lock held by another locker in that same thread of the control, the thread is said to be self-deadlocked.

## Deadlock Avoidance

The things that you do to avoid lock contention also help to reduce deadlocks (see Avoiding Blocks). Beyond that, you can also do the following in order to avoid deadlocks:

Make sure all threads access data in the same order as all other threads. So long as threads lock database pages in the same basic order, there is no possibility of a deadlock (threads can still block, however).


Be aware that if you are using secondary databases (indices), it is not possible to obtain locks in a consistent order because you cannot predict the order in which locks are obtained in secondary databases. If you are writing a concurrent application and you are using secondary databases, you must be prepared to handle deadlocks.


Declare a read/modify/write lock for those situations where you are reading a record in preparation of modifying and then writing the record. Doing this causes Figaro to give your read operation a write lock. This means that no other thread of control can share a read lock (which might cause contention), but it also means that the writer thread will not have to wait to obtain a write lock when it is ready to write the modified data back to the database.


* For information on declaring read/modify/write locks, see [Read/Modify/Write](xref:readmodifywrite.md).
* Use snapshot isolation for read-only threads that operate concurrently with writer threads. This will avoid read-write contention for your writer threads.
* For information on snapshot isolation, see [Snapshot Isolation](xref:snapshot-isolation.md).

## See Also


#### Other Resources
* [Locks, Blocks and Deadlocks](xref:locks-blocks-and-deadlocks.md)
* [Snapshot Isolation](xref:snapshot-isolation.md)
