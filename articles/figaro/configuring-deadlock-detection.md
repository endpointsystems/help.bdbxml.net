---
uid: configuring-deadlock-detection.md
---

# Configuring Deadlock Detection

In order for Figaro to know that a deadlock has occurred, some mechanism must be used to perform deadlock detection. There are three ways that deadlock detection can occur:

Allow Figaro to internally detect deadlocks as they occur.


To do this, you use the @Figaro.FigaroEnv.DeadlockDetectPolicy property. This setting causes Figaro to walk its internal lock table looking for a deadlock whenever a lock request is blocked. This method also identifies how Figaro decides which lock requests are rejected when deadlocks are detected. For example, Figaro can decide to reject the lock request for the transaction that has the most number of locks, the least number of locks, holds the oldest lock, holds the most number of write locks, and so forth (see the API reference documentation for a complete list of the lock detection policies).


You can call this method at any time during your application's lifetime, but typically it is used before you open your environment.


Note that how you want Figaro to decide which thread of control should break a deadlock is extremely dependent on the nature of your application. It is not unusual for some performance testing to be required in order to make this determination. That said, a transaction that is holding the maximum number of locks is usually indicative of the transaction that has performed the most amount of work. Frequently you will not want a transaction that has performed a lot of work to abandon its efforts and start all over again. It is not therefore uncommon for application developers to initially select the transaction with the _minimum_ number of write locks to break the deadlock.


Using this mechanism for deadlock detection means that your application will never have to wait on a lock before discovering that a deadlock has occurred. However, walking the lock table every time a lock request is blocked can be expensive from a performance perspective.

*Use a dedicated thread or external process to perform deadlock detection*. Note that this thread must be performing no other container operations beyond deadlock detection.


To externally perform lock detection, you can use either the @Figaro.FigaroEnv.DeadlockDetectPolicy property, or use the `db_deadlock` command line utility. This method (or command) causes Figaro to walk the lock table looking for deadlocks.

Note that like @Figaro.FigaroEnv.DeadlockDetectPolicy, you also use this method (or command line utility) to identify which lock requests are rejected in the event that a deadlock is detected.


Applications that perform deadlock detection in this way typically run deadlock detection between every few seconds and a minute. This means that your application may have to wait to be notified of a deadlock, but you also save the overhead of walking the lock table every time a lock request is blocked.
Lock timeouts.


You can configure your locking subsystem such that it times out any lock that is not released within a specified amount of time. To do this, use the @Figaro.FigaroEnv.SetTimeout(System.UInt32,Figaro.EnvironmentTimeoutType) method. Note that lock timeouts are only checked when a lock request is blocked or when deadlock detection is otherwise performed. Therefore, a lock can have timed out and still be held for some length of time until Figaro has a reason to examine its locking tables.


Be aware that extremely long-lived transactions, or operations that hold locks for a long time, may be inappropriately timed out before the transaction or operation has a chance to complete. You should therefore use this mechanism only if you know your application will hold locks for very short periods of time.

## See Also

* @Figaro.FigaroEnv.DeadlockDetectPolicy
* @Figaro.FigaroEnv.SetTimeout(System.UInt32,Figaro.EnvironmentTimeoutType)

#### Other Resources
* [The Locking Subsystem](xref:the-locking-subsystem.md)