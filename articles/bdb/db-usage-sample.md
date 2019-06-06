# Database Usage Sample

In the first [database example](db-example.md) we created a class that opens and closes a database for us. In this example we expand and formalize our data access through the use of a [repository pattern](https://msdn.microsoft.com/en-us/library/ff649690.aspx), abstracting our database operations for us. 

>[!NOTE]
> Full source code this and other examples can be found at our GitHub repository at https://github.com/endpointsystems/BerkeleyDB.Core.Examples 

In this example we have two item types we're trying to store - vendors and vendor inventory. Using the random data generator at [generatedata.com](http://generatedata.com/), we generate 50,000 vendors with five inventory items for each vendor, for a total of 250k items. Our incoming data is stored in CSV format, so we'll use the [CsvHelper](https://www.nuget.org/packages/CsvHelper/) NuGet package as well.

Let's take a look at our data structures:

```
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using CsvHelper.Configuration;

namespace GettingStarted.Writing
{
    [Serializable]
    public class Vendor
    {
      public string VendorName;
      public string Street;
      public string City;
      public string State;
      public string Zip;
      public string PhoneNumber;
      public string SalesRep;
      public string SalesRepPhone;
    }

  public class VendorMap : ClassMap<Vendor>
  {
    public VendorMap()
    {
      Map(m => m.VendorName);
      Map(m => m.Street);
      Map(m => m.City);
      Map(m => m.State);
      Map(m => m.Zip);
      Map(m => m.PhoneNumber);
      Map(m => m.SalesRep);
      Map(m => m.SalesRepPhone);
    }
  }

  [Serializable]
  public class Inventory
  {
    public Inventory(string vendor)
    {
      Vendor = vendor;
    }

    public double Price;
    public int Quantity;
    public string Name;
    //this will also be our key, so we won't store it here
    //public string Sku;
    public string Category;
    public string Vendor;
  }

  public static class Extensions
  {
    public static byte[] ToByteArray(this string value)
    {
      return Encoding.Default.GetBytes(value);
    }

    public static byte[] ToByteArray(this Vendor vendor)
    {
      BinaryFormatter formatter = new BinaryFormatter();
      using (var ms = new MemoryStream())
      {
        formatter.Serialize(ms,vendor);
        //ms.Seek(0, SeekOrigin.Begin);
        return ms.ToArray();
      }
    }
    public static byte[] ToByteArray(this Inventory inv)
    {
      BinaryFormatter formatter = new BinaryFormatter();
      using (var ms = new MemoryStream())
      {
        formatter.Serialize(ms,inv);
        ms.Seek(0, SeekOrigin.Begin);
        return ms.ToArray();
      }
    }
  }
}
```
As you can see, we have two data types - Vendor and Inventory.  There are a couple of other things to take notice of:

- We use the Vendor name for the key in the Vendor database, and we use the product SKU for the key in our Inventory database.
- All of our data is saved into the system as byte arrays. Fortunately, this is easy to mark up in our data objects by using extension methods. 
- We're currently using [System.Runtime.Serialization.BinaryFormatter](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter?view=netcore-2.0) to serialize our objects, but we don't have to - we could use any formatter we please, or even save it in the original CSV format. 
- There isnt' much structure or logic to our application so far - we're just storing data as we pick it up. 

Let's take a look at our data repositories.

```
using System;
using System.IO;
using System.Text;
using BerkeleyDB.Core;

namespace GettingStarted.Writing
{
  public abstract class Repository: IDisposable
  {
    protected string path;
    protected BTreeDatabase db;
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
    }

    ~Repository()
    {
      Dispose(false);
    }

    protected void AddToDb(string keyval, byte[] dataval)
    {
      var key = new DatabaseEntry(Encoding.UTF8.GetBytes(keyval));
      var data = new DatabaseEntry(dataval);
      db.Put(key, data);
    }

    public void Sync()
    {
      Console.WriteLine("I'm syncing!");
      db.Sync();
    }

    private void Dispose(bool disposing)
    {
      if (!disposing) return;
      db?.Close(true);
      db?.Dispose();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
  /// <summary>
  /// The data repository for our Vendor database.
  /// </summary>
  public class VendorRepository: Repository
  {
    public VendorRepository(string dataPath) : base(dataPath, "vendor"){}

    public void AddVendor(Vendor v){AddToDb(v.VendorName,v.ToByteArray());}
  }

  /// <summary>
  /// The repository for our inventory database.
  /// </summary>
  public class InventoryRepository: Repository
  {
    public InventoryRepository(string dataPath) : base(dataPath,"inventory"){}
    public void AddInventory(string sku, Inventory inv){AddToDb(sku,inv.ToByteArray());}
  }
}
```
- All databases are of type @BTreeDatabase
- We implement the @System.IDisposable interface to make sure we synchronize __and__ close our databases as we free up our resources. This is important!
- We make the `Sync` method public so that we can be sure to manually flush our data to disk once we write it. We're going to need to do this manually since we're not using [Environments](environments.md) and/or the other settings that enable this to be done automatically, so we need to make sure it's done.

Lastly, let's take a look at our main code.

```
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CsvHelper;

namespace GettingStarted.Writing
{
    class Program
    {
        static void Main()
        {
          List<long> commitTimes = new List<long>();

          //this value was set in the project settings. We'll read CSVs and write DB files there.
          var dataPath = Environment.GetEnvironmentVariable("DATA_DIR");
          var vendorRepo = new VendorRepository(dataPath);
          var invRepo = new InventoryRepository(dataPath);
      
          var readTimer = new Stopwatch();
          readTimer.Start();
          var vr = new CsvReader(File.OpenText(Path.Combine(dataPath,"vendor-master.csv")));
          vr.Configuration.HasHeaderRecord = false;
          vr.Configuration.RegisterClassMap<VendorMap>();
          vr.Configuration.DetectColumnCountChanges = true;
          var ir = new CsvReader(File.OpenText(Path.Combine(dataPath,"inv-master.csv")));
          ir.Configuration.HasHeaderRecord = false;
          ir.Configuration.DetectColumnCountChanges = true;
    
          int z = 0;
          var writeTimer = new Stopwatch();
          Vendor vendor;
          Inventory inv;
          while (vr.Read())
          {
            writeTimer.Start();
            int y = z + 5;
            vendor = vr.GetRecord<Vendor>();
            vendorRepo.AddVendor(vendor);

            for (int x = z; x < y; x++)
            {
              if (ir.Read())
              {
                inv = new Inventory(vendor.VendorName)
                {
                  Category = ir.GetField(2),
                  Name = ir.GetField(3),
                  Price = ir.GetField<double>(0),
                  Quantity = ir.GetField<int>(1)
                };
                invRepo.AddInventory(ir.GetField(5), inv);
              }
              else
              {
                Console.WriteLine($"{DateTime.Now}\tno more inventory");
              }
            }
            z = y;
            writeTimer.Stop();
            commitTimes.Add(writeTimer.ElapsedTicks);
            writeTimer.Reset();
          }


          readTimer.Stop();
          Console.WriteLine("-----------------------------");
          Console.WriteLine();
          Console.WriteLine($"Read of CSV files took {readTimer.Elapsed.Minutes}:{readTimer.Elapsed.Seconds}.{readTimer.Elapsed.Milliseconds}.");
          readTimer.Reset();
          readTimer.Start();
          vendorRepo.Sync();
          invRepo.Sync();
          readTimer.Stop();
          Console.WriteLine($"syncing databases took {readTimer.ElapsedMilliseconds} milliseconds.");
          Console.WriteLine();
          Console.WriteLine($" Average write for each iteration (1 vendor, 5 inventory) averaged {commitTimes.Average()} ticks. Min: {commitTimes.Min()} Max: {commitTimes.Max()}");
          Console.WriteLine("Press any key to exit.");

          vendorRepo.Dispose();
          invRepo.Dispose();

          Console.ReadLine();
        }
    }
}
```

In this code we simply load the CSV files, parse them, and submit their data into the repositories for storage. We set up some timers so we can see how fast it takes us. On a Surface 4 Pro running 64-bit Windows 10 with 16GB of RAM and a 2.2 GhZ Intel i7-6650U processor, we were able to load all of the data in roughly 12 seconds, with an average write (one vendor record and 5 inventory records) taking an average of 500 ticks (there are 10,000 ticks in a millisecond). 

Now let's take a look at retrieving data from our database.