---
uid: aborting-a-transaction.md
---

# Aborting a Transaction

When you abort a transaction, all database modifications performed under the protection of the transaction are discarded, and all locks currently held by the transaction are released. In this event, your data is simply left in the state that it was in before the transaction began performing data modifications.


Note that aborting a transaction may result in disk I/O if your logs are backed by the file system. It is possible that during the course of your transaction, logging data and/or database pages were written to backing files on disk. For this reason, Figaro notes that the abort occurred in its log files so that at a minimum the container can be brought into a consistent state at recovery time.


Also, once you have aborted a transaction, the transaction handle that you used for the transaction is no longer valid. To perform database activities under the control of a new transaction, you must obtain a fresh transactional handle.


To abort a transaction, call @Figaro.XmlTransaction.Abort.
