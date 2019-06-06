---
uid: log-file-limits.md
---

# Log File Limits

Log filenames and sizes impose a limit on how long databases may be used in a database environment. It is quite unlikely that an application will reach this limit; however, if the limit is reached, the environment's databases must be dumped and reloaded.

The log filename consists of **log.** followed by 10 digits, with a maximum of 2,000,000,000 log files. Consider an application performing 6000 transactions per second for 24 hours a day, logged into 10MB log files, in which each transaction is logging approximately 500 bytes of data. The following calculation:


```
(10 * 2^20 * 2000000000) / (6000 * 500 * 365 * 60 * 60 * 24) = ~221
```

indicates that the system will run out of log filenames in roughly 221 years.

There is no way to reset the log filename space in Figaro. If your application is reaching the end of its log filename space, you must do the following:

Archive your databases as if to prepare for catastrophic failure (see Database and log file archival for more information).
Remove all of the log files from your environment. Note that this is the only situation in which all of the log files are removed from an environment; in all other cases, at least a single log file is retained.
Restart your application.

## Configuring the Logging Region Size

he logging subsystem's default region size is 60 KB. The logging region is used to store filenames, and so you may need to increase its size if a large number of files (that is, if you have a very large number of databases) will be opened and registered with Figaro's log manager.

You can set the size of your logging region by using the @Figaro.FigaroEnv.SetMaxLogRegion(System.UInt32) method. Note that this method can only be called before the first environment handle for your application is opened.


## Configuring In-Memory Logging

It is possible to configure your logging subsystem such that logs are maintained entirely in memory. When you do this, you give up your transactional durability guarantee. Without log files, you have no way to run recovery so any system or software failures that you might experience can corrupt your containers.


However, by giving up your durability guarantees, you can greatly improve your application's throughput by avoiding the disk I/O necessary to write logging information to disk. In this case, you still retain your transactional atomicity, consistency, and isolation guarantees.


To configure your logging subsystem to maintain your logs entirely in-memory:

* Make sure your log buffer is capable of holding all log information that can accumulate during the longest running transaction. See [Setting the In-Memory Log Buffer Size](xref:setting-the-in-memory-log-buffer-size.md) for details.
* Do not run normal recovery when you open your environment. In this configuration, there are no log files available against which you can run recovery. As a result, if you specify recovery when you open your environment, it is ignored.
* Specify @Figaro.EnvLogOptions.InMemory to the @Figaro.FigaroEnv.SetLogOptions(Figaro.EnvLogOptions,System.Boolean) method. Note that you must specify this before your application opens its first environment handle.

## Setting the In-Memory Log Buffer Size

When your application is configured for on-disk logging (the default behavior for transactional applications), log information is stored in-memory until the storage space fills up, or a transaction commit forces the log information to be flushed to disk. It is possible to increase the amount of memory available to your file log buffer. Doing so improves throughput for long-running transactions, or for transactions that produce a large amount of data.


When you have your logging subsystem configured to maintain your log entirely in memory (see [Configuring In-Memory Logging](xref:configuring-in-memory-logging.md)), it is very important to configure your log buffer size because the log buffer must be capable of holding all log information that can accumulate during the longest running transaction. You must make sure that the in-memory log buffer size is large enough that no transaction will ever span the entire buffer. You must also avoid a state where the in-memory buffer is full and no space can be freed because a transaction that started the first log "file" is still active.

When your logging subsystem is configured for on-disk logging, the default log buffer space is 32 KB. When in-memory logging is configured, the default log buffer space is 1 MB. You can increase your log buffer space by using the @Figaro.FigaroEnv.SetLogBufferSize(System.UInt32) method. Note that this method can only be called before the first environment handle for your application is opened.

## See Also

* @Figaro.FigaroEnv.SetMaxLogRegion(System.UInt32)
* @Figaro.EnvLogOptions
* @Figaro.FigaroEnv.SetLogOptions(Figaro.EnvLogOptions,System.Boolean)
* @Figaro.FigaroEnv.SetLogBufferSize(System.UInt32)

#### Other Resources
* [Setting the In-Memory Log Buffer Size](xref:setting-the-in-memory-log-buffer-size.md)
* [Configuring In-Memory Logging](xref:configuring-in-memory-logging.md)

