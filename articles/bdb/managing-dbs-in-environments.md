# Managing Databases in Environments

In [Environments](environments.md) we introduced environments.  While environments are not used in the example built in this book, they are so commonly used for a wide class of DB applications that it is necessary to show their basic usage, if only from a completeness perspective. 

To use an environment, you must first open it. At open time, you must identify the directory in which it resides. This directory must exist prior to the open attempt. You can also identify open properties, such as whether the environment can be created if it does not already exist. 

You will also need to initialize the in-memory cache when you open your environment. 

For example, to create an environment handle and open an environment: 

```

using System;

namespace BerkeleyDB.Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      var envConfig = new DatabaseEnvironmentConfig
      {
        Create = true,
        UseMPool = true,
        MPoolSystemCfg = new MPoolConfig {CacheSize = new CacheInfo(1, 0, 0)}
      };

      //initialize a database environment and assign it to the BTree database instance.
      var env = DatabaseEnvironment.Open(@"C:\temp\BerkeleyDb.Demo", envConfig);
      var btree = BTreeDatabase.Open("my_btree.db",new BTreeDatabaseConfig{Creation = CreatePolicy.IF_NEEDED,ReadOnly = false, Truncate = false, Env = env});
      btree.Msgfile = @"C:\temp\btree-msg.log";
      //set the prefix to the database name so we know which database made it
      btree.ErrorPrefix = "my_btree";
      btree.ErrorFeedback = (prefix, message) => { Console.WriteLine($"{prefix}: {message}"); };
      btree.Feedback = (opcode, percent) => { Console.WriteLine($"{opcode.ToString()} at {percent}%"); };
      
      // ...

      //close the environment - this will also close the database that's open.
      env.Close();
    }
  }
}

```