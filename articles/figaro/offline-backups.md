---
uid: offline-backups.md
---

# Offline Backups

To create an offline backup:

* Commit or abort all on-going transactions.
* Pause all database writes.
* Force a checkpoint. See [Checkpoints](xref:checkpoints.md) for details.
* Copy all your container files to the backup location. However, be aware that backing up just the modified databases only works if you have all of your log files. If you have been removing log files for any reason then using can result in an unrecoverable backup because you might not be notified of a database file that was modified.
* Copy the last log file to your backup location. Your log files are named `_log.xxxxxxxxxx_`, where `_xxxxxxxxxx_` is a sequential number. The last log file is the file with the highest number.

## See Also

* [Checkpoints](xref:checkpoints.md)
* [Backup Procedures](xref:backup-procedures.md)
