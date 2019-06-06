---
uid: db_checkpoint.md
---

# db_checkpoint

The `db_checkpoint` utility is a daemon process that monitors the database log, and periodically calls @Figaro.FigaroEnv.SetEnvironmentTransactionCheckpoint(System.Boolean,System.UInt32,System.UInt32) to checkpoint it.

```
db_checkpoint [-1Vv] [-h home] [-k kilobytes] [-L file] [-P password] [-p min]
```

The `db_checkpoint` utility uses a Figaro BDB environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing a Figaro BDB environment). In order to avoid environment corruption when using a Figaro BDB environment, `db_checkpoint` should always be given the chance to detach from the environment and exit gracefully. To cause `db_checkpoint` to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT).

The `db_checkpoint` utility does not attempt to create the Figaro BDB shared memory regions if they do not already exist. The application that creates the region should be started first, and once the region is created, the `db_checkpoint` utility should be started.

#### -1

Force a single checkpoint of the log (regardless of whether or not there has been activity since the last checkpoint), and then exit.


When the `-1` flag is specified, the `db_checkpoint` utility will checkpoint the log even if unable to find an existing database environment. This functionality is useful when upgrading database environments from one version to another.


#### -h

Specify a home directory for the database environment; by default, the current working directory is used.



#### -k

Checkpoint the database at least as often as every **kilobytes** of log file are written.



#### -L

Log the execution of the `db_checkpoint` utility to the specified **file** in the following format, where _###_ is the process ID, and the date is the time the utility was started.


```
db_checkpoint: ### Wed Jun 15 01:23:45 EDT 2009
```

This file will be removed if the `db_checkpoint` utility exits gracefully.


#### -P

Specify an environment password. Although utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -p

Checkpoint the database at least every **min** minutes if there has been any activity since the last checkpoint.



#### -V

Write the library version number to the standard output, and exit.



#### -v

Write the time of each checkpoint attempt to the standard output.



## See Also

[Utilities](xref:utilities.md)