---
uid: committed-reads.md
---

# Committed Reads


Committed reads control the behavior of a Berkeley DB mechanism called a _cursor_. Cursors are not something you would normally be using directly with your Figaro application, unless you are using Berkeley DB databases alongside of your Figaro containers. 

You can configure your transaction so that the data being read by a transactional cursor is consistent so long as it is being addressed by the cursor. However, once the cursor is done reading the record (that is, reading records from the page that it currently has locked), the cursor releases its lock on that record or page. This means that the data the cursor has read and released may change before the cursor's transaction has completed.


For example, suppose you have two transactions, `Ta` and `Tb`. Suppose further that `Ta` has a cursor that reads record `R`, but does not modify it. Normally, Tb would then be unable to write record `R` because `Ta` would be holding a read lock on it. But when you configure your transaction for committed reads, `Tb` can modify record `R` before `Ta` completes, so long as the reading cursor is no longer addressing the record or page.


When you configure your application for this level of isolation, you may see better performance throughput because there are fewer read locks being held by your transactions. Read committed isolation is most useful when you have a cursor that is reading and/or writing records in a single direction, and that does not ever have to go back to re-read those same records. In this case, you can allow Figaro to release read locks as it goes, rather than hold them for the life of the transaction.


To configure your application to use committed reads, create your transaction such that it allows committed reads. You do this by specifying `DB_READ_COMMITTED` when you open the transaction.


For example, the following creates a transaction that allows committed reads:



``` C#
using System;
using System.Diagnostics;
using System.IO;
using Figaro;

namespace Figaro.Documentation.Examples
{
internal class CommittedReads
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

// Notice that we do not have to specify any flags to the
// container to allow committed reads (this is as opposed
// to uncommitted reads where we DO have to specify a
// flag on the container open).
private const ContainerSettings containerSettings =
ContainerSettings.Create |
ContainerSettings.Transactional;

private const string baseUri = @"C:\dev\db\samples\CommittedReads";
private const string testdb = "CommittedReads.dbxml";

private static void Main()
{
BuildDir(baseUri, true);
FigaroEnv environment = new FigaroEnv();
try
{
environment.Open(baseUri, envOpenOptions);
using (XmlManager mgr = new XmlManager(environment))
{
using (Container myContainer =
mgr.CreateContainer(testdb, containerSettings,
XmlContainerType.WholeDocContainer))
{
try
{
// Transaction handle
// Open the transaction and enable committed reads.  All
// queries performed with this transaction handle will
// use read committed isolation.
XmlTransaction myTransaction =
mgr.CreateTransaction(TransactionType.ReadCommitted);

// From here, you perform your container reads and writes as normal,
// committing and aborting the transactions as is necessary,  as well as
// testing for deadlock exceptions as normal (omitted for brevity).
}
finally
{
myContainer.Close();
}
}
}
}
catch (FigaroException fe)
{
//handle the exception
}
}
}
}
```

