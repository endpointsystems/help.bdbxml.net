---
uid: temporary-files.md
---

# Temporary Files

All Figaro .NET applications are capable of writing temporary files to disk. This happens when the disk cache fills up and so Figaro is forced to write overflow pages. For the most part, these temporary files can be safely ignored.


However, for some class of applications, the presence of the temporary overflow files can be problematic. You can prevent temporary files from being created on your hard drive by creating your disk cache large enough that it can contain your entire working set of data. You do this using the @Figaro.FigaroEnv.SetCacheSize(Figaro.EnvCacheSize,System.Int32) method prior to opening your environment.

>[!NOTE]
>It is always safe to delete temporary overflow files written by Figaro after the application has shutdown.

Temporary database files are placed in the directory identified by the @Figaro.FigaroEnv.TempDirectory property. If this method is not called by the application, then the application will use the directory identified on an environment variable, if your application is configured to do this. Assuming that it is appropriately configured, then the following environment variables are checked to see if they have been set. The following order of precedence matters; the first of the following environment variables found to be set is used to determine the location of the temporary directory:

* `TMPDIR`
* `TEMP`
* `TMP`
* `TempFolder`

If none of these environment variables are set, Figaro checks the value returned by the `GetTempPath` interface to see if that is set. If not, then the default location identified above are attempted.

>[!NOTE]
>Environment variables are not used by Figaro applications unless the @Figaro.EnvOpenOptions or @Figaro.EnvOpenOptions.UseEnvironmentRoot flags are set when the environment is opened.

If no other method of determining the location of the temporary file directory can be found, then Figaro will resort to using built-in default values. That is, the first of the following locations found to exist is used, if a temporary file directory is not otherwise identified for Figaro:

* The directory `C:\Temp`
* The directory `C:\Tmp`

## See Also

* @Figaro.EnvOpenOptions
* @administering-figaro-applications.md
