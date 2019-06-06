---
uid: db_hotbackup.md
---

# db_hotbackup

The `db_hotbackup` utility creates "hot backup" or "hot failover" snapshots of database environments.

```
db_hotbackup [-cDuVv] [-d data_dir ...] [-h home] [-l log_dir] [-P password] -b backup_dir
```

The db_hotbackup utility performs the following steps:

* If the `-c` option is specified, checkpoint the source home database environment, and remove any unnecessary log files.
* If the target directory for the backup does not exist, it is created with mode read-write-execute for the owner.
* If the target directory for the backup does exist and the -u option was specified, all log files in the target directory are removed; if the `-u` option was not specified, all files in the target directory are removed.

* If the `-u` option was not specified, copy application-specific files found in the database environment home directory, and any directories specified using the -d option, into the target directory for the backup.
* Copy all log files found in the directory specified by the -l option (or in the database environment home directory, if no `-l` option was specified), into the target directory for the backup.
* Perform catastrophic recovery in the target directory for the backup.
* Remove any unnecessary log files from the target directory for the backup.

The `db_hotbackup` utility does not resolve pending transactions that are in the prepared state. Applications should specify @Figaro.EnvOpenOptions.RecoverFatal when opening the environment when failing over to the backup.


#### -b

Specify the target directory for the backup.

#### -c

Before performing the backup, checkpoint the source database environment and remove any log files that are no longer required in that environment. **To avoid making catastrophic recovery impossible, log file removal must be integrated with log file archival.**


#### -d

Specify one or more directories that contain data files to be copied to the target directory.


**As all database files are copied into a single target directory, files named the same, stored in different source directories, would overwrite each other when copied to the target directory.**


Please note the database environment recovery log references database files as they are named by the application program. **If the application uses absolute or relative pathnames to name database files, running recovery in the target directory may not properly find the copies of the files or might even find the source files, potentially resulting in corruption.**


#### -h

Specify the target directory for the backup, that is, the database environment home directory.

#### -l

Specify a source directory that contains log files; if none is specified, the database environment home directory will be searched for log files.

#### -P

Specify an environment password. Although the utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.

#### -u

Update a pre-existing hot backup snapshot by copying in new log files. If the `-u` option is specified, no databases will be copied into the target directory.

#### -V

Write the library version number to the standard output, and exit.

#### -v

Run in verbose mode, listing operations as they are done.



#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).

## See Also

#### Other Resources
[Utilities](xref:utilities.md)
