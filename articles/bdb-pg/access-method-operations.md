# Access Method Operations

Once a database ahndle has been created, there are several standard access method operations. Each of these operations is performed using a method referred to by the returned handle. Generally, the database will be opened using <xref:Database.Open>. If the database is from an old release of Berkeley DB, it may need to be upgraded to the current release before it is opened using <xref:Database.Upgrade>.

Once a database has been opened, records may be retrieved (<xref:Database.Get>), stored (<xref:Database.Put>), and deleted (<xref:Database.Delete>).

Additional operations supported by the database handle include statistics (<xref:Database.Stats>), truncation (<xref:Database.Truncate>), version upgrade (<xref:Database.Upgrade>), verification and salvage (<xref:Database.Verify>), flushing to a backing file (<xref:Database.Sync>), and association of secondary indices. Database handles are eventually closed using <xref:Database.Close>.

For more information on the access method operations supported by the database handle, see the API documentation.