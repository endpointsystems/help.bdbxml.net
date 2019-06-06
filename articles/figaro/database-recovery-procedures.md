---
uid: database-recovery-procedures.md
---

# Database Recovery Procedures

Figaro supports two types of recovery:

* _Normal recovery_, which is run when your environment is opened upon application startup, examines only those log records needed to bring the containers to a consistent state since the last checkpoint. Normal recovery starts with any logs used by any transactions active at the time of the last checkpoint, and examines all logs from then to the current logs.
* _Catastrophic recovery_, which is performed in the same way that normal recovery is except that it examines all available log files. You use catastrophic recovery to restore your containers from a previously created backup.

Of these two, normal recovery should be considered a routine matter; in fact you should run normal recovery whenever you start up your application.


Catastrophic recovery is run whenever you have lost or corrupted your container files and you want to restore from a backup. You also run catastrophic recovery when you create a hot backup ( see [Using Hot Failovers](xref:using-hot-failovers.md) for more information).



## In This Section
* [Normal Recovery](xref:normal-recovery.md)
* [Catastrophic Recovery](xref:catastrophic-recovery.md)
