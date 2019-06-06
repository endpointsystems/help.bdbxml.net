---
uid: configuring-in-memory-logging.md
---

# Configuring In-Memory Logging

It is possible to configure your logging subsystem such that logs are maintained entirely in memory. When you do this, you give up your transactional durability guarantee. Without log files, you have no way to run recovery so any system or software failures that you might experience can corrupt your containers.


However, by giving up your durability guarantees, you can greatly improve your application's throughput by avoiding the disk I/O necessary to write logging information to disk. In this case, you still retain your transactional atomicity, consistency, and isolation guarantees.


To configure your logging subsystem to maintain your logs entirely in-memory:

* Make sure your log buffer is capable of holding all log information that can accumulate during the longest running transaction. See [Setting the In-Memory Log Buffer Size](xref:setting-the-in-memory-log-buffer-size.md) for details.
* Do not run normal recovery when you open your environment. In this configuration, there are no log files available against which you can run recovery. As a result, if you specify recovery when you open your environment, it is ignored.
* Specify @Figaro.EnvConfig.LogInMemory to the @Figaro.FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,System.Boolean) method. Note that you must specify this before your application opens its first environment handle.

For example:

``` C#
// Set the normal flags for a transactional subsystem. Note that
// we do NOT specify Recover. 
const EnvOpenOptions envOpenOptions =
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

    ContainerConfig cfg = new ContainerConfig
                              {
                                  AllowCreate = true,
                                  Transactional = true
                              };

    var env = new FigaroEnv();
    // Indicate that logging is to be performed only in memory. 
    // Doing this means that we give up our transactional durability
    // guarantee.
    env.SetEnvironmentOption(EnvConfig.LogInMemory, true);
    // Configure the size of our log memory buffer. This must be
    // large enough to hold all the logging information likely
    // to be created for our longest running transaction. The
    // default size for the logging buffer is 1 MB when logging
    // is performed in-memory. For this example, we arbitrarily
    // set the logging buffer to 5 MB.
    env.SetLogBufferSize(5*1024*1024);
    env.Open(homePath, envOpenOptions);

    using (var mgr = new XmlManager(env, ManagerInitOptions.AllOptions))
    {
        var trans = mgr.CreateTransaction();
        Container container = null;
        try
        {
            container = mgr.OpenContainer(trans, Path.Combine(homePath, "inMemory.dbxml"), cfg);
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
        var watch = new Stopwatch();
        watch.Start();
        var uc = mgr.CreateUpdateContext();
        foreach (string file in Directory.GetFiles(simpleData,"*.xml"))
        {
            trans = mgr.CreateTransaction();
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
        watch.Stop();
        trace("loaded in-memory log container in {0} ms", watch.ElapsedMilliseconds);
    }
```


## See Also
* @Figaro.EnvConfig.LogInMemory
* @Figaro.FigaroEnv.SetEnvironmentOption(Figaro.EnvConfig,System.Boolean)

#### Other Resources
* [Setting the In-Memory Log Buffer Size](xref:setting-the-in-memory-log-buffer-size.md)
* [Configuring the Logging Subsystem](xref:configuring-the-logging-subsystem.md)
