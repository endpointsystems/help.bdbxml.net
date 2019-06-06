---
uid: database-and-log-file-archival.md
---

# Database and Log File Archival

The third component of the administrative infrastructure, archival for catastrophic recovery, concerns the recoverability of the database in the face of catastrophic failure. Recovery after catastrophic failure is intended to minimize data loss when physical hardware has been destroyed -- for example, loss of a disk that contains databases or log files. Although the application may still experience data loss in this case, it is possible to minimize it.

## Database and Log File Archival

First, you may want to periodically create snapshots (that is, backups) of your databases to make it possible to recover from catastrophic failure. These snapshots are either a standard backup, which creates a consistent picture of the databases as of a single instant in time; or an on-line backup (also known as a hot backup), which creates a consistent picture of the databases as of an unspecified instant during the period of time when the snapshot was made. The advantage of a hot backup is that applications may continue to read and write the databases while the snapshot is being taken. The disadvantage of a hot backup is that more information must be archived, and recovery based on a hot backup is to an unspecified time between the start of the backup and when the backup is completed.


Second, after taking a snapshot, you should periodically archive the log files being created in the environment. It is often helpful to think of database archival in terms of full and incremental file system backups. A snapshot is a full backup, whereas the periodic archival of the current log files is an incremental backup. For example, it might be reasonable to take a full snapshot of a database environment weekly or monthly, and archive additional log files daily. Using both the snapshot and the log files, a catastrophic crash at any time can be recovered to the time of the most recent log archival; a time long after the original snapshot.


To create a standard backup of your database that can be used to recover from catastrophic failure, take the following steps:

Commit or abort all ongoing transactions.
Stop writing your databases until the backup has completed. Read-only operations are permitted, but no write operations and no file system operations may be performed (for example, the @Figaro.FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction) and @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) methods may not be called).
Force an environment checkpoint (see [db_checkpoint](xref:db_checkpoint.md) for more information).
Run [db_archive](xref:db_archive.md) -s to identify all the database data files, and copy them to a backup device such as CD-ROM, alternate disk, or tape.


If the database files are stored in a separate directory from the other Berkeley DB files, it may be simpler to archive the directory itself instead of the individual files (see DB_ENV->set_data_dir for additional information).
Run [db_archive](xref:db_archive.md) -l to identify all the log files, and copy the last one (that is, the one with the highest number) to a backup device such as CD-ROM, alternate disk, or tape.

>[!NOTE]
>If any of the database files did not have an open DB handle during the lifetime of the current log files, [db_archive](xref:db_archive.md) will not list them in its output! This is another reason it may be simpler to use a separate database file directory and archive the entire directory instead of archiving only the files listed by [db_archive](xref:db_archive.md).

To create a hot backup of your database that can be used to recover from catastrophic failure, take the following steps:

Archive your databases, as described in the previous step #4. You do not have to halt ongoing transactions or force a checkpoint. As this is a hot backup, and the databases may be modified during the copy, the utility you use to copy the databases must read database pages atomically (as described by [db_hotbackup](xref:db_hotbackup.md)).
_Archive all of the log files._ The order of these two operations is required, and the database files must be archived before the log files. This means that if the database files and log files are in the same directory, you cannot simply archive the directory; you must make sure that the correct order of archival is maintained.


To archive your log files, run the [db_archive](xref:db_archive.md) utility using the `-l` option to identify all the database log files, and copy them to your backup media. If the database log files are stored in a separate directory from the other database files, it may be simpler to archive the directory itself instead of the individual files (see the @Figaro.FigaroEnv.SetLogDirectory(System.String) method for more information).
To minimize the archival space needed for log files when doing a hot backup, run db_archive to identify those log files which are not in use. Log files which are not in use do not need to be included when creating a hot backup, and you can discard them or move them aside for use with previous backups (whichever is appropriate), before beginning the hot backup.


After completing one of these two sets of steps, the database environment can be recovered from catastrophic failure (see [Recovery Procedures](xref:recovery-procedures.md) for more information).


To update either a hot or cold backup so that recovery from catastrophic failure is possible to a new point in time, repeat step #2 under the hot backup instructions and archive all of the log files in the database environment. Each time both the database and log files are copied to backup media, you may discard all previous database snapshots and saved log files. Archiving additional log files does not allow you to discard either previous database snapshots or log files. Generally, updating a backup must be integrated with the application's log file removal procedures.


The time to restore from catastrophic failure is a function of the number of log records that have been written since the snapshot was originally created. Perhaps more importantly, the more separate pieces of backup media you use, the more likely it is that you will have a problem reading from one of them. For these reasons, it is often best to make snapshots on a regular basis.


Obviously, the reliability of your archive media will affect the safety of your data.

>[!WARNING]
>For archival safety, ensure that you have multiple copies of your database backups, verify that your archival media is error-free and readable, and that copies of your backups are stored offsite!

The functionality provided by the [db_archive](xref:db_archive.md) utility is also available directly from the Figaro library.

## See Also

* @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)
* @Figaro.FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction)

#### Other Resources
* [Recovery Procedures](xref:recovery-procedures.md)
* [db_archive](xref:db_archive.md)
* [Environment Administrative Infrastructure](xref:environment-administrative-infrastructure.md)
* [db_hotbackup](xref:db_hotbackup.md)
