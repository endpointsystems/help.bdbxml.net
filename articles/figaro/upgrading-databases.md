---
uid: upgrading-databases.md
---

# Upgrading Databases


When upgrading databases to support a newer version of the libraries, it may be necessary to upgrade the on-disk format of already-created database files. Berkeley DB database upgrades are done in place, and so are potentially destructive. This means that if the system crashes during the upgrade procedure, or if the upgrade procedure runs out of disk space, the databases may be left in an inconsistent and unrecoverable state. To guard against failure, the procedures outlined in this guide should be carefully followed.
>[!WARNING]
>If you are not performing catastrophic archival as part of your application upgrade process, you should at least copy your database to an archival location, verify that your archival location is error-free and readable, and that copies of your backups are stored offsite!

Database upgrades can be performed at the command-line level using [db_upgrade](xref:db_upgrade.md) or through the API using @Figaro.XmlManager.UpgradeContainer(System.String,Figaro.UpdateContext).


After an upgrade, Berkeley DB applications must be recompiled to use the new Berkeley DB library before they can access an upgraded database. There is no guarantee that applications compiled against previous releases of Berkeley DB will work correctly with an upgraded database format, nor is there any guarantee that applications compiled against newer releases of Berkeley DB will work correctly with the previous database format. It is guaranteed that any archived database may be upgraded using a current Berkeley DB software release and the @Figaro.XmlManager.UpgradeContainer(System.String,Figaro.UpdateContext) method, and there is no need to step-wise upgrade the database using intermediate releases of Berkeley DB. Sites should consider archiving appropriate copies of their application or application sources if they may need to access archived databases without first upgrading them.


The following information describes the general process of upgrading applications using the Figaro XML database library. There are four areas to be considered when upgrading Berkeley DB applications and database environments:

* The application API
* The database environment's region files
* The underlying database formats
* Log files (for transactional environments)

The upgrade procedures required depend on whether or not the release is a major or minor release (in which either the major or minor number of the version changed), or a patch release (in which only the patch number in the version changed). Figaro BDB major and minor releases may optionally include changes in all four areas; that is, the application API, region files, database formats, and log files may not be backward-compatible with previous releases. A Berkeley DB database formats in incompatible ways, and so applications may only need to be recompiled or reconfigured to point to the new assembly version.


### Upgrading a Patch Release

* Shut down all applications using the Figaro library.
* Install the library update.
* Restart the applications.

### Upgrading non-transactional environments

* Shut down all applications using the Figaro library.
* Remove any old environment using the @Figaro.FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction) method or an appropriate system utility.
* Install the new version of the Figaro library.
* If necessary, upgrade the application databases.
* Restart the applications.

### Upgrading transactional environments without upgrading log or database files

* Shut down all applications using the Figaro library.
* Run recovery on the database environment using the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method (using the @Figaro.EnvOpenOptions.Recover option), or the [db_recover](xref:db_recover.md) utility.
* Remove any old environment using the @Figaro.FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction) method or an appropriate system utility.
* Install the new version of the Figaro library.
* If necessary, upgrade the application databases.
* Restart the applications.

### Upgrading transactional environments and log files without upgrading database files

* Shut down all applications using the Figaro library.
* Run recovery on the database environment using the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method (using the @Figaro.EnvOpenOptions.Recover option), or the [db_recover](xref:db_recover.md) utility.
* Archive the database environment for catastrophic recovery.
* Install the new version of the Figaro library.
* Force a checkpoint using the @Figaro.FigaroEnv.SetEnvironmentTransactionCheckpoint(System.Boolean,System.UInt32,System.UInt32) or the [db_checkpoint](xref:db_checkpoint.md) utility. If you use the [db_checkpoint](xref:db_checkpoint.md) utility, make sure to use the new version of the utility - that is, the version that came with the installation you are upgrading with.
* Restart the applications.

### Upgrading transactional environments, log files and databases

* Shut down all applications using the Figaro library.
* Run recovery on the database environment using the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method (using the @Figaro.EnvOpenOptions.Recover option), or the [db_recover](xref:db_recover.md) utility.
* Remove any old environment using the @Figaro.FigaroEnv.Remove(System.String,Figaro.EnvironmentRemoveAction) method or an appropriate system utility.
* Archive the database environment for catastrophic recovery.
* Install the new version of the Figaro library.
* Upgrade the databases.
* Archive the database for catastrophic recovery again (using different media than before, of course). 
>[!NOTE]
>This archival is not strictly necessary. However, if you have to perform catastrophic recovery after restarting the application, that recovery must be done based on the last archive you have made. If you make this second archive, you can use it as the basis of that catastrophic recovery. If you do not make this second archive, you have to use the archive you made in step 4 as the basis of your recovery, and you have to do a full upgrade on it before you can apply log files created after the upgrade to it.
* Force a checkpoint using the @Figaro.FigaroEnv.SetEnvironmentTransactionCheckpoint(System.Boolean,System.UInt32,System.UInt32) or the [db_checkpoint](xref:db_checkpoint.md) utility. If you use the [db_checkpoint](xref:db_checkpoint.md) utility, make sure to use the new version of the utility - that is, the version that came with the installation you are upgrading with.
* Restart the applications.

## See Also
* [Utilities](xref:utilities.md)
* [Backup Procedures](xref:backup-procedures.md)
* [Recovery Procedures](xref:recovery-procedures.md)