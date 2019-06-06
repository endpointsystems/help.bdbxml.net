---
uid: recovery-for-multi-process-applications.md
---

# Recovery for Multi-Process Applications


Frequently, Figaro applications use multiple processes to interact with the containers. For example, you may have a long-running process, such as some kind of server, and then a series of administrative tools that you use to inspect and administer the underlying containers. Or, in some web-based architectures, different services are run as independent processes that are managed by the server.

In any case, recovery for a multi-process environment is complicated for two reasons:

* In the event that recovery must be run, you might want to notify processes interacting with the environment that recovery is about to occur and give them a chance to gracefully terminate. Whether it is worthwhile for you to do this is entirely dependent upon the nature of your application. Some long-running applications with multiple processes performing meaningful work might want to do this. Other applications with processes performing container operations that are likely to be harmed by error conditions in other processes will likely find it to be not worth the effort. For this latter group, the chances of performing a graceful shutdown may be low anyway.
* Unlike single process scenarios, it can quickly become wasteful for every process interacting with the containers to run recovery when it starts up. This is partly because recovery does take some amount of time to run, but mostly you want to avoid a situation where your server must reopen all its environment handles just because you fire up a command line container administrative utility that always runs recovery.</li></ol>

Figaro offers you two methods by which you can manage recovery for multi-process Figaro applications. Each has different strengths and weaknesses, and they are described in the next sections.



## Effects of Multi-Process Recovery

Before continuing, it is worth noting that the following sections describe recovery processes than can result in one process running recovery while other processes are currently actively performing container operations.


When this happens, the current container operation will abnormally fail, throwing @Figaro.RunRecoveryException. This means that your application should immediately abandon any container operations that it may have on-going, discard any environment handles it has opened, and obtain and open new handles.


The net effect of this is that any writes performed by unresolved transactions will be lost. For persistent applications (servers, for example), the services it provides will also be unavailable for the amount of time that it takes to complete a recovery and for all participating processes to reopen their environment handles.

## Process Registration

One way to handle multi-process recovery is for every process to "register" its environment. In doing so, the process gains the ability to see if any other applications are using the environment and, if so, whether they have suffered an abnormal termination. If an abnormal termination is detected, the process runs recovery; otherwise, it does not.


Note that using process registration also ensures that recovery is serialized across applications. That is, only one process at a time has a chance to run recovery. Generally this means that the first process to start up will run recovery, and all other processes will silently not run recovery because it is not needed.


To cause your application to register its environment, you specify the @Figaro.EnvOpenOptions.Register flag when you open your environment. Note that you must also specify @Figaro.EnvOpenOptions.Recover or @Figaro.EnvOpenOptions.RecoverFatal for your environment open. If during the open, Figaro determines that recovery must be run, this indicates the type of recovery that is run. If you do not specify either type of recovery, then no recovery is run if the registration process identifies a need for it. In this case, the environment open simply fails by throwing @Figaro.RunRecoveryException.


Be aware that there are some limitations/requirements if you want your various processes to coordinate recovery using this registration process:

* There can be only one environment handle per environment per process. In the case of multi-threaded processes, the environment handle must be shared across threads.
* All processes sharing the environment must use registration. If registration is not uniformly used across all participating processes, then you can see inconsistent results in terms of your application's ability to recognize that recovery must be run.
* You can not use this mechanism with the @Figaro.FigaroEnv.FailCheck mechanism.

## Failure Checking

For very large and robust multi-process applications, the most common way to ensure all the processes are working as intended is to make use of a watchdog process. To assist a watchdog process, Figaro offers a failure checking mechanism.


When a thread of control fails with open environment handles, the result is that there may be resources left locked or corrupted. Other threads of control may encounter these unavailable resources quickly or not at all, depending on data access patterns.


In any case, the Figaro failure checking mechanism allows a watchdog to detect if an environment is unusable as a result of a thread of control failure. It should be called periodically (for example, once a minute) from the watchdog process. If the environment is deemed unusable, then the watchdog process is notified that recovery should be run. It is then up to the watchdog to actually run recovery. It is also the watchdog's responsibility to decide what to do about currently running processes before running recovery. The watchdog could, for example, attempt to gracefully shutdown or kill all relevant processes before running recovery.


Note that failure checking need not be run from a separate process, although conceptually that is how the mechanism is meant to be used. This same mechanism could be used in a multi-threaded application that wants to have a watchdog thread.


To use failure checking you must:
* Enable the @Figaro.FigaroEnv.OnIsAlive event using the @Figaro.FigaroEnv.IsAliveEnabled method. Figaro uses this method to determine whether a specified process and thread is alive when the failure checking is performed.
* Call the @Figaro.FigaroEnv.FailCheck method periodically. You can do this either periodically (once per minute, for example), or whenever a thread of control exits for your application.

If this method determines that a thread of control exited holding read locks, those locks are automatically released. If the thread of control exited with an unresolved transaction, that transaction is aborted. If any other problems exist beyond these such that the environment must be recovered, the method will throw @Figaro.RunRecoveryException.


Note that this mechanism should not be mixed with the process registration method of multi-process recovery described in the previous section.


## See Also

* @Figaro.FigaroEnv.OnIsAlive
* @Figaro.FigaroEnv.IsAliveEnabled
* @Figaro.FigaroEnv.FailCheck
* @Figaro.RunRecoveryException

#### Other Resources
* [Designing Your Application For Recovery](xref:designing-your-application-for-recovery.md)