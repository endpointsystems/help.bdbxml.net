---
uid: system-shared-memory-on-windows.md
---

# System Shared Memory on Windows

On Windows, system paging file memory is freed on last close. For this reason, multiple processes sharing a database environment created using the @Figaro.EnvOpenOptions.SystemSharedMem flag must arrange for at least one process to always have the environment open, or alternatively that any process joining the environment be prepared to re-create it.


If a system memory environment is closed by all processes, subsequent attempts to open it will return an error. To successfully open a transactional environment in this state, recovery must be run by the next process to open the environment. For non-transactional environments, applications should remove the existing environment and then create a new database environment.



## See Also


#### Other Resources
[Environments](xref:environments.md)