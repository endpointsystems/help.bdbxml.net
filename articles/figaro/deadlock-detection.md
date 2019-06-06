---
uid: deadlock-detection.md
---

# Deadlock Detection

The first component of the infrastructure, deadlock detection, is not so much a requirement specific to transaction-protected applications, but instead is necessary for almost all applications in which more than a single thread of control will be accessing the database at one time. Even when Figaro automatically handles database locking, it is normally possible for deadlock to occur. Because the underlying database access methods may update multiple pages during a single Figaro API call, deadlock is possible even when threads of control are making only single update calls into the database. The exception to this rule is when all the threads of control accessing the database are read-only or when the Figaro Concurrent Data Store product is used; Figaro CDS guarantees deadlock-free operation at the expense of reduced concurrency.


When the deadlock occurs, two (or more) threads of control each request additional locks that can never be granted because one of the threads of control waiting holds the requested resource. For example, consider two processes: A and B. Let's say that A obtains a write lock on item X, and B obtains a write lock on item Y. Then, A requests a lock on Y, and B requests a lock on X. A will wait until resource Y becomes available and B will wait until resource X becomes available. Unfortunately, because both A and B are waiting, neither will release the locks they hold and neither will ever obtain the resource on which it is waiting. For another example, consider two transactions, A and B, each of which may want to modify item X. Assume that transaction A obtains a read lock on X and confirms that a modification is needed. Then it is descheduled and the thread containing transaction B runs. At that time, transaction B obtains a read lock on X and confirms that it also wants to make a modification. Both transactions A and B will block when they attempt to upgrade their read locks to write locks because the other already holds a read lock. This is a deadlock. Transaction A cannot make forward progress until Transaction B releases its read lock on X, but Transaction B cannot make forward progress until Transaction A releases its read lock on X.


In order to detect that deadlock has happened, a separate process or thread must review the locks currently held in the database. If deadlock has occurred, a victim must be selected, and that victim will then return the 
@Figaro.DBDeadlockException from whatever call it was making. Figaro provides a separate utility that can be used to perform this deadlock detection, named [db_deadlock](xref:db_deadlock.md). Alternatively, applications can create their own deadlock utility or thread using the underlying @Figaro.FigaroEnv.LockDetect(Figaro.DeadlockDetectType) function, or specify that Figaro run the deadlock detector internally whenever there is a conflict over a lock (see @Figaro.FigaroEnv.DeadlockDetectPolicy for more information).


Deciding how often to run the deadlock detector and which of the deadlocked transactions will be forced to abort when the deadlock is detected is a common tuning parameter for Figaro .NET applications.



## See Also
* @Figaro.FigaroEnv.LockDetect(Figaro.DeadlockDetectType)
* @Figaro.FigaroEnv.DeadlockDetectPolicy
* @Figaro.DBDeadlockException

#### Other Resources
* [db_deadlock](xref:db_deadlock.md)
* [Environment Administrative Infrastructure](xref:environment-administrative-infrastructure.md)
