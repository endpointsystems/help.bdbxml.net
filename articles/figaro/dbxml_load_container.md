---
uid: dbxml_load_container.md
---

# dbxml_load_container

```
dbxml_load_container [-c container] [-h home] [-s node|wholedoc] [-f file_list] [-p file_list_path] [--v] [--V] [--P password] file1.xml file2.xml ...
```

The `dbxml_load_container` utility loads XML documents into the specified container. XML files can either be specified as arguments, or they can be specified in a file using the `-f` option.


This utility will attempt to join an environment if one is active. It is recommended, however, that this tool be used offline. If the joined environment is transactional, this utility will also be transactional, with a separate transaction for each document added.



#### -c

Specify the name of the container into which your want to load the identified documents. If the container does not currently exist, it is created for you. This is a required parameter.



#### -f

Specify a file that contains a list of XML files to load into the container.



#### -h

Specify a home directory for the database environment; by default, the current working directory is used.



#### -P

Specify an environment password. Although utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -p

Specify a path prefix to prepend to every filename contained in the file list specified by the `-f` option.



#### -s

Specify the container type. Valid values are:

_node_. Use node-level storage. XML documents are broken into their individual nodes, and the nodes are stored as individual records in the underlying database. This is the default.
_wholedoc_. Use whole-document storage. Entire XML documents are stored as individual records in the underlying database.
Note that if the container already exists, this option is ignored.



#### -V

Write the library version number to the standard output, and exit.



#### -v

Generate verbose output.



#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).


## See Also

[Utilities](xref:utilities.md)