---
uid: normal-recovery.md
---

# Normal Recovery

Normal recovery examines the contents of your environment's log files, and uses this information to ensure that your container files are consistent relative to the information contained in the log files.

Normal recovery also recreates your environment's region files. This has the desired effect of clearing any unreleased locks that your application may have held at the time of an unclean application shutdown.


Normal recovery is run only against those log files created since the time of your last checkpoint. For this reason, your recovery time is dependent on how much data has been written since the last checkpoint, and therefore on how much log file information there is to examine. If you run checkpoints infrequently, then normal recovery can take a relatively long time.
>[!NOTE]
>You should run normal recovery every time you perform application startup.&nbsp;

To run normal recovery:

* Make sure all your environment handles are closed.
* Normal recovery must be single-threaded.
* Provide the @Figaro.EnvOpenOptions.Recover flag when you open your environment.

You can also run recovery by pausing or shutting down your application and using the [db_recover](xref:db_recover.md) command line utility.

## See Also

* @Figaro.EnvOpenOptions.Recover

#### Other Resources
* [db_recover](xref:db_recover.md)
* [Backup Procedures](xref:backup-procedures.md)
* [Recovery Procedures](xref:recovery-procedures.md)