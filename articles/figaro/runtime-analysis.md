---
uid: runtime-analysis.md
---

# Runtime Analysis

The examples presented in this section allow you to manipulate certain runtime characteristics that will affect the number of deadlocks the program will encounter. You can modify:
* The number of threads the program will use to write the container.
* The number of nodes that will be created per document written to the container. The key thing here is the size of the documents, as we will see later on in this section.
* Whether default isolation is used for the container writes, or if read committed should be used instead.
* Whether the container uses `Wholedoc` or `node` storage.
The point of the application is to measure the number of deadlocks encountered for a given program run. By counting the number of deadlocks, we can get a sense of the overall amount of lock contention occurring in our application. Remember that deadlocks represent a race condition that your application lost. In order to occur, two more threads had to have attempted to lock database pages in such a way that the threads blocked waiting for locks that will never be released (see @locks-blocks-and-deadlocks.md for a more complete description). So by examining the number of deadlocks that we see, we can indirectly get a sense for the amount of lock contention that the application encountered. Roughly speaking, the more deadlocks seen, the more lock contention that was going on during the application run.


Note that as you modify these constraints, you will see that the program will encounter differing numbers of deadlocks per program run. No two program runs will indicate the same number of deadlocks, but changing these constraint can on average increase or decrease the number of deadlocks reported by the application.


The reason why this application sees deadlocks is because of what Figaro does under the hood. Recall that Figaro writes XML documents to underlying Berkeley DB databases. Also, recall the Berkeley DB databases are usually organized in pages; multiple database entries will exist on any given page. Also, Berkeley DB uses page-level locking. The result is that multiple XML documents (or portions of XML documents) can and will be stored on the same database page. When multiple threads attempt to lock a database page, you get lock contention. When multiple database pages are in use and they are locked out of order by the threads of control, you can see deadlocks.


Therefore, the things that will immediately affect the amount of lock contention our application will encounter are:
* _Number of threads._ If you only ever use a single thread to write to your containers, you will never see any lock contention or deadlocks. On the other hand, increasing the number of writer threads will increase the number of deadlocks that are reported — up to a point. Recall that deadlocks are the result of losing a race condition. As you increase the number of threads in use, your system will slow down due to the overhead from context switching. This system slowdown will result in at least a leveling out of the number of deadlocks, if not an outright reduction in them. Of course, the point at which this occurs depends on the hardware in use.
* _XML document size relative to the underlying database page size._ The fewer documents that share a database page, the less chance there is for lock contention and therefore deadlocks. For our workload, the worst thing you can do is have lots of little database entries and a very large page size. Using large documents relative to the page size allows the document to fill up the page, which means that, for this example program anyway, there will only ever be one locker for that page. 
Note that selecting whole document versus node storage for the container plays into this equation. Whole document storage causes the XML document to be written using a single database entry. As a result, the entry itself is fairly large and so the underlying page is less likely to be shared by another document (depending on document size, of course). Conversely, node storage stores the document's individual nodes as individual database entries. Depending on the document, this can result in a lot of tiny database entries, which can adversely affect write performance due to increased lock contention. (Of course, the flip side to that is that node storage actually improves container query and read performance, but you will have to take our word for it because our sample application does not model that behavior.)
* _Isolation level._ Recall that by default, Berkeley DB hangs on to all write locks until the transaction either commits or aborts. It does this so as to provide your threads of control with the maximum isolation protection possible. However, hanging on to write locks like this means that our example application will encounter more lock contention and therefore see more deadlocks. 
If your application can accept a lessened isolation guarantee, and this one can, then you can reduce the isolation so as to reduce the amount of lock contention. In our case, we provide a way to use read committed (degree 2) isolation. Read committed causes the transaction to release the write lock as soon as it is finished writing to the page. Since the write locks are held for a shorter amount of time, there is less risk of lock contention and, again, deadlocks.


For this workload, using read committed isolation results in a dramatic decrease in the reported number of deadlocks, which means that our application is simply working more efficiently.

## In This Section
* @default-program-run.md
* @varying-the-node-size.md
* @using-wholedoc-storage.md
* @using-readcommitted-isolation.md
* @read-committed-with-wholedoc-storage.md
