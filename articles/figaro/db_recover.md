---
uid: db_recover.md
---

# db_recover

The `db_recover` utility must be run after an unexpected application, database, or system failure to restore the database to a consistent state. All committed transactions are guaranteed to appear after `db_recover` has run, and all uncommitted transactions will be completely undone.


```
db_recover [-cefVv] [-h home] [-P password] [-t [[CC]YY]MMDDhhmm[.SS]]]
```

In the case of catastrophic recovery, an archival copy -- or snapshot -- of all database files must be restored along with all of the log files written since the database file snapshot was made. (If disk space is a problem, log files may be referenced by symbolic links). For further information on creating a database snapshot, see [Backup Procedures](xref:backup-procedures.md). For further information on performing recovery, see [Recovery Procedures](xref:recovery-procedures.md).


If the failure was not catastrophic, the files present on the system at the time of failure are sufficient to perform recovery.


If log files are missing, `db_recover` will identify the missing log file(s) and fail, in which case the missing log files need to be restored and recovery performed again.


The `db_recover` utility uses a Figaro BDB environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing an existing environment). In order to avoid environment corruption when using an existing environment, `db_recover` should always be given the chance to detach from the environment and exit gracefully. To cause `db_recover` to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT, or ^C).



#### -c

Perform catastrophic recovery instead of normal recovery.



#### -e

Retain the environment after running recovery. Regions will be created with default parameter values.



#### -f

Display a message on the standard output showing the percent of recovery completed.



#### -h

Specify a home directory for the database environment; by default, the current working directory is used.



#### -P

Specify an environment password. Although utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -t

Recover to the time specified rather than to the most current possible date. The timestamp argument should be in the form **[[CC]YY]MMDDhhmm[.SS]** where each pair of letters represents the following:

Format Text | Description
----------- | -----------
`CC` | the first two digits of the year.
`YY` | The second two digits of the year. if `YY` if specified, but `CC` is not, a value for `YY` between 67 and 99 results in a `CC` value of 19. Otherwise, a `CC` value of 20 is used.
`MM` | the month of the year, from 1 to 12.
`DD` | the day of the month, from 1 to 31.
`hh` | the hour of the day, from 0 to 23.
`mm` | the minute of the hour, from 0 to 59.
`ss` | the second of the minute, from 0 to 61.

#### -V

Write the library version number to the standard output, and exit.

#### -v

Run in verbose mode.

#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).



## See Also

* [Utilities](xref:utilities.md)