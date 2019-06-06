---
uid: no-wait-on-transaction-blocks.md
---

# No Wait on Transaction Blocks

Normally when a Figaro transaction is blocked on a lock request, it must wait until the requested lock becomes available before its thread of control can proceed. However, it is possible to configure a transaction handle such that it will report a deadlock rather than wait for the block to clear.


You do this on a transaction-by-transaction basis by specifying @Figaro.TransactionType.NoWaitTransaction to the @Figaro.XmlManager.CreateTransaction method.


## See Also

* @Figaro.TransactionType
* @Figaro.XmlManager.CreateTransaction

#### Other Resources
[Concurrency](xref:concurrency.md)
