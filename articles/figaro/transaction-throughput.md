---
uid: transaction-throughput.md
---

# Transaction Throughput

Generally, the speed of a database system is measured by the transaction throughput, expressed as a number of transactions per second. The two gating factors for Figaro DB performance in a transactional system are usually the underlying database files and the log file. Both are factors because they require disk I/O, which is slow relative to other system resources such as CPU.

## Transaction Throughput

In the worst-case scenario:

* Database access is truly random and the database is too large for any significant percentage of it to fit into the cache, resulting in a single I/O per requested key/data pair.
* Both the database and the log are on a single disk.
This means that for each transaction, Figaro is potentially performing several file system operations:

* Disk seek to database file
* Database file read
* Disk seek to log file
* Log file write
* Flush log file information to disk
* Disk seek to update log file metadata (for example, `inode` information)
* Log metadata write
* Flush log file metadata to disk

There are a number of ways to increase transactional throughput, all of which attempt to decrease the number of file system operations per transaction. First, the Berkeley DB software includes support for group commit. Group commit simply means that when the information about one transaction is flushed to disk, the information for any other waiting transactions will be flushed to disk at the same time, potentially amortizing a single log write over a large number of transactions. There are additional tuning parameters which may be useful to application writers:

* Tune the size of the database cache. If the Figaro DB key/data pairs used during the transaction are found in the database cache, the seek and read from the database are no longer necessary, resulting in two fewer file system operations per transaction. To determine whether your cache size is too small, see [Selecting a Cache Size](xref:selecting-a-cache-size.md).
* Put the database and the log files on different disks. This allows reads and writes to the log files and the database files to be performed concurrently.
* Set the file system configuration so that file access and modification times are not updated. Note that although the file access and modification times are not used by Figaro, this may affect other programs -- so be careful.
* Upgrade your hardware. When considering the hardware on which to run your application, however, it is important to consider the entire system. The controller and bus can have as much to do with the disk performance as the disk itself. It is also important to remember that throughput is rarely the limiting factor, and that disk seek times are normally the true performance issue for Figaro DB.
* Turn on the @Figaro.EnvConfig.WriteNoSyncTransaction flag. This changes the behavior so that the log files are not written and/or flushed when transactions are committed. Although this change will greatly increase your transaction throughput, it means that transactions will exhibit the ACI (atomicity, consistency, and isolation) properties, but not D (durability). Database integrity will be maintained, but it is possible that some number of the most recently committed transactions may be undone during recovery instead of being redone.

If you are bottlenecked on logging, the following test will help you confirm that the number of transactions per second that your application does is reasonable for the hardware on which you're running. Your test program should repeatedly perform the following operations:

* Seek to the beginning of the file
* Write to the file
* Flush the file write to disk

The number of times that you can perform these three operations per second is a rough measure of the minimum number of transactions per second of which the hardware is capable. This test simulates the operations applied to the log file. (As a simplifying assumption in this experiment, we assume that the database files are either on a separate disk; or that they fit, with some few exceptions, into the database cache.) We do not have to directly simulate updating the log file directory information because it will normally be updated and flushed to disk as a result of flushing the log file write to disk.

Note that the number of bytes being written to the log as part of each transaction can dramatically affect the transaction throughput. The test run used 256, which is a reasonable size log write. Your log writes may be different. To determine your average log write size, use the db_stat utility to display your log statistics.

As a quick sanity check, the average seek time is 9.4 ms for this particular disk, and the average latency is 4.17 ms. That results in a minimum requirement for a data transfer to the disk of 13.57 ms, or a maximum of 74 transfers per second. This is close enough to the previous 60 operations per second (which wasn't done on a quiescent disk) that the number is believable.


## See Also
* [Selecting a Cache Size](xref:selecting-a-cache-size.md)
* @Figaro.EnvConfig