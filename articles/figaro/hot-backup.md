---
uid: hot-backup.md
---

# Hot Backup

To create a hot backup, you do not have to stop database operations. Transactions may be on-going and you can be writing to your database at the time of the backup. However, this means that you do not know exactly what the state of your database is at the time of the backup.


You can use the @db_hotbackup.md command line utility to create a hot backup for you. This utility will (optionally) run a checkpoint and the copy all necessary files to a target directory.


Alternatively, you can manually create a hot backup as follows:
* Copy all your container files to the backup location.
* Copy all logs to your backup location.

>[!NOTE]
>It is important to copy your container files and then your logs. In this way, you can complete or roll back any container operations that were only partially completed when you copied the databases.
