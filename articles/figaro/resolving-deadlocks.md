---
uid: resolving-deadlocks.md
---
# Resolving Deadlocks

When Figaro determines that a deadlock has occurred, it will select a thread of control to resolve the deadlock and then throws a @Figaro.DBDeadlockException in that thread. If a deadlock is detected, the thread must:

* Cease all read and write operations.
* Abort the transaction.
* Optionally retry the operation. If your application retries deadlocked operations, the new attempt must be made using a new transaction.

>[!NOTE]
>If a thread has deadlocked, it may not make any additional container calls using the handle that has deadlocked.

## See Also


#### Other Resources
* @Figaro.DBDeadlockException
* [The Locking Subsystem](xref:the-locking-subsystem.md)