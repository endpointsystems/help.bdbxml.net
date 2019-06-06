---
uid: the-in-memory-transaction-example.md
---

# The In-Memory Transaction Example

Some applications use XML documents in a transient manner. That is, they create and store XML documents as a part of their run time, but there is no need for the documents to persist between application restarts. For these class of applications, overall throughput can be improved by abandoning the transactional durability guarantee. To do this, you keep your environment, containers, and logs entirely in-memory so as to avoid the performance impact of unneeded disk I/O.


To do this:
* Refrain from specifying a home directory when you open your environment.
* Configure your environment to back your regions from system memory instead of the file system.
* Configure your logging subsystem such that log files are kept entirely in-memory.
* Increase the size of your in-memory log buffer so that it is large enough to hold the largest set of concurrent write operations.
* Increase the size of your in-memory cache so that it can hold your entire data set. You do not want your cache to page to disk.
* Specify an empty string when you open your container. Note that for in-memory operations, you are limited to just one container.

As an example, this section takes the transaction example provided in [Transaction Example](xref:transaction-example.md) and it updates that example so that the environment, container, log files, and regions are all kept entirely in-memory.


``` C#
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Figaro;
using Wintellect.Threading.AsyncProgModel;
using Wintellect.CommandArgumentParser;

namespace Figaro.Documentation.Examples
{

internal class InMemoryTransApp
{
#region envOpenOptions
// Set the normal flags for a transactional subsystem. Note that
// we DO NOT specify Recover.
private const EnvOpenOptions envOpenOptions =
//create the environment if it doesn't exist
EnvOpenOptions.Create |
//run normal recovery
EnvOpenOptions.Recover |
//initialize locking
EnvOpenOptions.InitLock |
//initialize logging
EnvOpenOptions.InitLog |
//initialize the cache
EnvOpenOptions.InitMemoryBufferPool |
//Region files are not backed by the file system.
//Instead, they are backed by heap memory.
EnvOpenOptions.Private |
//free-threaded environment handle
EnvOpenOptions.Thread |
//initialize transactions
EnvOpenOptions.InitTransaction;
#endregion

public struct ThreadInfo
{
public Container container;
public bool useReadCommitted;
public int numNodes;
} ;

#region containerSettings
private const ContainerSettings containerSettings =
//Create if it doesn't exist.
ContainerSettings.Create |
//Support transactions.
ContainerSettings.Transactional |
//Create a free-threaded container
ContainerSettings.Thread;
#endregion

private static XmlContainerType containerType = XmlContainerType.NodeContainer;
private static CmdArgOpts cmdArgs;
public delegate void TransactionWorkDelegate(ThreadInfo info);
private static void Main(string[] args)
{
cmdArgs = CmdArgOpts.Parse(args);

if (cmdArgs.ShowUsage) return;

//show our command-line settings
//cmdArgs.Validate();

if (cmdArgs.WholeDocStorage)
containerType = XmlContainerType.WholeDocContainer;
else
containerType = XmlContainerType.NodeContainer;

//if our environment already exists, delete everything and rebuild it
//BuildDir(cmdArgs.HomeDirectory, true);
FigaroEnv environment = new FigaroEnv();
try
{
//specify in-memory logging
environment.SetEnvironmentOption(EnvConfig.LogInMemory, true);
//set the size of the in-memory log buffer
environment.LogBufferSize = 10*1024*1024;

//specify the size of the in-memory cache
environment.SetCacheSize(new EnvCacheSize(0,10*1024*1024),1);

// Indicate that we want to internally perform deadlock
// detection.  Using the Random policy will ensure you have
// the best chance of deadlock detection and management --
// keeping in mind that we're performing some pretty static
// behavior in this test application.
environment.DeadlockDetectPolicy = DeadlockDetectType.Random;

environment.Open(string.Empty, envOpenOptions);
using (XmlManager mgr = new XmlManager(environment))
{
if (mgr.ExistsContainer(testdb))
mgr.RemoveContainer(testdb);

using (Container myContainer =
mgr.CreateContainer(testdb, containerSettings,
containerType))
{
try
{
ThreadInfo threadInfo = new ThreadInfo();
threadInfo.container = myContainer;
threadInfo.numNodes = cmdArgs.NodesPerDocument;
ThreadPool.SetMinThreads(cmdArgs.NumThreads, cmdArgs.NumThreads);
AsyncEnumerator ae = new AsyncEnumerator();
ae.Execute(RunWriterThreads(ae,threadInfo));
}
catch (FigaroException)
{
Debugger.Break();
}
finally
{
myContainer.Close();
}
}
}
}
catch (FigaroException)
{
Debugger.Break();
}
}

public static IEnumerator<Int32> RunWriterThreads(AsyncEnumerator ae, ThreadInfo info)
{
for (int i = 0; i < cmdArgs.NumThreads; i++)
{
TransactionWorkDelegate twd = writerThread;
twd.BeginInvoke(info, ae.End(), null);
}
yield return cmdArgs.NumThreads;

for (int i = 0; i < cmdArgs.NumThreads; i++)
{
ae.DequeueAsyncResult();
}
}

/// <summary>
/// A function that performs a series of writes to a
/// Figaro container.
/// The mechanism of transactional commit/abort and
/// deadlock detection is illustrated here.
/// </summary>
/// <param name="threadInfo"> The Figaro objects used to insert documents.</param>
private static void writerThread(ThreadInfo threadInfo)
{
int maxRetries = 30;
int numDeadlocks = 0;

XmlManager mgr = threadInfo.container.GetManager();
TransactionType transType = threadInfo.useReadCommitted
? TransactionType.ReadCommitted
: TransactionType.None;
StringBuilder contents = new StringBuilder();
System.Random random = new Random();
Console.WriteLine("thread start thread id: {0}", Thread.CurrentThread.ManagedThreadId);
for (int h = 0; h < 50; h++)
{
bool retry = true;
int retryCount = 0;
while (retry)
{
try
{
XmlTransaction transaction = mgr.CreateTransaction(transType);
try
{
UpdateContext updateContext = mgr.CreateUpdateContext();
for (int i = 0; i < 10; i++)
{
contents = new StringBuilder();
string fileName = Guid.NewGuid().ToString("N");
contents.Append("<testDoc>");
for (int k = 0; k < threadInfo.numNodes; k++)
{
contents.AppendFormat("<payload>{0}{1}{2}</payload>", random.Next(), k, i);
}
contents.Append("</testDoc>");
//Console.WriteLine("contents: {0}",contents);
threadInfo.container.PutDocument(transaction, fileName, contents.ToString(),
updateContext,
PutDocumentOptions.None);
}
transaction.Commit();
retry = false;
}
catch (XmlException xxe)
{
//first thing we MUST do is abort the transaction
if (transaction != null) transaction.Abort();

if (xxe.ExceptionCategory == FigaroExceptionCategory.Deadlock)
{

// Now we decide if we want to retry the operation.
// If we have retried less than max_retries,
// increment the retry count and goto retry.
if (retryCount < maxRetries)
{
Console.WriteLine("{0} a deadlock occured - we must retry the transaction",
DateTime.Now);
retryCount++;
retry = true;
}
else
{
retry = false;
}
numDeadlocks++;
}
else
{
Console.WriteLine(xxe.Message);
if (transaction != null)
{
transaction.Abort();
transaction = null;
}
retry = false;
}
}
catch (NullReferenceException)
{
//first thing we MUST do is abort the transaction
if (transaction != null) transaction.Abort();
Console.WriteLine("Ran out of memory allocation transaction; adjust the transaction subsystem");
return;
}
catch (Exception)
{
//first thing we MUST do is abort the transaction
if (transaction != null) transaction.Abort();
Debugger.Break();
}
}
catch (NullReferenceException)
{
Console.WriteLine("Ran out of memory allocation transaction; adjust the transaction subsystem");
return;
}
catch(XmlException xe)
{
Debugger.Break();
}
catch (Exception)
{
Debugger.Break();
}
}
}

Console.WriteLine("total deadlocks in thread {0}: {1}", Thread.CurrentThread.ManagedThreadId,numDeadlocks);
}
}

internal sealed class CmdArgOpts: ICmdArgs
{

#region ICmdArgs Members

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional, Description = "Number of threads",
ArgName = "t", RequiredArg = false)]
public int NumThreads = 5;

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional, Description="Number of nodes per document",
ArgName = "n", RequiredArg = false)]
public int NodesPerDocument= 3;

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional,Description = "Use Read Committed isolation",
ArgName = "2",RequiredArg = false)]
public bool UseReadCommitted;

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional, Description = "Use Wholedoc storage",
ArgName = "w", RequiredArg = false)]
public bool WholeDocStorage ;

[CmdArg(ArgName = "?", Description = "Show this usage help")]
public Boolean ShowUsage;   // Default to not show usage

public void ProcessStandAloneArgument(string arg)
{
throw new NotImplementedException();
}

public void Usage(string errorInfo)
{
if (!string.IsNullOrEmpty(errorInfo))
{
// A command-line argument error occurred, report it to user
// errorInfo identifies the argument that is in error.
Console.WriteLine("Command-line switch error: {0}{1}", errorInfo,
Environment.NewLine);
}
Console.WriteLine(CmdArgParser.Usage(typeof(CmdArgOpts)));
}

public void Validate()
{
if (ShowUsage) return;
NumThreads = NumThreads < 1 ? 1 : NumThreads;
Console.WriteLine("number of threads: {0}", NumThreads);

NodesPerDocument = NodesPerDocument < 1 ? 1 : NodesPerDocument;
Console.WriteLine("nodes per document: {0}", NodesPerDocument);

string iso = UseReadCommitted ? "read committed" : "default";
Console.WriteLine("Isolation level: {0}", iso);

string storage = WholeDocStorage ? "Wholedoc storage" : "node storage";
Console.WriteLine("Container type: {0}", storage);

}
// Called to construct an instance of this class populating the fields from command-line arguments
public static CmdArgOpts Parse(String[] args)
{
CmdArgOpts options = new CmdArgOpts();
try
{
CmdArgParser.Parse(options, args);
}
catch(Exception e)//catch (Exception<InvalidCmdArgumentExceptionArgs> e)
{
if (!options.ShowUsage)
{
options.Usage(e.Message);
throw;
}
}
if (options.ShowUsage) options.Usage(null);
return options;
}
#endregion
}
}
```
