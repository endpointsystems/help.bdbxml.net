# Retrieving Records

The <xref:Database.Get> method retrieves records from the database. In general, <xref:Database.Get> takes a key and returns the associated data from the database.

There are a few different ways to customize retrieval:

<xref:Database.GetBoth>
Search for a matching key and data item, that is, only return success if both the key and the data items match those stored in the database.

<xref:LockingInfo.ReadModifyWrite>
Read-modify-write: acquire write locks instead of read locks during retrieval. This can enhance performance in threaded applications by reducing the chance of deadlock.

DB_SET_RECNO
If the underlying database is a Btree, and was configured so that it is possible to search it by logical record number, retrieve a specific record.
If the database has been configured to support duplicate records, DB->get() will always return the first data item in the duplicate set.

>[!NOTE]
> The DB_SET_RECNO data option is set internally if a <xref:LockingInfo> object is passed into the <xref:Database.Get> operation.

