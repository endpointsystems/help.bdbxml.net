---
uid: durability.md
---

# Durability

An important part of Figaro's transactional guarantees is durability. Durability means that once a transaction has been committed, the container modifications performed under its protection will not be lost due to system failure.


In order to provide the transactional durability guarantee, Figaro uses a write-ahead logging system. Every operation performed on your containers is described in a log before it is performed on your containers. This is done in order to ensure that an operation can be recovered in the event of an untimely application or system failure.


Beyond logging, another important aspect of durability is recoverability - that is, backup and restore operations. Figaro supports a normal recovery that runs against a subset of your log files. This is a routine procedure used whenever your environment is first opened upon application startup, and it is intended to ensure that your container is in a consistent state. Figaro also supports archival backup and recovery in the case of catastrophic failure, such as the loss of a physical disk drive. See [Recovery Procedures](xref:recovery-procedures.md) for more information.


The documentation describes several different backup procedures you can use to protect your on-disk data. These procedures range from simple offline backup strategies to hot failovers. Hot failovers provide not only a backup mechanism, but also a way to recover from a fatal hardware failure. See [Backup Procedures](xref:backup-procedures.md) for more information.



## See Also


#### Other Resources
* [Recovery Procedures](xref:recovery-procedures.md)
* [Recovery Procedures](xref:recovery-procedures.md)
* [Non-Durable Transactions](xref:non-durable-transactions.md)
