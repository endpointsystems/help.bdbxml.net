---
uid: dbxml_dump.md
---

# dbxml_dump

The `dbxml_dump` utility reads the XML container in file **file** and writes it to the standard output using a portable flat-text format understood by the [dbxml_load](xref:dbxml_load.md) utility. The argument **xml_container** must be a file produced using the Figaro library functions.


```
dbxml_dump [-NRrV] [-f output] [-h home] [-P password] xml_container
```

The `dbxml_dump` utility may be used with a Figaro BDB environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing an existing environment). In order to avoid environment corruption when using an existing environment, `dbxml_dump` should always be given the chance to detach from the environment and exit gracefully. To cause `dbxml_dump `to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT or ^C).


Even when using an existing environment, the `dbxml_dump` utility does not use any kind of database locking if it is invoked with the `-R` or `-r` arguments. If used with one of these arguments, the `dbxml_dump` utility may only be safely run on XML containers that are not being modified by any other process; otherwise, the output may be corrupt.



#### -f

Write to the specified **file** instead of to the standard output.



#### -h

Specify a home directory for the database environment; by default, the current working directory is used.



#### -N

Do not acquire shared region mutexes while running. Other problems, such as potentially fatal errors, will be ignored as well. This option is intended only for debugging errors, and should not be used under any other circumstances.



#### -P

Specify an environment password. Although utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -R

Aggressively salvage data from a possibly corrupt file. The `-R` flag differs from the `-r` option in that it will return all possible data from the file at the risk of also returning already deleted or otherwise nonsensical items. Data dumped in this fashion will almost certainly have to be edited by hand or other means before the data is ready for reload into another container.



#### -r

Salvage data from a possibly corrupt file. When used on an uncorrupted container, this option should return equivalent data to a normal dump, but most likely in a different order.



#### -V

Write the library version number to the standard output, and exit.



#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).


## See Also

[dbxml_load](xref:dbxml_load.md)