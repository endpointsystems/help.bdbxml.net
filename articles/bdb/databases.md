# Databases
In Berkeley DB, a database is a collection of _records_. Records, in turn, consist of key/data pairings. 

Conceptually, you can think of a database as containing a two-column table where column 1 contains a key and column 2 contains data. Both the key and the data are managed using Dbt class instances (see Database Records for details on this class ). So, fundamentally, using a DB database involves putting, getting, and deleting database records, which in turns involves efficiently managing information encapsulated by Dbt objects. The next several chapters of this book are dedicated to those activities. 

## Opening Databases
You open a database by using the `Open` method on the appropriate database type.

Note that by default, DB does not create databases if they do not already exist. To override this behavior, you set the `CreatePolicy` in the database configuration object.

The following code demonstrates a database open for the four different database types:
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
    }
  }
}
```
