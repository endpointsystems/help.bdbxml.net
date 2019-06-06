---
uid: default-program-run.md
---

# Default Program Run

By default, the program makes the following choices:



```
number of threads: 5
nodes per document: 1
Isolation level: default
Container type: node storage
```

This represents a worse-case situation for the application in all ways but one; it uses small documents that are just one node in size. Running the example three times in a row results in 49, 31, and 49 reported deadlocks for an average of 43 deadlocks. Note that your own test results will likely differ depending on the number and speed of your CPUs and the speed of your hard drive. For the record, these test results were taken using a quad-core Intel Q6600 2.4 Ghz CPU Vista workstation with a 5600 RPM hard drive.


