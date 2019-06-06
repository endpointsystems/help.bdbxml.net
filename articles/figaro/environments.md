---
uid: environments.md
---
# Environments

All Figaro applications use environments. However, simple Figaro applications can use a default environment that only provide a small subset of features — most notably, multi-threaded access (but not multi-process) is enabled, as is the in-memory cache. For more advanced features, such as transactional protection, you must use an externally-managed environment.


An environment represents an encapsulation of one or more containers and any associated log and region files. They are used to support multi-threaded and multi-process applications by allowing different threads of control to share the in-memory cache, the locking tables, the logging subsystem, and the file namespace. By sharing these things, your concurrent application is more efficient than if each thread of control had to manage these resources on its own.


By default all Figaro containers are backed by files on disk. In addition to these files, transactional Figaro applications create logs that are also by default stored on disk (they can optionally be backed using shared memory). Finally, transactional Figaro applications also create and use shared-memory regions that are also typically backed by the file system. But like containers and logs, the regions can be maintained strictly in-memory if your application requires it. For an example of an application that manages all environment files in-memory, see [The In-Memory Transaction Example](xref:the-in-memory-transaction-example.md).

