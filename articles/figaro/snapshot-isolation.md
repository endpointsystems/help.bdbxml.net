---
uid: snapshot-isolation.md
---

# Snapshot Isolation

By default Figaro uses serializable isolation. An important side effect of this isolation level is that read operations obtain read locks on database pages, and then hold those locks until the read operation is completed. You can avoid this by using snapshot isolation. Snapshot isolation uses multi-version concurrency control to guarantee repeatable reads. What this means is that every time a writer would take a read lock on a page, instead a copy of the page is made and the writer operates on that page copy. This frees other writers from blocking due to a read lock held on the page.

>[!NOTE]
> Snapshot isolation is strongly recommended for read-only threads when writer threads are also running, as this will eliminate read-write contention and greatly improve transaction throughput for your writer threads. However, in order for snapshot isolation to work for your reader-only threads, you must of course use transactions for your Figaro reads.

## Snapshot Isolation Costs

Snapshot isolation does not come without a cost. Because pages are being duplicated before being operated upon, the cache will fill up faster. This means that you might need a larger cache in order to hold the entire working set in memory.


If the cache becomes full of page copies before old copies can be discarded, additional I/O will occur as pages are written to temporary "freezer" files on disk. This can substantially reduce throughput, and should be avoided if possible by configuring a large cache and keeping snapshot isolation transactions short.


You can estimate how large your cache should be by taking a checkpoint, followed by a call to the @Figaro.FigaroEnv.LogArchive(Figaro.LogArchiveOptions) method. The amount of cache required is approximately double the size of the remaining log files (that is, the log files that cannot be archived).

## Snapshot Isolation Transactional Requirements

In addition to an increased cache size, you may also need to increase the maximum number of transactions that your application supports. (See [Configuring the Transaction Subsystem](xref:configuring-the-transaction-subsystem.md) for details on how to set this.) In the worst case scenario, you might need to configure your application for one more transaction for every page in the cache. This is because transactions are retained until the last page they created is evicted from the cache.

## When To Use Snapshot Isolation

Snapshot isolation is best used when all or most of the following conditions are true:

* You have a *large cache* relative to its working size.
* You require repeatable reads.
* You will be using transactions that routinely work on the entire database, or more commonly, there is data in your database that will be very frequently written by more than one transaction.
* If your application uses a single write thread and multiple readers, then snapshot isolation can help performance. However, if your application uses multiple write threads, then snapshot isolation can result in additional deadlocks that may harm your application's performance.

## How to use Snapshot Isolation

You use snapshot isolation by:

* Opening the container with multi-version support. You can configure this either when you open your environment or when you open your container. Use the @Figaro.EnvConfig.MultiVersion flag to configure this support.
* Configure your transaction to use snapshot isolation. To do this, pass the @Figaro.TransactionType.SnapshotTransaction flag when you create the transaction.

The simplest way to take advantage of snapshot isolation is for queries: keep update transactions using full read/write locking and use snapshot isolation on read-only transactions or cursors. This should minimize blocking of snapshot isolation transactions and will avoid deadlock errors.


If the application has update transactions which read many items and only update a small set (for example, scanning until a desired record is found, then modifying it), throughput may be improved by running some updates at snapshot isolation as well. But doing this means that you must manage deadlock errors. See [Resolving Deadlocks](xref:resolving-deadlocks.md) > for details.


The following code fragment turns on snapshot isolation for a transaction:

``` C#
using System;
using System.IO;
using Figaro;

namespace Figaro.Documentation.Examples
{
internal class SnapshotIsolation
{

private const EnvOpenOptions envOpenOptions =
//create the environment if it doesn't exist
EnvOpenOptions.Create |
//initialize locking
EnvOpenOptions.InitLock |
//initialize logging
EnvOpenOptions.InitLog |
//initialize the cache
EnvOpenOptions.InitMemoryBufferPool |
//free-threaded environment handle
EnvOpenOptions.Thread |
//initialize transactions
EnvOpenOptions.InitTransaction;

private const ContainerSettings containerSettings =
//Create if it doesn't exist.
ContainerSettings.Create |
//Support transactions.
ContainerSettings.Transactional;

private const string baseUri = @"C:\dev\db\samples\SnapshotIsolation";
private const string testdb = "SnapshotIsolation.dbxml";
private static void Main()
{
FigaroEnv environment = new FigaroEnv();
try
{
environment.Open(baseUri, envOpenOptions);

//support snapshot isolation.
environment.SetEnvironmentOption(EnvConfig.MultiVersion, true);

using (XmlManager mgr = new XmlManager(environment))
{
using (Container myContainer =
mgr.CreateContainer(testdb, containerSettings,
XmlContainerType.WholeDocContainer))
{
var transaction =
mgr.CreateTransaction(TransactionType.SnapshotTransaction);
}
}
}
catch (FigaroException fe)
{
Debugger.Break();
}
}
}
}
```

