---
uid: configuring-locking-sizing-the-system.md
---

# Configuring Locking: Sizing the System

The lock system is sized using the following three methods:
* @Figaro.FigaroEnv.MaxLocks
* @Figaro.FigaroEnv.MaxLockers
* @Figaro.FigaroEnv.SetMaxLockedObjects

These three methods specify the maximum number of locks, lockers, and locked objects supported by the lock subsystem, respectively. The maximum number of locks is the number of locks that can be simultaneously requested in the system. The maximum number of lockers is the number of lockers that can simultaneously request locks in the system. The maximum number of lock objects is the number of objects that can simultaneously be locked in the system. Selecting appropriate values requires an understanding of your application and its databases. If the values are too small, requests for locks in an application will fail. If the values are too large, the locking subsystem will consume more resources than is necessary. It is better to err in the direction of allocating too many locks, lockers, and objects because increasing the number of locks does not require large amounts of additional resources. The default values are 1000 of each type of object.


When configuring a Figaro Concurrent Data Store application, the number of lock objects needed is two per open database (one for the database lock, and one for the cursor lock when the @Figaro.EnvConfig.AllConcurrentDatabases option is not specified). The number of locks needed is one per open database handle plus one per simultaneous cursor or non-cursor operation.


Configuring a Figaro Transactional Data Store application is more complicated. The recommended algorithm for selecting the maximum number of locks, lockers, and lock objects is to run the application under stressful conditions and then review the lock system's statistics to determine the maximum number of locks, lockers, and lock objects that were used. Then, double these values for safety. However, in some large applications, finer granularity of control is necessary in order to minimize the size of the lock subsystem.


The maximum number of lockers can be estimated as follows:

* If the database environment is using transactions, the maximum number of lockers can be estimated by adding the number of simultaneously active non-transactional cursors open database handles to the number of simultaneously active transactions and child transactions (where a child transaction is active until it commits or aborts, not until its parent commits or aborts).
* If the database environment is not using transactions, the maximum number of lockers can be estimated by adding the number of simultaneously active non-transactional cursors and open database handles to the number of simultaneous non-cursor operations.

The maximum number of lock objects needed for a single database operation can be estimated as one lock object per level of the database tree, at a minimum. You should then add an additional lock object per database for the database's metadata page. Note that transactions accumulate locks over the transaction lifetime, and the lock objects required by a single transaction is the total lock objects required by all of the database operations in the transaction. However, a database page that is accessed multiple times within a transaction only requires a single lock object for the entire transaction.


The maximum number of locks required by an application cannot be easily estimated. It is possible to calculate a maximum number of locks by multiplying the maximum number of lockers, times the maximum number of lock objects, times two (two for the two possible lock modes for each object, read and write). However, this is a pessimal value, and real applications are unlikely to actually need that many locks. Reviewing the Lock subsystem statistics is the best way to determine this value.


#### Other Resources
[Design and Usage Considerations](xref:design-and-usage-considerations.md)
