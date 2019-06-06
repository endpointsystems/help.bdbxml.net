---
uid: environment-tuning.md
---

# Environment Tuning

There are a few different issues to consider when tuning the performance of Figaro .NET applications.

## Cache Size

The database cache defaults to a fairly small size, and most applications concerned with performance will want to set it explicitly. Using a cache that's too small will result in horrible performance. The first step in tuning the cache size is to use the [db_stat](xref:db_stat.md) utility (or the statistics returned by the @Figaro.FigaroEnv.GetMemoryPoolStatistics(System.Boolean) function) to measure the effectiveness of the cache. The goal is to maximize the cache's hit rate. Typically, increasing the size of the cache until the hit rate reaches 100% or levels off will yield the best performance. However, if your working set is sufficiently large, you will be limited by the system's available physical memory. Depending on the virtual memory and file system buffering policies of your system, and the requirements of other applications, the maximum cache size will be some amount smaller than the size of physical memory. If you find the [db_stat](xref:db_stat.md) utility showing that increasing the cache size improves your hit rate, but performance is not improving (or is getting worse), then it's likely you've hit other system limitations. At this point, you should review the system's swapping/paging activity and limit the size of the cache to the maximum size possible without triggering paging activity.

>[!NOTE]
>Always remember to make your measurements under conditions as close as possible to the conditions your deployed application will run under, and to test your final choices under worst-case conditions.

## Shared Memory

By default, Figaro creates its database environment shared regions in memory backed by the file system. Some systems do not distinguish between regular file system pages and memory-mapped pages backed by the file system, when selecting dirty pages to be flushed back to disk. For this reason, dirtying pages in the cache may cause intense file system activity, typically when the file system sync thread or process is run. In some cases, this can dramatically affect application throughput. The workaround to this problem is to create the shared regions in system shared memory (@Figaro.EnvOpenOptions.SystemSharedMem) or application private memory (@Figaro.EnvOpenOptions.Private), or, in cases where this behavior is configurable, to turn off the operating system's flushing of memory-mapped pages.

## Large XML Data

Storing large XML nodes or documents in a database can alter the database performance characteristics. The first parameter to consider is the database page size. When a node or document item is too large to be placed on a database page, it is stored on "overflow" pages that are maintained outside of the normal database structure (typically, items that are larger than one-quarter of the page size are deemed to be too large). Accessing these overflow pages requires at least one additional page reference over a normal access, so it is usually better to increase the page size than to create a database with a large number of overflow pages. Use the [db_stat](xref:db_stat.md) utility to review the number of overflow pages in the database.


The second issue is using large data items instead of duplicate data items. While using large data can offer performance gains to some applications (because it is possible to retrieve several data items in a single call), once the key/data items are large enough to be pushed off-page, they will slow the application down. Avoiding page overflow is the better long-term choice in container configuration.

## Scalability and Mutexes

A common question when tuning applications is scalability. For example, people will ask why, when adding additional threads or processes to an application, the overall database throughput decreases, even when all of the operations are read-only queries.


First, while read-only operations are logically concurrent, they still have to acquire mutexes on internal data structures. For example, when searching a XML node, the node has to be locked against other threads of control attempting to add or remove pages from the node. The more threads of control you add, the more contention there will be for those shared data structure resources.


Second, once contention starts happening, applications will also start to see threads of control convoy behind locks On test-and-set architectures, threads of control waiting for locks must attempt to acquire the mutex, sleep, check the mutex again, and so on. Each failed check of the mutex and subsequent sleep wastes CPU and decreases the overall throughput of the system.


Third, every time a thread acquires a shared mutex, it has to shoot down other references to that memory in every other CPU on the system. Many modern snoopy cache architectures have slow shoot down characteristics.


Fourth, schedulers don't care what application-specific mutexes a thread of control might hold when descheduling a thread. If a thread of control is descheduled while holding a shared data structure mutex, other threads of control will be blocked until the scheduler decides to run the blocking thread of control again. The more threads of control that are running, the smaller their quanta of CPU time, and the more likely they will be descheduled while holding a mutex.


The results of adding new threads of control to an application, on the application's throughput, is application and hardware specific and almost entirely dependent on the application's data access pattern and hardware.

## See Also

#### Other Resources
* [Performance Tuning](xref:performance-tuning.md)