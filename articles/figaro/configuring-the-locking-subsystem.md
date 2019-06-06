---
uid: configuring-the-locking-subsystem.md
---

# Configuring the Locking Subsystem

You initialize the locking subsystem by specifying @Figaro.EnvOpenOptions.InitLock to the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method.


Before opening your environment, you can configure various maximum values for your locking subsystem. Note that these limits can only be configured before the environment is opened. Also, these methods configure the entire environment, not just a specific environment handle.


The limits that you can configure are as follows:

* The maximum number of lockers supported by the environment. This value is used by the environment when it is opened to estimate the amount of space that it should allocate for various internal data structures. By default, 1,000 lockers are supported. To configure this value, use the @Figaro.FigaroEnv.MaxLockers property.
* The maximum number of locks supported by the environment. By default, 1,000 locks are supported. To configure this value, use the @Figaro.FigaroEnv.MaxLocks property.
* The maximum number of locked objects supported by the environment. By default, 1,000 objects can be locked.To configure this value, use the @Figaro.FigaroEnv.MaxLockedObjects property.

For a definition of lockers, locks, and locked objects, see [Lock Resources](xref:locks.md#lock-resources).

## See Also
* [The Locking Subsystem](xref:the-locking-subsystem.md)