# Reading and Writing Database Records

When reading and writing database records, be aware that there are some slight differences in behavior depending on whether your database supports duplicate records. Two or more database records are considered to be duplicates of one another if they share the same key. The collection of records sharing the same key are called a duplicates set. In DB, a given key is stored only once for a single duplicates set. 

By default, DB databases do not support duplicate records. Where duplicate records are supported, cursors (see below) are typically used to access all of the records in the duplicates set. 

DB provides two basic mechanisms for the storage and retrieval of database key/data pairs: 
- The @Database.Put and @Database.Get methods provide the easiest access for all non-duplicate records in the database. These methods are described in this section. 
- Cursors provide several methods for putting and getting database records. Cursors and their database access methods are described in [Using Cursors](using-cursors.md).

## Writing Records to the Database

Records are stored in the database using whatever organization is required by the access method that you have selected. In some cases (such as BTree), records are stored in a sort order that you may want to define (see Setting Comparison Functions for more information). 

In any case, the mechanics of putting and getting database records do not change once you have selected your access method, configured your sorting routines (if any), and opened your database. From your code's perspective, a simple database put and get is largely the same no matter what access method you are using. 

You use @Database.Put to put, or write, a database record. This method requires you to provide the record's key and data in the form of a pair of @BerkeleyDB.DatabaseEntry objects. You can also provide one or more flags that control DB's behavior for the database write. 

If you're looking to prevent duplicates, use the @Database.PutNoDuplicate(DatabaseEntry,DatabaseEntry) method. Using this method will throw a @DatabaseException if a duplicate record exists.


```
    float money = 122.45f;
    string description = "grocery bill";
    DatabaseEntry key = new DatabaseEntry(BitConverter.GetBytes(money)); //new DatabaseEntry(money, sizeof(float));
    DatabaseEntry value = new DatabaseEntry(Encoding.UTF8.GetBytes(description));
    try
    {
    btree.PutNoDuplicate(key, value);
    }
    catch (DatabaseException ex)
    {
    Console.WriteLine($"{ex.GetType()}: {ex.Message}");
    }
```

## Getting Records from the Database

You can use the @Database.Get method to retrieve database records. Note that if your database supports duplicate records, then by default this method will only return the first record in a duplicate set. For this reason, if your database supports duplicates, the common solution is to use a cursor to retrieve records from it. Cursors are described in [Using Cursors](using-cursors.md). 

(You can also retrieve a set of duplicate records using a bulk get. To do this, you use the @Database.GetMultiple method. For more information, see the DB Programmer's Reference Guide). 

By default, @Database.Get returns the first record found whose key matches the key provide on the call to this method. If your database supports duplicate records, you can change this behavior slightly by using @Database.GetBoth. This method returns the first record that matches the provided key and data. 
If the specified key and/or data does not exist in the database, the method will throw a @DatabaseException .

```
      try
      {
        btree.GetBoth(key, value);
      }
      catch (DatabaseException ex)
      {
        Console.WriteLine($"{ex.GetType()}: {ex.Message}");
      }
```

## Deleting Records
You can use the @Database.Delete method to delete a record from the database. If your database supports duplicate records, then all records associated with the provided key are deleted. To delete just one record from a list of duplicates, use a cursor. Cursors are described in [Using Cursors](using-cursors.md). 
You can also delete every record in the database by using @Database.Truncate.

## Database Persistence
When you perform a database modification, your modification is made in the in-memory cache. This means that your data modifications are not necessarily flushed to disk, and so your data may not appear in the database after an application restart. 

Note that as a normal part of closing a database, its cache is written to disk. However, in the event of an application or system failure, there is no guarantee that your databases will close cleanly. In this event, it is possible for you to lose data. Under extremely rare circumstances, it is also possible for you to experience database corruption. 

Therefore, if you care if your data is durable across system failures, and to guard against the rare possibility of database corruption, you should use transactions to protect your database modifications. Every time you commit a transaction, DB ensures that the data will not be lost due to application or system failure. Transaction usage is described in the _Berkeley DB Getting Started with Transaction Processing_ guide. 

If you do not want to use transactions, then the assumption is that your data is of a nature that it need not exist the next time your application starts. You may want this if, for example, you are using DB to cache data relevant only to the current application runtime. 

If, however, you are not using transactions for some reason and you still want some guarantee that your database modifications are persistent, then you should periodically call @Database.Sync. Syncs cause any dirty entries in the in-memory cache and the operating system's file cache to be written to disk. As such, they are quite expensive and you should use them sparingly. 

Remember that by default a sync is performed any time a non-transactional database is closed cleanly. (You can override this behavior by setting the `sync` parameter to `false` on the call to @Database.Close.) That said, you can manually run a sync by calling @Database.Sync. 

>[!NOTE]
>If your application or system crashes and you are not using transactions, then you should either discard and recreate your databases, or verify them. You can verify a database using @Databse.Verify. If your databases do not verify cleanly, use the __db_dump__ command to salvage as much of the database as is possible. Use either the `-R` or `-r` command line options to control how aggressive __db_dump__ should be when salvaging your databases. 