---
uid: db_archive.md
---

# db_archive

The `db_archive` utility writes the pathnames of log files that are no longer in use (for example, no longer involved in active transactions), to the standard output, one pathname per line. These log files should be written to backup media to provide for recovery in the case of catastrophic failure (which also requires a snapshot of the database files), but they may then be deleted from the system to reclaim disk space.

```
db_archive [-adlsVv] [-h home] [-P password]
```

Log cursor handles may have open file descriptors for log files in the database environment. Also, the Berkeley DB interfaces to the database environment logging subsystem may allocate log cursors and have open file descriptors for log files as well. On operating systems where file system related system calls can fail if a process has an open file descriptor for the affected file, attempting to move or remove the log files listed by `db_archive` may fail. All Figaro BDB internal use of log cursors operates on active log files only and furthermore, is short-lived in nature. So, an application seeing such a failure should be restructured to dispose of any open @Figaro.Container, @Figaro.XmlManager or @Figaro.FigaroEnv objects it may have, and retry the operation until it succeeds. (Although the latter is not likely to be necessary; it is hard to imagine a reason to move or rename a log file in which transactions are being logged or aborted.)


The `db_archive` utility uses a Figaro BDB environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing a Figaro BDB environment). In order to avoid environment corruption when using a Figaro BDB environment, `db_archive` should always be given the chance to detach from the environment and exit gracefully. To cause `db_archive` to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT or ^C in the command line).

#### -a

Write all pathnames as absolute pathnames, instead of relative to the database home directories.


#### -d

Remove log files that are no longer needed; no filenames are written. Automatic log file removal is likely to make catastrophic recovery impossible.

#### -h

Specify a home directory for the database environment; by default, the current working directory is used.

#### -s

Write the pathnames of all the database files that need to be archived in order to recover the database from catastrophic failure. If any of the database files have not been accessed during the lifetime of the current log files, `db_archive` will not include them in this output.


It is possible that some of the files to which the log refers have since been deleted from the system. In this case, `db_archive` will ignore them. When `db_recover` is run, any files to which the log refers that are not present during recovery are assumed to have been deleted and will not be recovered.

#### -V

Write the library version number to the standard output, and exit.

#### -v

Run in verbose mode.



#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).

## See Also

* [Utilities](xref:utilities.md)