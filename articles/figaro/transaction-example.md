---
uid: transaction-example.md
---

# Transaction Example

The following code provides a fully functional example of a multi-threaded transactional Figaro application. For improved portability across platforms, this examples uses [Jeffrey Richter's .NET Power Threading Library](https://github.com/Wintellect/PowerThreading) to provide threading support.


The example creates multiple threads, each of which creates a set number of XML documents that it then writes to the container. Each thread creates and writes 10 documents under a single transaction before committing and writing another 10 documents. This activity is repeated 50 times.


From the command line, you can tell the program to vary:

* Item 1
* Item 2
* The number of threads that it should use.
* The number of nodes each XML document will contain.
* Whether the container used by the program is of type `Wholedoc` or `node` storage.
* Whether read committed (degree 2) isolation should be used for the container writes.

As we will see in [Runtime Analysis](xref:runtime-analysis.md) each of these variables plays a role in the number of deadlocks the program encounters during its run time.


Of course, each writer thread performs deadlock detection as described in this article. In addition, normal recovery is performed when the environment is opened.



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

internal class TransactionDemoApp
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

#region data and database paths
private const string baseUri = @"D:\dev\db\samples\TransactionDemoApp";
private const string testdb = "TransactionDemoApp.dbxml";
private const string testdata = @"D:\dev\db\xmlData\nsData\";
#endregion

private static XmlContainerType containerType = XmlContainerType.NodeContainer;

private static CmdArgOptions cmdArgs;

public delegate void TransactionWorkDelegate(ThreadInfo info);

private static void Main(string[] args)
{
cmdArgs = CmdArgOptions.Parse(args);

if (cmdArgs.ShowUsage) return;

//show our command-line settings
//cmdArgs.Validate();

if (cmdArgs.WholeDocStorage)
containerType = XmlContainerType.WholeDocContainer;

//if our environment already exists, delete everything and rebuild it
//BuildDir(cmdArgs.HomeDirectory, true);
FigaroEnv environment = new FigaroEnv();
try
{
// Indicate that we want to internally perform deadlock
// detection.  Using the Random policy will ensure you have
// the best chance of deadlock detection and management --
// keeping in mind that we're performing some pretty static
// behavior in this test application.
environment.DeadlockDetectPolicy = DeadlockDetectType.Random;

environment.Open(cmdArgs.HomeDirectory, envOpenOptions);
using (XmlManager mgr = new XmlManager(environment))
{
if (mgr.ExistsContainer(testdb))
mgr.RemoveContainer(testdb);

using (Container myContainer =
mgr.CreateContainer(testdb, containerSettings,
XmlContainerType.WholeDocContainer))
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
string fileName = Guid.NewGuid().ToString(&quot;N&quot;);
contents.Append(&quot;&lt;testDoc&gt;&quot;);
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
if (xxe.ExceptionCategory == FigaroExceptionCategory.Deadlock)
{
//first thing we MUST do is abort the transaction
if (transaction != null) transaction.Abort();

// Now we decide if we want to retry the operation.
// If we have retried less than max_retries,
// increment the retry count and goto retry.
if (retryCount < maxRetries)
{
Console.WriteLine(&quot;{0} a deadlock occured - we must retry the transaction&quot;,
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
Console.WriteLine("Ran out of memory allocation transaction; adjust the transaction subsystem");
return;
}
catch (Exception)
{
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

private static void BuildDir(string directory, bool reCreate)
{
if (!Directory.Exists(directory))
{
Directory.CreateDirectory(directory);
return;
}
if (reCreate)
{
Directory.Delete(directory, true);
Directory.CreateDirectory(directory);
}
}
}

internal sealed class CmdArgOptions: ICmdArgs
{

#region ICmdArgs Members

[CmdArg(RequiredValue = CmdArgRequiredValue.Yes, Description = "Database home directory",
ArgName = "h",RequiredArg = true)]
public string HomeDirectory = string.Empty;

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional, Description = "Number of threads",
ArgName = "t", RequiredArg = false)]
public int NumThreads = 5;

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional, Description="Number of nodes per document",
ArgName = "n", RequiredArg = false)]
public int NodesPerDocument = 3;

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional,Description = "Use Read Committed isolation",
ArgName = "2",RequiredArg = false)]
public bool UseReadCommitted = false;

[CmdArg(RequiredValue = CmdArgRequiredValue.Optional, Description = "Use Wholedoc storage",
ArgName = "w", RequiredArg = false)]
public bool WholeDocStorage = false;

[CmdArg(ArgName = "?", Description = "Show this usage help")]
public Boolean ShowUsage = false;   // Default to not show usage

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
Console.WriteLine(CmdArgParser.Usage(typeof(CmdArgOptions)));
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
public static CmdArgOptions Parse(String[] args)
{
CmdArgOptions options = new CmdArgOptions();
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

##See Also

* [Jeffrey Richter's .NET Power Threading Library](https://github.com/Wintellect/PowerThreading)
* [Runtime Analysis](xref:runtime-analysis.md)