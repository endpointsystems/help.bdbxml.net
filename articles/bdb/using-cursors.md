# Using Cursors

Cursors provide a mechanism by which you can iterate over the records in a database. Using cursors, you can get, put, and delete database records. If a database allows duplicate records, then cursors are the easiest way that you can access anything other than the first record for a given key. 

Using the Berkeley DB .NET library, we have the added benefit of our `Cursor` object being a LINQ IEnumerable of @BerkeleyDB.Core.DatabaseEntry `KeyValuePair` objects. This gives us a lot of added flexibility when it comes to 

This section introduces cursors. It explains how to open and close them, how to use them to modify databases, and how to use them with duplicate records. 

Cursors are obtained by calling the @BerkeleyDb.Core.Database.Cursor method:

```
db = BTreeDatabase.Open(Path.Combine(dataPath,databaseName +".db"),cfg);
var cursor = db.Cursor();
```

When you're done with a cursor you should close it using the `Close` method. 

>[!NOTE]
>Closing your database while cursors are still opened can have unpredictable results. It is recommended that you close all cursor handles after their use to ensure concurrency and to release resources such as page locks. 

If we go back to our sample project in the [database usage sample](db-usage-sample.md) and add a cursor to our `Repository` object, we get something that looks like this:

```
public abstract class Repository: IDisposable
  {
    protected string path;
    protected BTreeDatabase db;
    protected Cursor cursor;

    protected Repository(string dataPath, string databaseName)
    {
      path = dataPath;
      var cfg = new BTreeDatabaseConfig
      {
        Creation = CreatePolicy.IF_NEEDED, CacheSize = new CacheInfo(1, 0, 1),
        ErrorFeedback = (prefix, message) =>
        {
          var fg = Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine($"{prefix}: {message}");
          Console.ForegroundColor = fg;
        },
        ErrorPrefix = databaseName,Duplicates = DuplicatesPolicy.SORTED,        
      };
      db = BTreeDatabase.Open(Path.Combine(dataPath,databaseName +".db"),cfg);
      
      cursor = db.Cursor();
    }
    // ...
```
## Searching for Records

You can use cursors to search for database records. You can search based on just a key, or you can search based on both the key and the data. You can also perform partial matches if your database supports sorted duplicate sets. In all cases, the key and data parameters of these methods are filled with the key and data values of the database record to which the cursor is positioned as a result of the search. 

Using a cursor is as simple as iterating through the `KeyValuePair` items containing the key and the data. 

In our repository code, we could add something like this:

```
public void FindKey(string keyValue)
  {
    var pairs = (from c in cursor where Encoding.UTF8.GetString(c.Key.Data).Contains(keyValue) select c).ToList();
    var watch = new Stopwatch();
    watch.Start();
    foreach (var pair in pairs)
    {
      Console.WriteLine($"\t{Encoding.UTF8.GetString(pair.Key.Data)}");
    }
    watch.Stop();
    Console.WriteLine($"task completed in {watch.Elapsed.Seconds} seconds.");
    Console.WriteLine($"found {pairs.Count()} records.");      
    Console.WriteLine("---");
  }
```
And then in our main code we will do a keyword search for the word `Lorem`:

```
// find vendors with the word 'Lorem' in their key
vendorRepo.FindKey("Lorem");
```

