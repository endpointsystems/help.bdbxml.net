# Closing Databases
Once you are done using the database, you must close it. You use the `Close` method to do this. 

Closing a database causes it to become unusable until it is opened again. It is recommended that you close any open cursors before closing your database. Active cursors during a database close can cause unexpected results, especially if any of those cursors are writing to the database. You should always make sure that all your database accesses have completed before closing your database. 

Cursors are described in [Using Cursors](using-cursors.md) later in this manual. 

Be aware that when you close the last open handle for a database, then by default its cache is flushed to disk. This means that any information that has been modified in the cache is guaranteed to be written to disk when the last handle is closed. You can manually perform this operation using the `Sync` method, but for normal shutdown operations it is not necessary. For more information about syncing your cache, see [Data Persistence](data-persistence.md). 

The following code fragment illustrates a database close:
```

namespace BerkeleyDB.Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      var btree = BTreeDatabase.Open("my_btree.db",new BTreeDatabaseConfig{Creation = CreatePolicy.IF_NEEDED });
      var hash = HashDatabase.Open("my_hash.db", new HashDatabaseConfig{Creation = CreatePolicy.IF_NEEDED});
      var queue = QueueDatabase.Open("my_queue.db", new QueueDatabaseConfig {Creation = CreatePolicy.IF_NEEDED});
      var recno = RecnoDatabase.Open("my_recno.db", new RecnoDatabaseConfig {Creation = CreatePolicy.IF_NEEDED});

      btree.Close();
      hash.Close();
      queue.Close();
      recno.Close();
    }
  }
}
```