The <xref:Database.Delete> method deletes records from the database. In general, <xref:Database.Delete> takes a key and deletes the data item associated with it from the database.

If the database has been configured to support duplicate records, the <xref:Database.Delete> method will remove all of the duplicate records. To remove individual duplicate records, you must use a Berkeley DB cursor interface.
