---
uid: catastrophic-recovery.md
---

# Catastrophic Recovery

Use catastrophic recovery when you are recovering your containers from a previously created backup. Note that to restore your containers from a previous backup, you should copy the backup to a new environment directory, and then run catastrophic recovery. Failure to do so can lead to the internal database structures being out of sync with your log files.

>[!NOTE]
>Catastrophic recovery must be run single-threaded.

To run catastrophic recovery:

* Shut down all container operations.
* Restore the backup to an empty directory.
* Provide the @Figaro.EnvOpenOptions.RecoverFatal flag when you open your environment. This environment open must be single-threaded.

You can also run recovery by pausing or shutting down your application and using the [db_recover](xref:db_recover.md) command line utility with the `-c` option.

>[!NOTE]
>Catastrophic recovery examines every available log file — not just those log files created since the last checkpoint as is the case for normal recovery. For this reason, catastrophic recovery is likely to take longer than does normal recovery.

## See Also


@Figaro.EnvOpenOptions.RecoverFatal

#### Other Resources
* [db_recover](xref:db_recover.md)
* [Recovery Procedures](xref:recovery-procedures.md)
