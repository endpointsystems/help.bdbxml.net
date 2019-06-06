# Database Example

Throughout this book we will build a couple of applications that load and retrieve inventory data from DB databases. While we are not yet ready to begin reading from or writing to our databases, we can at least create the class that we will use to manage our databases. 

Note that subsequent examples in this book will build on this code to perform the more interesting work of writing to and reading from the databases. 

## Example 2.1 MyDb Class 
To manage our database open and close activities, we encapsulate them in the `MyDb` class. There are several good reasons to do this, the most important being that we can ensure our databases are closed by putting that activity in the `MyDb` class destructor. 
To begin, we create our class definition: 

```
using System;
using System.IO;
using BerkeleyDB;

namespace ConsoleApp1
{
  class MyDb
  {
    private readonly BTreeDatabase db;
    private readonly string dbPath;
    private readonly string dbName;

    public MyDb(string path, string dbName)
    {
      try
      {
        dbPath = Path.Combine(path, dbName, ".db");
        this.dbName = dbName;
        //create the database if it doesn't exist, and write all error feedback to the console.
        var cfg = new BTreeDatabaseConfig
        {
          Creation = CreatePolicy.IF_NEEDED,
          ErrorFeedback = (prefix, message) => { Console.WriteLine($"{prefix}: {message}"); }
        };
        db = BTreeDatabase.Open(dbPath, cfg);

      }
      catch (DatabaseException ex)
      {
        Console.WriteLine($"{ex.GetType()} opening databse: {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{ex.GetType()} opening databse: {ex.Message}");
      }
    }

    ~MyDb()
    {
      close();
    }

    private void close()
    {
      try
      {
        db?.Close();
      }
      catch (DatabaseException ex)
      {
        Console.WriteLine($"error closing database {dbName}: {ex.Message} ");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"error closing database {dbName}: {ex.Message} ");
      }
    }
  }
}
```