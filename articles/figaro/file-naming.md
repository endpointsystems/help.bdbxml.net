---
uid: file-naming.md
--

# File Naming

In order to operate, your Figaro application must be able to locate its container files, log files, and region files. If these are stored in the file system, then you must tell Figaro where they are located (a number of mechanisms exist that allow you to identify the location of these files – see below). Otherwise, by default they are located in the current working directory.



## Specifying the Environment Home Directory

The environment home directory is used to determine where Figaro files are located. Its location is identified using one of the following mechanisms, in the following order of priority:

* If no information is given as to where to put the environment home, then the current working directory is used.
* If a home directory is specified on the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method, then that location is always used for the environment home.
* If a home directory is not supplied to @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions), then the directory identified by the `DB_HOME` environment variable is used if you specify either the @Figaro.EnvironmentRemoveAction.UseEnvironment or @Figaro.EnvironmentRemoveAction.UseEnvironmentRoot flags to the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method. Both flags allow you to identify the path to the environment's home directory using the `DB_HOME` environment variable. However, @Figaro.EnvironmentRemoveAction.UseEnvironmentRoot is honored only if the process is run with administrative privileges.

## Specifying File Locations

By default, all Figaro files are created relative to the environment home directory. For example, suppose your environment home is in `C:\dev\db\home`. Also suppose you name your container `data\myContainer.dbxml`. Then in this case, the container is placed in: `C:\dev\db\home\data\myContainer.dbxml`.

That said, Figaro always defers to absolute pathnames. This means that if you provide an absolute filename when you name your container, then that file is not placed relative to the environment home directory. Instead, it is placed in the exact location that you specified for the filename.


On Windows systems, an absolute pathname is a name that begins with one of the following:

* A backslash ('\').
* Any alphabetic letter, followed by a colon (':'), followed by a backslash ('\').</li></ul>&nbsp;

>[!NOTE]
>Try not to use absolute path names for your environment's files. Under certain recovery scenarios, absolute path names can render your environment unrecoverable. This occurs if you are attempting to recover you environment on a system that does not support the absolute path name that you used.

## Identifying Specific File Locations

As described in the previous sections, Figaro will place all its files in or relative to the environment home directory. You can also cause a specific container file to be placed in a particular location by using an absolute path name for its name. In this situation, the environment's home directory is not considered when naming the file.


It is frequently desirable to place container, log, and region files on separate disk drives. By spreading I/O across multiple drives, you can increase parallelism and improve throughput. Additionally, by placing log files and container files on separate drives, you improve your application's reliability by providing your application with a greater chance of surviving a disk failure.


You can cause Figaro's files to be placed in specific locations using the following mechanisms:
File Type | To Override
--------- | -----------
Container Files | You can cause container files to be created in a directory other than the environment home by using the @Figaro.FigaroEnv.AddDataDirectory(System.String) method. The directory identified here must exist. If a relative path is provided, then the directory location is resolved relative to the environment's home directory. This method modifies the directory used for container files created and managed by a single environment handle; it does not configure the entire environment. This method may not be called after the environment has been opened. You can also set a default data location that is used by the entire environment by using the set_data_dir parameter in the environment's `DB_CONFIG` file. Note that the `set_data_dir` parameter overrides any value set by the Figaro.FigaroEnv.AddDataDirectory(System.String) 
Log Files | You can cause log files to be created in a directory other than the environment home directory by using the @Figaro.FigaroEnv.LogDirectory property. The directory identified here must exist. If a relative path is provided, then the directory location is resolved relative to the environment's home directory. This method modifies the directory used for container files created and managed by a single environment handle; it does not configure the entire environment. This method may not be called after the environment has been opened.
Region Files | If backed by the file system, region files are always placed in the environment home directory.

