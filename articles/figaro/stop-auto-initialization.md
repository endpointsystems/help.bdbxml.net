---
uid: stop-auto-initialization.md
---

# Stop Auto-Initialization

when a replication replica is synchronizing with its master, it will block DB operations at some point(s) during this process until the synchronization is completed. You can turn off this behavior (see [Managing Blocking Operations](xref:managing-blocking-operations.md)), but for replicas that have been out of touch from their master for a very long time, this may not be enough.


If a replica has been out of touch from its master long enough, it may find that it is not possible to perform synchronization. When this happens, by default the master and replica internally decide to completely re-initialize the replica. This re-initialization involves discarding the replica's current database(s) and transferring new ones to it from the master. Depending on the size of the master's databases, this can take a long time, during which time the replica will be completely non-responsive when it comes to performing database operations.


It is possible that there is a time of the day when it is better to perform a replica re-initialization. Or, you simply might want to decide to bring the replica up to speed by restoring its databases using a hot-backup taken from the master. Either way, you can decide to prevent automatic-initialization of your replica. To do this, set the @Figaro.ReplicationConfig.AutoInit property to `False`.



## See Also

* @Figaro.ReplicationConfig.AutoInit

#### Other Resources
* [Managing Blocking Operations](xref:managing-blocking-operations.md)