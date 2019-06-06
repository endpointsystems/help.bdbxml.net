# Storing Records

The <xref:Database.Put> method stores records into the database. In general, <xref:Database.Put> takes a key and stores the associated data into the database.

There are a options you can set to customize storage:

<xref:Database.Append>
Simply append the data to the end of the database, treating the database much like a simple log. This flag is only valid for the *Heap*, *Queue* and *Recno* access methods. This flag is required if you are creating a new record in a *Heap* database.

<xref:Database.PutNoDuplicate>
Only store the data item if the key does not already appear in the database.
If the database has been configured to support duplicate records, the <xref:Database.Put> method will add the new data value at the end of the duplicate set. If the database supports sorted duplicates, the new data value is inserted at the correct sorted location.

