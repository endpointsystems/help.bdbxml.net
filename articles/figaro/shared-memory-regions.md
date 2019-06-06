---
uid: shared-memory-regions.md
---

# Shared Memory Regions


The subsystems that you enable for an environment (in our case, transaction, logging, locking, and the memory pool) are described by one or more regions. The regions contain all of the state information that needs to be shared among threads and/or processes using the environment.

Regions may be backed by the file system, by heap memory, or by system shared memory.

## Regions Backed by Files

By default, shared memory regions are created as files in the environment's home directory (not the environment's data directory). If it is available, the POSIX `mmap` interface is used to map these files into your application's address space. If `mmap` is not available, then the UNIX `shmget` interfaces are used instead (again, if they are available).


In this default case, the region files are named `__db.###` (for example, `__db.001`, `__db.002`, and so on).

## Regions Backed by Heap Memory

If heap memory is used to back your shared memory regions, the environment may only be accessed by a single process, although that process may be multi-threaded. In this case, the regions are managed only in memory, and they are not written to the file system. You indicate that heap memory is to be used for the region files by specifying @Figaro.EnvOpenOptions.Private to the @Figaro.FigaroEnv.Open method.


(For an example of an entirely in-memory transactional application, see [The In-Memory Transaction Example](xref:the-in-memory-transaction-example.md).)



## Regions Backed by System Memory

Finally, you can cause system memory to be used for your regions instead of memory-mapped files. You do this by providing @Figaro.EnvOpenOptions.SystemSharedMem to the @Figaro.FigaroEnv.Open method.


When region files are backed by system memory, Figaro creates a single file in the environment's home directory. This file contains information necessary to identify the system shared memory in use by the environment. By creating this file, Figaro enables multiple processes to share the environment.


On Windows platforms, the use of system memory for the region files is problematic because the operating system uses reference counting to clean up shared objects in the paging file automatically. In addition, the default access permissions for shared objects are different from files, which may cause problems when an environment is accessed by multiple processes running as different users. See [System Shared Memory on Windows](xref:system-shared-memory-on-windows.md) for more information.

## See Also

* [Environments](xref:environments.md)
* [The In-Memory Transaction Example](xref:the-in-memory-transaction-example.md)
* [System Shared Memory on Windows](xref:system-shared-memory-on-windows.md)
