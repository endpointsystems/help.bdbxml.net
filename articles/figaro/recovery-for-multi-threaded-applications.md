---
uid: recovery-for-multi-threaded-applications.md
---

# Recovery for Multi-Threaded Applications

If your application uses only one environment handle, then handling recovery for a multi-threaded application is no more difficult than for a single threaded application. You simply open the environment in the application's main thread, and then pass that handle to each of the threads that will be performing Figaro operations. We illustrate this with our final example in the documentation (see [Transaction Example](xref:transaction-example.md) for more information).


Alternatively, you can have each worker thread open its own environment handle. However, in this case, designing for recovery is a bit more complicated.


Generally, when a thread performing container operations fails or hangs, it is frequently best to simply restart the application and run recovery upon application startup as normal. However, not all applications can afford to restart because a single thread has misbehaved.


If you are attempting to continue operations in the face of a misbehaving thread, then at a minimum recovery must be run if a thread performing container operations fails or hangs.


Remember that recovery clears the environment of all outstanding locks, including any that might be outstanding from an aborted thread. If these locks are not cleared, other threads performing database operations can back up behind the locks obtained but never cleared by the failed thread. The result will be an application that hangs indefinitely.


To run recovery under these circumstances:
* Suspend or shutdown all other threads performing database operations.
* Discarding any open environment handles. Note that attempting to gracefully close these handles may be asking for trouble; the close can fail if the environment is already in need of recovery. For this reason, it is best and easiest to simply discard the handle.
* Open new handles, running recovery as you open them. See [Normal Recovery](xref:normal-recovery.md) for more information.
* Restart all your container threads.

A traditional way to handle this activity is to spawn a watcher thread that is responsible for making sure all is well with your threads, and performing the above actions if not.


However, in the case where each worker thread opens and maintains its own environment handle, recovery is complicated for two reasons:

* For some applications and workloads, it might be worthwhile to give your database threads the ability to gracefully finalize any on-going transactions. If this is the case, your code must be capable of signaling each thread to halt Figaro activities and close its environment. If you simply run recovery against the environment, your container threads will detect this and fail in the midst of performing their container operations.
* Your code must be capable of ensuring only one thread runs recovery before allowing all other threads to open their respective environment handles. Recovery should be single threaded because when recovery is run against an environment, it is deleted and then recreated. This will cause all other processes and threads to "fail" when they attempt operations against the newly recovered environment. If all threads run recovery when they start up, then it is likely that some threads will fail because the environment that they are using has been recovered. This will cause the thread to have to re-execute its own recovery path. At best, this is inefficient and at worst it could cause your application to fall into an endless recovery pattern.

## See Also


* [Normal Recovery](xref:normal-recovery.md)
* [Designing Your Application For Recovery](xref:designing-your-application-for-recovery.md)
