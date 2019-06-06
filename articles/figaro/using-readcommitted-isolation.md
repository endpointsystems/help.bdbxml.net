---
uid: using-readcommitted-isolation.md
---

# Using ReadCommitted Isolation

Another way we can modestly improve our write performance is by using read committed isolation. This causes our transactions to release write locks immediately, instead of waiting until the transaction is resolved. Using read committed isolation does not gives us the dramatic write performance that does using `Wholedoc` storage (see the previous section) but it is still an improvement.



```
        Number nodes per document:       10
        Number of writer threads:        5
        Isolation level:                 Read Committed
        Container type:                  Node storage
```

The average number of deadlocks seen across three runs with these settings is 724, down from 869.667. This is a modest improvement to be sure, but then you do not have to pay the query penalty that `Wholedoc` containers might cost you.