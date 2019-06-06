---
uid: db_printlog.md
---

# db_printlog

The `db_printlog` utility is a debugging utility that dumps log files in a human-readable format.


```
db_printlog [-NrV] [-b start-LSN] [-e stop-LSN] [-h home] [-P password]
```

The `db_printlog` utility uses a Figaro BDB environment (as described for the `-h` option, the environment variable `DB_HOME`, or because the utility was run in a directory containing an existing environment). In order to avoid environment corruption when using an existing environment, `db_printlog` should always be given the chance to detach from the environment and exit gracefully. To cause `db_printlog` to release all environment resources and exit cleanly, send it an interrupt signal (SIGINT, or ^C).

#### -b

Display log records starting at log sequence number (LSN) _start-LSN_; _start-LSN_ is specified as a file number, followed by a slash (/) character, followed by an offset number, with no intervening whitespace.

#### -e

Stop displaying log records at log sequence number (LSN) _stop-LSN_; _stop-LSN_ is specified as a file number, followed by a slash (/) character, followed by an offset number, with no intervening whitespace.



#### -h

Specify a home directory for the database environment; by default, the current working directory is used.



#### -N

Do not acquire shared region mutexes while running. Other problems, such as potentially fatal errors, will be ignored as well. This option is intended only for debugging errors, and should not be used under any other circumstances.



#### -P

Specify an environment password. Although utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.



#### -r

Read the log files in reverse order.



#### -V

Write the library version number to the standard output, and exit.



#### Environment Variables

If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home, as described in @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions).



## Reviewing Log Files

If you are running with transactions and logging, the `db_printlog` utility can be a useful debugging aid. The `db_printlog` utility will display the contents of your log files in a human readable (and machine-readable) format.


The `db_printlog` utility will attempt to display any and all log files present in a designated _db_home_ directory. For each log record, `db_printlog` will display a line of the form:


```
[22][28]db_big: rec: 43 txnid 80000963 prevlsn [21][10483281]
```

The opening numbers in square brackets are the log sequence number (LSN) of the log record being displayed. The first number indicates the log file in which the record appears, and the second number indicates the offset in that file of the record.


The first character string identifies the particular log operation being reported. The log records corresponding to particular operations are described following. The rest of the line consists of name/value pairs.

The _rec_ field indicates the record type (this is used to dispatch records in the log to appropriate recovery functions).

The `txnid` field identifies the transaction for which this record was written. A `txnid` of 0 means that the record was written outside the context of any transaction. You will see these most frequently for checkpoints.


Finally, the `prevlsn` contains the LSN of the last record for this transaction. By following `prevlsn` fields, you can accumulate all the updates for a particular transaction. During normal abort processing, this field is used to quickly access all the records for a particular transaction.


After the initial line identifying the record type, each field of the log record is displayed, one item per line. There are several fields that appear in many different records and a few fields that appear only in some records.


The following table presents each currently written log record type with a brief description of the operation it describes.

Log Record Type | Description
bam_adj | Used when we insert/remove an index into/from the page header of a `Btree` page.
bam_cadjust | Keeps track of record counts in a `Btree` or `Recno` database.
bam_cdel | Used to mark a record on a page as deleted.
bam_curadj | Used to adjust a cursor location when a nearby record changes in a `Btree` database.
bam_merge | Used to merge two `Btree` database pages during compaction.
bam_pgno | Used to replace a page number in a `Btree` record.
bam_rcuradj | Used to adjust a cursor location when a nearby record changes in a `Recno` database.
bam_relink | Fix leaf page prev/next chain when a page is removed.
bam_repl | Describes a replace operation on a record.
bam_root | Describes an assignment of a root page.
bam_rsplit | Describes a reverse page split.
bam_split | Describes a page split.
crdel_inmem_create | Record the creation of an in-memory named database.
crdel_inmem_remove | Record the removal of an in-memory named database.
crdel_inmem_rename | Record the rename of an in-memory named database.
crdel_metasub | Describes the creation of a metadata page for a sub-database.
db_addrem | Add or remove an item from a page of duplicates.
db_big | Add an item to an overflow page (overflow pages contain items too large to place on the main page)
db_cksum | Unable to checksum a page.
db_debug | Log debugging message.
db_noop | This marks an operation that did nothing but update the LSN on a page.
db_ovref | Increment or decrement the reference count for a big item.
db_pg_alloc | Indicates we allocated a page to a database.
db_pg_free | Indicates we freed a page (freed pages are added to a free list and reused).
db_pg_freedata | Indicates we freed a page that still contained data entries (freed pages are added to a free list and reused.)
db_pg_init | Indicates we reinitialized a page during a truncate.
db_pg_new | Indicates that a page was allocated and put on the free list.
db_pg_prepare | Indicates a new page was allocated during a child transaction of a prepared transaction.
db_pg_sort | Sort the free page list and free pages at the end of the file.
dbreg_register | Records an open of a file (mapping the filename to a log-id that is used in subsequent log operations).
fop_create | Create a file in the file system.
fop_file_remove | Remove a name in the file system.
fop_remove | Remove a file in the file system.
fop_rename | Rename a file in the file system.
fop_write | Write bytes to an object in the file system.
ham_chgpg | Used to adjust a cursor location when a Hash page is removed, and its elements are moved to a different Hash page.
ham_copypage | Used when we empty a bucket page, but there are overflow pages for the bucket; one needs to be copied back into the actual bucket.
ham_curadj | Used to adjust a cursor location when a nearby record changes in a Hash database.
ham_groupalloc | Allocate some number of contiguous pages to the Hash database.
ham_insdel | Insert/delete an item on a Hash page.
ham_metagroup | Update the metadata page to reflect the allocation of a sequence of contiguous pages.
ham_newpage | Adds or removes overflow pages from a Hash bucket.
ham_replace | Handle updates to records that are on the main page.
ham_splitdata | Record the page data for a split.
qam_add | Describes the actual addition of a new record to a Queue.
qam_del | Delete a record in a Queue.
qam_delext | Delete a record in a Queue with extents.
qam_incfirst | Increments the record number that refers to the first record in the database.
qam_mvptr | Indicates we changed the reference to either or both of the first and current records in the file.
txn_child | Commit a child transaction.
txn_ckp | Transaction checkpoint.
txn_recycle | Transaction IDs wrapped.
txn_regop | Logs a regular (non-child) transaction commit.
txn_xa_regop | Logs a prepare message.

## See Also
* [Utilities](xref:utilities.md)