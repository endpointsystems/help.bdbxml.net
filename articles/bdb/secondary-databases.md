# Secondary Databases

Usually you find database records by means of the record's key. However, the key that you use for your record will not always contain the information required to provide you with rapid access to the data that you want to retrieve. For example, suppose your database contains records related to users. The key might be a string that is some unique identifier for the person, such as a user ID. Each record's data, however, would likely contain a complex object containing details about people such as names, addresses, phone numbers, and so forth. While your application may frequently want to query a person by user ID (that is, by the information stored in the key), it may also on occasion want to locate people by, say, their name. 

Rather than iterate through all of the records in your database, examining each in turn for a given person's name, you create indexes based on names and then just search that index for the name that you want. You can do this using __secondary databases__. In DB, the database that contains your data is called a __primary database__. A database that provides an alternative set of keys to access that data is called a __secondary database__. In a secondary database, the keys are your alternative (or secondary) index, and the data corresponds to a primary record's key. 

You create a secondary database by creating the database, opening it, and then __associating__ the database with the primary database (that is, the database for which you are creating the index). As a part of associating the secondary database to the primary, you must provide a callback that is used to create the secondary database keys. Typically this callback creates a key based on data found in the primary database record's key or data. 

Once opened, DB manages secondary databases for you. Adding or deleting records in your primary database causes DB to update the secondary as necessary. Further, changing a record's data in the primary database may cause DB to modify a record in the secondary, depending on whether the change forces a modification of a key in the secondary database. 

>[!NOTE]
> You can not write directly to a secondary database. Any attempt to write to a secondary database results in a non-zero status return. To change the data referenced by a secondary record, modify the primary database instead. The exception to this rule is that delete operations are allowed on the secondary database. See [Deleting Secondary Database Records](deleting-secondary-database-records.md) for more information. 

>[!NOTE]
> Secondary database records are updated/created by DB only if the key creator callback function returns 0. If a value other than 0 is returned, then DB will not add the key to the secondary database, and in the event of a record update it will remove any existing key. Note that the callback can use either DB_DONOTINDEX or some error code outside of DB's name space to indicate that the entry should not be indexed. 

See [Implementing Key Extractors](implementing-key-extractors.md) for more information.

When you read a record from a secondary database, DB automatically returns the data and optionally the key from the corresponding record in the primary database. 

## Opening and Closing Secondary Databases

You manage secondary database opens and closes in the same way as you would any normal database. The only difference is that: 

- You must associate the secondary to a primary database using the `SecondaryBTreeDatabaseConfig` constructor.
- When closing your databases, it is a good idea to make sure you close your secondaries before closing your primaries. This is particularly true if your database closes are not single threaded. 

When you associate a secondary to a primary database, you must provide a callback that is used to generate the secondary's keys. These callbacks are described in the next section. 

For example, to open a secondary database and associate it to a primary database: 
```
      db = BTreeDatabase.Open(Path.Combine(dataPath,databaseName +".db"),cfg);

      //set up secondary database
      var scfg = new SecondaryBTreeDatabaseConfig(db, KeyGenFunc){Creation = CreatePolicy.IF_NEEDED}; 
      indexDb = SecondaryBTreeDatabase.Open(Path.Combine(dataPath,databaseName +"-index.db"),scfg);

```

Closing the primary and secondary databases is accomplished exactly as you would for any database: 

```
private void Dispose(bool disposing)
{
    if (!disposing) return;
    cursor?.Close();
    indexDb?.Close(true);
    indexDb?.Dispose();
    db?.Close(true);
    db?.Dispose();
}
```


