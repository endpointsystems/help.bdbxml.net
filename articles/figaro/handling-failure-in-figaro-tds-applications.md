---
uid: handling-failure-in-figaro-tds-applications.md
---

# Handling Failure in Figaro TDS Applications

When building Figaro TDS .NET applications, there are design issues to consider whenever a thread of control with open handles fails for any reason (where a thread of control may be either a true thread or a process).


The first case is handling system failure: if the system fails, the database environment and the databases may be left in a corrupted state. In this case, recovery must be performed on the database environment before any further action is taken, in order to:

* recover the database environment resources
* release any locks or mutexes that may have been held to avoid starvation as the remaining threads of control convoy behind the held locks
* resolve any partially completed operations that may have left a database in an inconsistent or corrupted state

For details on performing recovery, see @recovery-procedures.md.


The second case is handling the failure of a thread of control. There are resources maintained in database environments that may be left locked or corrupted if a thread of control exits unexpectedly. These resources include data structure mutexes, logical database locks and unresolved transactions (that is, transactions which were never aborted or committed). While Transactional Data Store applications can treat the failure of a thread of control in the same way as they do a system failure, they have an alternative choice, the @Figaro.FigaroEnv.FailCheck method.


The @Figaro.FigaroEnv.FailCheck will return `true` if the database environment is unusable as a result of the thread of control failure (if a data structure mutex or a database write lock is left held by thread of control failure, the application should not continue to use the database environment, as subsequent use of the environment is likely to result in threads of control convoying behind the held locks). The @Figaro.FigaroEnv.FailCheck call will release any database read locks that have been left held by the exit of a thread of control, and abort any unresolved transactions. In this case, the application can continue to use the database environment.


A TDS application recovering from a thread of control failure should call @Figaro.FigaroEnv.FailCheck and, if it returns `false`, the application can continue. If it returns `true`, the application should proceed as described for the case of system failure.


It greatly simplifies matters that recovery may be performed regardless of whether recovery needs to be performed; that is, it is not an error to recover a database environment for which recovery is not strictly necessary. For this reason, applications should not try to determine if the database environment was active when the application or system failed. Instead, applications should run recovery any time the @Figaro.FigaroEnv.FailCheck method returns `true`, or, if the application is not calling the @Figaro.FigaroEnv.FailCheck method, any time any thread of control accessing the database environment fails, as well as any time the system reboots.


