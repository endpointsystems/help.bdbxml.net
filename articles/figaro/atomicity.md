---
uid: atomicity.md
---

# Atomicity

Multiple container operations are treated as a single unit of work. Once committed, all write operations performed under the protection of the transaction are saved to your containers. Further, in the event that you abort a transaction, all write operations performed during the transaction are discarded. In this event, your container is left in the state it was in before the transaction began, regardless of the number or type of write operations you may have performed during the course of the transaction.

>[!NOTE]
Figaro transactions can span one or more container handles. Also, transactions can span both containers and Berkeley DB databases, provided they exist within the same environment.

Any number of operations on any number of databases can be included in a single transaction to ensure the atomicity of the operations. There is, however, a trade-off between the number of operations included in a single transaction and both throughput and the possibility of deadlock. The reason for this is because transactions acquire locks throughout their lifetime and do not release the locks until commit or abort time. So, the more operations included in a transaction, the more likely it is that a transaction will block other operations and that deadlock will occur. However, each transaction commit requires a synchronous disk I/O, so grouping multiple operations into a transaction can increase overall throughput. (There is one exception to this: the @Figaro.EnvConfig.WriteNoSyncTransaction and @Figaro.TransactionType.NoSyncTransaction flags cause transactions to exhibit the ACI (atomicity, consistency and isolation) properties, but not D (durability); avoiding the write and/or synchronous disk I/O on transaction commit greatly increases transaction throughput for some applications.)


When applications do create complex transactions, they often avoid having more than one complex transaction at a time because simple operations are unlikely to deadlock with each other or the complex transaction; while multiple complex transactions are likely to deadlock with each other because they will both acquire many locks over their lifetime. Alternatively, complex transactions can be broken up into smaller sets of operations, and each of those sets may be encapsulated in a nested transaction. Because nested transactions may be individually aborted and retried without causing the entire transaction to be aborted, this allows complex transactions to proceed even in the face of heavy contention, repeatedly trying the sub operations until they succeed.


It is also helpful to order operations within a transaction; that is, access the databases and items within the databases in the same order, to the extent possible, in all transactions. Accessing databases and items in different orders greatly increases the likelihood of operations being blocked and failing due to deadlocks.


## See Also

* [Why Transactions?](xref:why-transactions.md)
* [Introduction](xref:introduction.md)