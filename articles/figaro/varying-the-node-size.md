# Varying the Node Size

With a default node size of 1, we saw an average of 43 reported deadlocks over three runs of the program. Now let's try increasing the size of the document to see what it does to the number of reported deadlocks:


```

          Number nodes per document:       10
          Number of writer threads:        5
          Isolation level:                 Default
          Container type:                  Node storage
        
```

This results in 222, 227, and 240 deadlocks for an average of 229.667 reported deadlocks. Clearly the amount of lock contention that we are seeing has increased, but why?


Remember that larger documents should fill up database pages, which should result in less lock contention because there are fewer lockers per database page. However, we are using node storage which means that the additional nodes results in additional small database entries. Given the way our application is writing documents, adding 9 additional nodes per document simply increases the chance of even more documents placing nodes on any given page.


Notice that there is a limit to the amount of lock contention that this application will see by simply adding nodes to the documents it creates. For example, suppose we created documents with 100 nodes:


```

          Number nodes per document:       100
          Number of writer threads:        5
          Isolation level:                 Default
          Container type:                  Node storage
        
```

In this case, we see lower average than the single node case. Why? First, the documents are now very large so there is a good chance that each document is filling up entire pages, even though we are still using node-level storage. In addition, each thread is now busy creating documents and then writing them to the containers, where they are being deconstructed into individual nodes. All of this is CPU-intensive activity that is likely helping to prevent lock contention because each thread is spending more time on document handling than it does with the smaller document sizes.


