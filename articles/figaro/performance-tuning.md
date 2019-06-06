---
uid: performance-tuning.md
---

# Performance Tuning

From a performance perspective, the use of transactions is not free. Depending on how you configure them, transaction commits usually require your application to perform disk I/O that a non-transactional application does not perform. Also, for multi-threaded and multi-process applications, the use of transactions can result in increased lock contention due to extra locking requirements driven by transactional isolation guarantees.

There is, therefore, a performance tuning component to transactional applications that is not applicable for non-transactional applications (although some tuning considerations do exist whether or not your application uses transactions). Where appropriate, these tuning considerations are introduced in the following sections.



## In This Section
* [Transaction Tuning](xref:transaction-tuning.md)
* [Environment Tuning](xref:environment-tuning.md)

## See Also

* [Deadlock Detection](xref:deadlock-detection.md)
* [Checkpoints](xref:checkpoints.md)
