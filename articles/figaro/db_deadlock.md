---
uid: db_deadlock.md
---

# db_deadlock

The `db_deadlock` utility traverses the database environment lock region, and aborts a lock request each time it detects a deadlock or a lock request that has timed out. By default, in the case of a deadlock, a random lock request is chosen to be aborted.

This utility should be run as a background daemon, or the underlying deadlock detection interfaces should be called in some other way, whenever there are multiple threads or processes accessing a database and at least one of them is modifying it.



`db_deadlock [-Vv] [-a e | m | n | o | W | w | y] [-h home] [-L file] [-t sec.usec]`

The `db_deadlock` utility uses a Figaro BDB environment (as described for the -h option, the environment variable `DB_HOME`, or because the utility was run in a directory containing a Figaro BDB environment). In order to avoid environment corruption when using an environment, `db_deadlock` should always be given the chance to detach from the environment and exit gracefully. To cause `db_deadlock` to release all environment resources and exit cleanly, send it an interrupt signal (`SIGINT`).


The `db_deadlock` utility does not attempt to create the shared memory regions if they do not already exist. The application which creates the region should be started first, and then, once the region is created, the `db_deadlock` utility should be started.


#### -a

When a deadlock is detected, abort the locker:
Option | Selected Locker
------ | ---------------
`m` | with the most locks
`n` | with the fewest locks
`o` | with the oldest locks
`W` | with the most write locks
`w` | with the fewest write locks
`Y` | with the youngest lock
`e` | with any lock requests that have timed out


#### -h

Specify a home directory for the database environment; by default, the current working directory is used.



#### -L

Log the execution of the `db_deadlock` utility to the specified **file** in the following format, where _###_ is the process ID, and the date is the time the utility was started.


```
db_deadlock: ### Wed Jun 15 01:23:45 EDT 2009
```

This file will be removed if the `db_deadlock` utility exits gracefully.



#### -t

Check the database environment every **sec** seconds plus `usec` microseconds to see if a process has been forced to wait for a lock; if one has, review the database environment lock structures.


If the `-t` option is not specified, the command will run once and exit.

#### -V

Write the library version number to the standard output, and exit.

#### -v

Run in verbose mode, generating messages each time the detector runs.

#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).


## See Also
* [Utilities](xref:utilities.md)