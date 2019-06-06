---
uid: configuring-the-logging-region-size.md
---

# Configuring the Logging Region Size

The logging subsystem's default region size is 60 KB. The logging region is used to store filenames, and so you may need to increase its size if a large number of files (that is, if you have a very large number of databases) will be opened and registered with Figaro's log manager.


You can set the size of your logging region by using the @Figaro.FigaroEnv.MaxLogRegion property. Note that this method can only be called before the first environment handle for your application is opened.


## See Also
[Configuring the Logging Subsystem](xref:configuring-the-logging-subsystem.md)