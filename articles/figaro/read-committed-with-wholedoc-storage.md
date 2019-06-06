---
uid: read-committed-with-wholedoc-storage.md
---

# Read Committed with Wholedoc Storage

Finally, the best improvement we can hope to see for this application, using 10 node documents and 5 writer threads, is to use read committed isolation to write to whole document containers.

```
Number nodes per document:       10
Number of writer threads:        5
Isolation level:                 Read Committed
Container type:                  Wholedoc storage
```

For three runs of the program with these settings, we observe 228.333 deadlocks — a remarkable improvement over the worst-case 869.667 that we saw for 10 nodes, 5 writer threads!
