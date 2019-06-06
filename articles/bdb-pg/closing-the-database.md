# Closing the Database

The <xref:Database.Close> database handle closes the DB handle. By default, <xref:Database.Close> also flushes all modified records from the database cache to disk.

There is a `NoSync` parameter you can optionally specify which, if `true`, disables flushing cached information to disk. 

>[!IMPORTANT]
> Flushing cached information to disk only minimizes the window of opportunity for corrupted data - **it does not eliminate the possibility**.

While unlikely, it is possible for database corruption to happen if a system or application crash occurs while writing data to the database. To ensure that database corruption never occurs, applications must either:

- Use transactions and logging with automatic recovery.
- Use logging and application-specific recovery.
- Edit a copy of the database, and, once all applications using the database have successfully called <xref:Database.Close>, use system operations (for example, the POSIX `rename` system call) to atomically replace the original database with the updated copy.