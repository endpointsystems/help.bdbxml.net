---
uid: using-hot-failovers.md
---

# Using Hot Failovers

You can maintain a backup that can be used for failover purposes. Hot failovers differ from the backup and restore procedures described previously in this section in that data used for traditional backups is typically copied to offline storage. Recovery time for a traditional backup is determined by:

* How quickly you can retrieve that storage media. Typically storage media for critical backups is moved to a safe facility in a remote location, so this step can take a relatively long time.
* How fast you can read the backup from the storage media to a local disk drive. If you have very large backups, or if your storage media is very slow, this can be a lengthy process.
* How long it takes you to run catastrophic recovery against the newly restored backup. As described earlier in this chapter, this process can be lengthy because every log file must be examined during the recovery process.
* When you use a hot failover, the backup is maintained at a location that is reasonably fast to access. Usually, this is a second disk drive local to the machine. In this situation, recovery time is very quick because you only have to reopen your environment and database, using the failover environment for the environment open.


Hot failovers obviously do not protect you from truly catastrophic disasters (such as a fire in your machine room) because the backup is still local to the machine. However, you can guard against more mundane problems (such as a broken disk drive) by keeping the backup on a second drive that is managed by an alternate disk controller.


To maintain a hot failover:

* Copy all the active database files to the failover directory. Use the `db_archive` command line utility with the `-s` option to identify all the active database files.
* Identify all the inactive log files in your production environment and move these to the failover directory. Use the `db_archive` command with no command line options to obtain a list of these log files.
* Identify the active log files in your production environment, and copy these to the failover directory. Use the `db_archive` command with the `-l` option to obtain a list of these log files.
* Run catastrophic recovery against the failover directory. Use the `db_recover` command with the `-c` option to do this.
* Optionally copy the backup to an archival location.

Once you have performed this procedure, you can maintain an active hot backup by repeating steps 2 - 5 as often as is required by your application.

>[!NOTE]
>If you perform step 1, you must follow steps 2-5 in order to ensure consistency of your hot backup.


Rather than use the previous procedure, you can use the `db_hotbackup` command line utility to do the same thing. This utility will (optionally) run a checkpoint and then copy all necessary files to a target directory for you.

To actually perform a failover, simply:

* Shut down all processes which are running against the original environment.
*If you have an archival copy of the backup environment, you can optionally try copying the remaining log files from the original environment and running catastrophic recovery against that backup environment. Do this only if you have a an archival copy of the backup environment.


This step can allow you to recover data created or modified in the original environment, but which did not have a chance to be reflected in the hot backup environment.

Reopen your environment and containers as normal, but use the backup environment instead of the production environment.

#### See Also
* [db_archive](xref:db_archive.md)
* [db_hotbackup](xref:db_hotbackup.md)
