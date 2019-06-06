---
uid: setting-the-in-memory-log-buffer-size.md
---

# Setting the In-Memory Log Buffer Size

When your application is configured for on-disk logging (the default behavior for transactional applications), log information is stored in-memory until the storage space fills up, or a transaction commit forces the log information to be flushed to disk.


It is possible to increase the amount of memory available to your file log buffer. Doing so improves throughput for long-running transactions, or for transactions that produce a large amount of data.


When you have your logging subsystem configured to maintain your log entirely in memory (see [Configuring In-Memory Logging](xref:configuring-in-memory-logging.md)), it is very important to configure your log buffer size because the log buffer must be capable of holding all log information that can accumulate during the longest running transaction. You must make sure that the in-memory log buffer size is large enough that no transaction will ever span the entire buffer. You must also avoid a state where the in-memory buffer is full and no space can be freed because a transaction that started the first log "file" is still active.


When your logging subsystem is configured for on-disk logging, the default log buffer space is 32 KB. When in-memory logging is configured, the default log buffer space is 1 MB.


You can increase your log buffer space using the @Figaro.FigaroEnv.LogBufferSize property. Note that this method can only be called before the first environment handle for your application is opened.


## See Also

@Figaro.FigaroEnv.LogBufferSize

#### Other Resources
[COnfiguring In-Memory Logging](xref:configuring-in-memory-logging.md)
[COnfiguring In-Memory Logging](xref:configuring-in-memory-logging.md)
[Configuring the Logging Subsystem](xref:configuring-the-logging-subsystem.md)