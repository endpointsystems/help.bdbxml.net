---
uid: using-wholedoc-storage.md
---

# Using Wholedoc Storage

In the previous section we saw that specifying a document node size of 10 resulted in an average of 229.667 deadlocks across three program runs. This indicates a fairly high level of lock contention. It also indicates that the program is not operating particularly efficiently.


One way we could improve the write throughput for our application is to use whole document storage instead of node-level storage. This will result in fewer, but larger, database entries. The result should be fewer threads of control fighting for locks on a given page because fewer individual documents will be held on any given page.


Specifying a node size of 10 with whole document storage:



```
Number nodes per document:       10
Number of writer threads:        5
Isolation level:                 Default
Container type:                  Wholedoc storage
```

gives us a lower average deadlock count across program runs. There's certainly a significant improvement over node-level storage, although for many workloads you will pay for it in terms of the higher query times that `Wholedoc` storage will cost you.


