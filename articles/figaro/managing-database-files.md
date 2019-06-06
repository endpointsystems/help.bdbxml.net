---
uid: managing-database-files.md
---

# Managing Database Files

Figaro is capable of storing several types of files on disk:
* **Data files**, which contain the actual data in your container.
* **Log files**, which contain information required to recover your database in the event of a system or application failure.
* **Region files**, which contain information necessary for the overall operation of your application.

Of these, you must manage your data and log files by ensuring that they are backed up. You should also pay attention to the amount of disk space your log files are consuming, and periodically remove any unneeded files. Finally, you can optionally tune your logging subsystem to best suit your application's needs and requirements. These topics are discussed in this section.



## In This Section
* [Checkpoints](xref:checkpoints.md)
* [Backup Procedures](xref:backup-procedures.md)
* [Recovery Procedures](xref:recovery-procedures.md)
* [Designing Your Application for Recovery](xref:designing-your-application-for-recovery.md)
* [Using Hot Failovers](xref:using-hot-failovers.md)
* [Removing Log Files](xref:removing-log-files.md)
* [Configuring the Logging Subsystems](xref:configuring-the-logging-subsystem.md)



## See Also


#### Other Resources
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)