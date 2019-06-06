---
uid: checkpoints.md
---

# Checkpoints

Before we can discuss file management, we need to describe checkpoints. When containers are modified (that is, a transaction is committed), the modifications are recorded in Figaro's logs, but they are not necessarily reflected in the actual container files on disk.

This means that as time goes on, increasingly more data is contained in your log files that is not contained in your data files. As a result, you must keep more log files around than you might actually need. Also, any recovery run from your log files will take increasingly longer amounts of time, because there is more data in the log files that must be reflected back into the data files during the recovery process.

You can reduce these problems by periodically running a checkpoint against your environment. The checkpoint:

* **Flushes dirty pages from the in-memory cache.** This means that data modifications found in your in-memory cache are written to the container files on disk. Note that a checkpoint also causes data dirtied by an uncommitted transaction to also be written to your container files on disk. In this latter case, Figaro's normal recovery is used to remove any such modifications that were subsequently abandoned by your application using a transaction abort. Normal recovery is described in [Recovery Procedures](xref:recovery-procedures.md).
* Writes a checkpoint record.
* Flushes the log. This causes all log data that has not yet been written to disk to be written.
* Writes a list of open containers.

There are several ways to run a checkpoint. One way is to use the [db_checkpoint](xref:db_checkpoint.md) command line utility. (Note, however, that this command line utility cannot be used if your environment was opened using @Figaro.EnvOpenOptions.Private.)


You can also run a thread that periodically checkpoints your environment for you by calling the @Figaro.FigaroEnv.SetEnvironmentTransactionCheckpoint(System.Boolean,System.UInt32,System.UInt32) method.


You can prevent a checkpoint from occurring unless more than a specified amount of log data has been written since the last checkpoint. You can also prevent the checkpoint from running unless more than a specified amount of time has occurred since the last checkpoint. These conditions are particularly interesting if you have multiple threads or processes running checkpoints.


Note that running checkpoints can be quite expensive. Figaro must flush every dirty page to the backing container files. On the other hand, if you do not run checkpoints often enough, your recovery time can be unnecessarily long and you may be using more disk space than you really need. Also, you cannot remove log files until a checkpoint is run. Therefore, deciding how frequently to run a checkpoint is one of the most common tuning activity for Figaro applications.



``` C#
	const EnvOpenOptions envOptions = EnvOpenOptions.TransactionDefaults | 
	    EnvOpenOptions.Create | EnvOpenOptions.Thread;
	var env = new FigaroEnv();
	env.Open(homePath, envOptions);
	var tb = new TimerCallback(delegate(object o)
	                               {
	                                   Trace.WriteLine("checkpoint!");
	                                   env.SetEnvironmentTransactionCheckpoint(true, 0, 0);
	                               });
	var cfg = new ContainerConfig
	              {
	                  AllowCreate = true,
	                  Transactional =  true,
	                  Threaded = true
	              };
	using (var mgr = new XmlManager(env, ManagerInitOptions.AllOptions))
	{
	    var trans = mgr.CreateTransaction();
	    Container container = null;
	    try
	    {
	        container = mgr.OpenContainer(trans, "checkpoint.dbxml", cfg);
	        trans.Commit();
	    }
	    catch(Exception)
	    {
	        trans.Abort();
	        Assert.Fail();
	        throw;
	    }
	    if (container == null)
	    {
	        Assert.Fail();
	        return;
	    }
	
	    var uc = mgr.CreateUpdateContext();
	    var timer = new Timer(tb, null, 250, 250);
	    foreach (string file in Directory.GetFiles(simpleData, "*.xml"))
	    {
	        trans = mgr.CreateTransaction(TransactionType.ReadUncommitted);
	        try
	        {
	            container.PutDocument(trans, file, uc);
	            trans.Commit();
	        }
	        catch(Exception)
	        {
	            trans.Abort();
	            Assert.Fail();
	            throw;
	        }
	    }
	
	    timer.Dispose();
	    container.Dispose();
	    env.SetErrorFile(null);
	    env.SetMessageFile(null);
	    env.Dispose();
	}
```


## See Also

@Figaro.FigaroEnv.SetEnvironmentTransactionCheckpoint(System.Boolean,System.UInt32,System.UInt32)

#### Other Resources
* [db_checkpoint](xref:db_checkpoint.md)
* [Recovery Procedures](xref:recovery-procedures.md)
* [Managing Database Files](xref:managing-database-files.md)
