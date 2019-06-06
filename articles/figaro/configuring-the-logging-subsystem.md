---
uid: configuring-the-logging-subsystem.md
---

# Configuring the Logging Subsystem

The aspects of logging that may be configured are the size of the logging subsystem's region, the size of the log files on disk and the size of the log buffer in memory. The @Figaro.FigaroEnv.SetMaxLogRegion(System.UInt32) method specifies the size of the logging subsystem's region, in bytes. The logging subsystem's default size is approximately 60KB. This value may need to be increased if a large number of files are registered with the log manager, for example, by opening a large number of containers in a transactional application.


The @Figaro.FigaroEnv.SetMaxLogRegion(System.UInt32) method specifies the individual log file size for all the applications sharing the environment. Setting the log file size is largely a matter of convenience and a reflection of the application's preferences in backup media and frequency. However, setting the log file size too low can potentially cause problems because it would be possible to run out of log sequence numbers, which requires a full archival and application restart to reset. See [Log File Limits](xref:log-file-limits.md) for more information.



## In This Section
* [Setting the Log File Size](xref:setting-the-log-file-size.md)
* [Configuring the Logging Region Size](xref:configuring-the-logging-region-size.md)
* [Configuring In-Memory Logging](xref:configuring-in-memory-logging.md)
* [Setting the In-Memory Log Buffer Size](xref:setting-the-in-memory-log-buffer-size.md)

## See Also
* @Figaro.FigaroEnv.SetMaxLogRegion(System.UInt32)

#### Other Resources
* [Log File Limits](xref:log-file-limits.md)
* [Managing Database Files](xref:managing-database-files.md)