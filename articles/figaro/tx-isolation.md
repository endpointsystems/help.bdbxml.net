---
uid: tx-isolation.md
---

# Isolation


Isolation guarantees are an important aspect of transactional protection. Transactions ensure the data your transaction is working with will not be changed by some other transaction. Moreover, the modifications made by a transaction will never be viewable outside of that transaction until the changes have been committed.

That said, there are different degrees of isolation, and you can choose to relax your isolation guarantees to one degree or another depending on your application's requirements. The primary reason why you might want to do this is because of performance; the more isolation you ask your transactions to provide, the more locking that your application must do. With more locking comes a greater chance of blocking, which in turn causes your threads to pause while waiting for a lock. Therefore, by relaxing your isolation guarantees, you can potentially improve your application's throughput. Whether you actually see any improvement depends, of course, on the nature of your application's data and transactions.



## In This Section

* [Supported Degrees of Isolation](xref:supported-degrees-of-isolation.md)
* [Reading Uncommitted Data](xref:reading-uncommitted-data.md)
* [Committed Reads](xref:committed-reads.md)
* [Snapshot Isolation](xref:snapshot-isolation.md)
