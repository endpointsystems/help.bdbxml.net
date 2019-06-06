---
uid: db_stat.md
---

# db_stat

The `db_stat` utility displays statistics for Berkeley DB environments.



`db_stat [-cEelmNrtVZ] [-C Aclop] [-h home] [-L A] [-M A] [-R A] [-P password]`

Values normally displayed in quantities of bytes are displayed as a combination of gigabytes (GB), megabytes (MB), kilobytes (KB), and bytes (B). Otherwise, values smaller than 10 million are displayed without any special notation, and values larger than 10 million are displayed as a number followed by "M".


The `db_stat` utility may be used with a Figaro BDB environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing an existing environment). In order to avoid environment corruption when using a Berkeley DB environment, `db_stat` should always be given the chance to detach from the environment and exit gracefully. To cause `db_stat` to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT or ^C).



#### -C

Display detailed information about the locking subsystem.

Option | Description
------ | -----------
`A` | display all information.
`c` | display lock conflict matrix.
`l` | display lockers within hash chains.
`o` | display lock objects within hash chains.
`p` | display locking subsystem parameters.

#### -c

Display locking subsystem parameters, as described in @Figaro.FigaroEnv.GetLockStatistics(System.Boolean).

#### -d

Display database statistics for the specified file.


If the database contains multiple databases and the `-s` flag is not specified, the statistics are for the internal database that describes the other databases the file contains, and not for the file as a whole.



#### -E

Display detailed information about the database environment.



#### -e

Display information about the database environment, including all configured subsystems of the database environment.



#### -f

Display only those database statistics that can be acquired without traversing the database.



#### -h

Specify a home directory for the database environment; by default, the current working directory is used.



#### -l

Display logging subsystem statistics.



#### -M

Display detailed information about the cache.


Option | Description
------ | -----------
`A` | display all information.
`h` | display buffers within hash chains.


#### -m

Display cache statistics.



#### -N

Do not acquire shared region mutexes while running. Other problems, such as potentially fatal errors, will be ignored as well. This option is intended only for debugging errors, and should not be used under any other circumstances.



#### -P

Specify an environment password. Although the utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -s

Display statistics for the specified database contained in the file specified with the `-d` flag.



#### -t

Display transaction subsystem statistics.



#### -V

Write the library version number to the standard output, and exit.



#### -Z

Reset the statistics after reporting them; valid only with the `-C, -c, -E, -e, -L, -l, -M, -m, -R, -r`, and `-t` options..


