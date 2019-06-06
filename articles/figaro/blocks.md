---
uid: blocks.md
---

# Blocks

Simply put, a thread of control is blocked when it attempts to obtain a lock, but that attempt is denied because some other thread of control holds a conflicting lock. Once blocked, the thread of control is temporarily unable to make any forward progress until the requested lock is obtained or the operation requesting the lock is abandoned.


Be aware that when we talk about blocking, strictly speaking the thread is not what is attempting to obtain the lock. Rather, some object within the thread (such as a cursor) is attempting to obtain the lock. However, once a locker attempts to obtain a lock, the entire thread of control must pause until the lock request is in some way resolved.


For example, if `Txn A` holds a write lock (an exclusive lock) on object 002, then if `Txn B` tries to obtain a read or write lock on that object, the thread of control in which `Txn B` is running is blocked:

**A write block**
![a write block](/images/writeblock.jpg)

However, if `Txn A` only holds a read lock (a shared lock) on object 002, then only those handles that attempt to obtain a write lock on that object will block.

**A read block**
[A read block](/images/readblock.jpg)

## Blocking and Application Performance

Multi-threaded and multi-process applications typically perform better than simple single-threaded applications because the application can perform one part of its workload (updating an XML document, for example) while it is waiting for some other lengthy operation to complete (performing disk or network I/O, for example). This performance improvement is particularly noticeable if you use hardware that offers multiple CPUs, because the threads and processes can run simultaneously.


That said, concurrent applications can see reduced workload throughput if their threads of control are seeing a large amount of lock contention. That is, if threads are blocking on lock requests, then that represents a performance penalty for your application.


Consider once again the previous diagram of a blocked write lock request. In that diagram, `Txn C` cannot obtain its requested write lock because `Txn A` and `Txn B` are both already holding read locks on the requested object. In this case, the thread in which `Txn C` is running will pause until such a time as `Txn C` either obtains its write lock, or the operation that is requesting the lock is abandoned. The fact that `Txn C`'s thread has temporarily halted all forward progress represents a performance penalty for your application.


Moreover, any read locks that are requested while `Txn C` is waiting for its write lock will also block until such a time as `Txn C` has obtained and subsequently released its write lock.



## Avoiding Blocks

Reducing lock contention is an important part of performance tuning your concurrent Figaro application. Applications that have multiple threads of control obtaining exclusive (write) locks are prone to contention issues. Moreover, as you increase the numbers of lockers and as you increase the time that a lock is held, you increase the chances of your application seeing lock contention.


As you are designing your application, try to do the following in order to reduce lock contention:

**Reduce the length of time your application holds locks.** Shorter lived transactions will result in shorter lock lifetimes, which will in turn help to reduce lock contention. In addition, by default transactional cursors hold read locks until such a time as the transaction is completed. For this reason, try to minimize the time you keep transactional cursors opened, or reduce your isolation levels – see below. If possible, access heavily accessed (read or write) items toward the end of the transaction. This reduces the amount of time that a heavily used page is locked by the transaction.

**Reduce your application's isolation guarantees.** By reducing your isolation guarantees, you reduce the situations in which a lock can block another lock. Try using uncommitted reads for your read operations in order to prevent a read lock being blocked by a write lock. In addition, for cursors you can use degree 2 (read committed) isolation, which causes the cursor to release its read locks as soon as it is done reading the record (as opposed to holding its read locks until the transaction ends). Be aware that reducing your isolation guarantees can have adverse consequences for your application. Before deciding to reduce your isolation, take care to examine your application's isolation requirements. For information on isolation levels, see [Isolation](xref:isolation.md).

**Use snapshot isolation for read-only threads.** Snapshot isolation causes the transaction to make a copy of the page on which it is holding a lock. When a reader makes a copy of a page, write locks can still be obtained for the original page. This eliminates entirely read-write contention. Snapshot isolation is described in @snapshot-isolation.md .

**Consider your data access patterns.**Depending on the nature of your application, this may be something that you can not do anything about. However, if it is possible to create your threads such that they operate only on non-overlapping portions of your database, then you can reduce lock contention because your threads will rarely (if ever) block on one another's locks.

>[!NOTE]
It is possible to configure Figaro's transactions so that they never wait on blocked lock requests. Instead, if they are blocked on a lock request, they will notify the application of a deadlock (see the next section).


You configure this behavior on a transaction by transaction basis. See [No Wait on Transaction Blocks](xref:no-wait-on-transaction-blocks.md) for more information.
