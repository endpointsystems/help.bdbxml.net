---
uid: why-transactions.md
---

# Why Transactions?

Figaro is designed to support multi-threaded and multi-process applications, but their usage means you must pay careful attention to issues of concurrency. Transactions help your application's concurrency by providing various levels of isolation for your threads of control. In addition, Figaro provides mechanisms that allow you to detect and respond to deadlocks (but strictly speaking, this is not limited to just transactional applications).


Perhaps the first question to answer is "Why transactions?" There are a number of reasons to include transactional support in your applications. The most common ones are the following:



## In This Section

* [Atomicity](xref:atomicity.md)
* [Consistency](xref:consistency.md)
* [Isolation](xref:isolation.md)
* [Durability](xref:durability.md)



#### Other Resources

* [Why Transactions?](xref:why-transactions.md)
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)