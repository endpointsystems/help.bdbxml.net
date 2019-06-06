---
uid: application-requirements.md
---

# Application Requirements

In order to use transactions, your application has certain requirements beyond what is required of non-transactional protected applications. They are:

## Environments

Environments are optional for non-transactional applications, but they are required for transactional applications.

Environments are always used by Figaro, but for transactional applications you must instantiate and manage your @Figaro.FigaroEnv object independently of your @Figaro.XmlManager.


Environment usage is described in detail in [Transaction Basics](xref:transaction-basics.md).



## Transaction Subsystem

In order to use transactions, you must explicitly enable the transactional subsystem for your application, and this must be done at the time that your environment is first created.



## Logging Subsystem

The logging subsystem is required for recovery purposes, but its usage also means your application may require a little more administrative effort than it does when logging is not in use. See [Managing Database Files](xref:managing-database-files.md) for more information.



## XmlTransaction Handles

In order to obtain the atomicity guarantee offered by the transactional subsystem (that is, combine multiple operations in a single unit of work), your application must use 
@Figaro.XmlTransaction handles. These handles are obtained from your @Figaro.XmlManager objects. They should normally be short-lived, and their usage is reasonably simple. To complete a transaction and save the work it performed, you call its 
@Figaro.XmlTransaction.Commit method. To complete a transaction and discard its work, you call its 
@Figaro.XmlTransaction.Abort method.



## Container Open Requirements

In addition to using environments and initializing the correct subsystems, your application must transaction protect the container opens if subsequent operations on the containers are to be transaction protected. The container open and secondary index association are commonly transaction protected using auto commit.



## Deadlock Detection

Typically transactional applications use multiple threads of control when accessing the database. Any time multiple threads are used on a single resource, the potential for lock contention arises. In turn, lock contention can lead to deadlocks. See [Locks, Blocks and Deadlocks](xref:locks-blocks-and-deadlocks.md) for more information.


Therefore, transactional applications must frequently include code for detecting and responding to deadlocks. Note that this requirement is not specific to transactions – you can certainly write concurrent non-transactional Figaro applications. Furthermore, not every transactional application uses concurrency and so not every transactional application must manage deadlocks. Still, deadlock management is frequently a characteristic of transactional applications. See 
[Concurrency](xref:concurrency.md) for more information.



## See Also

* [Introduction](xref:introduction.md)
* [Why Transactions?](xref:why-transactions.md)
* [Transaction Basics](xref:transaction-basics.md)
* [Managing Database Files](xref:managing-database-files.md)
* [Locks, Blocks and Deadlocks](xref:locks-blocks-and-deadlocks.md)
* [Concurrency](xref:concurrency.md)
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)
