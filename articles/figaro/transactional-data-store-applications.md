---
uid: transactional-data-store-applications.md
---

# Transactional Data Store Applications

Perhaps the first question to answer is "Why transactions?" There are a number of reasons to include transactional support in your applications. The most common ones are the following:

* **Recoverability.** Applications often need to ensure that no matter how the system or application fails, previously saved data is available the next time the application runs. This is often called Durability.
* **Atomicity**. Applications may need to make multiple changes to one or more databases, but ensure that either all of the changes happen, or none of them happens. Transactions guarantee that a group of changes are atomic; that is, if the application or system fails, either all of the changes to the databases will appear when the application next runs, or none of them.
* **Isolation**. Applications may need to make changes in isolation, that is, ensure that only a single thread of control is modifying a key/data pair at a time. Transactions ensure each thread of control sees all records as if all other transactions either completed before or after its transaction.

## In This Section
* [Database Recoverability](xref:database-recoverability.md)
* [Architecting Figaro TDS Applications](xref:architecting-figaro-tds-applications.md)
* [Transaction Tuning](xref:transaction-tuning.md)
* [Transaction Throughput](xref:transaction-throughput.md)
* [Handling Failure in Figaro TDS Applications](xref:handling-failure-in-figaro-tds-applications.md)