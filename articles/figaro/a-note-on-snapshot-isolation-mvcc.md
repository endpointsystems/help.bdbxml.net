---
uid: a-note-on-snapshot-isolation-mvcc.md
---

# A Note on Snapshot Isolation (MVCC)

Snapshot isolation, or multi-version concurrency control, can be configured when you are using transactions with your Figaro application. While transactions are not described in this manual, since snapshot isolation is so commonly used for Figaro applications, it is worth mentioning here that the use of snapshot isolation means you must:

Increase the maximum number of concurrent transactions supported by the environment.
Increase the size of your disk cache.
This is because snapshot isolation causes your Figaro transactional application to use much larger amounts of resources than does a normal transactional application.


For more information, see the [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md) guide.

## See Also

* @Figaro.EnvConfig.SnapshotTransaction (@Figaro.EnvConfig)
* @Figaro.TransactionType.SnapshotTransaction (@Figaro.TransactionType)

#### Other Resources
* [Administering Apps](xref:administering-apps.md)
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)