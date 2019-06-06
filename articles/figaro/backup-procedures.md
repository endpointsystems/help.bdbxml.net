---
uid: backup-procedures.md
---

# Backup Procedures

Durability is an important part of your transactional guarantees. It means that once a transaction has been successfully committed, your application will always see the results of that transaction. Of course, no software algorithm can guarantee durability in the face of physical data loss. Hard drives can fail, and if you have not copied your data to locations other than your primary disk drives, then you will lose data when those drives fail. Therefore, in order to truly obtain a durability guarantee, you need to ensure that any data stored on disk is backed up to secondary or alternative storage, such as secondary disk drives, or offline tapes.



There are three different types of backups that you can perform with Figaro containers and log files. They are:

* **Offline backups.** This type of backup is perhaps the easiest to perform as it involves simply copying database and log files to an offline storage area. It also gives you a snapshot of the database at a fixed, known point in time. However, you cannot perform this type of a backup while you are performing writes to the database.
* **Hot backups.** This type of backup gives you a snapshot of your database. Since your application can be writing to the database at the time that the snapshot is being taken, you do not necessarily know what the exact state of the database is for that given snapshot.
* **Incremental backups.** This type of backup refreshes a previously performed backup.

Once you have performed a backup, you can perform catastrophic recovery to restore your containers from the backup. See [Catastrophic Recovery](xref:catastrophic-recovery.md) for more information.


Note that you can also maintain a hot failover. See [Using Hot Failovers](xref:using-hot-failovers.md) for more information.


## In This Section

* [Offline Backups](xref:offline-backups.md)
* [Hot Backup](xref:hot-backup.md)
* [Incremental Backups](xref:incremental-backups.md)

## See Also


#### Other Resources
* [Using Hot Failovers](xref:using-hot-failovers.md)
* [Catastrophic Recovery](xref:catastrophic-recovery.md)
* [Managing Database Files](xref:managing-database-files.md)
