---
uid: delayed-synchronization.md
---

# Delayed Synchronization

When a replication group has a new master, all replicas must synchronize with that master. This means they must ensure that the contents of their local database(s) are identical to that contained by the new master.


This synchronization process can result in quite a lot of network activity. It can also put a large strain on the master server, especially if is part of a large replication group or if there is somehow a large difference between the master's database(s) and the contents of its replicas.

It is therefore possible to delay synchronization for any replica that discovers it has a new master. You would do this so as to give the master time to synchronize other replicas before proceeding with the delayed replicas.

To delay synchronization of a replica environment, you set the @Figaro.ReplicationConfig.DelayClientSync property to `True`.

>[!IMPORTANT]
>If you use delayed synchronization, then you must manually synchronize the replica at some future time. Until you do this, the replica is out of sync with the master, and it will ignore all database changes forwarded to it from the master.

You synchronize a delayed replica by calling the @Figaro.ReplicationManager.Sync on the replica that has been delayed.



## See Also

* @Figaro.ReplicationConfig.DelayClientSync
* @Figaro.ReplicationManager.Sync