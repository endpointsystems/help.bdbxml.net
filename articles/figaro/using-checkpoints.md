---
uid: using-checkpoints.md
---

# Using Checkpoints

The second component of the infrastructure is performing checkpoints of the log files. Performing checkpoints is necessary for two reasons.


First, you may be able to remove Figaro log files from your database environment after a checkpoint. Change records are written into the log files when databases are modified, but the actual changes to the database are not necessarily written to disk. When a checkpoint is performed, changes to the database are written into the backing database file. Once the database pages are written, log files can be archived and removed from the database environment because they will never be needed for anything other than catastrophic failure. (Log files which are involved in active transactions may not be removed, and there must always be at least one log file in the database environment.)


The second reason to perform checkpoints is because checkpoint frequency is inversely proportional to the amount of time it takes to run database recovery after a system or application failure. This is because recovery after failure has to redo or undo changes only since the last checkpoint, as changes before the checkpoint have all been flushed to the databases.


The [db_checkpoint](xref:db_checkpoint.md) utility can be used to perform checkpoints. Alternatively, applications can write their own checkpoint utility using the underlying @Figaro.FigaroEnv.SetEnvironmentTransactionCheckpoint(System.Boolean,System.UInt32,System.UInt32) function.


Because checkpoints can be quite expensive, choosing how often to perform a checkpoint is a common tuning parameter for Figaro .NET applications.



## See Also

* @Figaro.FigaroEnv.SetEnvironmentTransactionCheckpoint(System.Boolean,System.UInt32,System.UInt32)
* [db_checkpoint](xref:db_checkpoint.md)
* [Performance Tuning](xref:performance-tuning.md)
* [Environment Administrative Infrastructure](xref:environment-administrative-infrastructure.md)