---
uid: recovery-procedures.md
---

# Recovery Procedures

The fifth component of the administrative infrastructure, recovery procedures, concerns the recoverability of the database. After any application or system failure, there are two possible approaches to database recovery:
* There is no need for recoverability, and all databases can be re-created from scratch. _ Although these applications may still need transaction protection for other reasons, recovery usually consists of removing the Figaro environment home directory and all files it contains, and then restarting the application. Such an application may use the @Figaro.ContainerConfig.TransactionNotDurable flag to avoid writing log records.
* _It is necessary to recover information after system or application failure._ In this case, recovery processing must be performed on any database environments that were active at the time of the failure. Recovery processing involves running the [db_recover](xref:db_recover.md) utility or calling the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method with the @Figaro.EnvOpenOptions.Recover or @Figaro.EnvOpenOptions.RecoverFatal flags.

During recovery processing, all database changes made by aborted or unfinished transactions are undone, and all database changes made by committed transactions are redone, as necessary. Database applications must not be restarted until recovery completes. After recovery finishes, the environment is properly initialized so that applications may be restarted.


If performing recovery, there are two types of recovery processing: normal and catastrophic. Which you choose depends on the source for the database and log files you are using to recover.


If up-to-the-minute database and log files are accessible on a stable file system, normal recovery is sufficient. Run the [db_recover](xref:db_recover.md) utility or call the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method specifying the @Figaro.EnvOpenOptions.Recover flag. However, the normal recovery case never includes recovery using hot backups of the database environment. For example, you cannot perform a hot backup of databases and log files, restore the backup and then run normal recovery -- you must always run catastrophic recovery when using hot backups.


If the database or log files have been destroyed or corrupted, or normal recovery fails, catastrophic recovery is required. For example, catastrophic failure includes the case where the disk drive on which the database or log files are stored has been physically destroyed, or when the underlying file system is corrupted and the operating system's normal file system checking procedures cannot bring that file system to a consistent state. This is often difficult to detect, and a common sign of the need for catastrophic recovery is when normal recovery procedures fail, or when checksum errors are displayed during normal database procedures. When catastrophic recovery is necessary, take the following steps:
* Restore the most recent snapshots of the database and log files from the backup media into the directory where recovery will be performed.
* If any log files were archived since the last snapshot was made, they should be restored into the directory where recovery will be performed.
* If any log files are available from the database environment that failed (for example, the disk holding the database files crashed, but the disk holding the log files is fine), those log files should be copied into the directory where recovery will be performed. Be sure to restore all log files in the order they were written. The order is important because it's possible the same log file appears on multiple backups, and you want to run recovery using the most recent version of each log file.
* Run the [db_recover](xref:db_recover.md) utility, specifying its `-c` option; or call the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method, specifying the @Figaro.EnvOpenOptions.RecoverFatal flag. The catastrophic recovery process will review the logs and database files to bring the environment databases to a consistent state as of the time of the last uncorrupted log file that is found. It is important to realize that only transactions committed before that date will appear in the databases.


It is possible to re-create the database in a location different from the original by specifying appropriate pathnames to the `-h` option of the [db_recover](xref:db_recover.md) utility. In order for this to work properly, it is important that your application refer to files by names relative to the database home directory or the pathname(s) specified in calls to @Figaro.FigaroEnv.AddDataDirectory(System.String), instead of using full pathnames.

## See Also

@Figaro.FigaroEnv

#### Other Resources
[db_recover](xref:db_recover.md)