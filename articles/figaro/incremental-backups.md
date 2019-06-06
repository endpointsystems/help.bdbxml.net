---
uid: incremental-backups.md
---

# Incremental Backups

Once you have created a full backup (that is, either a offline or hot backup), you can create incremental backups. To do this, simply copy all of your currently existing log files to your backup location.


Incremental backups do not require you to run a checkpoint or to cease container write operations.


When you are working with incremental backups, remember that the greater the number of log files contained in your backup, the longer recovery will take. You should run full backups on some interval, and then do incremental backups on a shorter interval. How frequently you need to run a full backup is determined by the rate at which your containers change and how sensitive your application is to lengthy recoveries (should one be required).


You can also shorten recovery time by running recovery against the backup as you take each incremental backup. Running recovery as you go means that there will be less work for Figaro to do if you should ever need to restore your environment from the backup.



## See Also


#### Other Resources
[Backup Procedures](xref:backup-procedures.md)