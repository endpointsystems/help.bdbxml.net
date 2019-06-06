---
uid: concurrency.md
---

# Concurrency

Figaro offers a great deal of support for multi-threaded and multi-process applications even when transactions are not in use. Many of Figaro's handles are thread-safe, or can be made thread-safe by providing the appropriate flag at handle creation time, and Figaro provides a flexible locking subsystem for managing containers in a concurrent application. Further, Figaro provides a robust mechanism for detecting and responding to deadlocks.

>[!WARNING]
> Concurrent processing is a feature of the Figaro Concurrent Data Store (CDS), Transactional Data Store (TDS) or High Availability (HA) products. These objects and the functionality discussed is not available for Figaro Data Store (DS) edition.

It is useful to define a few terms:

* [Thread of Control](xref:glossary.md#thread-of-control)
* [Locking](xref:glossary.md#locking)
* [Free-Threaded](xref:glossary.md#free-threaded-thread-safe)
* [Block](xref:glossary.md#blocked)
* [Deadlock](xref:glossary.md#deadlock)

## In This Section

* [Which Handles are Free-Threaded?](xref:which-handles-are-free-threaded.md)
* [Locks, Blocks and Deadlocks](xref:locks-blocks-and-deadlocks.md)
* [The Locking Subsystem](xref:the-locking-subsystem.md)
* [Isolation](xref:isolation.md)
* [Read/Modify/Write](xref:readmodifywrite.md)
* [No Wait on Transaction Blocks](xref:no-wait-on-transaction-blocks.md)
