---
uid: anatomy-of-a-transactional-application.md
---

# Anatomy of a Transactional Application

Transactional applications are characterized by performing the following activities:

Create your environment handle.
Open your environment, specifying that the following subsystems be used:

* Transactional Subsystem (this also initializes the logging subsystem).
* Memory pool (the in-memory cache).
* Logging subsystem.
* Locking subsystem (if your application is multi-process or multi-threaded).

It is also highly recommended that you run normal recovery upon first environment open. Normal recovery examines only those logs required to ensure your container files are consistent relative to the information found in your log files.

Open your manager, passing to it your opened environment.

Optionally spawn off any utility threads that you might need. Utility threads can be used to run checkpoints periodically, or to periodically run a deadlock detector if you do not want to use Figaro's built-in deadlock detector.

Open whatever container handles that you need.

Spawn off worker threads. How many of these you need and how they split their Figaro workload is entirely up to your application's requirements. However, any worker threads that perform write operations against your containers will do the following:

* Begin a transaction.
* Perform one or more read and write operations against your containers.
* Commit the transaction if all goes well.
* Abort and retry the operation if a deadlock is detected.
* Abort the transaction for most other errors.

>[!NOTE]
>If you have read-only threads that are operating concurrently with read-write or write-only threads, then you should also transaction-protect the read operations in these threads.

On application shutdown:

* Make sure there are no active transactions. Either abort or commit all transactions before shutting down.
* Close your environment if you did not allow your manager to adopt it.

>[!NOTE]
>Robust applications should monitor their container worker threads to make sure they have not died unexpectedly. If a thread does terminate abnormally, you must shutdown all your worker threads and then run normal recovery (you will have to reopen your environment to do this). This is the only way to clear any resources (such as a lock or a mutex) that the abnormally exiting worker thread might have been holding at the time that it died.


Failure to perform this recovery can cause your still-functioning worker threads to eventually block forever while waiting for a lock that will never be released.

In addition to these activities, which are all entirely handled by code within your application, there are some administrative activities that you should perform:

* Periodically checkpoint your application. Checkpoints will reduce the time to run recovery in the event that one is required. See [Checkpoints](xref:checkpoints.md) for details.
* Periodically back up your container and log files. This is required in order to fully obtain the durability guarantee made by Figaro's transaction ACID support. See [Backup Procedures](xref:backup-procedures.md) for more information.

You may want to maintain a hot failover if 24x7 processing with rapid restart in the face of a disk hit is important to you. See [Using Hot Failovers](xref:using-hot-failovers.md) for more information.

## See Also
* [Backup Procedures](xref:backup-procedures.md)
* [Checkpoints](xref:checkpoints.md)
* [Using Hot Failovers](xref:using-hot-failovers.md)
