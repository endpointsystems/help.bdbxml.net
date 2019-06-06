---
uid: supported-degrees-of-isolation.md
---

# Supported Degrees of Isolation

Figaro supports the following levels of isolation:
Degree of Isolation | ANSI Term | Definition
------------------- | --------- | ----------
1 | Read Uncommitted | Uncommitted reads means that one transaction will never overwrite another transaction's dirty data. Dirty data is data that a transaction has modified but not yet committed to the underlying data store. However, uncommitted reads allows a transaction to see data dirtied by another transaction. In addition, a transaction may read data dirtied by another transaction, but which subsequently is aborted by that other transaction. In this latter case, the reading transaction may be reading data that never really existed in the container.
2 | Read Committed | Committed read isolation means that degree 1 is observed, except that dirty data is never read. In addition, this isolation level guarantees that data will never change so long as it is addressed by the cursor, but the data may change before the reading cursor is closed. In the case of a transaction, data at the current cursor position will not change, but once the cursor moves, the previous referenced data can change. This means that readers release read locks before the cursor is closed, and therefore, before the transaction completes. Note that this level of isolation causes the cursor to operate in exactly the same way as it does in the absence of a transaction.
3 | Serializable | Committed read is observed, plus the data read by a transaction, T, will never be dirtied by another transaction before T completes. This means that both read and write locks are not released until the transaction completes. In addition, no transactions will see phantoms. Phantoms are records returned as a result of a search, but which were not seen by the same transaction when the identical search criteria was previously used. This is Figaro's default isolation guarantee.

By default, Figaro transactions and transactional cursors offer serializable isolation. You can optionally reduce your isolation level by configuring Figaro to use uncommitted read isolation. See [Reading Uncommitted Data](xref:reading-uncommitted-data.md) for more information. You can also configure Figaro to use committed read isolation. See [Committed Reads](xref:committed-reads.md) for more information.


Finally, in addition to Figaro's normal degrees of isolation, you can also use snapshot isolation. This allows you to avoid the read locks that serializable isolation requires. See [Snapshot Isolation](xref:snapshot-isolation.md) for details.



## See Also

* [Isolation](xref:isolation.md)
* [Committed Reads](xref:committed-reads.md)
* [Snapshot Isolation](xref:snapshot-isolation.md)