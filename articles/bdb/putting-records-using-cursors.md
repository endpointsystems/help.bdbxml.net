# Putting Records Using Cursors

You can use cursors to put records into the database. DB's behavior when putting records into the database differs depending on the flags that you use when writing the record, on the access method that you are using, and on whether your database supports sorted duplicates. 

Note that when putting records to the database using a cursor, the cursor is positioned at the record you inserted. 

You use `Cursor.Add` to put (write) records into the database. 

```
    /// <summary>
    /// Add records directly to the database.
    /// </summary>
    /// <param name="keyval">The key.</param>
    /// <param name="dataval">The data value.</param>
    protected void AddToDb(string keyval, byte[] dataval)
    {
      var key = new DatabaseEntry(Encoding.UTF8.GetBytes(keyval));
      var data = new DatabaseEntry(dataval);
      db.Put(key, data);
    }

    /// <summary>
    /// Add records to the database through the cursor.
    /// </summary>
    /// <param name="keyval">The key.</param>
    /// <param name="dataval">The data value.</param>
    protected void AddToCursor(string keyval, byte[] dataval)
    {
      var key = new DatabaseEntry(Encoding.UTF8.GetBytes(keyval));
      var data = new DatabaseEntry(dataval);
      cursor.Add(new KeyValuePair<DatabaseEntry, DatabaseEntry>(key,data));      
    }

  /// <summary>
  /// The data repository for our Vendor database.
  /// </summary>
  public class VendorRepository: Repository
  {
    public VendorRepository(string dataPath) : base(dataPath, "vendor"){}
    public void AddVendor(Vendor v){AddToCursor(v.VendorName,v.ToByteArray());}
  }
```
In our main class, we'll add a new vendor:

```
vendorRepo.FindKey("Endpoint Systems");

var vendor = new Vendor
{
    City = "Boynton Beach",
    PhoneNumber = "866-637-7274",
    SalesRep = "Lucas Vogel",
    SalesRepPhone = "866-637-7274",
    State = "Florida",
    Street = "2500 Quantum Lakes Dr",
    VendorName = "Endpoint Systems",
    Zip = "33426"
};

vendorRepo.AddVendor(vendor);

Console.WriteLine("Press any key to exit.");

```
The first time we run this, we don't find our record in the `FindKey` method because it doesn't exist. The next time we run the program, we see that the record exists, and we get the following error when trying to insert the record again:
```
vendor: BDB0696 Duplicate data items are not supported with sorted data
```

This is because BTree databases do not allow duplicates by default. We'll see a workaround for this in [Allowing Duplicate Records](allowing-duplicate-records.md)