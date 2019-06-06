---
uid: removing-log-files.md
---

# Removing Log Files

By default Figaro does not delete log files for you. For this reason, log files will eventually grow to consume an unnecessarily large amount of disk space. To guard against this, you should periodically take administrative action to remove log files that are no longer in use by your application.


You can remove a log file if all of the following are true:
* The log file is not involved in an active transaction.
* A checkpoint has been performed after the log file was created.
* The log file is not the only log file in the environment.
* The log file that you want to remove has already been included in an offline or hot backup. Failure to observe this last condition can cause your backups to be unusable.

Figaro provides several mechanisms to remove log files that meet all but the last criteria (Figaro has no way to know which log files have already been included in a backup). The following mechanisms make it easy to remove unneeded log files, but can result in an unusable backup if the log files are not first saved to your archive location. All of the following mechanisms automatically delete unneeded log files for you:

* Run the [db_archive](xref:db_archive.md) command line utility with the `-d` option.
* From within your application, call the @Figaro.FigaroEnv.LogArchive(Figaro.LogArchiveOptions) method with the @Figaro.LogArchiveOptions.Remove flag.
* Call the @Figaro.FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,System.Boolean) method with the @Figaro.EnvConfig.AutoLogRemove flag. Note that this flag can be set at any point in the lifetime of your application. Setting this parameter affects all environment handles opened against the environment; not just the handle used to set the flag.

Note that unlike the other log removal mechanisms identified here, this method actually causes log files to be removed on an on-going basis as they become unnecessary. This is extremely desirable behavior if what you want is to use the absolute minimum amount of disk space possible for your application. This mechanism will leave you with the log files that are required to run normal recovery. However, it is highly likely that this mechanism will prevent you from running catastrophic recovery.

>[!CAUTION]
>Do NOT use this mechanism if you want to be able to perform catastrophic recovery, or if you want to be able to maintain a hot backup.

In order to safely remove log files and still be able to perform catastrophic recovery, use the [db_archive](xref:db_archive.md) command line utility as follows:

* Run either a normal or hot backup as described in [Backup Procedures](xref:backup-procedures.md). Make sure that all of this data is safely stored to your backup media before continuing.
* If you have not already done so, perform a checkpoint. See [Checkpoints](xref:checkpoints.md) for more information.
* If you are maintaining a hot backup, perform the hot backup procedure as described in [Using Hot Failovers](xref:using-hot-failovers.md).
* Run the [db_archive](xref:db_archive.md) command line utility with the `-d` option against your production environment.
* Run the [db_archive](xref:db_archive.md) command line utility with the `-d` option against your failover environment, if you are maintaining one.

## See Also

* [db_archive](xref:db_archive.md)
* [Using Hot Failovers](xref:using-hot-failovers.md)
* [Checkpoints](xref:checkpoints.md)
* [Recovery Procedures](xref:recovery-procedures.md)
* [Managing Database Files](xref:managing-database-files.md)
