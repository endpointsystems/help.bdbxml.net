---
uid: dbxml_load.md
---

# dbxml_load

The `dbxml_load` utility reads from the standard input and loads it into the XML container **xml_container**. The XML container **xml_container** is created if it does not already exist.


The input to `dbxml_load` must be in the output format specified by the `dbxml_dump` utility.

## dbxml_load [-V] [-f file] [-h home] [-P password] xml_container

The `dbxml_load` utility may be used with a Figaro BDB environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing an existing environment). In order to avoid environment corruption when using an existing environment, `dbxml_load` should always be given the chance to detach from the environment and exit gracefully. To cause `dbxml_load` to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT, or ^C).



#### -f

Read from the specified file instead of from the standard input.



#### -h

Specify a home directory for the database environment.


If a home directory is specified, the database environment is opened using the @Figaro.EnvOpenOptions.InitLock, @Figaro.EnvOpenOptions.InitLog, @Figaro.EnvOpenOptions.InitMemoryBufferPool, @Figaro.EnvOpenOptions.InitTransaction, and @Figaro.EnvOpenOptions.UseEnvironment flags to @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions). (This means that `dbxml_load` can be used to load data into XML containers while they are in use by other processes.) If the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) call fails, or if no home directory is specified, the XML container is still updated, but the environment is ignored; for example, no locking is done.



#### -P

Specify an environment password. Although utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -V

Write the library version number to the standard output, and exit.



#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).

## See Also


#### Reference
* @Figaro.EnvOpenOptions
* [A Note on System Failure](xref:a-note-on-system-failure.md)
* @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)

#### Other Resources
& [Utilities](xref:utilities.md)