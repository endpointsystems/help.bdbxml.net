# Secondary Indexes

A secondary index, put simply, is a way to efficiently access records in a database (the primary) by means of some piece of information other than the usual (primary) key. In Berkeley DB, this index is simply another database whose keys are these pieces of information (the secondary keys), and whose data are the primary keys. Secondary indexes can be created manually by the application; there is no disadvantage, other than complexity, to doing so. However, when the secondary key can be mechanically derived from the primary key and datum that it points to, as is frequently the case, Berkeley DB can automatically and transparently manage secondary indexes.

As an example of how secondary indexes might be used, consider a database containing a list of students at a college, each of whom has a unique student ID number. A typical database would use the student ID number as the key; however, one might also reasonably want to be able to look up students by last name. To do this, one would construct a secondary index in which the secondary key was this last name.

In Berkeley DB for .NET Core, this would work as follows:

```csharp
        static void Main()
        {
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("DATA_DIR")))
                throw new ApplicationException("DATA_DIR must be set to the directory containing student data!");

            var dataPath = Environment.GetEnvironmentVariable("DATA_DIR");

            var config = new BTreeDatabaseConfig
            {                
                Creation = CreatePolicy.IF_NEEDED,
                CacheSize = new CacheInfo(1, 0, 1),
                ErrorFeedback = (prefix, message) =>
                {
                    var fg = ForegroundColor;
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"{prefix}: {message}");
                    ForegroundColor = fg;
                },
                ErrorPrefix = "students",                
            };

            var db = BTreeDatabase.Open(Path.Combine(dataPath, "students.db"), config);

            var cfg = new SecondaryBTreeDatabaseConfig(db, (key, data) =>
            {
                var last = Encoding.Default.GetString(data.Data).Split('#').Last();
                //Console.WriteLine($"adding {last} to index");
                return new DatabaseEntry(Encoding.Default.GetBytes(last));
            })
            {
                Creation = CreatePolicy.IF_NEEDED,Duplicates = DuplicatesPolicy.SORTED,
                ErrorFeedback = (prefix, message) => WriteLine($"{prefix}: {message}")

            };

            var dbIdx = SecondaryBTreeDatabase.Open(Path.Combine(dataPath, "studentsIdx.db"), cfg);

            if (db.Stats().nKeys < 1)
            {
                foreach (var line in File.ReadAllLines(Path.Combine(dataPath, "students.csv")))
                {
                    var split = line.Split("#");
                    var key = new DatabaseEntry(Encoding.Default.GetBytes(split[0]));
                    var bytes = new List<byte>();

                    //Add first and last name delimited by the '#' still
                    bytes.AddRange(Encoding.Default.GetBytes(split[1]));
                    bytes.AddRange(BitConverter.GetBytes('#'));
                    bytes.AddRange(Encoding.Default.GetBytes(split[2]));
                    var dbval = new DatabaseEntry(bytes.ToArray());
                    db.Put(key, dbval);
                }

                db.Sync();
                dbIdx.Sync();
            }

            var dbKeys = db.Stats().nKeys;            
            /* Data upload complete. Now let's find and delete some records. */

            var idxCursor = dbIdx.Cursor();
            idxCursor.MoveFirst();

            int results = 0;

            //var results = from c in idxCursor where c.Key == barrera select c;
            do
            {
                if (Encoding.Default.GetString(idxCursor.Current.Key.Data) == "Barrera") results++;
            } while (idxCursor.MoveNext());

            WriteLine($"found {results} results containing Barrera.");
            WriteLine();
}
```

> [!NOTE]
> The secondary index is configured by passing in the <xref:BTreeDatabase> to the <xref:SecondaryBTreeDatabaseConfig> constructor.

After the secondary index is created, the secondary indexes become alternate interfaces to the primary database. All updates to the primary will be automatically reflected in each secondary index that has been associated with it. All get operations using the <xref:Database.Get> or <xref:SecondaryDatabase.Get> methods on the secondary index return the primary datum associated with the specified (or otherwise current, in the case of cursor operations) secondary key. The `Move...` methods also become usable; these behave just like the `Get` operations, but return the primary key in addition to the primary datum, for those applications that need it as well.

Cursor get operations on a secondary index perform as expected; although the data returned will by default be those of the primary database, a position in the secondary index is maintained normally, and records will appear in the order determined by the secondary key and the comparison function or other structure of the secondary database.

Delete operations on a secondary index delete the item from the primary database and all relevant secondaries, including the current one.

Put operations of any kind are forbidden on secondary indexes, as there is no way to specify a primary key for a newly put item. Instead, the application should use the <xref:Databse.Add()> or <xref:Cursor.Add()> methods on the primary database.

Any number of secondary indexes may be associated with a given primary database, up to limitations on available memory and the number of open file descriptors.

Note that although Berkeley DB guarantees that updates made using any DB handle with an associated secondary will be reflected in the that secondary, associating each primary handle with all the appropriate secondaries is the responsibility of the application and is not enforced by Berkeley DB. It is generally unsafe, but not forbidden by Berkeley DB, to modify a database that has secondary indexes without having those indexes open and associated. Similarly, it is generally unsafe, but not forbidden, to modify a secondary index directly. Applications that violate these rules face the possibility of outdated or incorrect results if the secondary indexes are later used.

If a secondary index becomes outdated for any reason, it should be discarded using the DB->remove() method and a new one created using the DB->associate() method. If a secondary index is no longer needed, all of its handles should be closed using the DB->close() method, and then the database should be removed using a new database handle and the DB->remove() method.

Closing a primary database handle automatically dis-associates all secondary database handles associated with it.

# Error Handling With Secondary Indexes
An error return during a secondary update in CDS or DS (which requires an abort in TDS) may leave a secondary index inconsistent in CDS or DS. There are a few non-error returns:

- `0`
- `DB_BUFFER_SMALL`
- `DB_NOTFOUND`
- `DB_KEYEMPTY`
- `DB_KEYEXIST`

In the case of any other error return during a secondary update in CDS or DS, delete the secondary indices, recreate them. Some examples of error returns that need to be handled this way are:

- `ENOMEM` - indicating there is insufficient memory to return the requested item
- `EINVAL` - indicating that an invalid flag value or parameter is specified

Note that `DB_RUNRECOVERY` and `DB_PAGE_NOTFOUND` are fatal errors which should never occur during normal use of CDS or DS. If those errors are returned by Berkeley DB when running without transactions, check the database integrity with the <xref:Database.Verify()> method before rebuilding the secondary indices.