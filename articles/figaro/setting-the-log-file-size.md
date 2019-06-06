---
uid: setting-the-log-file-size.md
---

# Setting the Log File Size

Whenever a predefined amount of data is written to a log file (10 MB by default), Figaro stops using the current log file and starts writing to a new file. You can change the maximum amount of data contained in each log file by using the @Figaro.FigaroEnv.MaxLogSize property. Note that this method can be used at any time during an application's lifetime.


Setting the log file size to something larger than its default value is largely a matter of convenience and a reflection of the application's preference in backup media and frequency. However, if you set the log file size too low relative to your application's traffic patterns, you can cause yourself trouble.


From a performance perspective, setting the log file size to a low value can cause your active transactions to pause their writing activities more frequently than would occur with larger log file sizes. Whenever a transaction completes the log buffer is flushed to disk. Normally other transactions can continue to write to the log buffer while this flush is in progress. However, when one log file is being closed and another created, all transactions must cease writing to the log buffer until the switch over is completed.

## See Also

* @Figaro.FigaroEnv.MaxLogSize

#### Other Resources
* [Log File Limits](xref:log-file-limits.md)
* [Configuring the Logging Subsystem](xref:configuring-the-logging-subsystem.md)
