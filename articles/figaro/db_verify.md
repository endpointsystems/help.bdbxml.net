---
uid: db_verify.md
---

# db_verify

The `db_verify` utility verifies the structure of one or more files and the databases they contain.

```
db_verify [-NoqV] [-h home] [-P password] db_file ...
```

**The `db_verify` utility does not perform any locking, even in environments that are configured with a locking subsystem. As such, it should only be used on files that are not being modified by another thread of control.**


The `db_verify` utility may be used with an environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing an existing environment). In order to avoid environment corruption, `db_verify` should always be given the chance to detach from the environment and exit gracefully. To cause `db_verify` to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT, or ^C).



#### -h

Specify the target directory for the backup, that is, the database environment home directory.



#### -o

Skip the database checks for `Btree `and duplicate sort order and for hashing.


If the file being verified contains databases with non-default comparison or hashing configurations, calling the `db_verify` utility without the `-o` flag will usually return failure. The `-o` flag causes `db_verify` to ignore database sort or hash ordering and allows `db_verify` to be used on these files.



#### 

Do not acquire shared region mutexes while running. Other problems, such as potentially fatal errors, will be ignored as well. This option is intended only for debugging errors, and should not be used under any other circumstances.



#### -P

Specify an environment password. Although the utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -q

Suppress the printing of any error descriptions, simply exit success or failure.



#### -V

Write the library version number to the standard output, and exit.



#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).

## See Also
[Utilities](xref:utilities.md)